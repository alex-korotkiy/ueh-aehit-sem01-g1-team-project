using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models.DbDto
{
    public class BookInfo : BaseListElement
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }
        public int? PublisherId { get; set; }
        public string Publisher { get; set; }
        public string Image_URL_S { get; set; }
        public string Image_URL_M { get; set; }
        public string Image_URL_L { get; set; }
        public decimal? Price { get; set; }

    }
}
