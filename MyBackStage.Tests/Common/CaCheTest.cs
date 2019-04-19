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
    public class CacheTest
    {

        /// <summary>
        /// 缓存读取测试
        /// </summary>
        [TestMethod]
        public void CacheGetAndSetTest()
        {

            //可在CacheManager配置可以创建不同的cache对象
            CacheManager.Add("chars", "小严");
            var chars = CacheManager.GetData<string>("chars");

            var testModel = new TestModel { TsetName = "小严" };
            CacheManager.Add("chars", testModel);
            //读取缓存
            testModel = CacheManager.GetData<TestModel>("chars");

            //写入list缓存
            var programList = Query();
            CacheManager.Add("TestModel", programList);
            //读取缓存
            if (CacheManager.Contains("TestModel"))
                programList = CacheManager.GetData<List<TestModel>>("TestModel");

            //同名key不同value会认为属于一条缓存
            var cacheCount = CacheManager.Count;

            Assert.AreEqual(chars, "小严");
            Assert.AreEqual(testModel.TsetName, "小严");
        }





        /// <summary>
        /// 查询耗时数据
        /// </summary>
        /// <returns></returns>
        public List<TestModel> Query()
        {
            var modelList = new List<TestModel>();
            for (int i = 0; i < 100; i++)
            {
                modelList.Add(new TestModel() { TsetName = "创建名称" + i });
            }
            return modelList;
        }
    }

    /// <summary>
    /// 测试对象
    /// </summary>
    public class TestModel
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string TsetName { get; set; }

    }

}
