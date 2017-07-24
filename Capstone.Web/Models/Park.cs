using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.Models
{
    public class Park : Landmark
    {
        public List<string> Activities { get; set; }
        public bool IsPetFriendly { get; set; }
        public double AreaInSqFt { get; set; }
        public bool HasPicnicArea { get; set; }
        public bool HasRestroom { get; set; }
        public bool HasWaterFountain { get; set; }
    }
}