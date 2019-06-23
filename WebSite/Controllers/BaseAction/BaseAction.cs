
using BackStageIBLL;
using Common;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackStageBLL;
using WebSite.Controllers.Filter;
using DBModel.Result;

namespace WebSite.Controllers
{
    //[Export]
    [HttpsToHttp]
    public class BaseAction : Controller
    {
        [Import("Sys_NavMenu")]
        public ISys_NavMenuBLL _navMenuBll { get; set; }

        [Import("Sys_ButtonBLL")]
        private ISys_ButtonBLL _buttonBll { get; set; }

        /// <summary>
        /// 当前用户菜单信息
        /// </summary>
        public List<Sys_NavMenu> userMenus;

        /// <summary>
        /// 当前用户菜单信息
        /// </summary>
        public List<UserMenuButtonResult> userButtonAccess;
        public BaseAction()
        {
            //_navMenuBll = new Sys_NavMenuBLL();
        }

        ///// <summary>
        ///// 菜单id
        ///// </summary>
        //public static string MenuId;

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

            if (!ExistAllowAnonymous(context))
            {
                if (!IsUserAccess(context))
                {
                    context.Result = Content("抱歉，您没有权限访问此页面,请联系管理员！", "text/json");
                    return;
                }
            }
        }

        /// <summary>
        /// 获取用户按钮权限
        /// </summary>
        public void GetButtonAccess(string menuId)
        {
            var userId = Session[ConstString.SysUserLoginId];
            userButtonAccess = _buttonBll.ButtonQueryByMendId(userId.ToString(), menuId) ?? new List<UserMenuButtonResult>();
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
            var userId = Session[ConstString.SysUserLoginId];
            if (userId != null)
            {
                //用户菜单信息
                userMenus = _navMenuBll.GetNavMenuByUserId(userId.ToString());
                var menu = userMenus.FirstOrDefault(x => x.Url.Contains(url));
                if (menu != null)
                {
                    GetButtonAccess(menu.MenuId);
                    isAccess = true;
                }
                else
                {
                    userButtonAccess = new List<UserMenuButtonResult>();//目的：清除之前权限数据
                }
            }
            return isAccess;
        }

        /// <summary>
        /// 检查是否存在免验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool ExistAllowAnonymous(AuthorizationContext context)
        {
            //过滤验证
            return context.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                context.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
        }


        /// <summary>
        /// 用于封装返回对象直接转json输出
        /// 
        /// 小知识：使用这些访问修饰符可指定下列五个可访问性级别：
        /// public：访问不受限制。
        /// protected：访问仅限于包含类或从包含类派生的类型。
        /// Internal：访问仅限于当前程序集。
        /// protected internal:访问限制到当前程序集或从包含派生的类型的类别。
        /// private：访问仅限于包含类型。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        internal RequestActionResult<T> RequestAction<T>(T t) where T : class
        {
            return new RequestActionResult<T>(t);
        }
    }
}