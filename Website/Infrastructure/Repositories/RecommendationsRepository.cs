using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.DbDto;

namespace Website.Infrastructure.Repositories
{
    public class RecommendationsRepository : BaseRepository, IRecommendationsRepository
    {
        public RecommendationsRepository(string connString) : base(connString)
        {
        }
        public List<BookInfo> GetForUser(long userId)
        {
            var sql = @"IF EXISTS(SELECT 1 FROM UserRecommendations where UserId = @userId)
                SELECT bl.* FROM vBooksList bl JOIN (SELECT * FROM UserRecommendations where UserId = @userId) ur
                ON bl.Id = ur.ItemId
                ORDER BY ur.Id
                ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<BookInfo>(sql, new { userId }).ToList();
            }
        }
    }
}
