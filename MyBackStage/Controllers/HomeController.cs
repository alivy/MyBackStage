using BackStageIBLL;
using Common;
using DBModel;
using MyBackStage.Controllers.HomeAciton;
using MyBackStage.Models.HomeModel;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace MyBackStage.Controllers
{
    [Export]
    public class HomeController : Controller
    {
        [Import("Sys_ButtonBLL")]
        private ISys_ButtonBLL<Sys_button> buttonBll { get; set; }

        [Import("Sys_UserBLL")]
        private ISys_UserBLL<Sys_User> userBll { get; set; }


        public ActionResult Index()
        {
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

            return new LogionAction().Action(user);
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