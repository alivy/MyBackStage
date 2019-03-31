using BackStageIBLL;
using TestDB;

namespace BackStageBLL
{
    public class Sys_buttonBLL : BaseBLL<Sys_button>, ISys_buttonBLL
    {
        public int GetButtonCount()
        {
            return this.GetCount(x => x.ButtonName != "");
        }

    }
}
