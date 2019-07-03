using DBModel;
using DBModel.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BackStageIBLL
{
    [InheritedExport]
    public interface ISys_UserBLL
    {
        void testUser();


        List<ResUserInfoAPI> GetUserInfo(string userid, ReqBasePage page);
    }
}
