using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models.Requests
{
    public class BookUISearchRequest: BaseListRequest
    {

        public BookUISearchRequest()
        {
            Start = 1;
            Count = 21;
        }
        public string Text { get; set; }
        public int RatedOnly { get; set; }
        public int? SortType { get; set; }

        public new BookUISearchRequest MemberwiseClone()
        {
            return base.MemberwiseClone() as BookUISearchRequest;
        }


    }
}
