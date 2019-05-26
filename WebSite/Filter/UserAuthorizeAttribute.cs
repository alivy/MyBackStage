using Common;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSite.Controllers.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var se_userId = filterContext.HttpContext.Session[ConstString.SysUserLoginId];
            //var ck_userId = HttpContext.Current.Request.Cookies[ConstString.SysUidCookieName];
            if (se_userId == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "WebLogin",
                    backurl = filterContext.HttpContext.Request.RawUrl
                }));
            }
        }
    }
}