using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Models.Enums;
using Website.Models.Requests;

namespace Website.Models.Converters
{
    public class BookSearchConverter
    {
        public static BookSearchRequest Convert(BookUISearchRequest request)
        {
            var result = new BookSearchRequest()
            {
                Text = request.Text, 
                RatedOnly = request.RatedOnly,
                AuthorId = request.AuthorId,
                Start = request.Start,
                Count = request.Count
            };

            if (request.SortType == null)
            {
                result.SortType = null;
            }
            else
            {
                var sortType = (BookUISortType)request.SortType;

                switch (sortType)
                {
                    case BookUISortType.Relevance:
                        result.SortType = (int)BookSortType.TotalRank;
                        result.SortOrder = 1;
                        break;

                    case BookUISortType.TitleAsc:
                        result.SortType = (int)BookSortType.Title;
                        result.SortOrder = 0;
                        break;

                    case BookUISortType.TitleDesc:
                        result.SortType = (int)BookSortType.Title;
                        result.SortOrder = 1;
                        break;

                    case BookUISortType.Popularity:
                        result.SortType = (int)BookSortType.Popularity;
                        result.SortOrder = 1;
                        break;

                    case BookUISortType.PriceAsc:
                        result.SortType = (int)BookSortType.Price;
                        result.SortOrder = 0;
                        break;

                    case BookUISortType.PriceDesc:
                        result.SortType = (int)BookSortType.Price;
                        result.SortOrder = 1;
                        break;

                    default:
                        break;
                }
            }

            return result;
        }
    }
}
