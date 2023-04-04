using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.DbDto;

namespace Website.Infrastructure.Repositories
{
    public interface IRecommendationsRepository
    {
        List<BookInfo> GetForUser(long? userId);
    }
}
