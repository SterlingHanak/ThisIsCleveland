using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public List<Landmark> Landmarks { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TripDate { get; set; }
    }
}