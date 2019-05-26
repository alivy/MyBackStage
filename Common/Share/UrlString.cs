using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 用于存储链接
    /// </summary>
    public static class UrlString
    {

        /// <summary>
        /// 后台成功登陆后跳转地址
        /// </summary>
        public const string LoginJumpUrl = "/BootStrap/Share";


        /// <summary>
        /// 后台登陆地址
        /// </summary>
        public const string LoginUrl = "/Home/WebLogin";

        /// <summary>
        /// 后台首页
        /// </summary>
        public const string ExhibitionViewUrl = "/ShowBoard/ShowBoardIndex";
    }
}
