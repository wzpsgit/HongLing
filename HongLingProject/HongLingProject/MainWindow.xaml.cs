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
        }

        /// <summary>
        /// 标类型
        /// </summary>
        public void MarkBind(DealData dealData)
        {
            var lsComb= dealData.DealMarkType();
            Mark_ComboBox.ItemsSource = lsComb;
            Mark_ComboBox.DisplayMemberPath = "DisplayName";
            Mark_ComboBox.SelectedValuePath = "ID";
            Mark_ComboBox.SelectedValue = lsComb.Where(p => p.IsDefault).Select(p => p.ID).First();
        }
    }
}
