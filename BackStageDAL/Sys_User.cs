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
    [Export(typeof(ISys_UserDAL<Sys_User>))]
    public class Sys_UserDal : BaseDal<Sys_User>, ISys_UserDAL<Sys_User>
    {

    }
}
