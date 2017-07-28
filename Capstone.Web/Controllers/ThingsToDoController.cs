using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class ThingsToDoController : CityToursController
    {
        private IUserDAL userDAL;

        public ThingsToDoController(IUserDAL userDAL) : base(userDAL)
        {
            this.userDAL = userDAL;
        }

        public ActionResult Category(int id)
        {
            return View("Category", id);
        }
    }
}