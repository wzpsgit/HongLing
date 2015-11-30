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
            errorMsg = null;
            return true;
        }

    }
}
