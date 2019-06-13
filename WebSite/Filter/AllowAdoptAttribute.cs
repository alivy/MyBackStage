using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Filter
{
    /// <summary>
    /// 允许通过的接口
    /// </summary>
    public class AllowAdoptAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 只做权限验证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controller = filterContext.RouteData.Values.Keys.First(p => p == "controller");
            var action = filterContext.RouteData.Values.Keys.First(p => p == "action");

            //思路
            //缓存中存入用于对用于增删查改所包含的关键字
            //查询用户拥有按钮权限


        }
    }
}