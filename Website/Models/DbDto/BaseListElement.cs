using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models.DbDto
{
    public class BaseListElement
    {
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
    }
}
