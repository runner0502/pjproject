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
    /// AddGroupMemberWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddGroupMemberWindow : Window
    {
        public AddGroupMemberWindow(List<DeviceEntry> devicelist)
        {
            InitializeComponent();

            cmbDevice.ItemsSource = devicelist;
            cmbDevice.Items.Refresh();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (listSelectedContent.Count<1)
            {
                MessageBox.Show("请选择终端号");
                return;
            }
            else
            {
                //if (!string.IsNullOrEmpty(txtName2.Text.Trim()))
                //{
                //    int number1 = int.Parse(txtName.Text.Trim());
                //    int number2 = int.Parse(txtName2.Text.Trim());
                //    int num = number2 - number1;
                //    if (num <= 0)
                //    {
                //        MessageBox.Show("结束终端号应大于开始终端号！");
                //        return;
                //    }
                //    if (num > 50)
                //    {
                //        MessageBox.Show("请输入小于50个终端号");
                //        return;
                //    }
                //}
                this.DialogResult = true;
            }
        }

        private void txtName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumberic(e.Text))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }

        
        /// <summary>
        /// 检测粘贴 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Pasting(object sender, DataObjectPastingEventArgs e) 
        { 
            if (e.DataObject.GetDataPresent(typeof(String))) 
            { 
                String text = (String)e.DataObject.GetData(typeof(String)); 
                if (!isNumberic(text)) 
                { e.CancelCommand(); } 
            } 
            else { e.CancelCommand(); } 
        } 

        
        public static bool isNumberic(string _string) 
        { 
            if (string.IsNullOrEmpty(_string)) 
                return false; 
            foreach (char c in _string) 
            { 
                if (!char.IsDigit(c)) 
                    //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右 
                    return false; 
            } 
            return true; 
        }

        public List<DeviceEntry> devicelist = new List<DeviceEntry>();
        public List<string> listSelectedContent = new List<string>();
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            DeviceEntry deviceInfo = chk.DataContext as DeviceEntry;
            devicelist.Add(deviceInfo);

            listSelectedContent.Add(deviceInfo.Device_id);
            string selectContent = string.Join<string>(",", listSelectedContent);
            cmbDevice.Tag = selectContent;

            //lbdevice.Items.Add(deviceInfo.Device_id);
            //lbdevice.Items.Refresh();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            DeviceEntry deviceInfo = chk.DataContext as DeviceEntry;
            if (devicelist.Contains(deviceInfo))
            {
                devicelist.Remove(deviceInfo);
            }

            if (listSelectedContent.Contains(deviceInfo.Device_id))
            {
                listSelectedContent.Remove(deviceInfo.Device_id);
            }
            string selectContent = string.Join<string>(",", listSelectedContent);
            cmbDevice.Tag = selectContent;

            //lbdevice.Items.Remove(deviceInfo.Device_id);
            //lbdevice.Items.Refresh();
        }

    }
}
