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
            MarkBind();
        }

        /// <summary>
        /// 标类型
        /// </summary>
        public void MarkBind()
        {
            List<ComboBox> MarkList = new List<ComboBox>();
            MarkList.Add(new ComboBox() { Value = 1, DisplayName = "信用标" });
            MarkList.Add(new ComboBox() { Value = 2, DisplayName = "净值标" });
            MarkList.Add(new ComboBox() { Value = 3, DisplayName = "快借标" });
            MarkList.Add(new ComboBox() { Value = 4, DisplayName = "推荐标" });
            MarkList.Add(new ComboBox() { Value = 5, DisplayName = "资产标" });
            MarkList.Add(new ComboBox() { Value = 6, DisplayName = "秒还标" });
            MarkList.Add(new ComboBox() { Value = 7, DisplayName = "公信贷" });
            MarkList.Add(new ComboBox() { Value = 8, DisplayName = "特定标" });
            MarkList.Add(new ComboBox() { Value = 9, DisplayName = "议标" });
            Mark_ComboBox.ItemsSource = MarkList;
            Mark_ComboBox.DisplayMemberPath = "DisplayName";
            Mark_ComboBox.SelectedValuePath = "Value";
            Mark_ComboBox.SelectedValue = 2;
        }
    }

    public class ComboBox
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }
    }
}
