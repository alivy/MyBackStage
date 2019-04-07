using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBackStage.Tests.Common
{
    /// <summary>
    /// 日志测试类
    /// </summary>
    [TestClass]
    public class LogTest
    {

        /// <summary>
        /// 日志写入测试
        /// </summary>
        [TestMethod]
        public void log4net_Write()
        {
            Log.Write(LogLevel.Info, "Info 日志写入测试");
            Log.Write(LogLevel.Error, "Error 日志写入测试");
            Log.Write(LogLevel.Warn, "Warn 日志写入测试");
            Log.Write(LogLevel.Debug, "Debug 日志写入测试");
            Log.Write(LogLevel.Fatal, "Fatal 日志写入测试");
        }

        /// <summary>
        /// 多线程异步写入测试
        /// </summary>
        [TestMethod]
        public void Action_LogWrite()
        {
            for (int i = 0; i < 100; i++)
            {
                var task = Task.Run(() => Log.Write(LogLevel.Info, "Info 日志写入测试" + i));
            }
            Task.WaitAll();
            Log.Write(LogLevel.Info, "打印完成");
        }
    }
}
