using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using WebSite.Controllers;

namespace WebSite.Filter
{
    /// <summary>
    /// 自定义异常处理
    /// </summary>
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 使用异常消息
        /// </summary>
        public CustomHandleErrorAttribute()
        {

        }

        /// <summary>
        /// 使用自定义错误消息
        /// </summary>
        /// <param name="errorMsg"></param>
        public CustomHandleErrorAttribute(string errorMsg)
        {
            ErrorMsg = errorMsg;
        }

        /// <summary>
        /// 使用自定义错误消息拼接错误日志
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <param name="IsSplicing"></param>
        public CustomHandleErrorAttribute(string errorMsg, bool IsSplicing)
        {
            ErrorMsg = errorMsg;
        }

        /// <summary>
        /// 自定义错误消息
        /// </summary>
        private string ErrorMsg = string.Empty;

        /// <summary>
        /// 是否拼接
        /// </summary>
        private bool IsSplicing = false;

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
                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "",//跳转页面
                //    ViewData =new ViewDataDictionary<string>(filterContext.Exception.Message)
                //};
                filterContext.HttpContext.Response.ContentType = "application/json";
                string exMsg = filterContext.Exception.Message;
                Log.Write(LogLevel.Error, exMsg);
                if (filterContext.Exception.Source.ToLower().Equals("entityframework"))
                    exMsg = EFErrorMsg(filterContext);
                if (string.IsNullOrWhiteSpace(ErrorMsg))
                {
                    if (IsSplicing)
                        exMsg = $"自定义消息：{ErrorMsg}异常消息：{exMsg}";
                    ErrorMsg = JsonConvert.SerializeObject(ResMessage.CreatMessage(ResultTypeEnum.Exception, exMsg));
                }
                Log.Write(LogLevel.Error, ErrorMsg);
                filterContext.HttpContext.Response.Write(ErrorMsg);
                filterContext.ExceptionHandled = true; //表示异常已被处理
            }
        }

        /// <summary>
        /// 处理EF异常
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public string EFErrorMsg(ExceptionContext filterContext)
        {
            string error = string.Empty;
            var efException = (DbEntityValidationException)filterContext.Exception;
            foreach (var item in efException.EntityValidationErrors)
            {
                foreach (var item2 in item.ValidationErrors)
                {
                    error = $"{item2.PropertyName}:{item2.ErrorMessage}\r\n";
                }
            }
            return error;
        }
    }
}