using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class CityToursController : Controller
    {
        private const string usernameKey = "CityToursUsername";
        private readonly IUserDAL userDAL;

        public CityToursController(IUserDAL userDAL)
        {
            this.userDAL = userDAL;
        }
        
        // GET: CityTours
        public ActionResult Index()
        {
            return View();
        }

        public string CurrentUser
        {
            get
            {
                string username = String.Empty;

                if (Session[usernameKey] != null)
                {
                    username = (string)Session[usernameKey];
                }

                return username;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return Session[usernameKey] != null;
            }
        }

        public void LogUserIn(string username)
        {
            Session[usernameKey] = username;
        }

        public void LogUserOut()
        {
            Session.Abandon();
        }

        [ChildActionOnly]
        public ActionResult GetAuthenticatedUser()
        {
            User model = null;

            if (IsAuthenticated)
            {
                model = userDAL.GetUser(CurrentUser);
            }

            return PartialView("_AuthenticationBar", model);
        }
    }
}