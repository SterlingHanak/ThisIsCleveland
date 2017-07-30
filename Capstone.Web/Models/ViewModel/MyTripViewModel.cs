using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class MyTripViewModel
    {
        public List<string> Categories { get; set; }
        public string TripName { get; set; }
        public string TripDescription { get; set; }
        public DateTime TripDate { get; set; }
        public int[] SelectedLandmarkIds { get; set; }
    }
}