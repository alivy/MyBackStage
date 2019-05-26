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
    [Export("Sys_RoleDAL", typeof(ISys_RoleDAL))]
    public class Sys_RoleDal : DataAccessBase, ISys_RoleDAL
    {

      
    }
}
