using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backStageIDal;
using System.ComponentModel.Composition;

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
            return result;
        }
    }
}
