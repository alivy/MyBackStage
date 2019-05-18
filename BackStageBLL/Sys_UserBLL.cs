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
    [Export("Sys_UserBLL", typeof(ISys_UserBLL))]
    public class Sys_UserBLL : ISys_UserBLL
    {
        [Import("Sys_UserDAL")]
        private  ISys_UserDAL _user { get; set; }

        public Sys_UserBLL()
        {

        }


        public void testUser()
        {
           // MEFBase.Compose(this);
            var userList = _user.QueryAllUser();
        }
    }
}
