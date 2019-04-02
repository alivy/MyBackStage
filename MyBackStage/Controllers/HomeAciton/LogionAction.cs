using BackStageIBLL;
using Common;
using DBModel;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace MyBackStage.Controllers.HomeAciton
{
    public class LogionAction : BaseAction
    {
        [Import(typeof(ISys_UserBLL<Sys_User>))]
        private ISys_UserBLL<Sys_User> userBLL;

        public ActionResult Action()
        {
            MEFBase.Compose(this);
            var userName = "";
            var pwd = "";
            var user = userBLL.FirstOrDefault<Sys_User>(x => x.UserNickName.Equals(userName) && x.Password.Equals(pwd));
            return Json(user);
        }
    }
}