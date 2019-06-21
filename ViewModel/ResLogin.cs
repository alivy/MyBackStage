using Common;
using System;
using System.Reflection;

namespace ViewModel
{
    /// <summary>
    /// 返回API登陆消息
    /// </summary>
    public class ResLoginAPI
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }

        public static ResLoginAPI GetInstance(Enum em, string token ="")
        {
            return new ResLoginAPI
            {
                code = Convert.ToInt32(em),
                msg = em.GetRemark(),
                Token = token
            };
        }
    }
}