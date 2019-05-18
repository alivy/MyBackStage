using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.Filter;

namespace WebSite.Controllers
{
    [UserAuthorize]
    public class ShowBoardController : Controller
    {
        // GET: ShowBoard
        public ActionResult Index()
        {


            return View();
        }
    }
}