using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HongLingProject.BLL;
using HongLingProject.Helper;

namespace HongLingProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DealData dealData = new DealData();
            MarkBind(dealData);
            PaymentMethodBind(dealData);
        }

        /// <summary>
        /// 标类型
        /// </summary>
        public void MarkBind(DealData dealData)
        {
            var lsComb= dealData.DealMarkType();
            Bind(Mark_ComboBox, lsComb);
        }

        /// <summary>
        /// 还款方式
        /// </summary>
        /// <param name="dealData"></param>
        public void PaymentMethodBind(DealData dealData)
        {
            var lsComb = dealData.DealPaymentMethod();
            Bind(Repayment_ComboBox, lsComb);
        }

        public void Bind(ComboBox combBox,List<ComboBoxModel> lsComb)
        {
            combBox.ItemsSource = lsComb;
            combBox.DisplayMemberPath = "DisplayName";
            combBox.SelectedValuePath = "ID";
            combBox.SelectedValue = lsComb.Where(p => p.IsDefault).Select(p => p.ID).First();
        }
    }
}
