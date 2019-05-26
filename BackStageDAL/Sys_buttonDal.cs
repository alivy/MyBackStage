using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backStageIDal;
using DBModel.Result;

namespace BackStageDAL
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class Sys_buttonDal : DataAccessBase, ISys_ButtonDal
    {

        /// <summary>
        /// 获取用户页面按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserMenuButtonResult> ButtonQueryByuserId(string userId)
        {
            string sql = string.Format(@"select A.UserId,
                                         C.MendId,
                                         E.ButtonId,
                                         E.ButtonName
                                        from Sys_User A
                                        left join Sys_UserRoleMap B on a.UserId = b.UserId
                                        inner join Sys_MenuRoleMap C on b.RoleId = C.RoleId
                                        inner join  Sys_MenuButttonMap D on c.MendId = D.MenuId
                                        inner join Sys_button E on D.ButtonId = E.ButtonId 
                                        where A.UserId ={0}", userId);
            var result = CurrentContext.Database.SqlQuery<UserMenuButtonResult>(sql).ToList();
            return result;
        }

    }
}
