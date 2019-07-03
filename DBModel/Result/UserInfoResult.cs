using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Result
{
   public class UserInfoResult
    {
       
        public string UserId { get; set; }

       
        public string UserRoleName { get; set; }

     
        public string UserNickName { get; set; }

       
        public string PassWord { get; set; }

       
        public string OrganizeName { get; set; }

        public List<UserInfoResult> userInfoResult { get; set; }
    }
}
