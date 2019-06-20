
namespace WebSite
{
    public class ModelValidMsg
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }

        // 摘要:
        //     获取或设置一条在验证失败的情况下与验证控件关联的错误消息。
        //
        // 返回结果:
        //     与验证控件关联的错误消息。
        public string ErrorMessage { get; set; }

    }
}
