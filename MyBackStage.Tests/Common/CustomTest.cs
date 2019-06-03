using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyBackStage.Tests.Common
{
    [TestClass]
    public class CustomTest
    {

        /// <summary>
        /// 取两个list的jion结果
        /// </summary>
        [TestMethod]
        public void LinqJoinTest()
        {
            var listA = new List<ListA>();
            for (int i = 0; i < 10; i++)
            {
                listA.Add(new ListA { Id = i + 5, Msg = "我是A" + i });
            }
            listA = listA.OrderByDescending(x => x.Id).ToList();


            var listB = new List<ListB>();
            for (int i = 0; i < 5; i++)
            {
                listB.Add(new ListB { BId = i, BMsg = "我是A" + i });
            }
            listB.Add(new ListB { BId = 99999, BMsg = "我是B" });
            //左表关联查询 查询得到10条结果
            var list = (from a in listA
                        join b in listB on a.Msg equals b.BMsg into ab
                        from b1 in ab.DefaultIfEmpty()
                        orderby b1.BId
                        select a).ToList();

            var list1 = (from a in listB
                         join b in listA on a.BMsg equals b.Msg into ab
                         from b1 in ab.DefaultIfEmpty()
                         where b1 != null
                         orderby a.BId
                         select b1).ToList();


            var list2 = listA.Where(x => !list1.Exists(t => t.Msg == x.Msg)).ToList();
            list1.AddRange(list2);


        }


        /// <summary>
        /// 中文排序
        /// </summary>
        [TestMethod]
        public void StrSortTest()
        {

            var str = Regex.Replace("我是1班2班", @"\d", "");
            str = Regex.Replace("我是1班2班", @"[^\d]*", "");
            int intput;
            int.TryParse("das", out intput);



            string input = "adasdsadaAfd";
            string pattern = @"[+-]?\d+[\.]?\d*";
            string output = Regex.Match(input, pattern).Value;

            var listA = new List<ListA>();
            listA.Add(new ListA { Id = 1, Msg = "我是1班2班" });
            listA.Add(new ListA { Id = 3, Msg = "我是12班" });
            listA.Add(new ListA { Id = 4, Msg = "我是4班" });
            listA.Add(new ListA { Id = 2, Msg = "我不是2班" });
            listA.Add(new ListA { Id = 5, Msg = "我是5班学生" });


            listA.Add(new ListA { Id = 1, Msg = "我是一班" });
            listA.Add(new ListA { Id = 3, Msg = "我是三" });
            listA.Add(new ListA { Id = 4, Msg = "我是四" });
            listA.Add(new ListA { Id = 2, Msg = "我不是二班" });
            listA.Add(new ListA { Id = 5, Msg = "我是五班学生" });

            var result = listA.OrderBy(x => x.Msg, new SemiNumericComparer()).ToList();
        }
    }



    public class SemiNumericComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (IsNumeric(s1) && IsNumeric(s2))
            {
                if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
                if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
                if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
            }
            if (IsNumeric(s1) && !IsNumeric(s2))
                return -1;
            if (!IsNumeric(s1) && IsNumeric(s2))
                return 1;
            return String.CompareOrdinal(s1, s2);
        }
        public static bool IsNumeric(object value)
        {
            try
            {
                int i = Convert.ToInt32(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }




    public class ListA
    {
        public int Id { get; set; }
        public string Msg { get; set; }
    }

    public class ListB
    {
        public int BId { get; set; }
        public string BMsg { get; set; }
    }

}
