﻿using System;
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
            SortOrder = 0;
        }

        public long? UserId { get; set; }
        public string Text { get; set; }

        public int RatedOnly { get; set; }
        public int? SortType { get; set; }
        public int SortOrder { get; set; }
        public int? AuthorId { get; set; }

        public void Correct() 
        {
            var textSearch = !string.IsNullOrEmpty(Text);

            if (SortType == null || (!Enum.IsDefined(typeof(BookSortType), SortType.Value)))
            {
                if (textSearch)
                {
                    SortType = (int)BookSortType.TotalRank;
                    SortOrder = 1;
                }
                else
                {
                    SortType = (int)BookSortType.Title;
                    SortOrder = 0;
                }
            }

        }
    }
}
