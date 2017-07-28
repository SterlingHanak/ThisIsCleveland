using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.Models
{
    public class Landmark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public Dictionary<string, Hours> Schedule { get; set; }
        public List<string> Highlights { get; set; }
        public int AvgRating { get; set; }
        public int? YearFounded { get; set; }
        public int RelativeCost { get; set; }
        public int? AnnualNumVisitors { get; set; }
        public string WebsiteUrl { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public List<string> Categories { get; set; }
    }
}