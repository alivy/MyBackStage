using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Filter
{
    /// <summary>
    /// Action执行过滤验证
    /// </summary>
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action方法执行后
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            Console.WriteLine(filterContext.HttpContext);
            string msg = $"this{nameof(CustomActionFilterAttribute)}  " +
                $"{ nameof(OnActionExecuted)} " +
                $"{ DateTime.Now.ToString("yyyy-MM-dd")}";
            
            //HttpContext.Current.Handler
            filterContext.HttpContext.Response.Write(msg);
        }

        /// <summary>
        /// Action方法执行前
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }


        /// <summary>
        /// 执行操作结果后
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        ///执行操作结果前
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}