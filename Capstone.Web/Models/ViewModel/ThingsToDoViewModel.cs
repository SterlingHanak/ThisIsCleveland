using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModel
{
    public class ThingsToDoViewModel
    {
        public string Category { get; set; }
        public List<Landmark> Landmarks { get; set; }
    }
}