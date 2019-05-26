using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebSite.Controllers
{
    public class RequestActionResult<T> : ActionResult
    {
        public object Data { get; set; }
        public RequestActionResult(T t)
        {
            Data = t;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            string json = JsonConvert.SerializeObject(Data);
            context.HttpContext.Response.Write(json);
        }
    }

    /// <summary>
    /// js请求返回的结果对象
    /// </summary>
    public class RequestResult
    {
        public RequestResult()
            : this(null)
        {
        }

        public RequestResult(string message)
            : this(ResultTypeEnum.Success, message, null)
        {
        }

        public RequestResult(ResultTypeEnum resultType, string message)
            : this(resultType, message, null)
        {
        }

        public RequestResult(ResultTypeEnum resultType, string message, object returnEntity)
        {
            ResultType = resultType;
            Message = message;
            ReturnEntity = returnEntity;
        }

        [ScriptIgnore]
        public ResultTypeEnum ResultType { get; set; }

        public int ResultTypeId { get { return (int)ResultType; } set { ResultType = (ResultTypeEnum)value; } }

        public string Message { get; set; }

        public object ReturnEntity { get; set; }

        public static RequestResult Success()
        {
            return new RequestResult();
        }

        public static RequestResult Success(string message)
        {
            return new RequestResult(message);
        }

        public static RequestResult Success(string message, object entity)
        {
            return new RequestResult(ResultTypeEnum.Success, message, entity);
        }

        public static RequestResult Success(object entity)
        {
            return new RequestResult(ResultTypeEnum.Success, null, entity);
        }

        public static RequestResult Error(string message)
        {
            return new RequestResult(ResultTypeEnum.Error, message);
        }

        public static RequestResult Error()
        {
            return new RequestResult(ResultTypeEnum.Error, null);
        }

        public static RequestResult Error(string message, object entity)
        {
            return new RequestResult(ResultTypeEnum.Error, message, entity);
        }

        public static RequestResult Exception(string message)
        {
            return new RequestResult(ResultTypeEnum.Exception, message);
        }

        public static RequestResult ValidateError()
        {
            return new RequestResult(ResultTypeEnum.ValidateError, null);
        }

        public static RequestResult ValidateError(string message)
        {
            return new RequestResult(ResultTypeEnum.ValidateError, message);
        }

        public static RequestResult ValidateError(string message, string entity)
        {
            return new RequestResult(ResultTypeEnum.ValidateError, message, entity);
        }
    }
    /// <summary>
    /// 成功失败的枚举
    /// </summary>
    public enum ResultTypeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 验证失败
        /// </summary>
        ValidateError = 2,

        /// <summary>
        /// 错误，已知的错误
        /// </summary>
        Error = 3,

        /// <summary>
        /// 错误，写在try catch中的错误
        /// </summary>
        Exception = 4,

        /// <summary>
        /// 权限验证
        /// </summary>
        AuthorityCheck = 5,

        /// <summary>
        /// 基础权限校验失败
        /// 如：用户不存在，ip不允许等等
        /// </summary>
        BaseAuthorityCheckFailed = 6,
    }
}