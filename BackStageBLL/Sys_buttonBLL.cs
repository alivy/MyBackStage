using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using Common;
using DBModel;
using DBModel.Result;

namespace BackStageBLL
{
    [Export("Sys_ButtonBLL", typeof(ISys_ButtonBLL))]
    public class Sys_buttonBLL : ISys_ButtonBLL
    {
        [Import("Sys_UserDAL")]
        private ISys_UserDAL _user { get; set; }

        [Import("Sys_LoginHistoryDAL")]
        private ISys_LoginHistoryDAL _loginHistory { get; set; }

        [Import]
        private IBaseDal<Sys_User> _shareUser { get; set; }

        [Import]
        private IBaseDal<Sys_LoginHistory> _shareLoginHistory { get; set; }

        [Import]
        private ISys_ButtonDal _button { get; set; }
        #region 实现抽象类
        /// <summary>
        /// 实现抽象类
        /// </summary>
        //public override void SetDal()
        //{
        //    _baseDal = new Sys_buttonDal();
        //}
        #endregion

        public int GetButtonCount()
        {
            var hcount = _shareUser.GetCount();
            return _shareUser.GetCount(x => x.OrganizeName.Equals(""));
        }


        /// <summary>
        /// 获取用户页面按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserMenuButtonResult> ButtonQueryByuserId(string userId)
        {
            var result = _button.ButtonQueryByuserId(userId);
            if (result != null)
            {
                var key = Sys_button.GetKey(userId);
                CacheManager.Add(key, result);
            }
            return result;
        }
    }
}
