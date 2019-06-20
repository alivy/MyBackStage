using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 获取菜单对应按钮
    /// </summary>
    public class ResMenuButtons
    {

        /// <summary>
        /// 菜单id
        /// </summary>
        public string MenuId { get; set; }


        /// <summary>
        /// 所有button
        /// </summary>
        public List<Sys_button> AllButtons { get; set; }


        /// <summary>
        /// 菜单拥有的buttons
        /// </summary>
        public List<Sys_button> MenuButtons { get; set; }
    }

     
}
