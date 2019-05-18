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
    public class ShareTest
    {
        [Import]
        private IShareBLL<Sys_User> userBll { get; set; }

        [Import]
        private IShareBLL<Sys_button> buttonBll { get; set; }

        [Import("Sys_NavMenu")]
        private ISys_NavMenuBLL _navMenuBll { get; set; }

        [Import("Sys_UserBLL")]
        private ISys_UserBLL _userBll { get; set; }
        /// <summary>
        /// 测试MEF的泛型接口依赖注入
        /// </summary>
        [TestMethod]
        public void ShareMefDIQuery()
        {


            int getCount = 1;
            MEFBase.Compose(this);
            if (userBll != null)
            {
                _userBll.testUser();
                var menu = _navMenuBll.GetNavMenuByUserId("1");
                getCount = userBll.GetCount();
                getCount = buttonBll.GetCount();
            }
            Assert.AreEqual(getCount > 0, true);
        }






    }
}
