using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroApp.Models
{
    public class RatingViewModel
    {
        public int RatingHistorialId { get; set; }
        public int? Rating { get; set; }
        public string RaterName { get; set; }
    }
}