using BackStageIBLL;
using DBModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using ViewModel;
using System.Linq;

namespace WebSite.Controllers
{
    public class NavMenuAction : BaseAction
    {
        private IShareBLL<Sys_NavMenu> _menuShareBll { get; set; }

      
        private ISys_NavMenuBLL _navMenuBll { get; set; }

        public NavMenuAction(IShareBLL<Sys_NavMenu> menuShareBll, ISys_NavMenuBLL navMenuBll)
        {

            _menuShareBll = menuShareBll;
            _navMenuBll = navMenuBll;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult QueryNavMenuList(ReqBasePage page, string userId)
        {
            //int total;
            //Expression<Func<Sys_NavMenu, bool>> wehre = (x) => x.MenuId != "";
            //Func<Sys_NavMenu, int> order = (x) => x.Seq ?? 0;
            //List<Sys_NavMenu> roleList = _menuShareBll.GetPageList(wehre, order, out total, page.pageSize, page.pageIndex);


            //用户菜单信息
            var userMenus = _navMenuBll.GetNavMenuByUserId(userId);
            var menuList = userMenus.OrderBy(x=>x.Seq)
                                       .Skip((page.pageIndex - 1) * page.pageSize)
                                       .Take(page.pageSize).ToList();
            var pageList = new ResBasePage<Sys_NavMenu>
            {
                TotalRecordCount = userMenus.Count,
                Data = menuList
            };
            return RequestAction(RequestResult.Success("", pageList));
        }
    }
}