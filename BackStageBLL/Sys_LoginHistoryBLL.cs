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
    [Export("Sys_LoginHistoryBLL", typeof(ISys_LoginHistoryBLL))]
    public class Sys_LoginHistoryBLL : ISys_LoginHistoryBLL
    {
        [Import("Sys_LoginHistoryDAL")]
        private ISys_LoginHistoryDAL _loginHistory { get; set; }

        [Import("Sys_UserDAL")]
        private ISys_UserDAL _user { get; set; }

     

        public int GetUserCountTest()
        {
            return 0;
        }

    }
}
