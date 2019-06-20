using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 设置角色关联菜单
    /// </summary>
    public class ResSetRoleMenus
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [Required(ErrorMessage = "角色id不能为空")]
        [StringLength(100)]
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单id集合
        /// </summary>
        public List<string> Menus { get; set; }
    }
}
