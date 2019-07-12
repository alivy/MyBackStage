using BackStageIBLL;
using Common;
using Common.Share;
using DBModel;
using Newtonsoft.Json;
using OpenPlatform.Tool;
using OpenPlatform.Tool.Commom;
using OpenPlatform.Tool.OAuthClient.TencentQQ;
using OpenPlatform.Tool.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ViewModel;
using WebSite.Controllers.HomeAciton;
using WebSite.Models.HomeModel;

namespace WebSite.Controllers
{
    [Export]
    public class HomeController : Controller
    {

        private string PlatformCode = "qq";
        static readonly string ClientId = ConfigurationManager.AppSettings["clientId"];
        static readonly string ClientSecret = ConfigurationManager.AppSettings["clientSecret"];
        static readonly string CallbackUrl = ConfigurationManager.AppSettings["callbackUrl"];

        private IOAuthClient oauthClient;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HomeController()
        {
            oauthClient = new TencentQQClient(ClientId, ClientSecret, CallbackUrl);
            oauthClient.Option.State = PlatformCode;
        }

        [Import]
        private IShareBLL<Sys_User> userBll { get; set; }

        [Import]
        private IShareBLL<Sys_LoginHistory> loginHistoryBll { get; set; }

        [Import("Sys_NavMenu")]
        private ISys_NavMenuBLL _navMenuBll { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// web登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult WebLogin()
        {
            var ssionid = Session[ConstString.SysUserLoginId];
            //var cookie = CookiesManager.Get(ConstString.SysUserLoginGuid);
            //if (cookie != null && (bool)cookie)
            //    return Redirect(UrlString.LoginJumpUrl);
            if (ssionid != null)
                return Redirect(UrlString.LoginJumpUrl);
            Session.Clear();
            return View();
        }

        public ActionResult Logion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WebUserLogin(ViewUserLogin user)
        {
            var action = new LogionAction(userBll, loginHistoryBll, _navMenuBll);
            return action.Action(user);
        }
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Log4net()
        {
            Log.Write(LogLevel.Info, "Info 日志写入测试");
            Log.Write(LogLevel.Error, "Error 日志写入测试");
            Log.Write(LogLevel.Warn, "Warn 日志写入测试");
            Log.Write(LogLevel.Debug, "Debug 日志写入测试");
            Log.Write(LogLevel.Fatal, "Fatal 日志写入测试");

            return View();
        }




        /// <summary>
        /// 接受外部回调
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceCallback()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            string json = Encoding.UTF8.GetString(b);
            Log.Write(LogLevel.Debug, json);
            return Content(json);
        }


        /// <summary>
        /// QQ授权登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult GetQQLoginTest()
        {
            //第一步：获取开放平台授权地址
            string authorizeUrl = oauthClient.GetAuthorizeUrl(ResponseType.Code);
            ViewData["AuthorizeUrl"] = authorizeUrl;
            return View();
        }


        /// <summary>
        /// QQ登陆授权回调
        /// </summary>
        /// <returns></returns>
        public ActionResult Service()
        {
            string xmlDataPath = Server.MapPath("~/DataXML/AuthorizeXML.xml");
            string serverCallBackCode = Request["code"];
            //第二步：认证成功获取Code
            Dictionary<string, IOAuthClient> m_oauthClients = new Dictionary<string, IOAuthClient>();
            AuthToken accessToken = oauthClient.GetAccessTokenByAuthorizationCode(serverCallBackCode);
            if (accessToken.AccessToken == null)
                return Content("认证失败");
            oauthClient.User.GetUserInfo();
            Log.Write(LogLevel.Debug, JsonConvert.SerializeObject(accessToken));
            string nickName = oauthClient.Token.User.Nickname;
            string openId = oauthClient.Token.OAuthId;
            string responseResult = oauthClient.Token.TraceInfo;
            var user = JsonConvert.DeserializeObject<QQUserInfo>(oauthClient.Token.TraceInfo);
            ViewBag.user = user;
            return View();
        }

        /// <summary>
        /// 保存用户信息到xml
        /// </summary>
        private void saveUserInfoXml(List<QQUserInfo> userInfos)
        {


        }
    }
}