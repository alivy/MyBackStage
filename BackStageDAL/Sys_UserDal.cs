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
                string sql = string.Format(@"select * 
                                    from (select top {0} * 
                                    from (select top {1} * 
                                    from Sys_User where 1=1 and {2}
                                    order by UserId asc ) 
                                    as temp_sum_student 
                                    order by UserId desc ) temp_order
                                    order by UserId asc", page.pageSize, page.pageIndex* page.pageSize, !string.IsNullOrWhiteSpace(userid)? string.Format("  userid='{0}'", userid):"");
                //string sql = string.Format(@"select  UserId,UserRoleName,UserNickName,PassWord,OrganizeName  from Sys_User");
                
                // CurrentContext.Database.Log = (sql1) => Log.Write(LogLevel.Info, sql);
                Logger.AddLog(sql);
                var result = CurrentContext.Database.SqlQuery<ResUserInfoAPI>(sql).Skip(page.pageSize * (page.pageIndex - 1)).Take(page.pageSize).ToList();
                return result;
            }
        }

    }
}
;