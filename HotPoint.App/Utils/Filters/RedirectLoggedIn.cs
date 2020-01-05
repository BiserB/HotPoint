
using HotPoint.App.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace HotPoint.App.Utils.Filters
{
    public class RedirectLoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isLoggedIn = context.HttpContext.User.Identity.IsAuthenticated;

            if (isLoggedIn)
            {
                var routeValue = new RouteValueDictionary(new { action = "Panel", controller = "Customer", area = string.Empty });

                context.Result = new RedirectToRouteResult(routeValue);
            }
        }
    }
}
