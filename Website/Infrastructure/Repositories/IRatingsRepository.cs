using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Infrastructure.Repositories
{
    public interface IRatingsRepository
    {
        public void SetRating(long userId, int itemId, decimal rating);
    }
}
