using Common;

namespace ViewModel.Enums
{
    /// <summary>
    /// 测试枚举
    /// </summary>
    public enum TestEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("正常")]
        Normal = 0,
        /// <summary>
        /// 冻结
        /// </summary>
        [Remark("冻结")]
        Frozen = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Remark("心情异常")]
        Error = 2,
        /// <summary>
        /// 少儿不宜
        /// </summary>
        [Remark("少儿不宜")]
        AgeSmall = 3
    }

}
