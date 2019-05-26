using DBModel;
using DBModel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackStageIBLL
{
    public interface ISys_ButtonBLL
    {
        int GetButtonCount();


        List<UserMenuButtonResult> ButtonQueryByuserId(string userId);
    }
}
