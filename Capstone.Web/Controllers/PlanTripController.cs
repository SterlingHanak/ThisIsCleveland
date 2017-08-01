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
        private ITripDAL tripDAL;

        public PlanTripController(IUserDAL userDAL, ILandmarkDAL landmarkDAL, ITripDAL tripDAL) 
            : base(userDAL)
        {
            this.userDAL = userDAL;
            this.landmarkDAL = landmarkDAL;
            this.tripDAL = tripDAL;
        }

        [HttpGet]
        public ActionResult NewTrip()
        {
            if (!base.IsAuthenticated)
            {
                return RedirectToAction("Login", "Users");
            }
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

            // Insert new trip into database
            int currentUserId = userDAL.GetUserId(base.CurrentUser);
            Trip newTrip = PopulateTrip(myTripViewModel);
            int newTripId = tripDAL.SaveNewTrip(newTrip, currentUserId);

            // Insert trip landmarks into database
            for (int i = 0; i < myTripViewModel.SelectedLandmarkIds.Length; i++)
            {
                tripDAL.SaveTripLandmark(newTripId, myTripViewModel.SelectedLandmarkIds[i], i + 1);
            }

            // Redirect user to view list of all their stored trips
            return RedirectToAction("MyTrips", "MyTrips");
        }

        public ActionResult LandmarksInCategoryJSON(string category)
        {
            List<Landmark> landmarks = landmarkDAL.GetAllLandmarksInCategory(category);
            foreach (Landmark landmark in landmarks)
            {
                landmark.Schedule = landmarkDAL.GetLandmarkSchedule(landmark.Id);
            }
            return Json(landmarks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LandmarksFromKeywordsJSON(string keywords)
        {
            List<Landmark> landmarks = landmarkDAL.GetAllLandmarksFromKeywords(keywords);
            foreach (Landmark landmark in landmarks)
            {
                landmark.Schedule = landmarkDAL.GetLandmarkSchedule(landmark.Id);
                landmark.Categories = landmarkDAL.GetLandmarkCategories(landmark.Id);
            }
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
                landmark.Schedule = landmarkDAL.GetLandmarkSchedule(landmark.Id);
                landmark.Categories = landmarkDAL.GetLandmarkCategories(landmark.Id);
            }

            return myTripViewModel;
        }

        private Trip PopulateTrip(MyTripViewModel model)
        {
            // Create new Trip
            Trip trip = new Trip();

            // Assign trip name, description, and date
            trip.Name = model.TripName;
            trip.Description = model.TripDescription;
            trip.TripDate = model.TripDate;

            // Assign trip landmarks selected by user and bound to ViewModel
            List<Landmark> landmarksInTrip = new List<Landmark>();
            foreach (int landmarkId in model.SelectedLandmarkIds)
            {
                landmarksInTrip.Add(landmarkDAL.GetLandmark(landmarkId));
            }
            trip.Landmarks = landmarksInTrip;

            return trip;
        }
    }
}