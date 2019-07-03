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
using System.Data.SqlClient;

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
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<ResUserInfoAPI> GetUserInfo(string userid, ReqBasePage page)
        {

            string sql = string.Format(@"exec Common_PageList '{0}','{1}','{2}',{3},{4},'{5}',{6}",
                "Sys_User", "*", !string.IsNullOrWhiteSpace(userid) ? string.Format("  userid=''{0}''", userid) : "1=1",
                page.pageIndex, page.pageSize, "userid", 0);
            //第一 net fk4.8环境下已经改善字符拼接写法
            string field = !string.IsNullOrWhiteSpace(userid) ? string.Format("  userid=''{0}''", userid) : "1 = 1";
            string sql1 = $"exec Common_PageList '{"Sys_User"}''{"*"}''{ field}'{page.pageIndex},{page.pageSize}'{"userid"}',{0}";

            //第二，可以用这种方式声明，简单明了
            SqlParameter[] m_parms = new SqlParameter[7]
            {
                new SqlParameter("@tab","Sys_User"),
                new SqlParameter("@strFld","*"),
                new SqlParameter("@strWhere",field),
                new SqlParameter("@PageIndex",page.pageIndex),
                new SqlParameter("@PageSize",page.pageSize),
                new SqlParameter("@Sort","userid"),
                new SqlParameter("@IsGetCount",0)
            };
            string sql2 = $"exec Common_PageList @tab,@strFld,@strWhere,@PageIndex,@PageSize,@Sort,@IsGetCount";

            using (var db = CurrentContext)
            {
                var result = db.Database.SqlQuery<ResUserInfoAPI>(sql2, m_parms).ToList();
                db.Database.Log = (x) => Log.Write(LogLevel.Info, x); ///这个是获取db的sql执行代码，内部委托
                return result;
            }
        }

    }
}
;