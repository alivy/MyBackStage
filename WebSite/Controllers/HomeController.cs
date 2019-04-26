using BackStageIBLL;
using Common;
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
        [Import("Sys_ButtonBLL")]
        public ISys_ButtonBLL<Sys_button> buttonBll { get; set; }

        [Import("Sys_UserBLL")]
        public ISys_UserBLL<Sys_User> userBll { get; set; }

        [Import("Sys_LoginHistoryBLL")]
        public ISys_LoginHistoryBLL<Sys_LoginHistory> loginHistoryBll { get; set; }
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
            loginHistoryBll.GetCount();
            loginHistoryBll.GetUserCountTest();
            userBll.GetCount();
            var action = new LogionAction(userBll,loginHistoryBll);
            return action.Action(user);
        }

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
    }
}