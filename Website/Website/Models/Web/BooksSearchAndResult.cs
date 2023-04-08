using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.DbDto;
using Website.Models.Requests;

namespace Website.Models.Web
{
    public class BooksSearchAndResult
    {
        public BookUISearchRequest Request { get; set; }
        public List<BookInfo> Result { get; set; } 
    }
}
