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
        DealData dealData = new DealData();
        public MainWindow()
        {
            InitializeComponent();
            MarkBind();
            PaymentMethodBind();
            TimeBind();
        }

        /// <summary>
        /// 标类型
        /// </summary>
        public void MarkBind()
        {
            var lsComb= dealData.DealMarkType();
            Bind(Mark_ComboBox, lsComb);
        }

        /// <summary>
        /// 还款方式
        /// </summary>
        /// <param name="dealData"></param>
        public void PaymentMethodBind()
        {
            var lsComb = dealData.DealPaymentMethod();
            Bind(Repayment_ComboBox, lsComb);
        }

        public void TimeBind()
        {
            string time = DateTime.Now.ToString("H-m-s");
            string[] strTime = time.Split('-');
            Hour_Text.Text = strTime[0];
            Minute_Text.Text = strTime[1];
            Second_Text.Text = strTime[2];
        }

        public void Bind(ComboBox combBox,List<ComboBoxModel> lsComb)
        {
            combBox.ItemsSource = lsComb;
            combBox.DisplayMemberPath = "DisplayName";
            combBox.SelectedValuePath = "ID";
            combBox.SelectedValue = lsComb.Where(p => p.IsDefault).Select(p => p.ID).First();
        }

        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            int AutoBidNo = int.Parse(AutomaticBid_Text.Text);
            dealData.InsertAutoBid(AutoBidNo);
        }
    }
}
