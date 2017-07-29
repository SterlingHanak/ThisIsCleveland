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

            List<Landmark> landmarksInTrip = new List<Landmark>();
            //foreach (int landmarkId in landmarkIds)
            //{
            //    landmarksInTrip.Add(landmarkDAL.GetLandmark(landmarkId));
            //}
            //Trip trip = new Trip();
            //trip.Landmarks = landmarksInTrip;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LandmarkDetails(int landmarkId)
        {
            return PartialView("_LandmarkDetails");
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

            // Initialize new trip
            return myTripViewModel;
        }
    }
}