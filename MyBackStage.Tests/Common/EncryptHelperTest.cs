using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBackStage.Tests.Common
{


    /// <summary>
    /// 缓存测试类
    /// </summary>
    [TestClass]
    public class EncryptHelperTest
    {

        /// <summary>
        /// 缓存读取测试
        /// </summary>
        [TestMethod]
        public void EncryptTest()
        {
            string str = "测试";
            var encryptStr = str.Encrypt();
            var decryptStr = encryptStr.Decrypt();
            Assert.AreEqual(decryptStr, str);
        }
    }
}
