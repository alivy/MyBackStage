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
    [Export("Sys_LoginHistoryDAL", typeof(ISys_LoginHistoryDAL<Sys_LoginHistory>))]
    public class Sys_LoginHistoryDal : BaseDal<Sys_LoginHistory>, ISys_LoginHistoryDAL<Sys_LoginHistory>
    {

    }
}
