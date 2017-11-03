using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Delete",
                url: "Home/DeleteEmployee/{employeeID}/{employeeName}",
                defaults: new { controller = "Home" , action = "DeleteEmployee", employeeID = UrlParameter.Optional , employeeName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

