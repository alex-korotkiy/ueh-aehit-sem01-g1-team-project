using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.DbDto;
using Website.Models.Requests;

namespace Website.Infrastructure.Repositories
{
    public interface IBooksRepository
    {
        BookInfo Get(int id);
        List<BookInfo> Search(BookSearchRequest request);
    }
}
