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
        public List<ComboBox> DealMarkType()
        {
            var lsComb = new List<ComboBox>();
            DataTable dt = queryData.GetMarkType();
            foreach(DataRow dr in dt.Rows)
            {
                lsComb.Add(new ComboBox() { ID = int.Parse(dr["ID"].ToString()), DisplayName = dr["DisplayName"].ToString(),IsDefault=dr["IsDefault"].ToString()=="True"?true:false });
            }

            return lsComb;
        }
    }
}
