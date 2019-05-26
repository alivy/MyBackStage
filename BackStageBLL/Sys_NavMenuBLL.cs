using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using BackStageIBLL;
using backStageIDal;
using Common;
using DBModel;

namespace BackStageBLL
{
    [Export("Sys_NavMenu", typeof(ISys_NavMenuBLL))]
    public class Sys_NavMenuBLL : ISys_NavMenuBLL
    {
        [Import("NavMenuDal")]
        private ISys_NavMenuDal _navMenu { get; set; }

        /// <summary>
        /// 根据用户id获取用户菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_NavMenu> GetNavMenuByUserId(string userId)
        {
            var result = new List<Sys_NavMenu>();
            //用户菜单信息
            var userMenuKey = Sys_NavMenu.GetKey(userId);
            result = CacheManager.GetData<List<Sys_NavMenu>>(userMenuKey);
            if (result == null || result.Any())
            {
                result = _navMenu.GetNavMenuByUserId(userId);
                result.ForEach(x =>
                {
                    x.Url = x.Url ?? "#";
                    x.IconClass = x.IconClass ?? "icon icon-target";
                    x.IconUrl = string.Format("<i class='{0}'></i>", x.IconClass);
                });
                CacheManager.Add(userMenuKey, result);
            }
            return result;
        }
    }
}
