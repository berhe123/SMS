using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allActiveReport}", new { allActiveReport = @".*\.ar8(/.*)?" });            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Teacher",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TeacherIndex", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Student",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "StudentIndex", id = UrlParameter.Optional }
            );
        }
    }
}
