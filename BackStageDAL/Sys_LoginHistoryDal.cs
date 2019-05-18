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
    [Export("Sys_LoginHistoryDAL", typeof(ISys_LoginHistoryDAL))]
    public class Sys_LoginHistoryDal : DataAccessBase, ISys_LoginHistoryDAL
    {

    }
}
