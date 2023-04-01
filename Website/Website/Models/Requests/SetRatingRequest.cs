using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models.Requests
{
    public class SetRatingRequest
    {
        [Required]
        public int ItemId { get; set; }

        [Required] 
        public decimal Rating { get; set; }
    }
}
