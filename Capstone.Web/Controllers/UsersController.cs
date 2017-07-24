using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserDAL userDAL;

        public UsersController(IUserDAL userDAL)
        {
            this.userDAL = userDAL;
        }
        
        
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // GET
        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            
            return View("Login", model);
        }
    }
}