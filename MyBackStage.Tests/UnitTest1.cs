using System;
using BackStageBLL;
using BackStageIBLL;
using backStageIDal;
using DBModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyBackStage.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           
            var ss = new Sys_buttonBLL().GetCount(x => x.ButtonName != "");
            ISys_buttonBLL bll = new Sys_buttonBLL();
            var count = bll.GetButtonCount();
        }
    }
}
