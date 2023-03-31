using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models.DbDto
{
    public class UserInfo
    {
        public long ? Id { get; set; }
        public Guid UniqueId { get; set; }
    }
}
