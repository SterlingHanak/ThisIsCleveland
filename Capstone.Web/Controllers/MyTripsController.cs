using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class MyTripsController : CityToursController
    {
        private IUserDAL userDAL;
        private ITripDAL tripDAL;
        private ILandmarkDAL landmarkDAL;

        public MyTripsController(IUserDAL userDAL, ITripDAL tripDAL, ILandmarkDAL landmarkDAL) 
            : base(userDAL)
        {
            this.userDAL = userDAL;
            this.tripDAL = tripDAL;
            this.landmarkDAL = landmarkDAL;
        }

        public ActionResult MyTrips()
        {
            // Get all trips associated with current user
            int currentUserId = userDAL.GetUserId(base.CurrentUser);
            List<Trip> allUserTrips = tripDAL.GetAllUserTrips(currentUserId);

            // Assign landmarks to each trip
            foreach (Trip t in allUserTrips)
            {
                t.Landmarks = landmarkDAL.GetAllLandmarksInTrip(t.Id);
            }

            return View("MyTrips", allUserTrips);
        }
    }
}