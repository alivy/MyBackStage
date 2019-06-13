﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Filter
{
    /// <summary>
    /// 自定义异常处理
    /// </summary>
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 异常发生处理方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            if (!filterContext.ExceptionHandled)
            {
                Console.WriteLine(filterContext.HttpContext.Request);
                Log.Write(LogLevel.Error, filterContext.Exception.Message, filterContext.Exception);
                filterContext.Result = new ViewResult
                {
                    ViewName = "",//跳转页面
                    ViewData =new ViewDataDictionary<string>(filterContext.Exception.Message)
                };
                //表示异常已被处理
                filterContext.ExceptionHandled = true;
            } 
        }
    }
}