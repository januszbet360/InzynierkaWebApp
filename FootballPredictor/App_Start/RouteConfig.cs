using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FootballPredictor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Schedule",
            url: "{controller}/Schedule/{selectedRound}",
            defaults: new
            {
                controller = "Home",
                action = "Schedule",
                selectedRound = UrlParameter.Optional
            }
        );

            routes.MapRoute(
                name: "TeamsCompare",
                url: "{controller}/TeamsCompare/{Name}",
                defaults: new
                {
                    controller = "Home",
                    action = "TeamsCompare",
                    Name = UrlParameter.Optional
                }
            );

            routes.MapRoute(
           name: "TeamStatistics",
           url: "{controller}/TeamStatistics/{Name}",
           defaults: new
           {
               controller = "Home",
               action = "TeamStatistics",
               Name = UrlParameter.Optional
           }
       );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
