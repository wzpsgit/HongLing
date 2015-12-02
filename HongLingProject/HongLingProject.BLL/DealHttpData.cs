using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HongLingProject.Helper;

namespace HongLingProject.BLL
{
    public class DealHttpData
    {
        public void GetHttpData()
        {
            string url = "https://www.my089.com/Loan/default.aspx?&oc=3&ou=1";
            string IntersetRateMark= "<dd class=\"dd_2 mar_top_18\"><span class=\"number\">";
            String[] spilt = new String[1] { "<dl class=\"LoanList\">" };

            string webData = HttpHelper.HttpGet(url, "");
            string[] strArray = webData.Split(spilt, StringSplitOptions.RemoveEmptyEntries);
            var lsRate = new List<InterestRateModel>();
            for (int i = 1; i < 11; i++)
            {
                var rate = new InterestRateModel();
                int start = strArray[i].LastIndexOf(IntersetRateMark);
                string str1 = strArray[i].Substring(start);
                rate.InterestRate=decimal.Parse(str1.Substring(IntersetRateMark.Length, str1.IndexOf("<b>%/年</b>") - IntersetRateMark.Length));
            }
        }
    }
}
