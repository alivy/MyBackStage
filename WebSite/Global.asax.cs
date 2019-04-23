using System;
using System.ComponentModel.Composition.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //设置MEF依赖注入容器
            DirectoryCatalog catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
            MefDependencySolver solver = new MefDependencySolver(catalog);
            DependencyResolver.SetResolver(solver);
        }
    }
}
