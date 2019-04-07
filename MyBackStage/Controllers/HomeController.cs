using BackStageIBLL;
using DBModel;
using MyBackStage.Controllers.HomeAciton;
using MyBackStage.Models.HomeModel;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace MyBackStage.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logion(ViewUserLogin user)
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