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
    public class NavMenuAction
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
        public RequestResult QueryNavMenuList(ReqBasePage page, string userId)
        {
            //用户菜单信息
            var userMenus = _navMenuBll.GetNavMenuByUserId(userId);
            userMenus.ForEach(x =>
            {
                x.Url = x.Url ?? "#";
                x.IconClass = x.IconClass ?? "icon icon-target";
                x.IconUrl = string.Format("<i class='{0}'></i>", x.IconClass);
            });
            var menuList = userMenus.OrderBy(x => x.Seq)
                                       .Skip((page.pageIndex - 1) * page.pageSize)
                                       .Take(page.pageSize).ToList();
            var pageList = ResBasePage<Sys_NavMenu>.GetInstance(menuList, userMenus.Count);
            ResMessage.CreatMessage(ResultTypeEnum.Success, null, pageList);
            return RequestResult.Success("", pageList);
        }


        /// <summary>
        /// API获取菜单列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ResMessage APIQueryNavMenus(ReqBasePage page)
        {
            //用户菜单信息
            var userMenus = _menuShareBll.LoadEntities();
            var menuList = userMenus.OrderBy(x => x.Seq)
                                       .Skip((page.pageIndex - 1) * page.pageSize)
                                       .Take(page.pageSize).ToList();
            var userMenuAPI = menuList.Select(x => new ResUserMenuAPI
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName,
                ParentMenId = x.ParentMenId,
                Level = x.Level,
                Url = x.Url,
                IconClass = x.IconClass,
                IconUrl = x.IconUrl,
                Seq = x.Seq ?? 0
            }).ToList();
            var pageList = ResBasePage<ResUserMenuAPI>.GetInstance(userMenuAPI, userMenus.Count);
            return ResMessage.CreatMessage(ResultTypeEnum.Success, null, pageList);
        }
    }
}