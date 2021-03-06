﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 返回查询结果基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResBasePage<T>
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalRecordCount { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static ResBasePage<T> GetInstance(List<T> ts, int total)
        {
            return new ResBasePage<T>
            {
                Data= ts,
                TotalRecordCount = total
            };
        }
    }
}
