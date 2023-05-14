using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.DbDto;

namespace Website.Infrastructure.Repositories
{
    public interface IAuthorsRepository
    {
        public List<AuthorInfo> GetAuthors(string filterString);
    }
}
