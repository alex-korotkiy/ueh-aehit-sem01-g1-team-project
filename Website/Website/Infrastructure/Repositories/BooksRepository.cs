﻿using System;
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

        protected string GetSearchSql(BookSearchRequest request)
        {


            request.Correct();

            var sortType = (BookSortType)request.SortType;

            var baseSelect = $"SELECT bl.*,";
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

            
            baseSelect = baseSelect + $" RowNumber = ROW_NUMBER() OVER (ORDER BY {orderBy}), TotalCount = COUNT(1) OVER() FROM {fromPart}";
            return $"WITH x AS ({baseSelect}) SELECT * FROM x WHERE RowNumber >= @StartRow AND RowNumber <= @EndRow ORDER BY RowNumber";

        }

        public BooksRepository(string connString) : base(connString)
        {
        }

        public BookInfo Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<BookInfo>("SELECT * FROM vBooksList WHERE Id = @Id", new { id }).FirstOrDefault();
            }
        }

        public List<BookInfo> Search(BookSearchRequest request)
        {
            long longEndRowNumber = request.Start + request.Count - 1;
            int endRowNumber = longEndRowNumber <= int.MaxValue ? (int)longEndRowNumber : int.MaxValue;

            var sql = GetSearchSql(request);
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<BookInfo>(sql, new { request.Text, StartRow = request.Start, EndRow = endRowNumber, request.UserId }).ToList();
            }
        }

    }
}