using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HongLingProject.Helper
{
    public static class DBhelper
    {
        //连接字符串  
        private static readonly string ConnectionString = ConfigurationManager.AppSettings["SqlConnection"];

        /// <summary>
        /// 执行SQL查询语句返回DataTable
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="cmdParams">命令参数</param>
        /// <returns>查询DataTable</returns>
        public static DataTable ExecuteDataTable(string sqlString, IEnumerable<SqlParameter> cmdParams = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, sqlString, cmdParams);
                try
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 执行SQL查询语句返回DataSet
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="cmdParams">命令参数</param>
        /// <returns>查询DataSet</returns>
        public static DataSet ExecuteDataSet(string sqlString, IEnumerable<SqlParameter> cmdParams = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, sqlString, cmdParams);
                try
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="cmdParams">命令参数</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery(string sqlString, IEnumerable<SqlParameter> cmdParams)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, sqlString, cmdParams);
                try
                {
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 批量执行非查询语句
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="lsCmdParams">命令参数</param>
        /// <returns></returns>
        public static int BatchExecuteNonQuery(string sqlString,List<IEnumerable<SqlParameter>> lsCmdParams)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                int affectedRows = 0;
                foreach(var cmdParam in lsCmdParams)
                {
                    var cmd = new SqlCommand();
                    PrepareCommand(cmd, conn, null, sqlString, cmdParam);
                    affectedRows += cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                conn.Close();
                return affectedRows;
            }
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, IEnumerable<SqlParameter> cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (cmdText.Trim().StartsWith("SELECT")
             | cmdText.Trim().StartsWith("select")
             | cmdText.Trim().StartsWith("INSERT")
             | cmdText.Trim().StartsWith("insert")
             | cmdText.Trim().StartsWith("UPDATE")
             | cmdText.Trim().StartsWith("update")
             | cmdText.Trim().StartsWith("DELETE")
             | cmdText.Trim().StartsWith("delete"))
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            if (cmdParams != null)
            {
                foreach (var parameter in cmdParams)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput
                        || parameter.Direction == ParameterDirection.Input)
                        && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
    }
}
