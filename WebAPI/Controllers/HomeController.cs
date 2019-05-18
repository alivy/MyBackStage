using BackStageIBLL;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    [Export]
    public class HomeController : Controller
    {
        [Import]
        private IShareBLL<Sys_User> userBll { get; set; }

        [Import]
        private IShareBLL<Sys_button> buttonBll { get; set; }

        [Import(typeof(IBookService))]
        public IBookService Service { get; set; }

        public ActionResult Index()
        {
            int userCount = userBll.GetCount();
            ViewBag.Title = "Home Page";
            return View();
        }
    }

    public class test
    {
        //Export出去的类型和名称都要和Import标注的属性匹配，类型可以写IBookService, 也可以写MusicBook  
        [Export(typeof(IBookService))]
        public class MusicBook : IBookService
        {
            public string BookName { get; set; }

            public string GetBookName()
            {
                return "MusicBook";
            }
        }
    }

    public interface IBookService
    {
        string BookName { get; set; }
        string GetBookName();
    }
}
