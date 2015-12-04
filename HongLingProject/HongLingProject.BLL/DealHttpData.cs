using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HongLingProject.Helper;
using HongLingProject.DAL;
using System.Data;

namespace HongLingProject.BLL
{
    public class DealHttpData
    {
        #region 私有全局变量
        /// <summary>
        /// 目标URL
        /// </summary>
        private static readonly string URL = "https://www.my089.com/Loan/default.aspx?&oc=3&ou=1";
        /// <summary>
        /// 分割字符串
        /// </summary>
        private static readonly String[] SpiltString = new String[1] { "<dl class=\"LoanList\">" };
        /// <summary>
        /// 利率标签
        /// </summary>
        private static readonly string IntersetRateMark = "<dd class=\"dd_2 mar_top_18\"><span class=\"number\">";
        private static readonly string IntersetRateEndMark = "<b>%/年</b>";
        /// <summary>
        /// 期限，还款方式标签
        /// </summary>
        private static readonly string TimePaymentMark = "<dd class=\"dd_4 mar_top_18\"><span class=\"number\">";
        private static readonly string TimePaymentEndMark = "</dd>";

        /// <summary>
        /// 借款时间标签（第一个）
        /// </summary>
        private static readonly string LoadTimeMark = "<span class=\"lf\">";
        private static readonly string LoadTimeEndMark = "</span>";
        /// <summary>
        /// 标类型标签
        /// </summary>
        private static readonly string MarkTypeMark = "<a href=\"/Loan/CateInfo.aspx\" target=\"_blank\">";
        private static readonly string MarkTypeMark2= "class=\"";
        private static readonly string MarkTypeEndMark = "\">";

        private static Dictionary<string, string> MarkTypeDic;

        #endregion 私有全局变量

        InsertData insertData = new InsertData();
        QueryData queryData = new QueryData();
        public DealHttpData()
        {
            MarkTypeDic = new Dictionary<string, string>();
            MarkTypeDic.Add("SubL1", "信用标");
            MarkTypeDic.Add("SubL90", "净值标");
            MarkTypeDic.Add("SubL50", "快借标");
            MarkTypeDic.Add("SubL60", "推荐标");
            MarkTypeDic.Add("SubL110", "资产标");
            MarkTypeDic.Add("SubL20", "秒还标");
            MarkTypeDic.Add("SubL120", "公信贷");
            MarkTypeDic.Add("SubL130", "特定标");
            MarkTypeDic.Add("SubL150", "议标");
        }

        /// <summary>
        /// 保存Http数据
        /// </summary>
        public List<InterestRateModel> SaveHttpData()
        {
            var lsRate = GetHttpData();
            var dt= queryData.QueryLatestInterestRateTime();
            DateTime datetime = DateTime.Parse(dt.Rows[0][0].ToString());
            var rate = lsRate.Where(p => !DateTime.Equals(p.LoadTime, datetime)).ToList();
            if (rate.Count != 0)
            {
                insertData.BathInsertInterestRate(rate);
            }
            return rate;
        }

        public List<InterestRateModel> GetHttpData()
        {
            string webData = HttpHelper.HttpGet(URL, "");
            string[] strArray = webData.Split(SpiltString, StringSplitOptions.RemoveEmptyEntries);
            var lsRate = new List<InterestRateModel>();

            int i = 1;

            var rate = new InterestRateModel();
            //利率
            string interestRate = strArray[i].Substring(strArray[i].IndexOf(IntersetRateMark) + IntersetRateMark.Length);
            interestRate = interestRate.Substring(0, interestRate.IndexOf(IntersetRateEndMark));
            //借款时间
            string loadTime = strArray[i].Substring(strArray[i].IndexOf(LoadTimeMark)+LoadTimeMark.Length);
            loadTime = loadTime.Substring(0, loadTime.IndexOf(LoadTimeEndMark));
            //标类型
            string markType = strArray[i].Substring(strArray[i].IndexOf(MarkTypeMark));
            markType = markType.Substring(markType.IndexOf(MarkTypeMark2)+MarkTypeMark2.Length);
            markType = markType.Substring(0, markType.IndexOf(MarkTypeEndMark));
            //期限还款方式
            string timePayment = strArray[i].Substring(strArray[i].IndexOf(TimePaymentMark) + TimePaymentMark.Length);
            timePayment = timePayment.Substring(0, timePayment.IndexOf(TimePaymentEndMark));
            timePayment = timePayment.Replace(" ", "");

            int spanIndex = timePayment.IndexOf("</span>");
            //期限
            int timeLimint =int.Parse(timePayment.Substring(0,spanIndex));
            timePayment = timePayment.Substring(spanIndex + "</span>".Length);
            //3</span>个月/按月分期
            if (timePayment.Substring(0, timePayment.IndexOf("/")).Contains("月"))
            {
                //一个月按30天算
                timeLimint *= 30;
            }
            //还款方式
            string paymentMethod = timePayment.Substring(timePayment.IndexOf("/") + "/".Length);

            rate.InterestRate = decimal.Parse(interestRate);
            rate.MarkTypeName = MarkTypeDic[markType];
            rate.PaymentMethod = paymentMethod;
            rate.LoadTime = DateTime.Parse(loadTime);
            rate.TimeLimit = timeLimint;

            lsRate.Add(rate);

            return lsRate;
        }
    }
}
