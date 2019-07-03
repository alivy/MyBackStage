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
    /// 用户信息外部API接口
    /// </summary>
    [Export]
    public class SysSetUserInfoAPIController : Controller
    {
        

        [Import("Sys_UserBLL")]
        private ISys_UserBLL _userBll { get; set; }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [TokenAuthorize]
        public ActionResult GetUserInfo(string userid, ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            if (userId == null)
                return Json(ResMessage.CreatMessage(QueryUserMenu.NullUser));
            var userinfo = _userBll.GetUserInfo(userid, page);
            if (userinfo != null)
            {
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Success, "获取用户信息成功", userinfo));
            }
                return Json(ResMessage.CreatMessage(ResultTypeEnum.Error, "当前没有数据", userinfo));
        }
    }
}