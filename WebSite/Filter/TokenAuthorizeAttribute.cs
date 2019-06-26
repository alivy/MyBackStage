using Common;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BackStageIBLL;
using DBModel;
using Newtonsoft.Json;
using WebSite.Filter;
using ViewModel;

namespace WebSite.Controllers.Filter
{
    [Export]
    [CompressActionFilter]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TokenAuthorizeAttribute : AuthorizeAttribute
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
            var token = filterContext.HttpContext.Request.Headers["token"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                string userId = token.Decrypt();
                if (CheckToken(userId))
                {
                    SessionManager.Add(ConstString.SysUserLoginId, userId);
                    return;
                } 
            }
            filterContext.HttpContext.Response.ContentType = "application/json";
            var result = ResMessage.CreatMessage(ResultTypeEnum.Error, "无Token用户权限,请登录获取token");
            string json = JsonConvert.SerializeObject(result);
            filterContext.HttpContext.Response.Write(json);
            filterContext.HttpContext.Response.End();
        }


        public bool CheckToken(string userId)
        {
            string userkey = Sys_User.GetKey(userId);
            var user = CacheManager.GetData<Sys_User>(userkey);
            return user != null;
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