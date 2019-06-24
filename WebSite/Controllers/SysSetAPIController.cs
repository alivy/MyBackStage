using BackStageIBLL;
using Common;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using ViewModel.Enums;
using WebSite.Controllers.Filter;
using WebSite.Filter;

namespace WebSite.Controllers
{

    /// <summary>
    /// 系统设置
    /// </summary>
    /// <returns></returns>
    [Export]
    [TokenAuthorize]
    public class SysSetAPIController : Controller
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
            //ViewBag.Btn = userButtonAccess;
            return View();
        }


        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [CustomHandleError]
        public ActionResult QueryNavMenuList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userRole = new NavMenuAction(_menuShareBll, _navMenuBll);
            var result = userRole.APIQueryNavMenus(page, userId.ToString());
            return Json(result);
        }

        /// <summary>
        /// 操作菜单增删改 
        /// </summary>
        /// <returns></returns>
        [CustomHandleError]
        public ActionResult NavMenuExecutive(ReqNavMenuView navMenu)
        {
            bool result = false;
            if (navMenu.GetIsValid())
            {
                string errorMsg = navMenu.GetErrorMessageList().First().ErrorMessage;
                return Json(RequestResult.Exception(errorMsg));
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
                    return Json(ResMessage.CreatMessage(ResultTypeEnum.Error, "记录不存在"));
                }
                _menuShareBll.DeleteEntity(nav);
                result = true;
            }

            if (result)
            {
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Success));
            }
            else
            {
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Exception));
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
            return Json(RequestResult.Success("执行成功", result));
        }


        /// <summary>
        /// 设置菜单对应按钮,没有回滚，
        /// </summary>
        /// <param name="navMenu"></param>
        /// <returns></returns>
        public ActionResult SetMenuButtons(ResSetMenuBtns btns)
        {
            try
            {
                if (btns.GetIsValid())
                {
                    string errorMsg = btns.GetErrorMessageList().First().ErrorMessage;
                    return Json(RequestResult.Exception(errorMsg));
                }
                var menuId = btns.MenuId;
                _buttonMenuShareBll.BulkDelete(x => x.MenuId.Equals(menuId));
                var btnMenuMaps = new List<Sys_MenuButttonMap>();
                btns.Btns.ForEach(x => btnMenuMaps.Add(new Sys_MenuButttonMap { ButtonId = x, MenuId = menuId }));
                if (btnMenuMaps.Any())
                    _buttonMenuShareBll.BulkInsert(btnMenuMaps);
                return Json(RequestResult.Success("执行成功"));
            }
            catch (Exception e)
            {
                Log.Write(LogLevel.Error, "设置菜单页面错误", e);
                return Json(RequestResult.Error("执行失败"));
            }
        }
        #endregion
    }
}