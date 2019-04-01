using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using DBModel;

namespace BackStageBLL
{
    public class Sys_buttonBLL :
        BaseBLL<Sys_button>, 
        ISys_buttonBLL
    {
        private static ISys_buttonDal<Sys_button> _buttonDal;
        public Sys_buttonBLL() : 
            base(_buttonDal)
        {
            _buttonDal = new Sys_buttonDal();
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
            return _buttonDal.GetCount(x => x.ButtonName != "");
        }

    }
}
