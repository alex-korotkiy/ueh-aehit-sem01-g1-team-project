using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Website.Models.Requests;
using Website.Models.DbDto;
using Website.Models.Enums;

namespace Website.Infrastructure.Repositories
{
    public class BooksRepository : BaseRepository, IBooksRepository
    {
        public const int MaxSearchResults = 2000;

        protected BookInfo AdjustRating(BookInfo book)
        {
            book.Rating = book.Rating / 2 + 2.5m;
            return book;
        }

        protected string GetSearchSql(BookSearchRequest request)
        {


            request.Correct();

            var sortType = (BookSortType)request.SortType;

            var topCount = Math.Max(MaxSearchResults, request.Start + request.Count) + 1;

            var baseSelect = $"SELECT TOP {topCount} bl.*,";
            var fromPart = "vBooksList bl";

            if (sortType == BookSortType.Popularity)
            {
                fromPart = fromPart + " LEFT JOIN (SELECT ItemId, Popularity = TotalRating from ItemTotalRatings) p ON bl.Id = p.ItemId";
                request.SortOrder = 1;
            }

            if (!string.IsNullOrEmpty(request.Text))
            {
                fromPart = fromPart + " JOIN dbo.FilterBooksBySearchTerm(@Text) ft ON bl.Id = ft.Id";
                if (sortType == BookSortType.TotalRank) request.SortType = 1;
            }
            else
            {
                if (sortType == BookSortType.TotalRank)
                {
                    sortType = BookSortType.YearOfPublication;
                    request.SortType = 1;
                }
            }

            if(request.RatedOnly!=0 && request.UserId != null)
            {
                fromPart = fromPart + " JOIN (SELECT ItemId, Rating FROM Ratings WHERE UserId = @UserId) ur ON bl.Id = ur.ItemId";
                baseSelect = baseSelect + " ur.Rating,";
            }

            var orderBy = sortType.ToString();
            if (request.SortOrder != 0) orderBy = orderBy + " DESC";

            
            baseSelect = baseSelect + $" RowNumber = ROW_NUMBER() OVER (ORDER BY {orderBy}) FROM {fromPart}";
            return $"WITH x AS ({baseSelect})," +
                $"y AS (SELECT x.*, TotalCount = COUNT(1) OVER() FROM x)" +
                $" SELECT * FROM y WHERE RowNumber >= @StartRow AND RowNumber <= @EndRow ORDER BY RowNumber";

        }

        public BooksRepository(string connString) : base(connString)
        {
        }

        public BookInfo Get(int id, long? userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = db.Query<BookInfo>("SELECT * FROM vBooksList WHERE Id = @Id", new { id }).FirstOrDefault();
                if (result == null) return result;
                if(userId.HasValue)
                {
                    var rating = db.Query<decimal?>("SELECT Rating FROM Ratings WHERE UserId = @userId AND ItemId = @itemId", new { userId = userId.Value, itemId = id }).FirstOrDefault();
                    if(rating.HasValue)
                    {
                        result.Rating = rating;
                        AdjustRating(result);
                    }
                }
                return result;
            }
        }

        public List<BookInfo> Search(BookSearchRequest request)
        {
            long longEndRowNumber = request.Start + request.Count - 1;
            int endRowNumber = longEndRowNumber <= int.MaxValue ? (int)longEndRowNumber : int.MaxValue;

            var sql = GetSearchSql(request);
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<BookInfo>(sql: sql, param: new { request.Text, StartRow = request.Start, EndRow = endRowNumber, request.UserId }, commandTimeout: 360).Select(AdjustRating).ToList();
            }
        }

    }
}
