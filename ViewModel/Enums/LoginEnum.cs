using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Enums
{
    public enum LoginEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("登陆成功")]
        Suceess = 0,

        [Remark("用户或密码错误")]
        LandError = 1,
    }
}
