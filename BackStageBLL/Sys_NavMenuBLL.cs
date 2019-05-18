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
    [Export("Sys_NavMenu", typeof(ISys_NavMenuBLL))]
    public class Sys_NavMenuBLL : ISys_NavMenuBLL
    {
        [Import("NavMenuDal")]
        private  ISys_NavMenuDal _navMenu { get; set; }


        public Sys_NavMenuBLL()
        {
            //MEFBase.Compose(this);
        }

        /// <summary>
        /// 根据用户id获取用户菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_NavMenu> GetNavMenuByUserId(string userId)
        {
            return _navMenu.GetNavMenuByUserId(userId);
        }
    }
}
