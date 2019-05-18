using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using BackStageBLL;
using BackStageIBLL;
using Common;
using DBModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyBackStage.Tests
{
    [TestClass]
    public class Sys_buttonTest
    {

        [Import("Sys_ButtonBLL")]
        private ISys_ButtonBLL buttonBll { get; set; }

        [Import("Sys_UserBLL")]
        private ISys_UserBLL userBll { get; set; }


        /// <summary>
        /// 测试MEF依赖注入
        /// </summary>
        [TestMethod]
        public void ButtonMefDIQuery()
        {
            int getCount = 1;
            MEFBase.Compose(this);
            if (buttonBll != null)
            {
                getCount = buttonBll.GetButtonCount();
                //getCount = userBll.GetCount();
            }
            Assert.AreEqual(getCount, 0);
        }





        [TestMethod]
        public void ButtonGetCountTest()
        {
            ISys_ButtonBLL button = new Sys_buttonBLL();
            ISys_UserBLL user = new Sys_UserBLL();
            int getCount = button.GetButtonCount();
            //getCount = user.GetCount(x => x.OrganizeName.Equals(""));
            Assert.AreEqual(getCount, 0);
        }
    }
}
