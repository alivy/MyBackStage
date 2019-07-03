using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 设置菜单页面按钮
    /// </summary>
    public class ResSetMenuBtns
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        [Required(ErrorMessage ="菜单id不能为空")]
        [StringLength(50, ErrorMessage = "字符长度超长")]
        public string MenuId { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        public string BtnIds { get; set; }
    }

}
