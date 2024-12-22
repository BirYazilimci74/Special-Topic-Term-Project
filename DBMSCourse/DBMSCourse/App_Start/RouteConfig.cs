using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DBMSCourse
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HomePage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomePage", action = "HomePage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "QuizPage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuizPage", action = "QuizPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "QAPage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QAPage", action = "QAPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CheckStudentInfoPage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "CheckStudentInfoPage", action = "CheckStudentInfoPage", id = UrlParameter.Optional }
            );

        }
    }
}
