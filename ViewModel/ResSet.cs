using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 设置角色关联用户信息列表
    /// </summary>
    public class ResSetRoleUserInfos
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
        public List<string> UserInfos { get; set; }
    }



    /// <summary>
    /// 设置用户关联角色信息列表
    /// </summary>
    public class ResSetUserRoles
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "用户id不能为空")]
        [StringLength(100)]
        public string UserId { get; set; }

        /// <summary>
        /// 角色id集合
        /// </summary>
        public List<string> RoleIds { get; set; }
    }



    /// <summary>
    /// 设置用户关联机构信息列表
    /// </summary>
    public class ResSetUserOrganizes
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "用户id不能为空")]
        [StringLength(100)]
        public string UserId { get; set; }

        /// <summary>
        /// 角色id集合
        /// </summary>
        public List<string> OrganizeIds { get; set; }
    }
}
