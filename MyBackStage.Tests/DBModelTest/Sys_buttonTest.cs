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

        [Import(typeof(ISys_ButtonBLL<Sys_button>))]
        private ISys_ButtonBLL<Sys_button> buttonBll;
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
                buttonBll.AddEntity(new Sys_button()
                {

                });
            }
            Assert.AreEqual(getCount, 0);
        }





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
