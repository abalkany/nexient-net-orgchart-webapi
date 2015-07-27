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
                name: "WithIdsToDifferentiate",
                routeTemplate: "api/{controller}/{id1}/{id2}/{id3}"
            );
        }
    }
}
