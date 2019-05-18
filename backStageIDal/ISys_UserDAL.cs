using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backStageIDal
{
    [InheritedExport]
    public interface ISys_UserDAL
    {
        List<Sys_User> QueryAllUser();
    }
}
