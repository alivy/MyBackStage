using DBModel;
using DBModel.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackStageIBLL
{
    [InheritedExport]
    public interface ISys_ButtonBLL
    {

        /// <summary>
        /// 获取用户页面按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<UserMenuButtonResult> ButtonQueryByUserId(string userId);



        /// <summary>
        /// 获取用户页面按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mendId"></param>
        /// <returns></returns>
        List<UserMenuButtonResult> ButtonQueryByMendId(string userId, string mendId);


        /// <summary>
        /// 根据菜单id获取button
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        List<Sys_button> GetMenuButtonsByMenuId(string menuId);
    }
}
