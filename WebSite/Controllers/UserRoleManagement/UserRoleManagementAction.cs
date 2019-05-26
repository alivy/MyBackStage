using BackStageIBLL;
using Common;
using DBModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace WebSite.Controllers.UserRoleManagement
{
    public class UserRoleManagementAction : BaseAction
    {
        private IShareBLL<Sys_Role> _roleShareBll { get; set; }


        public UserRoleManagementAction(IShareBLL<Sys_Role> roleShareBll)
        {

            _roleShareBll = roleShareBll;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult QueryUserRoleList(ReqBasePage page,string userId)
        {
            int total;
            Expression<Func<Sys_Role, bool>> wehre = (x) => x.RoleId != "";
            Func<Sys_Role, string> order = (x) => x.RoleSeq;
            List<Sys_Role> roleList = _roleShareBll.GetPageList(wehre, order, out total, page.pageSize, page.pageIndex);
            var pageList = new ResBasePage<Sys_Role>
            {
                TotalRecordCount = total,
                Data = roleList
            };
            return RequestAction(RequestResult.Success("", pageList));
        }
    }
}