using BackStageIBLL;
using Common;
using Common.TypeConvert;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using WebSite.Controllers.Filter;
using WebSite.Controllers.UserRoleManagement;

namespace WebSite.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [Export]
    [UserAuthorize]
    public class JurisdictionController : BaseAction
    {
        [Import]
        private IShareBLL<Sys_Role> _roleShareBll { get; set; }

        [Import]
        private IShareBLL<Sys_NavMenu> _menuShareBll { get; set; }


        //[Import("Sys_NavMenu")]
        //private ISys_NavMenuBLL _navMenuBll { get; set; }




        /// <summary>
        /// 用户角色管理
        /// </summary>
        /// <returns></returns>
        // GET: UserRole
        public ActionResult UserRoleManagement()
        {
            var userId = Session[ConstString.SysUserLoginId];
            return View();
        }


        /// <summary>
        /// 菜单管理
        /// </summary>
        /// <returns></returns>
        // GET: UserRole
        public ActionResult NavMenuManagement()
        {
            var userId = Session[ConstString.SysUserLoginId];
            ViewBag.Btn = userButtonAccess;
            return View();
        }

     

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QueryUserRoleList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userRole = new UserRoleManagementAction(_roleShareBll);
            return RequestAction(userRole.QueryUserRoleList(page, userId.ToString()));
        }

        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QueryNavMenuList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userRole = new NavMenuAction(_menuShareBll, _navMenuBll);
            var result = userRole.QueryNavMenuList(page, userId.ToString());
            return RequestAction(result);
        }

        /// <summary>
        /// 操作菜单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult NavMenuExecutive(ReqNavMenuView navMenu)
        {

            bool result = false;
            var sysNav = TransExpV2<ReqNavMenuView, Sys_NavMenu>.Trans(navMenu);
            if (navMenu.ExecutiveAction == (int)Operation.Add)
                result = _menuShareBll.AddEntity(sysNav);
            else if (navMenu.ExecutiveAction == (int)Operation.Update)
                result = _menuShareBll.UpdateEntity(sysNav);
            else
                _menuShareBll.DeleteEntity(sysNav);

            if (result)
                return RequestAction(RequestResult.Success());
            else
                return RequestAction(RequestResult.Exception("执行操作出错"));
        }

    }
}