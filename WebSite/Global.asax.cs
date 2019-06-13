using System;
using System.ComponentModel.Composition.Hosting;
using System.Web;
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
            MEFResolver();
        }

        /// <summary>
        /// 设置MEF依赖注入容器
        /// </summary>
        private void MEFResolver()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
            var solver = new MefDependencySolver(catalog);
            DependencyResolver.SetResolver(solver);
        }
        /// <summary>
        /// 处理在http请求时发生的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void Application_Error(object sender,EventArgs args)
        {
            var error = Server.GetLastError();
            string msg = error.Message;
            //可以处理404 500的错误
            var httpCode = ((HttpException)error).GetHttpCode();
            if (httpCode == 404)
            {

            }
            else if (httpCode == 500)
            {

            }
            Server.ClearError();
            Response.Write(msg);
        }
    }
}
