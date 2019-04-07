using Common;
using System.Web;
using System.Web.Mvc;

namespace MyBackStage.Controllers.Filter
{
    /// <summary>
    /// 错误重定向页面
    /// </summary>
    public class ExceptionHandleAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is HttpException)
            {
                return;
            }
            if (!filterContext.ExceptionHandled)
            {
                base.OnException(filterContext);
                Logger.WriteException(filterContext.Exception);
                filterContext.ExceptionHandled = true;
                if(filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.Result = new JsonResult()
                    {
                        Data = new
                        {
                            IsAuthrizeFail = true,//验证失败
                            IsLoginOther =false,
                            backurl = "/User/UserLogin"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    filterContext.Controller.ControllerContext.HttpContext.SkipAuthorization = true;
                    filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                    filterContext.HttpContext.Response.StatusCode = 504;
                    //filterContext.HttpContext.Response.End();
                    return;
                }
                filterContext.Result = new RedirectResult("/Error.html");
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                //filterContext.HttpContext.Response.End();
                return;
            }
        }

    }
}
