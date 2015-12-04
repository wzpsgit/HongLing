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

using MessageBox = System.Windows.MessageBox;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace HongLingProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        List<DateTime> dates=new List<DateTime>();
        List<decimal> interestRate=new List<decimal>();
        EnumerableDataSource<DateTime> datesDataSource;
        EnumerableDataSource<decimal> interestRateDataSource;

        DealData dealData = new DealData();
        DealHttpData dealHttpData = new DealHttpData();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datesDataSource = new EnumerableDataSource<DateTime>(dates);
            datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));
            interestRateDataSource = new EnumerableDataSource<decimal>(interestRate);
            interestRateDataSource.SetYMapping(y => interestAxis.ConvertToDouble((int)(y * 100)) / 100);

            var compositeDataSource = new CompositeDataSource(datesDataSource, interestRateDataSource);
            
            plotter.AddLineGraph(compositeDataSource,
                new Pen(Brushes.Blue, 2),
                new CirclePointMarker { Size = 10.0, Fill = Brushes.Red },
                new PenDescription("Interest Rate"));

            plotter.Viewport.FitToView();
            timer.Interval = TimeSpan.FromSeconds(6);
            timer.Tick += new EventHandler(GetHttpData);
            timer.IsEnabled = true;
        }

        private void GetHttpData(object sender, EventArgs e)
        {
            var lsRateModel= dealHttpData.SaveHttpData();
           
            dealData.ReadInterestRate(ref dates,ref interestRate,lsRateModel);
            AnimatedPlot();
        }
        private void AnimatedPlot()
        {
            datesDataSource.RaiseDataChanged();
            interestRateDataSource.RaiseDataChanged();
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
    }
}
