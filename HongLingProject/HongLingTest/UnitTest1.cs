using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HongLingProject.Helper;
using System.Collections.Generic;

namespace HongLingTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           string str= HttpHelper.HttpGet("https://www.my089.com/Loan/default.aspx?&oc=3&ou=1","");
           String[] spilt =new String[1]{ "<dl class=\"LoanList\">"};
           string[] strArray = str.Split(spilt, StringSplitOptions.RemoveEmptyEntries);
            List<double> lsd = new List<double>();
           for(int i=1;i<11;i++)
           {
                string abc = "<dd class=\"dd_2 mar_top_18\"><span class=\"number\">";
                int start= strArray[i].LastIndexOf(abc);
                string str1=strArray[i].Substring(start);
                lsd.Add(double.Parse(str1.Substring(abc.Length, str1.IndexOf("<b>%/年</b>")-abc.Length)));
           }
            Assert.Fail();
        }
    }
}
