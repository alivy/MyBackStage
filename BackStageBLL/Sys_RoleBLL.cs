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
    [Export("Sys_RoleBLL", typeof(ISys_UserBLL))]
    public class Sys_RoleBLL : ISys_RoleBLL
    {
        [Import("Sys_RoleDAL")]
        private ISys_RoleDAL _user { get; set; }

    }
}
