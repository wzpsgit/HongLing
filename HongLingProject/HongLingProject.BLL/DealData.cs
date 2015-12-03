using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HongLingProject.DAL;
using HongLingProject.Helper;

namespace HongLingProject.BLL
{
    public class DealData
    {
        QueryData queryData = new QueryData();
        InsertData insertData = new InsertData();

        /// <summary>
        /// 处理标的类型
        /// </summary>
        /// <returns></returns>
        public List<ComboBoxModel> DealMarkType()
        {
            return DealDataTable(queryData.QueryMarkType());
        }

        /// <summary>
        /// 还款方式
        /// </summary>
        /// <returns></returns>
        public List<ComboBoxModel> DealPaymentMethod()
        {
            return DealDataTable(queryData.QueryPaymentMethod());
        }

        /// <summary>
        /// 插入自动排名
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        public bool InsertAutoBid(int No)
        {
            return insertData.InsertAutoBid(No) > 0 ? true : false;
        }

        public string GetPersonalAction(string key)
        {
            var dt = queryData.QueryPersonalAction(key);
            return dt.Rows[0][0].ToString();
        }

        public void SetPersonalAction(string key,string value)
        {
            insertData.SetPersonalAction(key, value);
        }

        private List<ComboBoxModel> DealDataTable(DataTable dt)
        {
            var lsComb = new List<ComboBoxModel>();
            foreach (DataRow dr in dt.Rows)
            {
                lsComb.Add(new ComboBoxModel() { ID = int.Parse(dr["ID"].ToString()), DisplayName = dr["DisplayName"].ToString(), IsDefault = dr["IsDefault"].ToString() == "True" ? true : false });
            }
            return lsComb;
        }

        public bool ReadInterestRate(string fileName,out string errorMsg)
        {
            string[] ExcelColumns = new string[] { "利率", "标类型", "还款方式", "借款时间", "借款期限" };
            var lsDt = ExcelHelper.ReadWholeExcel(fileName);
            //逐个DataTable校验
            int Count = lsDt.Count;
            for (int i = 0; i < Count; i++)
            {
                if(lsDt[i].Rows.Count==0)
                {
                    i--;Count--;
                    lsDt.RemoveAt(i);
                    continue;
                }
                foreach (var col in ExcelColumns)
                {
                    if (!lsDt[i].Columns.Contains(col))
                    {
                        i--; Count--;
                        lsDt.RemoveAt(i);
                        break;
                    }
                }
            }

            if (Count == 0)
            {
                errorMsg = "Excel 模板错误或导入模板为空.";
                return false;
            }

            var lsRate = new List<InterestRateModel>();
            try {
                foreach (var dt in lsDt)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var rate = new InterestRateModel()
                        {
                            InterestRate = decimal.Parse(dr["利率"].ToString())
                            ,
                            MarkTypeName = dr["标类型"].ToString()
                            ,
                            PaymentMethod = dr["还款方式"].ToString()
                            ,
                            LoadTime = DateTime.Parse(dr["借款时间"].ToString())
                            ,
                            TimeLimit = int.Parse(dr["借款期限"].ToString())
                        };
                        lsRate.Add(rate);
                    }
                }
            }catch(Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
            errorMsg = null;
            return insertData.BathInsertInterestRate(lsRate)>0;
        }

        /// <summary>
        /// 读取利率
        /// </summary>
        /// <returns></returns>
        private List<string[]> ReadInterestRate()
        {
            List<string[]> lsStrArray = new List<string[]>();
            var dt = queryData.QueryInterestRate();
            foreach(DataRow dr in dt.Rows)
            {
                string[] strArray = new string[2] { dr[0].ToString(), dr[1].ToString() };
                lsStrArray.Add(strArray);
            }

            return lsStrArray;
        }

        public void ReadInterestRate(out DateTime[] dateArray,out decimal[] interestRateArray)
        {
            var lsStrArray = ReadInterestRate();
            dateArray = new DateTime[lsStrArray.Count];
            interestRateArray = new decimal[lsStrArray.Count];
            int i = 0;
            foreach(var array in lsStrArray)
            {
                interestRateArray[i] = decimal.Parse(array[0]);
                dateArray[i] = DateTime.Parse(array[1]);
                i++;
            }
        }

    }
}
