using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ResButton
    {
        /// <summary>
        /// 按钮Id
        /// </summary>
        public string ButtonId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 按钮排序
        /// </summary>
        public int ButtonSeq { get; set; }

        /// <summary>
        /// 按钮图标
        /// </summary>
        public string ButtonIcon { get; set; }


        public static ResButton CreatesInstance(string btnId, string btnName, int btnSeq, string btnIcon)
        {
            return new ResButton
            {
                ButtonId = btnId,
                ButtonName = btnName,
                ButtonSeq = btnSeq,
                ButtonIcon = btnIcon
            };
        }


    }
}

