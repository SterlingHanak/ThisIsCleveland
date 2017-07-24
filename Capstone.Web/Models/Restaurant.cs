using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.Models
{
    public class Restaurant : Landmark
    {
        public string FormalityLevel { get; set; }
        public string CuisineType { get; set; }
    }
}