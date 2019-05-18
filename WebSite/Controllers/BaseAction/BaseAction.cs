﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Filter;

namespace WebSite.Controllers
{
    [HttpsToHttp]
    public class BaseAction : Controller
    {
        /// <summary>
        /// 公共验证过滤
        /// </summary>
        /// <param name="context"></param>
        protected override void OnAuthorization(AuthorizationContext context)
        {
            var b = context.HttpContext.Request.Browser;//浏览器判断 ie8 居然是7.0
            if (b.Browser == "IE" && float.Parse(b.Version) < 7)
            {
                context.Result = Content("ie浏览器就只支持ie8+", "text/json");
                return;
            }         
            if (IsUserAccess(context))
            {
                context.Result = Content("抱歉，您没有权限访问此页面", "text/json");
                return;
            }
        }

        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="context"></param>
        /// <returns>一个bool的数据，表示用户是否拥有其他权限</returns>
        public bool IsUserAccess(AuthorizationContext context)
        {
            bool isAccess = false;

            var controller = context.RouteData.Values.Keys.First(p => p == "controller");
            var action = context.RouteData.Values.Keys.First(p => p == "action");
            var url = "/" + context.RouteData.Values[controller] + "/" + context.RouteData.Values[action];

            //根据controller和action 可以判断权限了
            //isAccess = true;

            return isAccess;
        }


        internal RequestActionResult<T> RequestAction<T>(T t) where T : RequestResult
        {
            return new RequestActionResult<T>(t);
        }
    }
}