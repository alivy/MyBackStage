namespace OpenPlatform.Tool
{
    /// <summary>
    /// 
    /// 
    /// 
    
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        dynamic GetUserInfo();
        void EndSession();
    }

}
