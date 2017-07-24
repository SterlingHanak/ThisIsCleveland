using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.Models
{
    public class College : Landmark
    {
        public string AthleticDivision { get; set; }
        public bool IsPublic { get; set; }
    }
}