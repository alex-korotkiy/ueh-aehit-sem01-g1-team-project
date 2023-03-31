using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.DbDto;

namespace Website.Infrastructure.Repositories
{
    public interface IUsersRepository
    {
        UserInfo Get(long id);
        UserInfo GetByUniqueId(Guid uniqueId);
        void Add(UserInfo user);
    }
}
