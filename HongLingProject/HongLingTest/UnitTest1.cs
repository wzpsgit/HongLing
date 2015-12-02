using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HongLingProject.Helper;
using System.Collections.Generic;
using HongLingProject.BLL;

namespace HongLingTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DealHttpData deal = new DealHttpData();
            deal.GetHttpData();
            Assert.Fail();
        }
    }
}
