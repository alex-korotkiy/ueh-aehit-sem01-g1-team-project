using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected string connectionString = null;
        public BaseRepository(string connString)
        {
            connectionString = connString;
        }
    }
}
