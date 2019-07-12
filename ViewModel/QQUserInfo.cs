using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ViewModel
{
    /// <summary>
    /// QQ回调用户信息
    /// </summary>
    public class QQUserInfo
    {
        public string OpenId { get; set; }
        public string Nickname { get; set; }
        public string Gender { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Year { get; set; }
        public string Figureurl { get; set; }
        public string Figureurl_1 { get; set; }
        public string figureurl_2 { get; set; }
        public string Figureurl_qq_1 { get; set; }
        public string Figureurl_qq_2 { get; set; }

    }
}