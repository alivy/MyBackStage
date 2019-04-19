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
    [Export(typeof(ISys_ButtonBLL<Sys_button>))]
    public class Sys_buttonBLL : BaseBLL<Sys_button>, ISys_ButtonBLL<Sys_button>
    {
       [Import(typeof(ISys_UserDAL<Sys_User>))]
        private ISys_UserDAL<Sys_User> _user;
        public Sys_buttonBLL()
        {
            Compose(this);
        }



        #region 实现抽象类
        /// <summary>
        /// 实现抽象类
        /// </summary>
        //public override void SetDal()
        //{
        //    _baseDal = new Sys_buttonDal();
        //}
        #endregion

        public int GetButtonCount()
        {
            return _user.GetCount(x => x.OrganizeName.Equals(""));
        }
    }
}
