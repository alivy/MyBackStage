using Common;


namespace ViewModel.Enums
{
    /// <summary>
    /// 执行数据操作成功返回消息
    /// </summary>
    public enum ExecutSuccessMsgEnum
    {
        [Remark("执行添加操作失败")]
        AddSuccess = 1,

        [Remark("执行修改操作失败")]
        UpdateSuccess = 2,

        [Remark("执行删除操作失败")]
        DelSuccess = 3,

        [Remark("执行查询操作失败")]
        QuerySuccess = 4
    }
}

