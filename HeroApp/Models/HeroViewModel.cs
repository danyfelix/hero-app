using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroApp.Models
{
    public class HeroViewModel
    {
        public int HeroId { get; set; }
        public string HeroName { get; set; }
        public int? Age { get; set; }
        public string City { get; set; }
        public int? Rating { get; set; }
        public string Image { get; set; }
        public decimal RateAverage { get; set; }
        public List<RatingViewModel> RatingHistory { get; set; } = new List<RatingViewModel>();
    }
}