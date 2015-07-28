using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Orgchart
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "With2IdsToDifferentiate",
                routeTemplate: "api/{controller}/{id1}/{id2}"
            );

            config.Routes.MapHttpRoute(
                name: "With3IdsToDifferentiate",
                routeTemplate: "api/{controller}/{id1}/{id2}/{id3}"
            );

            config.Routes.MapHttpRoute(
                name: "WithAction",
                routeTemplate: "api/{controller}/{action}/{id1}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
