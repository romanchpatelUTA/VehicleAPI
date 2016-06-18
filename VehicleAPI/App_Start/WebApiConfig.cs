using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VehicleAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "secondAPi",
                routeTemplate: "api/{controller}/{make}/{model}/{year}",
                defaults: new {id= RouteParameter.Optional, make = RouteParameter.Optional, year = RouteParameter.Optional, model = RouteParameter.Optional }
            );
        }
    }
}
