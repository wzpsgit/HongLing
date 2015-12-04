using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HongLingProject.Helper;
using System.Data.SqlClient;

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

        /// <summary>
        /// 查询个人操作
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable QueryPersonalAction(string key)
        {
            string sql = @"SELECT Value FROM dbo.PersonalAction WHERE [Key]=@key";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@key",SqlDbType.NVarChar) {Value=key }
            };

            return DBhelper.ExecuteDataTable(sql, param);
        }

        /// <summary>
        /// 利率查询
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInterestRate()
        {
            string sql = @"SELECT InterestRate,LoadTime FROM dbo.InterestRate ORDER BY LoadTime ASC";
            return DBhelper.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 查询最近一次利率的时间
        /// </summary>
        /// <returns></returns>
        public DataTable QueryLatestInterestRateTime()
        {
            string sql = @"SELECT MAX(LoadTime) FROM dbo.InterestRate";
            return DBhelper.ExecuteDataTable(sql);
        }
    }
}
