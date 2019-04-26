using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using DBModel;

namespace BackStageBLL
{
    [Export("Sys_LoginHistoryBLL", typeof(ISys_LoginHistoryBLL<Sys_LoginHistory>))]
    public class Sys_LoginHistoryBLL : BaseBLL<Sys_LoginHistory>, ISys_LoginHistoryBLL<Sys_LoginHistory>
    {
        [Import("Sys_LoginHistoryDAL")]
        private ISys_LoginHistoryDAL<Sys_LoginHistory> _loginHistory { get; set; }

        [Import("Sys_UserDAL")]
        private ISys_UserDAL<Sys_User> _user { get; set; }

        public override void SetDal()
        {
            _baseDal = _loginHistory ?? new Sys_LoginHistoryDal();
        }

        public int GetUserCountTest()
        {
            return _user.GetCount();
        }

    }
}
