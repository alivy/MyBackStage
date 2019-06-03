using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 增删改共用操作
    /// </summary>
    public class ReqBaseOperation
    {
        /// <summary>
        /// 执行动作 ,见枚举<see cref="Operation"/>
        /// </summary>
        public int ExecutiveAction { get; set; }

      
    }


    public enum Operation
    {
        /// <summary>
        /// 增加
        /// </summary>
        Add = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3
    }
}
