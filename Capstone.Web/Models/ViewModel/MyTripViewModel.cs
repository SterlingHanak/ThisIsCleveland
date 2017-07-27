using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class MyTripViewModel
    {
        public List<Landmark> Landmark { get; set; }
        public List<string> Categories { get; set; }
        public Trip NewTrip { get; set; }
    }
}