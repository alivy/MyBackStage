using Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBackStage.Controllers.Filter
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            object m_userId = filterContext.HttpContext.Session[ConstString.UserLoginId];
            bool m_isLoginOther = false;
            if (m_userId != null)
            {
                //GenericIdentity m_genericIdentity = new GenericIdentity(m_userId.ToString());
                //GenericPrincipal m_genericPrincipal = new GenericPrincipal(m_genericIdentity, null);

                if(filterContext.HttpContext.Request[ConstString.UserLoginGuid] != null)
                {
                    string m_guid = filterContext.HttpContext.Request[ConstString.UserLoginGuid];
                    //if(UserService.CheckUserHash((int)m_userId,m_guid))
                    //{
                    //    //filterContext.HttpContext.User = m_genericPrincipal;
                    //    return;
                    //}
                    //else
                    //{
                        m_isLoginOther = true;
                    //}
                }
                //else
                //{
                //    //filterContext.HttpContext.User = m_genericPrincipal;
                //    return;
                //}
            }
            if (filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.Result = new JsonResult()
                {
                    Data = new
                    {
                        IsAuthrizeFail = true,//验证失败
                        IsLoginOther = m_isLoginOther,//true：已经在别的地方登录
                        backurl = "/User/UserLogin"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                filterContext.HttpContext.Response.StatusCode = m_isLoginOther ? 401 : 402;
                //filterContext.HttpContext.Response.End();
                return;
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "User",
                    action = "UserLogin",
                    backurl = filterContext.HttpContext.Request.RawUrl
                }));
                return;
            }
        }
    }
}