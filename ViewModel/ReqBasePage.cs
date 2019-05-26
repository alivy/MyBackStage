using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 分页查询公用请求参数基类
    /// </summary>
    public class ReqBasePage
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int pageIndex { get; set; }
    }
}
