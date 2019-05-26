using BackStageIBLL;
using Common;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Controllers.BootStrapAction;
using WebSite.Controllers.Filter;

namespace WebSite.Controllers
{
    [UserAuthorize]
    [Export]
    public class BootStrapController : Controller
    {
        [Import("Sys_NavMenu")]
        private ISys_NavMenuBLL _navMenuBll { get; set; }
        // GET: BootStrap
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 加载模板页面
        /// </summary>
        /// <param name="backUrl">跳转地址</param>
        /// <returns></returns>
        public ActionResult Share(string backUrl = "")
        {
            var userId = Session[ConstString.SysUserLoginId];
            if (userId ==null)
                return Redirect(UrlString.LoginUrl);
            var result = new ShareAciton(_navMenuBll);
            return result.Action(userId.ToString(), backUrl);
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


        #region UI套件
        /// <summary>
        /// 警报
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsAlerts()
        {
            return View();
        }

        /// <summary>
        /// 按钮样式
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsButtons()
        {
            return View();
        }

        /// <summary>
        /// 卡片样式
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsCards()
        {
            return View();

        }
        /// <summary>
        ///动态弹窗
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsModals()
        {
            return View();

        }
        /// <summary>
        ///标签
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsTabs()
        {
            return View();

        }

        /// <summary>
        ///进度条
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsProgressBars()
        {
            return View();

        }

        /// <summary>
        ///小工具
        /// </summary>
        /// <returns></returns>
        public ActionResult LayoutsWidgets()
        {
            return View();

        }

        #endregion
    }
}