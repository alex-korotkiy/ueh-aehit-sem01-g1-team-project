using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.Repositories;
using Website.Models.DbDto;
using Website.Models.Requests;

namespace Website.Models.Web
{
    public class BooksSearchAndResult
    {
        public BookUISearchRequest Request { get; set; }
        public List<BookInfo> Result { get; set; }
        
        public string TotalItems
        {
            get
            {
                if (Result == null || Result.Count == 0) return "0";
                var lastItem = Result.Last();
                var totalCount = lastItem.TotalCount;
                if (totalCount <= BooksRepository.MaxSearchResults) return totalCount.ToString();
                var lastRowNumber = lastItem.RowNumber;
                if (lastRowNumber == totalCount) return totalCount.ToString();
                return (totalCount - 1).ToString() + "+";
            }
        }

    }
}
