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

        public int BathInsertInterestRate(List<InterestRateModel> lsRate)
        {
            string sql = @"SELECT @markTypeID=ID FROM dbo.MarkType WHERE DisplayName=@MarkTypeName
            SELECT @paymentMethodID=ID FROM dbo.PaymentMethod WHERE DisplayName=@paymentMethod

            INSERT INTO dbo.InterestRate
                    ( InterestRate ,
                      MarkTypeID ,
                      PaymentMethodID ,
                      LoadTime ,
                      TimeLimit
                    )
            VALUES  ( @interestRate , -- InterestRate - float
                      @markTypeID ,-- MarkTypeID - int
                      @paymentMethodID ,-- PaymentMethodID - int
                      @loadTime , -- LoadTime - time
                      0  -- TimeLimit - int
                    )";
            var lsParam = new List<SqlParameter[]>();
            foreach(var rt in lsRate)
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@interestRate",SqlDbType.Float) {Value=rt.InterestRate }
                    ,new SqlParameter("@markTypeName",SqlDbType.NVarChar) {Value=rt.MarkTypeName }
                    ,new SqlParameter("@paymentMethod",SqlDbType.NVarChar) {Value=rt.PaymentMethod }
                    ,new SqlParameter("@loadTime",SqlDbType.Time) {Value=rt.LoadTime }
                    ,new SqlParameter("@timeLimit",SqlDbType.Int) {Value=rt.TimeLimit }
                    ,new SqlParameter("@markTypeID",SqlDbType.Int) {Value=0 }
                    ,new SqlParameter("@paymentMethodID",SqlDbType.Int) {Value=0 }
                };
                lsParam.Add(param);
            }

           return DBhelper.BatchExecuteNonQuery(sql, lsParam);
        }
    }
}
