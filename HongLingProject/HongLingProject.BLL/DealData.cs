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

        private List<ComboBoxModel> DealDataTable(DataTable dt)
        {
            var lsComb = new List<ComboBoxModel>();
            foreach (DataRow dr in dt.Rows)
            {
                lsComb.Add(new ComboBoxModel() { ID = int.Parse(dr["ID"].ToString()), DisplayName = dr["DisplayName"].ToString(), IsDefault = dr["IsDefault"].ToString() == "True" ? true : false });
            }
            return lsComb;
        }
    }
}
