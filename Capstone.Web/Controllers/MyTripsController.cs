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
            if (!base.IsAuthenticated)
            {
                RedirectToAction("Login", "Home");
            }
            
            // Get all trips associated with current user
            int currentUserId = userDAL.GetUserId(base.CurrentUser);
            List<Trip> allUserTrips = tripDAL.GetAllUserTrips(currentUserId);

            // Assign landmarks to each trip and include landmark categories
            for (int i = 0; i < allUserTrips.Count; i++)
            {
                Trip trip = allUserTrips[i];
                trip.Landmarks = landmarkDAL.GetAllLandmarksInTrip(trip.Id);
                foreach (Landmark landmark in trip.Landmarks)
                {
                    landmark.Categories = landmarkDAL.GetLandmarkCategories(landmark.Id);
                }
            }

            return View("MyTrips", allUserTrips);
        }

        [HttpPost]
        public ActionResult DeleteTripLandmark(int landmarkId)
        {
            MyTripViewModel model = new MyTripViewModel();
            tripDAL.DeleteTripLandmark(landmarkId);
            return View("My Trips", model);
        }
    }
}