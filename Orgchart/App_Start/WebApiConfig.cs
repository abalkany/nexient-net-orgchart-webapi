using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Nexient.Net.Orgchart.WebAPI
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
                name: "With2Ids",
                routeTemplate: "api/{controller}/{oldDescription}/{newDescription}",
                defaults: new { oldDescription = RouteParameter.Optional, newDescription = RouteParameter.Optional  }
            );

            config.Routes.MapHttpRoute(
                name: "With3Ids",
                routeTemplate: "api/{controller}/{name}/{managerId}/{parentDepartmentId}"
            );
        }
    }
}
