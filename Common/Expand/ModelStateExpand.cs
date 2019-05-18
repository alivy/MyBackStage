using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
//using System.Web.ModelBinding;

namespace Common.Expand
{
    /// <summary>
    /// EF的model消息验证拓展类
    /// </summary>
    public static class ModelStateExpand
    {
        public static IEnumerable<ShowErrorMsg> AllModelStateErrors(this ModelStateDictionary modelState)
        {
            var result = new List<ShowErrorMsg>();
            //找到出错的字段以及出错信息
            var errorFieldsAndMsgs = modelState.Where(m => m.Value.Errors.Any()).Select(x => new { x.Key, x.Value.Errors });
            foreach (var item in errorFieldsAndMsgs)
            {
                var fieldKey = item.Key;
                var fieldErrors = item.Errors.Select(e => new ShowErrorMsg(fieldKey, e.ErrorMessage));
                result.AddRange(fieldErrors);
            }
            return result;
        }

        public static IEnumerable<string> FristModelStateErrors(this ModelStateDictionary modelState)
        {
            try
            {
                //找到出错的字段以及出错信息
                var fristErrorMsg = modelState.FirstOrDefault();
                var result = fristErrorMsg.Value.Errors.Select(x => x.ErrorMessage);
                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }

    /// <summary>
    /// 错误消息对象
    /// </summary>
    public class ShowErrorMsg
    {
        public ShowErrorMsg(string key, string message)
        {
            Key = key;
            Message = message;
        }
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
