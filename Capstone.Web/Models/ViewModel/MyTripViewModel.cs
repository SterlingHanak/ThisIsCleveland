using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class MyTripViewModel
    {
        public List<string> Categories { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string TripName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string TripDescription { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public DateTime TripDate { get; set; }

        public int[] SelectedLandmarkIds { get; set; }
    }
}