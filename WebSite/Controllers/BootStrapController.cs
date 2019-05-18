using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class BootStrapController : Controller
    {
        // GET: BootStrap
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Share()
        {
            var loginId = Session[Common.ConstString.SysUserLoginId];
            var userInfo = Common.CacheManager.GetData<DBModel.Sys_User>(DBModel.Sys_User.GetKey(loginId.ToString())) ?? new DBModel.Sys_User();
            var userMenus = Common.CacheManager.GetData<List<DBModel.Sys_NavMenu>>(DBModel.Sys_NavMenu.GetKey(loginId.ToString())) ?? new List<DBModel.Sys_NavMenu>();
            ViewData["userInfo"] = userInfo;
            ViewData["userMenus"] = userMenus;
            return View();
        }

        /// <summary>
        /// 布局-正常页
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsNormal()
        {
            return View();
        }

        /// <summary>
        /// 布局-固定边栏
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsFixedSidebar()
        {
            return View();
        }


        /// <summary>
        /// 布局-固定标题
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsFixedHeader()
        {
            return View();
        }
        /// <summary>
        /// 布局-隐藏边栏
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsHiddenSidebar()
        {
            return View();
        }

        
    }
}