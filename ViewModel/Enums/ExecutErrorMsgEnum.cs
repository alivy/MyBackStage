using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Enums
{
    /// <summary>
    /// 执行数据操作失败返回消息
    /// </summary>
    public enum ExecutErrorMsgEnum
    {
        [Remark("执行添加操作失败")]
        AddFail = 1,

        [Remark("执行修改操作失败")]
        UpdateFail = 2,

        [Remark("执行删除操作失败")]
        DelFail = 3,

        [Remark("执行查询操作失败")]
        QueryFail = 4
    }
}
