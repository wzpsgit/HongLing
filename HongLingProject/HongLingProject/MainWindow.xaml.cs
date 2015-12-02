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
using System.Windows.Forms;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Diagnostics;
using System.Windows.Threading;

using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;

namespace HongLingProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableDataSource<Point> dataSource = new ObservableDataSource<Point>();
        private DispatcherTimer timer = new DispatcherTimer();
        private int i = 0;

        DealData dealData = new DealData();
        DealHttpData dealHttpData = new DealHttpData();
        public MainWindow()
        {
            InitializeComponent();
            MarkBind();
            PaymentMethodBind();
            TimeBind();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            plotter.AddLineGraph(dataSource, Colors.Green, 2, "Percentage");
            timer.Interval = TimeSpan.FromSeconds(60);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.Tick += new EventHandler(GetHttpData);
            timer.IsEnabled = true;
            plotter.Viewport.FitToView();
        }

        private void GetHttpData(object sender, EventArgs e)
        {
            dealHttpData.SaveHttpData();
        }
        private void AnimatedPlot(object sender, EventArgs e)
        {
            double x = i;
            double y = new Random().Next(100);

            Point point = new Point(x, y);
            dataSource.AppendAsync(base.Dispatcher, point);

            cpuUsageText.Text = String.Format("{0:0}%", y);
            i++;
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
            MessageBox.Show(dealData.InsertAutoBid(AutoBidNo) ? "插入成功" : "插入失败");
        }

        /// <summary>
        /// 利率的菜单点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterestRate_Menu_Click(object sender, RoutedEventArgs e)
        {
            InterestRateMenu.Visibility = Visibility.Visible;
            AutoBidMenu.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 自动排名菜单点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoBid_Menu_Click(object sender, RoutedEventArgs e)
        {
            InterestRateMenu.Visibility = Visibility.Hidden;
            AutoBidMenu.Visibility = Visibility.Visible;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {
            string errorMsg = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            //设置默认打开目录
            openFileDialog.InitialDirectory= dealData.GetPersonalAction("ImportInterestRate");
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                string directory = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf('\\'));
                dealData.SetPersonalAction("ImportInterestRate",directory);
                if(dealData.ReadInterestRate(openFileDialog.FileName, out errorMsg))
                {
                    MessageBox.Show("导入成功");
                }
                else
                {
                    MessageBox.Show("导入失败");
                }
            }
        }
    }
}
