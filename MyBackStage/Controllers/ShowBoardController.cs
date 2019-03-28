using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBackStage.Controllers
{
    public class ShowBoardController : Controller
    {
        // GET: ShowBoard
        public ActionResult Index()
        {
            return View();
        }

        

        // GET: ShowBoard
        public ActionResult layoutsNormal()
        {
            return View();
        }
    }
}