using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class ResUserInfoAPI 
    {
        [StringLength(100)]
        public string UserId { get; set; }

        [StringLength(100)]
        public string UserRoleName { get; set; }

        [StringLength(100)]
        public string UserNickName { get; set; }

        [StringLength(100)]
        public string PassWord { get; set; }

        [StringLength(100)]
        public string OrganizeName { get; set; }
    }
}
