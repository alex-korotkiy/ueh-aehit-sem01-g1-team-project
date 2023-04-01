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
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(string connString) : base(connString)
        {
        }

        public void Add(UserInfo user)
        {
            var sql = @"DECLARE @id bigint
                SELECT @id = Id FROM Users WHERE UniqueId = @UniqueId
                IF @id IS NULL
                    INSERT Users OUTPUT inserted.Id VALUES (@UniqueId) 
                ELSE
                    SELECT Id = @id
                ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var id = db.Query<long>(sql, new { user.UniqueId }).First();
                user.Id = id;
            }
        }

        public UserInfo Get(long id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<UserInfo>("SELECT * FROM Users WHERE Id = @id", new {id}).FirstOrDefault();
            }
        }

        public UserInfo GetByUniqueId(Guid uniqueId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<UserInfo>("SELECT * FROM Users WHERE UniqueId = @uniqueId", new { uniqueId }).FirstOrDefault();
            }
        }
    }
}
