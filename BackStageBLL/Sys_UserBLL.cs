using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using DBModel;

namespace BackStageBLL
{
    [Export("Sys_UserBLL",typeof(ISys_UserBLL<Sys_User>))]
    public class Sys_UserBLL : BaseBLL<Sys_User>, ISys_UserBLL<Sys_User>
    {

      
        public Sys_UserBLL()
        {
            _baseDal = new Sys_UserDal();
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

      
    }
}
