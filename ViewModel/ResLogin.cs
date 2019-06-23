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
        /// Token值
        /// </summary>
        public string Token { get; set; }

        public static ResLoginAPI GetInstance(string token = "")
        {
            return new ResLoginAPI
            {
                Token = token
            };
        }
    }
}