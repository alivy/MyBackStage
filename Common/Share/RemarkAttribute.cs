using System;
using System.Reflection;

namespace Common
{
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            _Remark = remark;
        }

        private string _Remark;

        public string Remark => _Remark;
    }

    /// <summary>
    /// 获取枚举
    /// </summary>
    public static class RemarkExtend
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="eValue"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum eValue)
        {
            return Remark(eValue);
        }

        public static string Remark(Enum eValue)
        {
            Type type = eValue.GetType();
            FieldInfo field = type.GetField(eValue.ToString());
            var remarkAttribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
            return remarkAttribute.Remark;
        }
    }
}
