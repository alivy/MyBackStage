using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Result
{
    /// <summary>
    /// 用户每个菜单对应的按钮权限
    /// </summary>
    public class UserMenuButtonResult
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MendId { get; set; }


        /// <summary>
        /// 按钮Id
        /// </summary>
        public string ButtonId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }
    }
}
