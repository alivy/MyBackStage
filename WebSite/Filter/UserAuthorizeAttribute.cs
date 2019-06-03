using Common;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BackStageIBLL;

namespace WebSite.Controllers.Filter
{
    [Export]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        [Import("Sys_NavMenu")]
        public ISys_NavMenuBLL _navMenuBll { get; set; }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //过滤验证
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
               filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;

            var userId = filterContext.HttpContext.Session[ConstString.SysUserLoginId];
            //var ck_userId = HttpContext.Current.Request.Cookies[ConstString.SysUidCookieName];
            if (userId == null || !IsUserAccess(filterContext, userId.ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "WebLogin",
                    backurl = filterContext.HttpContext.Request.RawUrl
                }));
            }
        }


        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="context"></param>
        ///    <param name="userId"></param>
        /// <returns>一个bool的数据，表示用户是否拥有其他权限</returns>
        public bool IsUserAccess(AuthorizationContext context, string userId)
        {
            bool isAccess = false;

            var controller = context.RouteData.Values.Keys.First(p => p == "controller");
            var action = context.RouteData.Values.Keys.First(p => p == "action");
            var url = "/" + context.RouteData.Values[controller] + "/" + context.RouteData.Values[action];
            //根据controller和action 可以判断权限了
            if (userId != null)
            {
                ////用户菜单信息
                //var userMenus = _navMenuBll.GetNavMenuByUserId(userId);
                //var menu = userMenus.FirstOrDefault(x => x.Url.Contains(url));
                //if (menu != null)
                isAccess = true;
            }
            return isAccess;
        }
    }
}