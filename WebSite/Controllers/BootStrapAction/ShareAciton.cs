using BackStageIBLL;
using Common;
using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers.BootStrapAction
{
    public class ShareAciton : BaseAction
    {


        private  new ISys_NavMenuBLL _navMenuBll { get; set; }


        public ShareAciton(ISys_NavMenuBLL navMenuBll)
        {
            _navMenuBll = navMenuBll;
        }
        public ActionResult Action(string loginId, string backUrl = "")
        {
            var key = Sys_User.GetKey(loginId);
            var userInfo = CacheManager.GetData<Sys_User>(key) ?? new Sys_User();
            var userMenus = _navMenuBll.GetNavMenuByUserId(loginId.ToString());
            ViewData["userInfo"] = userInfo;
            ViewData["userMenus"] = userMenus;
            ViewBag.backUrl = string.IsNullOrWhiteSpace(backUrl) ? UrlString.ExhibitionViewUrl : backUrl;
            return View();
        }


    }
}