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
            var ck_userId = HttpContext.Current.Request.Cookies[ConstString.SysUidCookieName];
            if (se_userId == null || ck_userId == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "WebLogin",
                    backurl = filterContext.HttpContext.Request.RawUrl
                }));
            }
        }

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    object m_userId = filterContext.HttpContext.Session[ConstString.UserLoginId];
        //    bool m_isLoginOther = false;
        //    if (m_userId != null)
        //    {
        //        //GenericIdentity m_genericIdentity = new GenericIdentity(m_userId.ToString());
        //        //GenericPrincipal m_genericPrincipal = new GenericPrincipal(m_genericIdentity, null);

        //        if(filterContext.HttpContext.Request[ConstString.UserLoginGuid] != null)
        //        {
        //            string m_guid = filterContext.HttpContext.Request[ConstString.UserLoginGuid];
        //            //if(UserService.CheckUserHash((int)m_userId,m_guid))
        //            //{
        //            //    //filterContext.HttpContext.User = m_genericPrincipal;
        //            //    return;
        //            //}
        //            //else
        //            //{
        //                m_isLoginOther = true;
        //            //}
        //        }
        //        //else
        //        //{
        //        //    //filterContext.HttpContext.User = m_genericPrincipal;
        //        //    return;
        //        //}
        //    }
        //    if (!filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
        //    {
        //        filterContext.HttpContext.Response.Clear();
        //        filterContext.Result = new JsonResult()
        //        {
        //            Data = new
        //            {
        //                IsAuthrizeFail = true,//验证失败
        //                IsLoginOther = m_isLoginOther,//true：已经在别的地方登录
        //                backurl = "/User/UserLogin"
        //            },
        //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //        };
        //        filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
        //        filterContext.HttpContext.Response.StatusCode = m_isLoginOther ? 401 : 402;
        //        //filterContext.HttpContext.Response.End();
        //        return;
        //    }
        //    else
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
        //        {
        //            controller = "User",
        //            action = "UserLogin",
        //            backurl = filterContext.HttpContext.Request.RawUrl
        //        }));
        //        return;
        //    }
        //}
    }
}