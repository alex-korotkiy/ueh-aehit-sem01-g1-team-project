using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models.Requests
{
    public class BaseListRequest
    {
        public int Start { get; set; }
        public int Count { get; set; }
    }
}
