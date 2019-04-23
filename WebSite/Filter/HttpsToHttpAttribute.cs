using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebSite.Controllers.Filter
{
    public class HttpsToHttpAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        #region Action执行前执行
        /// <summary>
        /// Action执行前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var host = filterContext.HttpContext.Request.Url.DnsSafeHost;
            //本地不进行处理
            //if (!host.Contains("192.168") && host != "localhost" && !filterContext.HttpContext.Request.UserHostName.Contains("127.0.0.1"))

            //本地不进行处理
            //var isLocal = filterContext.HttpContext.Request.IsLocal;  //是否为来自本地请求
            //if (!isLocal)
            //{
            //    var pages = GetRulePages();  //    获取公共页面集合

            //    var isPublic = false;          //是否为公共页面
            //    var url = filterContext.HttpContext.Request.Url.PathAndQuery.Trim();
            //    foreach (var u in pages)
            //    {
            //        Regex r = new Regex(u);
            //        if (r.IsMatch(url))
            //        {
            //            isPublic = true;
            //            break;
            //        }
            //    }

            //    var isHttps = filterContext.HttpContext.Request.Url.Scheme.Contains("https");  //是否为https
            //    //是否为公共页面
            //    if (isPublic)
            //    {
            //        //公共页面转http
            //        if (isHttps)
            //        {
            //            filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Url.ToString().Replace("https", "http"));
            //        }
            //    }
            //    else
            //    {
            //        if (!isHttps)
            //        {
            //            filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Url.ToString().Replace("http", "https"));
            //        }
            //    }
            //}

            base.OnActionExecuting(filterContext);
        }
        #region 获取公共页面集合
        /// <summary>
        /// 获取页面规则集合
        /// </summary>
        /// <returns></returns>
        private List<string> GetRulePages()
        {
            XDocument doc = XDocument.Load(Common.IOHelper.GetMapPath("/Configs/PublicPages.xml"));
            var pages = doc.Descendants("pages");
            List<string> pagelist = new List<string>();
            if (pages != null)
            {
                var pieceareas = pages.Elements();
                foreach (var item in pieceareas)
                {
                    pagelist.Add(item.Value);
                }
            }

            return pagelist;
        }
        #endregion

        #endregion

        /// <summary>
        /// Action返回后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// Action返回前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}
