using BackStageIBLL;
using Common;
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
    //[UserAuthorize]
    public class JurisdictionController : Controller
    {
        [Import]
        private IShareBLL<Sys_Role> _roleShareBll { get; set; }

        [Import]
        private IShareBLL<Sys_NavMenu> _menuShareBll { get; set; }


        [Import("Sys_NavMenu")]
        private ISys_NavMenuBLL _navMenuBll { get; set; }

        //[Import("Sys_RoleBLL")]
        //private ISys_RoleBLL _roleBll { get; set; }

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

            return View();
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryUserRoleList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userRole = new UserRoleManagementAction(_roleShareBll);
            return userRole.QueryUserRoleList(page, userId.ToString());
        }

        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult QueryNavMenuList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userRole = new NavMenuAction(_menuShareBll,_navMenuBll);
            return userRole.QueryNavMenuList(page, userId.ToString());
        }
    }
}