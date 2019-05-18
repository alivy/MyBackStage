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
    [Export("NavMenuDal", typeof(ISys_NavMenuDal))]
    public class Sys_NavMenuDal : DataAccessBase, ISys_NavMenuDal
    {

        /// <summary>
        /// 根据用户id获取用户菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_NavMenu> GetNavMenuByUserId(string userId)
        {
            using (CurrentContext)
            {
                string sql = string.Format(@"select
                                        F.*
                                        from Sys_User A
                                        left
                                        join Sys_UserRoleMap B on A.UserId = B.UserId
                                        inner
                                        join Sys_Role C on B.RoleId = C.RoleId
                                        left
                                        join Sys_MenuRoleMap D on B.RoleId = D.RoleId
                                        inner
                                        join Sys_NavMenu F on D.MendId = F.MenuId
                                        where a.UserId = {0}", userId);
                var userMenus = CurrentContext.Database.SqlQuery<Sys_NavMenu>(sql).ToList();
                return userMenus;
            }
        }
    }
}
