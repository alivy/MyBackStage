﻿using DBModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace BackStageIBLL
{
    [InheritedExport]
    public interface ISys_NavMenuBLL
    {
        /// <summary>
        /// 根据用户id获取用户菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Sys_NavMenu> GetNavMenuByUserId(string userId);
    }
}
