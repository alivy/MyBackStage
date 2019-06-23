using BackStageIBLL;
using Common;
using DBModel;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using WebSite.Controllers.HomeAciton;
using WebSite.Models.HomeModel;
using System.Linq;
using WebSite.Controllers.Filter;
using ViewModel;
using ViewModel.Enums;

namespace WebSite.Controllers
{
    /// <summary>
    /// 登陆API外部接口
    /// </summary>
    [Export]
    public class LoginAPIController : Controller
    {

        [Import]
        private IShareBLL<Sys_User> userBll { get; set; }

        [Import]
        private IShareBLL<Sys_LoginHistory> loginHistoryBll { get; set; }

        [Import("Sys_NavMenu")]
        private ISys_NavMenuBLL _navMenuBll { get; set; }

        // GET: LoginAPI

        /// <summary>
        /// Web端API登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult WebLoginAPI(ViewUserLogin user)
        {
            var action = new LogionAction(userBll, loginHistoryBll, _navMenuBll);
            return action.APIAction(user);
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [TokenAuthorize]
        public ActionResult UserMenu()
        {
            var userId = Session[ConstString.SysUserLoginId];
            if (userId == null)
                return Json(ResMessage.CreatMessage(QueryUserMenu.NullUser));
            var userMenus = _navMenuBll.GetNavMenuByUserId(userId.ToString());
            if (userMenus != null)
            {
                var userMenuAPI = userMenus.Select(x => new ResUserMenuAPI
                {
                    MenuId = x.MenuId,
                    MenuName = x.MenuName,
                    ParentMenId = x.ParentMenId,
                    Level = x.Level,
                    Url = x.Url,
                    IconClass = "",
                    IconUrl = ""
                }).ToList();
                return Json(ResMessage.CreatMessage(QueryUserMenu.Suceess, userMenuAPI));
            }
            return Json(ResMessage.CreatMessage(QueryUserMenu.NullMenu));
        }
    }
}