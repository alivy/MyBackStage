using System.Web.Http;
using WebActivatorEx;
using WebSite;
using Swashbuckle.Application;
using System.Reflection;
using System;
using System.IO;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace WebSite
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            //获取项目文件路径
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\";
            var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
            var commentsFile = Path.Combine(baseDirectory, commentsFileName);
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "WebSite");
                        c.IncludeXmlComments(commentsFile);
                    })
                .EnableSwaggerUi(c =>
                    {
                        //用于启用UI界面上的东西。
                        //加载汉化的js文件，注意 swagger.js文件属性必须设置为“嵌入的资源”。 APIUI.Scripts.swagger.js依次是：项目名称->文件夹->js文件名
                        c.InjectJavaScript(Assembly.GetExecutingAssembly(), "WebSite.Scripts.Swagger.js");
                    });
        }
    }
}
