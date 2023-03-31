using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Infrastructure.Repositories
{
    public class RatingsRepository : BaseRepository, IRatingsRepository
    {
        public RatingsRepository(string connString) : base(connString)
        {
        }

        public void SetRating(long userId, int itemId, decimal rating)
        {
            var procName = "SetRating";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute(procName, new { userId, itemId, rating }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
