using BackStageIBLL;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
            var db = DBContext.CreateContext();
            var sql = db.Sys_OrganzieRoleMap.AsQueryable().Where(x => x.Sys_Organize.ParentCode == "");
            var map = sql.FirstOrDefault();
            return View();
        }
    }
}