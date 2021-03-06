﻿using BackStageIBLL;
using Common;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using ViewModel;
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
        private ISys_NavMenuBLL _navMenuBll { get; set; }

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
            var result = userRole.APIQueryNavMenus(page);
            return Json(result);
        }

        /// <summary>
        /// 操作菜单新增 
        /// </summary>
        /// <returns></returns>
        [CustomHandleError]
        public ActionResult AddMenuExecutive(ReqNavMenuView navMenu)
        {
            bool result = false;
            if (!navMenu.GetIsValid())
            {
                string errorMsg = navMenu.GetErrorMessageList().First().ErrorMessage;
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Exception, errorMsg));
            }
            string newMenuId = string.Empty;
            if (navMenu.ParentMenId != null || navMenu.Level > 1)
            {
                var nav = _menuShareBll.FirstOrDefault<Sys_NavMenu>(x => x.MenuId.Equals(navMenu.ParentMenId));
                if (nav == null)
                    return Json(ResMessage.CreatMessage(ResultTypeEnum.Error, "父级菜单不存在"));
                newMenuId = _navMenuBll.maxSubMenuId(navMenu.ParentMenId);
                navMenu.Level = 2;
            }
            else
            {
                newMenuId = _navMenuBll.maxParentMenuId();
                navMenu.Level = 2;
                navMenu.ParentMenId = "$";
            }
            result = _menuShareBll.AddEntity(new Sys_NavMenu
            {
                MenuId = newMenuId,
                MenuName = navMenu.MenuName,
                ParentMenId = navMenu.ParentMenId,
                Level = navMenu.Level,
                Url = navMenu.Url,
                Seq = navMenu.Seq,
                IsVisible = navMenu.IsVisible
            });
            return Json(ResMessage.CreatMessage(result ? ResultTypeEnum.Success : ResultTypeEnum.Exception));
        }

        /// <summary>
        /// 操作菜单修改 
        /// </summary>
        /// <returns></returns>
        [CustomHandleError("执行菜单列表修改数据异常", true)]
        public ActionResult UpdateMenuExecutive(ReqNavMenuView navMenu)
        {
            var menuId = navMenu.MenuId;
            var opera = (int)Operation.Update;
            if (!navMenu.GetIsValid())
            {
                string errorMsg = navMenu.GetErrorMessageList().First().ErrorMessage;
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Exception, errorMsg));
            }
            var isBtn = _buttonBll.BtnJurisdiction(menuId, opera);
            if (!isBtn)
                return Json(ResMessage.CreatMessage(ResultTypeEnum.ValidateError, "您没有修改按钮权限"));
            var nav = _menuShareBll.FirstOrDefault<Sys_NavMenu>(x => x.MenuId.Equals(menuId));
            if (nav == null)
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Error, "菜单不存在"));
            nav.MenuId = navMenu.MenuId;
            nav.MenuName = navMenu.MenuName;
            nav.ParentMenId = navMenu.ParentMenId;
            nav.Level = navMenu.Level;
            nav.Url = navMenu.Url;
            var result = _menuShareBll.UpdateEntity(nav);
            return Json(ResMessage.CreatMessage(result ? ResultTypeEnum.Success : ResultTypeEnum.Exception));
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        [CustomHandleError("执行菜单列表删除数据异常", true)]
        public ActionResult DelMenuExecutive(string menuId, string delMenuId)
        {
            var isBtn = _buttonBll.BtnJurisdiction(menuId, (int)Operation.Delete);
            if (!isBtn)
                return Json(ResMessage.CreatMessage(ResultTypeEnum.ValidateError, "您没有删除按钮权限"));
            var delMenuIds = delMenuId.Split(',');
            if (delMenuIds == null || delMenuIds.Any())
                return Json(ResMessage.CreatMessage(ResultTypeEnum.ValidateError, "传入delMenuId值不存在元素"));
            var result = _menuShareBll.BulkDelete(x => delMenuIds.Contains(x.MenuId));
            return Json(ResMessage.CreatMessage(ResultTypeEnum.Success));
        }

        /// <summary>
        /// 获取菜单按钮权限
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [CustomHandleError]
        public ActionResult MenuButtonsByMenuId(string menuId)
        {
            Func<List<Sys_button>, List<ResButton>> func = (x) =>
                                                            x.Select(t =>
                                                            ResButton.CreatesInstance(t.ButtonId, t.ButtonName, t.ButtonSeq ?? 0, t.ButtonIcon)).ToList();
            var allbtns = _buttonShareBll.LoadEntities();
            var menubtns = _buttonBll.GetMenuButtonsByMenuId(menuId);
            var result = ResdSingleToMultiple<ResButton>.CreateObject(func(allbtns), func(menubtns), menuId);
            return Json(ResMessage.CreatMessage(ResultTypeEnum.Success, "执行成功", result));
        }

        /// <summary>
        /// 设置菜单对应按钮,没有回滚，
        /// </summary>
        /// <param name="btns"></param>
        /// <returns></returns>
        public ActionResult SetMenuButtons(ResSetMenuBtns btns)
        {
            try
            {
                if (!btns.GetIsValid())
                {
                    string errorMsg = btns.GetErrorMessageList().First().ErrorMessage;
                    return Json(RequestResult.Exception(errorMsg));
                }
                var menuId = btns.MenuId;
                _buttonMenuShareBll.BulkDelete(x => x.MenuId.Equals(menuId));
                var btnMenuMaps = new List<Sys_MenuButttonMap>();
                var btnIds = btns.BtnIds.Split(',').ToList();
                if (btnIds != null || !btnIds.Any())
                {
                    btnIds.ForEach(x => btnMenuMaps.Add(new Sys_MenuButttonMap { ButtonId = x, MenuId = menuId }));
                    if (btnMenuMaps.Any())
                        _buttonMenuShareBll.BulkInsert(btnMenuMaps);
                }
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