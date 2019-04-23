using BackStageIBLL;
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


        public ActionResult Index()
        {
            var count = userBll.GetCount();
            return View();
        }
        /// <summary>
        /// web登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult WebLogion()
        {
            return View();
        }
        public ActionResult Logion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogionValid(ViewUserLogin user)
        {
            var action = new LogionAction(userBll);
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