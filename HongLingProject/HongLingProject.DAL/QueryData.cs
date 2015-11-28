using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HongLingProject.Helper;

namespace HongLingProject.DAL
{
    public class QueryData
    {
        /// <summary>
        /// 获取标的类型
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMarkType()
        {
            string sql = @"SELECT ID,DisplayName,IsDefault FROM dbo.MarkType";
            return DBhelper.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 查询还款方式
        /// </summary>
        /// <returns></returns>
        public DataTable QueryPaymentMethod()
        {
            string sql = @"SELECT ID,DisplayName,IsDefault FROM dbo.PaymentMethod";
            return DBhelper.ExecuteDataTable(sql);
        }
    }
}
