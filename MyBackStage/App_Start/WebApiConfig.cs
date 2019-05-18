using MyBackStage.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace MyBackStage
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API 配置和服务
            //DirectoryCatalog catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
            //MefDependencySolver solver = new MefDependencySolver(catalog);
            //config.DependencyResolver = solver;
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
