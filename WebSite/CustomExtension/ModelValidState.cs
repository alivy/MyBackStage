using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WebSite
{
    public static class ModelValidState
    {
        /// <summary>
        /// 获取验证结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool GetIsValid<T>(this T t) where T : new()
        {
            return GetValidMode(t).IsValid;
        }
        /// <summary>
        /// 获取所有验证消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<ValidErrorMessage> GetErrorMessageList<T>(this T t) where T : new()
        {
            return GetErrorMessage(t);
        }


        /// <summary>
        /// 根据类型获取表名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ModelValidMsg GetModelValid<T>(this T t) where T : new()
        {
            return GetValidMode(t);
        }

        /// <summary>
        /// 验证合法性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private static ModelValidMsg GetValidMode<T>(T t)
        {
            var modelMsg = new ModelValidMsg();
            Type type = t.GetType();
            try
            {
                foreach (var property in type.GetProperties())
                {
                    object[] oAttributeList = property.GetCustomAttributes(true);
                    foreach (var item in oAttributeList)
                    {
                        var attribute = item as ValidationAttribute;
                        if (attribute == null)
                            continue;
                        var isValid = attribute.IsValid(property.GetValue(t));
                        if (!isValid)
                        {
                            modelMsg.IsValid = false;
                            modelMsg.ErrorMessage = attribute.ErrorMessage;
                            return modelMsg;
                        }
                    }
                }
                modelMsg.IsValid = true;
                modelMsg.ErrorMessage = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return modelMsg;
        }


        /// <summary>
        /// 验证所有数据合法性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private static List<ValidErrorMessage> GetErrorMessage<T>(T t)
        {
            var modelErrorList = new List<ValidErrorMessage>();
            Type type = t.GetType();
            try
            {
                foreach (var property in type.GetProperties())
                {
                    object[] oAttributeList = property.GetCustomAttributes(true);
                    foreach (var item in oAttributeList)
                    {
                        var attribute = item as ValidationAttribute;
                        if (attribute == null)
                            continue;
                        var isValid = attribute.IsValid(property.GetValue(t));
                        if (!isValid)
                        {
                            modelErrorList.Add(new ValidErrorMessage()
                            {
                                Key = property.Name,
                                ErrorMessage = attribute.ErrorMessage
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return modelErrorList;
        }
    }
}
