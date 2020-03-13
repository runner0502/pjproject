using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// TransferCallWin.xaml 的交互逻辑
    /// </summary>
    public partial class TransferCallWin : Window
    {
        protected string CmdGuid;
        private string _Sap_Type;
        /// <summary>
        /// 当前面板SAP TYPE
        /// </summary>
        public string Sap_Type
        {
            get
            {
                return "9";        //GlobalCommandName.SAPType_IPPBX; 电话系统
            }
            set
            {
                _Sap_Type = value;
            }
        }

        public delegate void TransferDelegate(object obj,string systemId, string mode);
        [Description("调度员选择完毕返回事件")]
        public event TransferDelegate TransferEvent;

        public TransferCallWin()
        {
            InitializeComponent();
        }

        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{

            //    if (string.IsNullOrEmpty(this.txtCallNumber.Text))
            //    {
            //        MessageBox.Show("请输入号码!");
            //        return;
            //    }

            //    if (string.IsNullOrEmpty(this.txt_SystemID.Text))
            //    {
            //        MessageBox.Show("请输入系统ID");
            //        return;
            //    }

            //    string callcmdguid = BusinessCenter.GUID;
            //    //号码
            //    string num = txtCallNumber.Text;
            //    BusinessCenter.Instance.SetupCall(callcmdguid, CommonData.GetInstance().CurrDispatch, CommonData.SAPType_IPPBX, num, CommonData.NUMBERTYPE_EXTERNAL);
            //    BusinessCenter.Instance.AddToCallList(callcmdguid);
            //    CmdGuid = callcmdguid;
            //    //<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_setup_call</cmd_name><cmd_guid>{3dc804b2-276c-4262-a09c-1ba7b7a56538}</cmd_guid><puc_id>00755</puc_id><sap_type>9</sap_type><call_type>4</call_type><priority>1</priority><end2end_encryption_flag>0</end2end_encryption_flag><hook_signaling_flag>1</hook_signaling_flag><ambience_listening_flag>0</ambience_listening_flag><duplex_flag>1</duplex_flag><caller><number>cfei</number><number_type>7</number_type></caller><called><number>pstnTest</number><number_type>2</number_type></called></hytera>
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            
            //号码去掉别名
            string num = txtCallNumber.Text;
            string systemId = this.txt_SystemID.Text;
            //var callInfo = CallCenter.CallCtrl.GetInstance().GetCC(this.Sap_Type, this.DTMFTextPad.tbTextBox.Text, GlobalCommandName.NUMBERTYPE_EXTERNAL, CommonDataList.dataVersion.local_pucid);
            if (TransferEvent != null)
            {
                if (!string.IsNullOrEmpty(CmdGuid))
                {
                    //咨询转接
                    TransferEvent(num,systemId, CmdGuid);
                }
                else
                {
                    //盲转
                    TransferEvent(num,systemId, "0");
                }
            }
            this.Close();
        }
    }
}
