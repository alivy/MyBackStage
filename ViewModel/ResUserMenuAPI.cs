using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 当前用户的菜单导航
    /// </summary>
    public class ResUserMenuAPI
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        [StringLength(50)]
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [StringLength(100)]
        public string MenuName { get; set; }


        /// <summary>
        /// 菜单父级编号 #为最上级编号
        /// </summary>
        [StringLength(50)]
        public string ParentMenId { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [StringLength(300)]
        public string Url { get; set; }

        /// <summary>
        /// 菜单样式
        /// </summary>
        [StringLength(50)]
        public string IconClass { get; set; }

        /// <summary>
        /// 菜单图标地址
        /// </summary>
        [StringLength(300)]
        public string IconUrl { get; set; }

     
    }
}
