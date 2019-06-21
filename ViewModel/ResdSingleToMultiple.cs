using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 获取单对多的关系
    /// </summary>
    public class ResdSingleToMultiple<T>
    {

        /// <summary>
        /// 单个id编号
        /// </summary>
        public string SingId { get; set; }


        /// <summary>
        /// 整体元素集合
        /// </summary>
        public IEnumerable<T> AllElements { get; set; }


        /// <summary>
        /// 单个结构拥有的多个元素
        /// </summary>
        public IEnumerable<T> HaveElements { get; set; }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="allElements"></param>
        /// <param name="haveElements"></param>
        /// <param name="singId"></param>
        /// <returns></returns>
        public static ResdSingleToMultiple<T> CreateObject(IEnumerable<T> allElements, IEnumerable<T> haveElements, string singId)
        {

            return new ResdSingleToMultiple<T>
            {
                SingId = singId,
                AllElements = allElements,
                HaveElements = haveElements
            };
        }
    }
}
