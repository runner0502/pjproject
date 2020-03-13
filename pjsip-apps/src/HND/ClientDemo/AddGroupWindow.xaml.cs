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
using System.Windows.Shapes;

namespace ClientDemo
{
    /// <summary>
    /// AddGroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddGroupWindow : Window
    {
        string Option = string.Empty;
        public AddGroupWindow(string option)
        {
            InitializeComponent();

            Option = option;
            if (Option == "EDIT")
            {
                this.Title = "编辑动态重组";
                //txtSystemId.IsEnabled = false;
                txtSystemId.Visibility = Visibility.Collapsed;
                labSys.Visibility = Visibility.Collapsed;
                labSysN.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Option == "EDIT")
            {
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    MessageBox.Show("别名不能为空");
                    return;
                }
                else
                {
                    this.DialogResult = true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtName.Text.Trim()) || string.IsNullOrEmpty(txtSystemId.Text.Trim()))
                {
                    MessageBox.Show("系统ID或别名不能为空");
                    return;
                }
                else
                {
                    this.DialogResult = true;
                }
            }
        }
    }
}
