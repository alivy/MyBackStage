using System;
using BackStageBLL;
using BackStageIBLL;
using backStageIDal;
using DBModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyBackStage.Tests
{
    [TestClass]
    public class Sys_buttonTest
    {
        [TestMethod]
        public void ButtonGetCountTest()
        {
            ISys_ButtonBLL<Sys_button> button = new Sys_buttonBLL();
            ISys_UserBLL<Sys_User> user = new Sys_UserBLL();
            int getCount = button.GetButtonCount();
            getCount = user.GetCount(x => x.OrganizeName.Equals(""));
            Assert.AreEqual(getCount, 0);
        }
    }
}
