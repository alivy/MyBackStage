using BackStageIBLL;
using Common;
using DBModel;
using ViewModel;
using ViewModel.Enums;

namespace WebSite.Controllers.UserRoleManagement
{
    public class UserRoleManagementAction
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
        public RequestResult QueryUserRoleList(ReqBasePage page, string userId)
        {
            int total;
            var roleList = _roleShareBll.GetPageList(x => !x.RoleId.Equals(""), x=> x.RoleSeq, out total, page.pageSize, page.pageIndex);
            var pageList = new ResBasePage<Sys_Role>
            {
                TotalRecordCount = total,
                Data = roleList
            };
            return RequestResult.Success(ExecutSuccessMsgEnum.QuerySuccess.GetRemark(), pageList);
        }
    }
}