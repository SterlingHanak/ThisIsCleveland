using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class ThingsToDoController : CityToursController
    {
        private IUserDAL userDAL;
        private ILandmarkDAL landmarkDAL;

        public ThingsToDoController(IUserDAL userDAL, ILandmarkDAL landmarkDAL) : base(userDAL)
        {
            this.userDAL = userDAL;
            this.landmarkDAL = landmarkDAL;
        }

        public ActionResult Category()
        {
            ThingsToDoViewModel viewModel = new ThingsToDoViewModel();
            viewModel.Landmarks = landmarkDAL.GetAllLandmarks();
            foreach(Landmark landmark in viewModel.Landmarks)
            {
                landmark.Categories = landmarkDAL.GetLandmarkCategories(landmark.Id);
                
            }
            viewModel.Category = "Theatres";
            return View("Category", viewModel);
        }

    }
}