using DBModel.Result;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace backStageIDal
{
    [InheritedExport]
    public interface ISys_ButtonDal
    {
        /// <summary>
        /// 获取用户页面按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<UserMenuButtonResult> ButtonQueryByuserId(string userId);
    }
}
