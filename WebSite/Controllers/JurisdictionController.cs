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
using ViewModel.Enums;
using WebSite.Controllers.Filter;
using WebSite.Controllers.UserRoleManagement;
using WebSite.Filter;

namespace WebSite.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [Export]
    [UserAuthorize]
    public class JurisdictionController : BaseAction
    {
        #region 创建接口
        /// <summary>
        /// 用户模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_User> _userShareBll { get; set; }
        /// <summary>
        /// 角色模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_Role> _roleShareBll { get; set; }
        /// <summary>
        /// 菜单模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_NavMenu> _menuShareBll { get; set; }
        /// <summary>
        /// 按钮模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_button> _buttonShareBll { get; set; }

        /// <summary>
        /// 菜单按钮模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_MenuButttonMap> _buttonMenuShareBll { get; set; }

        /// <summary>
        /// 菜单角色模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_MenuRoleMap> _menuRoleMapShareBll { get; set; }

        /// <summary>
        /// 用户角色模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_UserRoleMap> _userRoleMapShareBll { get; set; }

        /// <summary>
        /// 用户角色模板接口
        /// </summary>
        [Import]
        private IShareBLL<Sys_UserOrganizeMap> _userOrganizeMapShareBll { get; set; }

        /// <summary>
        /// 菜单接口
        /// </summary>
        [Import("Sys_NavMenu")]
        private new ISys_NavMenuBLL _navMenuBll { get; set; }

        /// <summary>
        /// 按钮接口
        /// </summary>
        [Import("Sys_ButtonBLL")]
        private ISys_ButtonBLL _buttonBll { get; set; }
        #endregion

        #region 角色管理
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
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QueryUserRoleList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userRole = new UserRoleManagementAction(_roleShareBll);
            var result = userRole.QueryUserRoleList(page, userId.ToString());
            return RequestAction(result);
        }

        /// <summary>
        ///获取角色对应菜单权限
        /// </summary>
        [AllowAnonymous]
        public ActionResult RoleMenusByRoleId(string roleId)
        {
            var roleMenus = _navMenuBll.GetRoleMenusByRoleId(roleId);
            var allMenus = _menuShareBll.LoadEntities();
            var result = ResdSingleToMultiple<Sys_NavMenu>.CreateObject(allMenus, roleMenus, roleId);
            return RequestAction(RequestResult.Success("获取成功", result));
        }

        /// <summary>
        /// 设置角色关联的菜单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetRoleRelationMenus(ResSetRoleMenus request)
        {
            if (request.GetIsValid())
            {
                string errorMsg = request.GetErrorMessageList().First().ErrorMessage;
                return RequestAction(RequestResult.Exception(errorMsg));
            }
            var roleId = request.RoleId;
            _menuRoleMapShareBll.BulkDelete(x => x.RoleId.Equals(roleId));
            return RequestAction(RequestResult.Success("获取成功"));
        }

        /// <summary>
        /// 设置角色关联的用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetRoleRelationUsers(ResSetRoleUserInfos request)
        {
            if (request.GetIsValid())
            {
                string errorMsg = request.GetErrorMessageList().First().ErrorMessage;
                return RequestAction(RequestResult.Exception(errorMsg));
            }
            var roleId = request.RoleId;
            var role = _roleShareBll.FirstOrDefault<Sys_Role>(x => x.RoleId.Equals(roleId));
            if (role == null)
                return RequestAction(RequestResult.Error("设置用户不存在"));
            var roles = new List<Sys_UserRoleMap>();
            request.UserInfos.ForEach(x => roles.Add(new Sys_UserRoleMap { RoleId = roleId, UserId = x }));
            _userRoleMapShareBll.BulkDelete(x => x.UserId.Equals(roleId));
            _userRoleMapShareBll.BulkInsert(roles);
            return RequestAction(RequestResult.Success("获取成功"));
        }
        #endregion

        #region 菜单管理
        /// <summary>
        /// 菜单管理
        /// </summary>
        /// <returns></returns>
        // GET: UserRole
        [CompressActionFilter]
        public ActionResult NavMenuManagement()
        {
            var userId = Session[ConstString.SysUserLoginId];
            ViewBag.Btn = userButtonAccess;
            return View();
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
        [CustomHandleError]
        [AllowAnonymous]
        public ActionResult NavMenuExecutive(ReqNavMenuView navMenu)
        {
            bool result = false;
            if (navMenu.GetIsValid())
            {
                string errorMsg = navMenu.GetErrorMessageList().First().ErrorMessage;
                return RequestAction(RequestResult.Exception(errorMsg));
            }
            var enumTest = TestEnum.AgeSmall.GetRemark();
            var nav = _menuShareBll.FirstOrDefault<Sys_NavMenu>(x => x.MenuId.Equals(navMenu.MenuId));
            if (navMenu.ExecutiveAction == (int)Operation.Add)
            {
                var sysNav = new Sys_NavMenu
                {
                    MenuId = navMenu.MenuId,
                    MenuName = navMenu.MenuName,
                    ParentMenId = navMenu.ParentMenId,
                    Level = navMenu.Level,
                    Url = navMenu.Url
                };
                result = _menuShareBll.AddEntity(sysNav);
            }
            else if (navMenu.ExecutiveAction == (int)Operation.Update)
            {
                nav.MenuId = navMenu.MenuId;
                nav.MenuName = navMenu.MenuName;
                nav.ParentMenId = navMenu.ParentMenId;
                nav.Level = navMenu.Level;
                nav.Url = navMenu.Url;
                result = _menuShareBll.UpdateEntity(nav);
            }
            else if (navMenu.ExecutiveAction == (int)Operation.Delete)
            {
                if (nav == null)
                {
                    return RequestAction(RequestResult.Exception("记录不存在"));
                }
                _menuShareBll.DeleteEntity(nav);
                result = true;
            }

            if (result)
            {
                return RequestAction(RequestResult.Success("执行成功"));
            }
            else
            {
                return RequestAction(RequestResult.Exception("执行操作出错"));
            }

        }

        /// <summary>
        /// 获取菜单按钮
        /// </summary>
        /// <param name="navMenu"></param>
        /// <returns></returns>
        [CustomHandleError]
        public ActionResult MenuButtonsByMenuId(string menuId)
        {
            var allbtns = _buttonShareBll.LoadEntities();
            var menubtns = _buttonBll.GetMenuButtonsByMenuId(menuId);
            var result = ResdSingleToMultiple<Sys_button>.CreateObject(allbtns, menubtns, menuId);
            return RequestAction(RequestResult.Success("执行成功", result));
        }


        /// <summary>
        /// 获取菜单按钮,没有回滚，
        /// </summary>
        /// <param name="navMenu"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetMenuButtons(ResSetMenuBtns btns)
        {
            try
            {
                if (btns.GetIsValid())
                {
                    string errorMsg = btns.GetErrorMessageList().First().ErrorMessage;
                    return RequestAction(RequestResult.Exception(errorMsg));
                }
                var menuId = btns.MenuId;
                _buttonMenuShareBll.BulkDelete(x => x.MenuId.Equals(menuId));
                var btnMenuMaps = new List<Sys_MenuButttonMap>();
                btns.BtnIds.Split(',').ToList().ForEach(x => btnMenuMaps.Add(new Sys_MenuButttonMap { ButtonId = x, MenuId = menuId }));
                if (btnMenuMaps.Any())
                    _buttonMenuShareBll.BulkInsert(btnMenuMaps);
                return RequestAction(RequestResult.Success("执行成功"));
            }
            catch (Exception e)
            {
                Log.Write(LogLevel.Error, "设置菜单页面错误", e);
                return RequestAction(RequestResult.Error("执行失败"));
            }
        }
        #endregion

        #region  用户管理
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManagement()
        {
            var userId = Session[ConstString.SysUserLoginId];
            return View();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult QueryUserList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userList = _userShareBll.GetPageList(x => !x.UserId.Equals(""), x => x.UserSeq, out int total, page.pageSize, page.pageIndex);
            var pageList = ResBasePage<Sys_User>.GetInstance(userList, total);
            var result = RequestResult.Success(ExecutSuccessMsgEnum.QuerySuccess.GetRemark(), pageList);
            return RequestAction(result);
        }

        /// <summary>
        /// 设置用户关联的角色
        /// 此处要加事务回滚
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetUserRelationtRoles(ResSetUserRoles request)
        {
            if (request.GetIsValid())
            {
                string errorMsg = request.GetErrorMessageList().First().ErrorMessage;
                return RequestAction(RequestResult.Exception(errorMsg));
            }
            var userId = request.UserId;
            var user = _userShareBll.FirstOrDefault<Sys_User>(x => x.UserId.Equals(userId));
            if (user == null)
                return RequestAction(RequestResult.Error("设置用户不存在"));
            var roles = new List<Sys_UserRoleMap>();
            request.RoleIds.ForEach(x => roles.Add(new Sys_UserRoleMap { RoleId = x, UserId = userId }));
            _userRoleMapShareBll.BulkDelete(x => x.UserId.Equals(userId));
            _userRoleMapShareBll.BulkInsert(roles);
            return RequestAction(RequestResult.Success("获取成功"));
        }



        /// <summary>
        /// 设置用户关联的机构
        /// 此处要加事务回滚
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SetUserRelationtOrganizes(ResSetUserRoles request)
        {
            if (request.GetIsValid())
            {
                string errorMsg = request.GetErrorMessageList().First().ErrorMessage;
                return RequestAction(RequestResult.Exception(errorMsg));
            }
            var userId = request.UserId;
            var user = _userShareBll.FirstOrDefault<Sys_User>(x => x.UserId.Equals(userId));
            if (user == null)
                return RequestAction(RequestResult.Error("设置用户不存在"));
            var roles = new List<Sys_UserOrganizeMap>();
            request.RoleIds.ForEach(x => roles.Add(new Sys_UserOrganizeMap { OrganizeCode = x, UserId = userId }));
            _userOrganizeMapShareBll.BulkDelete(x => x.UserId.Equals(userId));
            _userOrganizeMapShareBll.BulkInsert(roles);
            return RequestAction(RequestResult.Success("获取成功"));
        }
        #endregion
    }
}