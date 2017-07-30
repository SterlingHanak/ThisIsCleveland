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

        [HttpGet]
        public ActionResult NewTrip()
        {
            //if (!base.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Users");
            //}
            MyTripViewModel myTripViewModel = PopulateMyTripViewModel();
            return View("NewTrip", myTripViewModel);
        }

        [HttpPost]
        public ActionResult NewTrip(MyTripViewModel myTripViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("NewTrip", myTripViewModel);
            }

            // Create new Trip
            Trip trip = new Trip();

            // Assign trip name, description, and date
            trip.Name = myTripViewModel.TripName;
            trip.Description = myTripViewModel.TripDescription;
            trip.TripDate = myTripViewModel.TripDate;

            // Assign trip landmarks
            List<Landmark> landmarksInTrip = new List<Landmark>();
            foreach (int landmarkId in myTripViewModel.SelectedLandmarkIds)
            {
                landmarksInTrip.Add(landmarkDAL.GetLandmark(landmarkId));
            }
            trip.Landmarks = landmarksInTrip;

            // Insert new trip into database


            // Get all trips associated with user


            return RedirectToAction("MyTrips", "MyTrips");
        }

        public ActionResult LandmarksInCategoryJSON(string category)
        {
            List<Landmark> landmarks = landmarkDAL.GetAllLandmarksInCategory(category);
            return Json(landmarks, JsonRequestBehavior.AllowGet);
        }

        private MyTripViewModel PopulateMyTripViewModel()
        {
            MyTripViewModel myTripViewModel = new MyTripViewModel();

            // Retrieve all categories
            myTripViewModel.Categories = landmarkDAL.GetAllCategories();

            // Retrieve all landmarks and their properties
            List<Landmark> allLandmarks = landmarkDAL.GetAllLandmarks();
            foreach (Landmark landmark in allLandmarks)
            {
                landmark.Schedule = landmarkDAL.GetSchedule(landmark.Id);
                landmark.Categories = landmarkDAL.GetLandmarkCategories(landmark.Id);
            }

            return myTripViewModel;
        }
    }
}