using BackStageIBLL;
using Common;
using DBModel;
using MyBackStage.Models.HomeModel;
using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace MyBackStage.Controllers.HomeAciton
{
    public class LogionAction : BaseAction
    {
        [Import(typeof(ISys_UserBLL<Sys_User>))]
        private ISys_UserBLL<Sys_User> userBLL;

        /// <summary>
        /// 用户登录功能
        /// </summary>
        /// <param name="viewUser"></param>
        /// <returns></returns>
        public ActionResult Action(ViewUserLogin viewUser)
        {
            MEFBase.Compose(this);
            if (!ModelState.IsValid)
            {
                return View("UserLogin", viewUser);
            }
            string m_code = SessionManager.Get(ConstString.SysUserLoginValidateCode) as string;
            if (m_code == null)
            {
                ModelState.AddModelError("ValidateCode", "验证码超时，请重新获取");
                return View("UserLogin", viewUser);
            }
            //验证码
            //if (m_code.ToLower().Equals(viewUser.ValidateCode.Trim().ToLower()) == false)
            //{
            //    ModelState.AddModelError("ValidateCode", "验证码错误");
            //    return View("UserLogin", viewUser);
            //}
            viewUser.UserPwd = viewUser.UserPwd.GetMD5FromString();


            if (!CheckLogin(viewUser.UserName, viewUser.UserPwd))
            {
                ModelState.AddModelError("Password", "用户名或密码错误");
                return View("UserLogin", viewUser);
            }
            int m_userId = (int)SessionManager.Get(ConstString.SysUserLoginId);


            if (!string.IsNullOrEmpty(viewUser.BackUrl))
            {
                return new RedirectResult(viewUser.BackUrl.Trim());
            }
            return new RedirectResult("/Account/Index");
        }


        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool CheckLogin(string userName, string password)
        {
            bool validate = false;
            //防止注入
            userName = StringHelp.FilterSql(userName);
            password = StringHelp.FilterSql(password);
            var user = userBLL.FirstOrDefault<Sys_User>(x => x.UserNickName.Equals(userName) && x.Password.Equals(password));
            if (user != null)
            {
                //登录成功，添加Session
                SessionManager.Add(ConstString.UserLoginId, user.UserId);
                //验证ip,浏览器
                string IP = NetworkHelper.GetIp();
                string Browser = NetworkHelper.GetBrowser();
                //查询站内未读消息条数，并加入缓存
                //添加登录日志表，记录登录日志

                int m_guid = (user.UserId + Guid.NewGuid().ToString()).GetHashCode();
                //添加cookie消息
                CookiesManager.Add(ConstString.SysUserLoginGuid, user.UserId, DateTime.Now.AddDays(1));

                validate = true;
            }
            return validate;
        }
    }
}