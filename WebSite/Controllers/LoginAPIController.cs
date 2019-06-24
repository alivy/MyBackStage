﻿using BackStageIBLL;
using Common;
using DBModel;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using WebSite.Controllers.HomeAciton;
using WebSite.Models.HomeModel;
using System.Linq;
using WebSite.Controllers.Filter;
using ViewModel;
using ViewModel.Enums;
using System;
using System.Collections.Generic;

namespace WebSite.Controllers
{
    /// <summary>
    /// 登陆API外部接口
    /// </summary>
    [Export]
    public class LoginAPIController : Controller
    {

        [Import]
        private IShareBLL<Sys_User> userBll { get; set; }

        [Import]
        private IShareBLL<Sys_LoginHistory> loginHistoryBll { get; set; }

        [Import("Sys_NavMenu")]
        private ISys_NavMenuBLL _navMenuBll { get; set; }

        // GET: LoginAPI

        /// <summary>
        /// Web端API登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult WebLoginAPI(ViewUserLogin user)
        {
            var action = new LogionAction(userBll, loginHistoryBll, _navMenuBll);
            return action.APIAction(user);
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [TokenAuthorize]
        public ActionResult UserMenu()
        {
            var userId = Session[ConstString.SysUserLoginId];
            if (userId == null)
                return Json(ResMessage.CreatMessage(QueryUserMenu.NullUser));
            var userMenus = _navMenuBll.GetNavMenuByUserId(userId.ToString());
            if (userMenus != null)
            {
                Func<string, List<Sys_NavMenu>> funcMenus = null;
                funcMenus = (x) => userMenus.Where(t => t.ParentMenId.Equals(x)).ToList();
                Func<List<Sys_NavMenu>, List<ResUserMenuAPI>> funcs = null;
                funcs = (s) => s.Select(t => new ResUserMenuAPI
                {
                    MenuId = t.MenuId,
                    MenuName = t.MenuName,
                    ParentMenId = t.ParentMenId,
                    Level = t.Level,
                    Url = t.Url,
                    IconClass = "",
                    IconUrl = ""
                    //SubLevelMenus = t.ParentMenId != null ? funcMenus(t.ParentMenId) : null
                }).ToList();
                var parentMenus = userMenus.Where(x => x.Level.Equals(1)).ToList();
                var reslut = funcs(parentMenus);
                reslut.ForEach(x => x.SubLevelMenus.Equals(funcs(funcMenus(x.MenuId))));
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Success, "获取菜单成功", reslut));
            }
            return Json(ResMessage.CreatMessage(ResultTypeEnum.ValidateError, "当前用户无可用菜单"));
        }
    }
}