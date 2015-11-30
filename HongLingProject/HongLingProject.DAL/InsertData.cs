using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HongLingProject.Helper;

namespace HongLingProject.DAL
{
    public class InsertData
    {
        /// <summary>
        /// 插入自动排名
        /// </summary>
        /// <param name="No">自动排名编号</param>
        /// <returns>返回受影响的行数</returns>
        public int InsertAutoBid(int No)
        {
            string sql = @"INSERT INTO dbo.AutomaticBid
            ( Ranking, RankTime )
            VALUES  (@No, -- Ranking - int
            GETDATE()  -- RankTime - datetime
            )";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@No",SqlDbType.Int) {Value=No }
            };

            return DBhelper.ExecuteNonQuery(sql, param);
        }

        public int BathInsertInterestRate(List<DataTable> lsDt,string[] excelColumns)
        {
            return 0;
        }
    }
}
