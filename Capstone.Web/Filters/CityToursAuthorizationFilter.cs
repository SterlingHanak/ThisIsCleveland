using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Capstone.Web.Controllers;

namespace Capstone.Web.Filters
{
    public class CityToursAuthorizationFilter: ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("username") ||
                filterContext.ActionParameters.ContainsKey("forUser"))
            {
                // Get implied username
                var impliedUsername = (filterContext.ActionParameters.ContainsKey("username")) ?
                    (string)filterContext.ActionParameters["username"] :
                    (string)filterContext.ActionParameters["forUser"];

                // Get the real username
                var controller = (CityToursController)filterContext.Controller;
                var actualUsername = controller.CurrentUser;

                if (!controller.IsAuthenticated)
                {
                    var routeToVisit = new RouteValueDictionary(new
                    {
                        controller = "Users",
                        action = "Login",
                        landingPage = filterContext.HttpContext.Request.Url
                    });

                    filterContext.Result = new RedirectToRouteResult(routeToVisit);
                }
                else
                {
                    if (impliedUsername.ToLower() != actualUsername.ToLower())
                    {
                        filterContext.Result = new HttpStatusCodeResult(403);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}