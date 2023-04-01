using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.Enums;

namespace Website.Models.Requests
{
    public class BookSearchRequest: BaseListRequest
    {
        public BookSearchRequest()
        {
            Start = 1;
            Count = 10;
            SortOrder = 0;
        }

        public long? UserId { get; set; }
        public string Text { get; set; }

        public int RatedOnly { get; set; }
        public int? SortType { get; set; }
        public int SortOrder { get; set; }

        public void Correct() 
        {
            var textSearch = !string.IsNullOrEmpty(Text);

            if (SortType == null)
            {
                if (textSearch)
                {
                    SortType = (int)BookSortType.TotalRank;
                    SortOrder = 1;
                }
                else
                {
                    SortType = (int)BookSortType.YearOfPublication;
                    SortOrder = 1;
                }
            }

            var sortType = (BookSortType)SortType;
            if (!Enum.IsDefined(typeof(BookSortType), sortType))
            {
                if (textSearch)
                {
                    SortType = (int)BookSortType.TotalRank;
                    SortOrder = 1;
                }
                else
                {
                    SortType = (int)BookSortType.YearOfPublication;
                    SortOrder = 1;
                }

            };

            if (!textSearch && sortType == BookSortType.TotalRank)
            {
                SortType = (int)BookSortType.YearOfPublication;
                SortOrder = 1;
            }
        }
    }
}
