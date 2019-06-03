using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 菜单视图操作 
    /// </summary>
    public class ReqNavMenuView : ReqBaseOperation
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Key]
        [StringLength(50)]
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [StringLength(100)]
        public string MenuName { get; set; }

        /// <summary>
        /// 父级菜单id
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
        /// 图标样式
        /// </summary>
        [StringLength(50)]
        public string IconClass { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        [StringLength(300)]
        public string IconUrl { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int? Seq { get; set; }


        /// <summary>
        /// 是否可见
        /// </summary>
        public int? IsVisible { get; set; }
    }
}
