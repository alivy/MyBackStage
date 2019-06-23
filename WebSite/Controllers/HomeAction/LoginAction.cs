using BackStageIBLL;
using Common;
using Common.Expand;
using Common.Share.Http;
using DBModel;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using ViewModel;
using ViewModel.Enums;
using WebSite.Models.HomeModel;

namespace WebSite.Controllers.HomeAciton
{
    [Export]
    public class LogionAction : BaseAction
    {


        private IShareBLL<Sys_User> userBll { get; set; }


        private IShareBLL<Sys_LoginHistory> loginHistoryBLL { get; set; }


        private ISys_NavMenuBLL navMenuBll { get; set; }


        public LogionAction(IShareBLL<Sys_User> sys_UserBLL, IShareBLL<Sys_LoginHistory> sys_LoginHistoryBLL,
            ISys_NavMenuBLL sys_navMenuBll)
        {
            userBll = sys_UserBLL;
            loginHistoryBLL = sys_LoginHistoryBLL;
            navMenuBll = sys_navMenuBll;
        }


        /// <summary>
        /// 用户登录功能
        /// </summary>
        /// <param name="viewUser"></param>
        /// <returns></returns>
        public ActionResult Action(ViewUserLogin viewUser)
        {
            if (!ModelState.IsValid)
            {
                var errorMsg = ModelState.FristModelStateErrors().FirstOrDefault(); ;
                return RequestAction(RequestResult.ValidateError(errorMsg));
            }
            if (!CheckLogin(viewUser))
                return RequestAction(RequestResult.Error("用户名或密码错误", viewUser));

            var resultUrl = UrlString.LoginJumpUrl + (string.IsNullOrEmpty(viewUser.BackUrl) ? "" : "?backUrl=" + viewUser.BackUrl);
            var result = new { Code = 1, Url = resultUrl };
            return RequestAction(RequestResult.Success("", result));
        }
        

        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool CheckLogin(ViewUserLogin userInfo)
        {
            bool validate = false;
            userInfo.UserPwd = userInfo.UserPwd.GetMD5FromString();
            var userName = StringHelp.FilterSql(userInfo.UserName);
            var userPwd = StringHelp.FilterSql(userInfo.UserPwd);
            var user = userBll.FirstOrDefault<Sys_User>(x => x.UserNickName.Equals(userName) && x.Password.Equals(userPwd));
            if (user != null)
            {
                //var session = HttpContext.Session[ConstString.SysUserLoginId];
                //if (session == null)
                //{
                SessionManager.Add(ConstString.SysUserLoginId, user.UserId);
                string browser = NetworkHelper.GetBrowser();
                string hostIP = NetworkHelper.GetIp() != "0.0.0.0" ? NetworkHelper.GetIp() : ZHttp.ClientIP;
                string hostName = ZHttp.IsLanIP(ZHttp.ClientIP) ? ZHttp.ClientHostName : string.Empty; //如果是内网就获取，否则出错获取不到，且影响效率
                loginHistoryBLL.AddEntity(Sys_LoginHistory.CreateInstance(user.UserId, hostName, hostIP, userInfo.City, browser));
                SetUserCache(user);
                SetCookie(user.UserId);
                //}
                validate = true;
            }
            return validate;
        }






        /// <summary>
        /// 需要放入缓存的用户信息
        /// </summary>
        public void SetUserCache(Sys_User user)
        {
            //用户信息添加缓存
            string userId = user.UserId;
            CacheManager.Add(Sys_User.GetKey(userId), user);
            //用户菜单信息
            var userMenus = navMenuBll.GetNavMenuByUserId(userId);
            //查询站内未读消息条数，并加入缓存
        }



        /// <summary>
        /// 设置cookie，以及存在时间
        /// </summary>
        /// <param name="userId"></param>
        public void SetCookie(string userId)
        {
            CookiesManager.Add(ConstString.SysUidCookieName, userId.Encrypt(), DateTime.Now.AddMonths(1));
            CookiesManager.Add(ConstString.SysIsNeedAutoLogin, "true", DateTime.Now.AddMonths(1));
        }


        #region API用户登录功能

        /// <summary>
        /// API用户登录功能
        /// </summary>
        /// <param name="viewUser"></param>
        /// <returns></returns>
        public ActionResult APIAction(ViewUserLogin viewUser)
        {
            if (!ModelState.IsValid)
            {
                var errorMsg = ModelState.FristModelStateErrors().FirstOrDefault(); ;
                return RequestAction(ResMessage.CreatMessage(LoginEnum.ReqDateError, errorMsg));
            }
            var check = APICheckLogin(viewUser);
            if (!check.Item1)
            {
                return RequestAction(ResMessage.CreatMessage(LoginEnum.LandError));
            }
            var userId = check.Item2;
            LoginHistory(userId, viewUser.City);
            var token = userId.Encrypt();
            return RequestAction(ResMessage.CreatMessage(LoginEnum.Suceess, ResLoginAPI.GetInstance(token)));
        }

        /// <summary>
        /// API检查用户登录 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public Tuple<bool, string> APICheckLogin(ViewUserLogin userInfo)
        {
            bool validate = false;
            userInfo.UserPwd = userInfo.UserPwd.GetMD5FromString();
            var userName = StringHelp.FilterSql(userInfo.UserName);
            var userPwd = StringHelp.FilterSql(userInfo.UserPwd);
            var user = userBll.FirstOrDefault<Sys_User>(x => x.UserNickName.Equals(userName) && x.Password.Equals(userPwd));
            string userId = string.Empty;
            if (user != null)
            {
                userId = user.UserId;
                SetUserCacheAPI(user);
                validate = true;
            }
            return new Tuple<bool, string>(validate, userId);
        }

        /// <summary>
        /// 添加登陆记录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public void LoginHistory(string UserId, string City)
        {
            try
            {
                SessionManager.Add(ConstString.SysUserLoginId, UserId);
                string browser = NetworkHelper.GetBrowser();
                string hostIP = NetworkHelper.GetIp() != "0.0.0.0" ? NetworkHelper.GetIp() : ZHttp.ClientIP;
                string hostName = ZHttp.IsLanIP(ZHttp.ClientIP) ? ZHttp.ClientHostName : string.Empty; //如果是内网就获取，否则出错获取不到，且影响效率
                var loginHistory = Sys_LoginHistory.CreateInstance(UserId, hostName, hostIP, City, browser);
                loginHistoryBLL.AddEntity(loginHistory);
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.Error, "添加登陆记录日志表出错", ex);
            }
        }


        /// <summary>
        /// API用户过期信息缓存
        /// </summary>
        public void SetUserCacheAPI(Sys_User user)
        {
            //生成用户token
            var token = Sys_User.GetKey(user.UserId);
            CacheManager.Add(token, user, 12 * 60);
        }
        #endregion
    }
}