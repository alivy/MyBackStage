using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using Common;
using DBModel;

namespace BackStageBLL
{
    [Export("Sys_ButtonBLL", typeof(ISys_ButtonBLL<Sys_button>))]
    public class Sys_buttonBLL : BaseBLL<Sys_button>, ISys_ButtonBLL<Sys_button>
    {
        [Import("Sys_UserDAL")]
        private ISys_UserDAL<Sys_User> _user { get; set; }

        [Import("Sys_LoginHistoryDAL")]
        private ISys_LoginHistoryDAL<Sys_LoginHistory> _loginHistory { get; set; }
        public Sys_buttonBLL()
        {

        }
        public override void SetDal()
        {
            //_baseDal = _user ?? new Sys_buttonDal();
        }


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
            var hcount = _loginHistory.GetCount();
            return _user.GetCount(x => x.OrganizeName.Equals(""));
        }
    }
}
