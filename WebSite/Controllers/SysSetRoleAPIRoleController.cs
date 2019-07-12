using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class SysSetRoleAPIRoleController : Controller
    {
        // GET: SysSetRoleAPIRole
        public ActionResult Role()
        {
            return View();
        }
    }
}