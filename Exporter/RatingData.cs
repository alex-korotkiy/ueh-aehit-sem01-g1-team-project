using System;
using System.Collections.Generic;
using System.Text;

namespace Exporter
{
    public class RatingData
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ItemId { get; set; }
        public decimal? Rating { get; set; }
    }
}
