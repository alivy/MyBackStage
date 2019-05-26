using BackStageIBLL;
using Common;
using Common.Share;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.HomeAciton;
using WebSite.Models.HomeModel;

namespace WebSite.Controllers
{
    [Export]
    public class HomeController : Controller
    {
        [Import]
        private IShareBLL<Sys_User> userBll { get; set; }

        [Import]
        public IShareBLL<Sys_button> buttonBll { get; set; }

        [Import]
        private IShareBLL<Sys_Role> roleBll { get; set; }

        [Import]
        private IShareBLL<Sys_LoginHistory> loginHistoryBll { get; set; }

        [Import("Sys_NavMenu")]
        private ISys_NavMenuBLL _navMenuBll { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// web登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult WebLogin()
        {
            var ssionid = Session[ConstString.SysUserLoginId];
            //var cookie = CookiesManager.Get(ConstString.SysUserLoginGuid);
            //if (cookie != null && (bool)cookie)
            //    return Redirect(UrlString.LoginJumpUrl);
            if (ssionid != null)
                return Redirect(UrlString.LoginJumpUrl);
            Session.Clear();
            return View();
        }

        public ActionResult Logion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WebUserLogin(ViewUserLogin user)
        {
            var action = new LogionAction(userBll, loginHistoryBll, _navMenuBll);
            return action.Action(user);
        }
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

    }
}