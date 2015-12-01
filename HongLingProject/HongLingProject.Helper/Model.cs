using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HongLingProject.Helper
{
    public class ComboBoxModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public bool IsDefault { get; set; }
    }

    public class InterestRateModel
    {
        /// <summary>
        /// 利率
        /// </summary>
        public float InterestRate { get; set; }
        /// <summary>
        /// 标类型
        /// </summary>
        public string MarkTypeName { get; set; }
        /// <summary>
        /// 还款方式
        /// </summary>
        public string PaymentMethod { get; set; }
        /// <summary>
        /// 借款时间
        /// </summary>
        public DateTime LoadTime { get; set; }
        /// <summary>
        /// 借款时长
        /// </summary>
        public int TimeLimit { get; set; }
    }
}
