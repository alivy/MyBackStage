using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backStageIDal;
using System.ComponentModel.Composition;
using DBModel.Result;
using ViewModel;
using System.Data;
using Common;

namespace BackStageDAL
{
    [Export("Sys_UserDAL", typeof(ISys_UserDAL))]
    public class Sys_UserDal : DataAccessBase, ISys_UserDAL
    {

        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<Sys_User> QueryAllUser()
        {
            var result = CurrentContext.Set<Sys_User>().ToList();
            CurrentContext.Database.Log = (sql) => Log.Write(LogLevel.Info, sql);
            return result;
        }

        public List<ResUserInfoAPI> GetUserInfo(string userid, ReqBasePage page)
        {
            using (CurrentContext)
            {
                string sql = string.Format("exec Common_PageList '{0}','{1}','{2}',{3},{4},'{5}',{6}", "Sys_User", "*", !string.IsNullOrWhiteSpace(userid) ? string.Format("  userid=''{0}''", userid) : "1=1", page.pageIndex, page.pageSize, "userid", 0);
                CurrentContext.Database.Log = (sql1) => Log.Write(LogLevel.Info, sql);
                var result = CurrentContext.Database.SqlQuery<ResUserInfoAPI>(sql).ToList();
                return result;
            }
        }

    }
}
;