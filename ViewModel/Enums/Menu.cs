using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Enums
{
    public enum QueryUserMenu
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("查询成功")]
        Suceess = 0,
       
         /// <summary>
        /// 正常
        /// </summary>
        [Remark("用户不存在")]
        NullUser = 1,
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("当前用户无可用菜单")]
        NullMenu = 2,
    }
}
