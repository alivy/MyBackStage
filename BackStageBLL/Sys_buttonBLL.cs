﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using DBModel;

namespace BackStageBLL
{
    public class Sys_buttonBLL : BaseBLL<Sys_button>, ISys_ButtonBLL<Sys_button>
    {

        private ISys_ButtonDal<Sys_User> _user;
        public Sys_buttonBLL()
        {
            _baseDal = new Sys_buttonDal();
            _user = new Sys_UserDal();
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
            var userCount = _user.GetCount(x => x.OrganizeName.Equals(""));
            return _baseDal.GetCount(x => x.ButtonName.Equals(""));
        }
    }
}
