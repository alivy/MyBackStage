using Common;
using System;
using System.Reflection;

namespace ViewModel
{
    public class ResMessage
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }

        public  object data { get; set; }

        public static ResMessage CreatMessage(Enum em)
        {
            return new ResMessage
            {
                code = Convert.ToInt32(em),
                msg = em.GetRemark(),
                data = ""
            };
        }


        public static ResMessage CreatMessage(Enum em, object data)
        {
            return new ResMessage
            {
                code = Convert.ToInt32(em),
                msg = em.GetRemark(),
                data =data ?? ""
            };
        }


        public static ResMessage CreatMessage(Enum em, string msg, object data)
        {
            return new ResMessage
            {
                code = Convert.ToInt32(em),
                msg = msg,
                data = data ?? ""
            };
        }
    }


   
}