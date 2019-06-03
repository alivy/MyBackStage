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

            var tt = programList.Where((x, y) => x.TsetName.Contains(y.ToString())).ToList();

            Func<List<TestModel>, string, List<TestModel>> f = (p, y) => p.Where(x => x.TsetName.Contains(y)).ToList();

            var cc = f(programList, "1");


            CacheManager.Add("TestModel", programList);
            //读取缓存
            if (CacheManager.Contains("TestModel"))
                programList = CacheManager.GetData<List<TestModel>>("TestModel");

            //同名key不同value会认为属于一条缓存
            var cacheCount = CacheManager.Count;

            Assert.AreEqual(chars, "小严");
            Assert.AreEqual(testModel.TsetName, "小严");



           
            //from ba in ab.DefaultIfEmpty(new ModelXXXB())


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

        /// <summary>
        /// 大德需要的 
        /// </summary>
        [TestMethod]
        public void DadeTest()
        {
            List<int> result = new List<int>();
            for (int i = 100000000; i < 999999999; i++)
            {
                var intList = new List<int>();
                var str = i.ToString();
                if (str.Length == 9)
                {
                    for (int j = 0; j < str.Length; j++)
                    {
                        int c = j + 1;
                        intList.Add(int.Parse(str.Substring(j, c)));
                    }
                }
                var resultInt = intList.Distinct();
                if (resultInt.Count() == 9)
                {
                    result.Add(int.Parse(str));
                }
            }
            var cc = result.Count;

        }



        [TestMethod]
        public void DadeTest1()
        {
            var numArry = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int a = 0; a < numArry.Length; a++)
            {
                var reuInts = new List<int>();
                reuInts.Add(a);//选取第一个数
                for (int b = 0; b < numArry.Length; b++)
                {
                    if (reuInts.Contains(b)) //判断是否已经存在集合中
                        continue;
                    reuInts.Add(b);//选取第二个数
                    for (int c = 0; c < numArry.Length; c++)
                    {
                        if (reuInts.Contains(c))
                            continue;
                        reuInts.Add(c);//选取第三个数

                        for (int d = 0; d < numArry.Length; d++)
                        {
                            if (reuInts.Contains(d))
                                continue;
                            reuInts.Add(d);//选取第四个数

                            //以此类推
                            //假如已经有9个循环，那么将取得一个9位数集合
                            if (reuInts.Count == 9)
                            {
                                //拼接后，此处就会得到一个不重复的9位数
                            }
                        }
                    }
                }
            }



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reuInts"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public List<int> Loop(List<int> reuInts, int length)
        {
            for (int d = 0; d < length; d++)
            {
                if (!reuInts.Contains(d))
                {
                    reuInts.Add(d);
                    break;
                }
            }
            return reuInts;
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
