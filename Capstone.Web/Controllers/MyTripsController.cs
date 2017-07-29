using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class MyTripsController : CityToursController
    {
        private IUserDAL userDAL;
        public MyTripsController(IUserDAL userDAL) : base(userDAL)
        {
            this.userDAL = userDAL;
        }
    }
}