using BackStageIBLL;
using Common;
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using WebSite.Controllers.Filter;
using WebSite.Controllers.UserRoleManagement;

namespace WebSite.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [Export]
    //[UserAuthorize]
    public class JurisdictionController : Controller
    {
        [Import]
        private IShareBLL<Sys_Role> _roleShareBll { get; set; }

        //[Import("Sys_RoleBLL")]
        //private ISys_RoleBLL _roleBll { get; set; }

        /// <summary>
        /// 用户角色管理
        /// </summary>
        /// <returns></returns>
        // GET: UserRole
        public ActionResult UserRoleManagement()
        {
            var userId = Session[ConstString.SysUserLoginId];

            return View();
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryUserRoleList(ReqBasePage page)
        {
            var userId = Session[ConstString.SysUserLoginId];
            var userRole = new UserRoleManagementAction(_roleShareBll);

            return userRole.QueryUserRoleList(page,userId.ToString());
            //var userId = Session[ConstString.SysUserLoginId];
            //int total;
            //Expression<Func<Sys_Role, bool>> wehre = (x) => x.RoleId != "";
            //Func<Sys_Role, string> order = (x) => x.RoleSeq;
            //var roleList = _roleShareBll.GetPageList(wehre, order, out total, page.pageSize, page.pageIndex);

            //var pageList = new ResBasePage<Sys_Role>
            //{
            //    TotalRecordCount = total,
            //    Data = roleList
            //};
            //return Json(new RequestActionResult<RequestResult>(RequestResult.Success("", pageList)));
        }
    }
}