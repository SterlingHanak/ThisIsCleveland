using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Capstone.Web.Crypto;
using Capstone.Web.Filters;

namespace Capstone.Web.Controllers
{
    public class UsersController : CityToursController
    {
        private IUserDAL userDAL;

        public UsersController(IUserDAL userDAL): base(userDAL)
        {
            this.userDAL = userDAL;
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Login", model);
            }

            var user = userDAL.GetUser(model.Username);

            // Verify username
            if (user == null)
            {
                ModelState.AddModelError("invalid-login", "The username & password combination is invalid.");
                return View("Login", model);
            }

            // Verify hashed password
            HashProvider hashProvider = new HashProvider();
            if (!hashProvider.VerifyPasswordMatch(user.Password, model.Password, user.Salt))
            {
                ModelState.AddModelError("invalid-login", "The username & password combination is invalid.");
                return View("Login", model);
            }

            // If username and password combination found in database, log user in
            base.LogUserIn(user.Username);

            //If they are supposed to be redirected then redirect them; otherwise, send them to the home page
            var queryString = this.Request.UrlReferrer.Query;
            var urlParams = HttpUtility.ParseQueryString(queryString);
            if (urlParams["landingPage"] != null)
            {
                //If it is one of CityTours' web pages
                if (Url.IsLocalUrl(urlParams["landingPage"]))
                {
                    return new RedirectResult(urlParams["landingPage"]);
                }
                else
                {
                    return RedirectToAction("LeavingSite", "Users", new { destination = urlParams["landingPage"] });
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Route("users/register")]
        public ActionResult Register()
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();
            return View("Register", model);
        }

        [HttpPost]
        [Route("users/register")]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            var currentUser = userDAL.GetUser(model.Username);

            // If username already exists, return to page
            if (currentUser != null)
            {
                ViewBag.ErrorMessage = "This username is unavailable";
                return View("Register", model);
            }

            // Otherwise, give user new salt value
            HashProvider hashProvider = new HashProvider();
            string hashedPassword = hashProvider.HashPassword(model.Password);
            string salt = hashProvider.SaltValue;

            // Assign properties to user
            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.EmailAddress,
                Username = model.Username,
                Password = hashedPassword,
                Salt = salt
            };

            // Add the user to the database
            userDAL.RegisterUser(newUser);

            // Log the user in and redirect to the home screen
            base.LogUserIn(model.Username);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [CityToursAuthorizationFilter]
        [Route("users/{username}/changepassword")]
        public ActionResult ChangePassword(string username)
        {
            if (base.IsAuthenticated)
            {
                var model = new ChangePasswordViewModel();
                return View("ChangePassword", model);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        [CityToursAuthorizationFilter]
        [Route("users/{username}/changepassword")]
        public ActionResult ChangePassword(string username, ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePassword", model);
            }

            HashProvider hashProvider = new HashProvider();
            string hashedPassword = hashProvider.HashPassword(model.NewPassword);
            userDAL.ChangePassword(username, hashedPassword);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("leavingsite")]
        public ActionResult LeavingSite()
        {
            return View("LeavingSite");
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            base.LogUserOut();

            return RedirectToAction("Index", "Home");
        }
    }
}