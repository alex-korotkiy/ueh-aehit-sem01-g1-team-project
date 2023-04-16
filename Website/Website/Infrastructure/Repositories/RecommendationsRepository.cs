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
        public List<BookInfo> GetForUser(long? userId)
        {
            var sql = @"SET NOCOUNT ON;
                DECLARE @rCount int, @dCount int
                DECLARE @ur TABLE (Id int identity primary key clustered, ItemId int)
                
                INSERT @ur SELECT ItemId FROM UserRecommendations WHERE UserId = @userId ORDER BY Id
                SET @rCount = @@ROWCOUNT

                SELECT @dCount = COUNT(1) FROM DefaultRecommendations
                IF @rCount < @dCount    
                    INSERT @ur SELECT ItemId FROM DefaultRecommendations WHERE ItemId NOT IN (SELECT ItemId FROM @ur);

                WITH x AS (SELECT *, _n = ROW_NUMBER() OVER (ORDER BY Id) FROM @ur)
                DELETE x WHERE _n > @dCount

                SELECT bl.* FROM vBooksList bl JOIN @ur ur
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
