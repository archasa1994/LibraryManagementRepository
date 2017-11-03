using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryManagement
{    
    //To handle the case where a user tries to access a admin page.
        public class CustomAuthorization : AuthorizeAttribute
        {
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Home", action = "LoginRequired" }));
                }
            }
        }
    }
