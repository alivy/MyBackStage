using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using Common;
using DBModel;
using DBModel.Result;

namespace BackStageBLL
{
    [Export("Sys_ButtonBLL", typeof(ISys_ButtonBLL))]
    public class Sys_buttonBLL : ISys_ButtonBLL
    {

        [Import("Sys_buttonDal")]
        private ISys_ButtonDal _button { get; set; }

        /// <summary>
        /// 获取用户页面按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mendId"></param>
        /// <returns></returns>
        public List<UserMenuButtonResult> ButtonQueryByMendId(string userId, string mendId)
        {
            var result = ButtonQueryByUserId(userId);
            return result.Where(x => x.MendId.Equals(mendId)).ToList();
        }

        /// <summary>
        /// 获取用户页面按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserMenuButtonResult> ButtonQueryByUserId(string userId)
        {
            var result = _button.ButtonQueryByuserId(userId);
            if (result != null)
            {
                var key = Sys_button.GetKey(userId);
                CacheManager.Add(key, result);
            }
            return result;
        }
    }
}
