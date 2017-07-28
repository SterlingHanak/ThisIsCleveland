using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class PlanTripController : CityToursController
    {
        private IUserDAL userDAL;
        private ILandmarkDAL landmarkDAL;

        public PlanTripController(IUserDAL userDAL, ILandmarkDAL landmarkDAL): base(userDAL)
        {
            this.userDAL = userDAL;
            this.landmarkDAL = landmarkDAL;
        }

        public ActionResult NewTrip()
        {
            if (!base.IsAuthenticated)
            {
                RedirectToAction("Login", "Users");
            }
            MyTripViewModel myTripViewModel = new MyTripViewModel();
    
            return View("NewTrip");
        }

        public ActionResult LandmarkDetails(int landmarkId)
        {
            return PartialView("_LandmarkDetails");
        }
    }
}