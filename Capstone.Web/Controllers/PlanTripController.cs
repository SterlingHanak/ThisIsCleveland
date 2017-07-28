using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class PlanTripController : CityToursController
    {
        private IUserDAL userDAL;

        public PlanTripController(IUserDAL userDAL): base(userDAL)
        {
            this.userDAL = userDAL;
        }

        public ActionResult NewTrip()
        {
            if (!base.IsAuthenticated)
            {
                RedirectToAction("Login", "Users");
            }

            return View("NewTrip");
        }

        public ActionResult LandmarkDetails(int landmarkId)
        {
            return PartialView("_LandmarkDetails");
        }
    }
}