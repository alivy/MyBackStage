using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Filter
{
    public class CompressActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action方法执行后
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

        }

        /// <summary>
        /// Action方法执行前
        /// 响应数据压缩处理
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ///如果请求中有
            string acceptEncoding = filterContext.HttpContext.Request.Headers["accept-encoding"];
            if (!string.IsNullOrWhiteSpace(acceptEncoding)
                && acceptEncoding.ToLower().Contains("gzip"))
            {
                var response = filterContext.HttpContext.Response;
                response.AddHeader("content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}