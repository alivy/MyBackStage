using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backStageIDal
{
    [InheritedExport]
    public interface ISys_NavMenuDal
    {
        /// <summary>
        /// 根据用户id获取用户菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Sys_NavMenu> GetNavMenuByUserId(string userId);
    }
}
