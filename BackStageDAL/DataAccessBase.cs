
using DBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackStageDAL
{
    /// <summary>
    /// 数据库访问层需要使用的基类，主要用来生成数据库连接环境
    /// </summary>
    public class DataAccessBase
    {
        private const int TimeOut = 600;
        public DbContext CurrentContext;
        /// <summary>
        /// 默认的构造函数使用Config库
        /// </summary>
        public DataAccessBase(ConnectionStringType connectionStringType = ConnectionStringType.WebSite)
        {
            CurrentContext = DBContextFactory(connectionStringType);
            CurrentContext.Database.CommandTimeout = TimeOut; //时间单位是毫秒
        }


        /// <summary>
        /// 此处工厂根据传参数选择需要连接数据库
        /// 先暂时不拓展
        /// </summary>
        /// <param name="connectionStringType"></param>
        /// <returns></returns>
        public DbContext DBContextFactory(ConnectionStringType connectionStringType)
        {
            if (connectionStringType == ConnectionStringType.WebSite)
            {
                return DBContext.CreateContext();
            }
            return DBContext.CreateContext();
        }
    }
    /// <summary>
    /// 数据库连接串的类型枚举
    /// </summary>
    public enum ConnectionStringType
    {
        /// <summary>
        /// 
        /// </summary>
        WebSite = 1
    }
}
