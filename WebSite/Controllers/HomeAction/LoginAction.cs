using BackStageIBLL;
using Common;
using Common.Share.Http;
using DBModel;

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web.Mvc;
using WebSite.Models.HomeModel;

namespace WebSite.Controllers.HomeAciton
{
    [Export]
    public class LogionAction : BaseAction
    {
        private ISys_UserBLL<Sys_User> userBll { get; set; }

        private ISys_LoginHistoryBLL<Sys_LoginHistory> loginHistoryBLL { get; set; }

        public LogionAction(ISys_UserBLL<Sys_User> sys_UserBLL,ISys_LoginHistoryBLL<Sys_LoginHistory> sys_LoginHistoryBLL)
        {
            userBll = sys_UserBLL;
            loginHistoryBLL = sys_LoginHistoryBLL;
        }


        /// <summary>
        /// 用户登录功能
        /// </summary>
        /// <param name="viewUser"></param>
        /// <returns></returns>
        public ActionResult Action(ViewUserLogin viewUser)
        {
            int userCount = userBll.GetCount();
            if (!ModelState.IsValid)
            {
                return View("UserLogin", viewUser);
            }
            //if (!(SessionManager.Get(ConstString.SysUserLoginValidateCode) is string m_code))
            //{
            //    ModelState.AddModelError("ValidateCode", "验证码超时，请重新获取");
            //    return View("UserLogin", viewUser);
            //}
            //验证码
            //if (m_code.ToLower().Equals(viewUser.ValidateCode.Trim().ToLower()) == false)
            //{
            //    ModelState.AddModelError("ValidateCode", "验证码错误");
            //    return View("UserLogin", viewUser);
            //}
          
            if (!CheckLogin(viewUser))
            {
                ModelState.AddModelError("Password", "用户名或密码错误");
                return View("UserLogin", viewUser);
            }

          
            int m_userId = int.Parse(SessionManager.Get(ConstString.UserLoginId).ToString());

            return Redirect(string.IsNullOrEmpty(viewUser.BackUrl) ? "/ShowBoard/Index" : viewUser.BackUrl);
          
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
            //防止注入
            var userName = StringHelp.FilterSql(userInfo.UserName);
            var userPwd = StringHelp.FilterSql(userInfo.UserPwd);
           
            var user = userBll.FirstOrDefault<Sys_User>(x => x.UserNickName.Equals(userName) && x.Password.Equals(userPwd));
            if (user != null)
            {
                //登录成功，添加Session
                SessionManager.Add(ConstString.UserLoginId, user.UserId);
                CacheManager.Add(ConstString.SysUserInfo + user.UserId, user);
                //验证ip,浏览器
                string IP = NetworkHelper.GetIp();
                //浏览器
                string Browser = NetworkHelper.GetBrowser();
                //查询站内未读消息条数，并加入缓存
                //添加登录日志表，记录登录日志

                var lanIP = ZHttp.ClientIP;
                loginHistoryBLL.AddEntity(new Sys_LoginHistory
                {
                    UserId = user.UserId,
                    HostIP = ZHttp.ClientIP,
                    HostName = ZHttp.IsLanIP(ZHttp.ClientIP) ? ZHttp.ClientHostName : string.Empty, //如果是内网就获取，否则出错获取不到，且影响效率
                    LoginBrowser = Browser,
                    LoginLocal =userInfo.City,
                    LoginDate = DateTime.Now 
                });
                int m_guid = (user.UserId + Guid.NewGuid().ToString()).GetHashCode();
                //添加cookie消息
                CookiesManager.Add(ConstString.SysUserLoginGuid, user.UserId, DateTime.Now.AddDays(1));
                validate = true;
            }
            return validate;
        }


        public void Compose()
        {
            var catalog = new AssemblyCatalog(Assembly.GetEntryAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}