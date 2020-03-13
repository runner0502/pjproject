using ClientDemo.AnalysisData;
using ClientDemo.CallBusiness;
using Hytera.I18N;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace ClientDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow2 : Window
    {
        static paramclass pc;
        callparam  cp;
        SDSParam sds;
        List<GroupEntry> categories;     //声明动态分组对象
        private List<DeviceEntry> devicelist;
        List<SapEntity> SapEntityList;
        string CallGuid = string.Empty;
        string CallSapType = string.Empty;
        private const string fileName = "config/AutoConfig.xml";
        /// <summary>
        /// 设置目标窗体大小，位置
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="x">目标窗体新位置X轴坐标</param>
        /// <param name="y">目标窗体新位置Y轴坐标</param>
        /// <param name="nWidth">目标窗体新宽度</param>
        /// <param name="nHeight">目标窗体新高度</param>
        /// <param name="BRePaint">是否刷新窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        public static paramclass GetLocalParam()
        {
            return pc;
        }

        public MainWindow2()
        {
            InitializeComponent();
            pc = new paramclass();
            cp = new callparam();
            sds = new SDSParam();
            BusinessCenter.Instance.OnDataback += update;
            BusinessCenter.Instance.showDialog += showdialog;
            Hytera.Commom.Log.Logger.configLogger();
            //string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //Directory.SetCurrentDirectory(path);
            MediaManager.GetInstance().InitMediaTerm();

            InitLoginpara();
            //SetMoveWindow(pictureBox.Handle, 0, 0, panelBox.Width, panelBox.Height, true);
        }
        private void SetMoveWindow(IntPtr hWnd, int x, int y, double nWidth, double nHeight, bool BRePaint)
        {
            MoveWindow(hWnd, x, y, (int)nWidth, (int)nHeight, BRePaint);
        }
        private void InitLoginpara()
        {
            LoginPara loginpara = XMLHelper.DeSerializeFromFile<LoginPara>(fileName);
            txt1.Text = loginpara.LoginName;
            psword.Password = loginpara.Password;
            localSipIP.Text = loginpara.LocolSipIP;
            localSipPort.Text = loginpara.LocolSipPort;
            serverIP.Text = loginpara.ServerIP;
            serverPort.Text = loginpara.ServerPort;
            serverSipIP.Text = loginpara.ServerSipIP;
            serverSipPort.Text = loginpara.ServerSipPort;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pc.user_name = txt1.Text.ToString();
            pc.user_password = psword.Password.ToString();
           // pc.user_password = "NlGrPtPl";
            pc.PUC_ID = txtPUC_ID.Text.Trim().ToString();
            pc.LocalSipIP = localSipIP.Text.ToString();
            pc.ServerIP = serverIP.Text.ToString();
            pc.IP3 = IP3.Text.ToString();
            pc.ServerSipIP = serverSipIP.Text.ToString();

            pc.LocalSipPort = localSipPort.Text.ToString();
            pc.ServerPort = serverPort.Text.ToString();
            pc.ID3 = ID3.Text.ToString();
            pc.ServerSipPort = serverSipPort.Text.ToString();
            pc.BOverNat1 = (bool)BOverNat1.IsChecked;
            pc.BOverNat2 = (bool)BOverNat2.IsChecked;

            LoginSubmitEvent();

            Hytera.Commom.Log.Logger.Debug("Login Start");
            bool flag = BusinessCenter.Instance.login(pc);

            //update(flag ? "登录成功" : "登录失败");
            

           //   MessageBox.Show("hello");
            callnum.Text = txt1.Text;
            numstyle.Text = "Dispatcher";
            sendernum.Text = txt1.Text;
            sendernumstyle.Text = "Dispatcher";
            Thread.Sleep(5000);
            ////加载sap列表
            //BusinessCenter.Instance.sap_list_request(txtPUC_ID.Text.Trim());

            ////Thread.Sleep(8000);
            ////登录成功加载动态重组
            //BusinessCenter.Instance.dgna_request(txtPUC_ID.Text.Trim());

            //Thread.Sleep(8000);
            ////加载设备列表
            //BusinessCenter.Instance.device_list_request(txtPUC_ID.Text.Trim());

            
            Hytera.Commom.Log.Logger.Debug("Login End");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            pc.user_name = txt1.Text.ToString();
            pc.user_password = psword.Password.ToString();
            pc.LocalSipIP = localSipIP.Text.ToString();
            pc.ServerIP =serverIP.Text.ToString();
            pc.IP3 = IP3.Text.ToString();
            pc.ServerSipIP = serverSipIP.Text.ToString();

            pc.LocalSipPort = localSipPort.Text.ToString();
            pc.ServerPort = serverPort.Text.ToString();
            pc.ID3 = ID3.Text.ToString();
            pc.ServerSipPort = serverSipPort.Text.ToString();

            //LoginSubmitEvent();
            Dispatcher.Invoke(new Action(delegate()
            {
                txt.Text = "";
            }));
            BusinessCenter.Instance.loginout(pc);

        }

        //private void LoadGroup_Click(object sender, RoutedEventArgs e)
        //{
        //    BusinessCenter.Instance.dgna_request();

        //    Thread.Sleep(3000);
        //    if (categories == null || categories.Count <= 0)
        //    {
        //        MessageBox.Show("没有动态重组");
        //    }
        //}

        public void LoginSubmitEvent()
        {
            BusinessCenter.Instance.InitPucApi(pc);
            //BusinessCenter.Instance.login(pc);
        }


        private void mawin_Loaded(object sender, RoutedEventArgs e)
        {
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    PUCApiAdapter.VOC_CloseAudioDevice();
                    PUCApiAdapter.PUCAPI_Stop();
                    PUCApiAdapter.VOIP_Stop();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void update(string str)
        {
            string s = str;

            if (str.Contains(CommonData.GetInstance().puc_login_ack) && str.Contains(CommonData.GetInstance().successResult))//登陆成功
            {
                CommonData.GetInstance().CurrDispatch = pc.user_name;
                Thread.Sleep(2000);
                Dispatcher.Invoke(new Action(() =>
                {
                    BusinessCenter.Instance.sap_list_request(txtPUC_ID.Text.Trim());
                    BusinessCenter.Instance.device_list_request(txtPUC_ID.Text.Trim());
                    BusinessCenter.Instance.system_list_request(txtPUC_ID.Text.Trim());
                    BusinessCenter.Instance.dgna_request(txtPUC_ID.Text.Trim());

                }));
            }
            //加载动态分组
            if (s.Contains("dgna_request_ack"))
            {
                Hytera.Commom.Log.Logger.Debug("dgna_request_ack : " + str);

                if (categories == null)
                    categories = new List<GroupEntry>();
                Stream stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(s));
                XDocument categoriesXML = XDocument.Load(stream);
                XElement element = categoriesXML.Element("hytera");
                XElement elementPage = element.Element("package_info").Element("last_piece");
                if (elementPage != null && elementPage.Value == "1")
                {
                    Dispatcher.Invoke(new Action(delegate()
                    {
                        cmbGroup.ItemsSource = categories;
                        cmbGroup.Items.Refresh();
                    }));
                }
                else
                {
                    categories.AddRange(this.GetCategories(element.Element("dgna_list")));
                }
                
            }
            else if (s.Contains("device_list_request_ack"))
            {
                Hytera.Commom.Log.Logger.Debug("device_list_request_ack : " + str);

                if (devicelist == null)
                    devicelist = new List<DeviceEntry>();
                Stream stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(s));

                XDocument categoriesXML = XDocument.Load(stream);
                XElement element = categoriesXML.Element("hytera");

                this.GetDeviceList(element.Element("device_list"));
                Dispatcher.Invoke(new Action(delegate()
                {
                    cmbDevice.ItemsSource = devicelist;
                    cmbDevice.Items.Refresh();
                }));

            }
            else if (s.Contains("sap_list_request_ack"))
            {
                Hytera.Commom.Log.Logger.Debug("sap_list_request_ack : " + str);

                if (SapEntityList == null)
                    SapEntityList = new List<SapEntity>();
                Stream stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(s));

                XDocument categoriesXML = XDocument.Load(stream);
                XElement element = categoriesXML.Element("hytera");

                SapEntityList.AddRange(this.GetSapEntity(element.Element("sap_list")));
                //Dispatcher.Invoke(new Action(delegate()
                //{
                //    cmbDevice.ItemsSource = devicelist;
                //    cmbDevice.Items.Refresh();
                //}));

            }
            else if (s.Contains("device_list_status_check_evt"))
            {
                //设备状态变更事件（上下线）

            }
            else if (s.Contains("cc_incoming_evt"))
            {
                //有呼叫进来保存guid
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(str);
                string callGuid = xml.ChildNodes[0].SelectSingleNode("cmd_guid").InnerText;
                string sapType = xml.ChildNodes[0].SelectSingleNode("sap_type").InnerText;
                if (!string.IsNullOrEmpty(callGuid) && !string.IsNullOrEmpty(sapType))
                {
                    CallGuid = callGuid;
                    CallSapType = sapType;
                    Dispatcher.Invoke(new Action(delegate()
                    {
                        btnRevcCall.IsEnabled = true;
                        btnTransferCall.IsEnabled = true;
                        txt.Text += Environment.NewLine + s;
                        txt.ScrollToEnd();
                    }));
                }
            }
            else if (s.Contains("edit_dgna_ack"))
            {
                Hytera.Commom.Log.Logger.Debug("edit_dgna_ack : " + str);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(str);
                string result = xml.ChildNodes[0].SelectSingleNode("result").InnerText;
                if (!string.IsNullOrEmpty(result) && result == "0")
                {
                    categories.Clear();
                    Dispatcher.Invoke(new Action(() =>
                    {
                        BusinessCenter.Instance.dgna_request(txtPUC_ID.Text.Trim());
                    }));
                }
                Dispatcher.Invoke(new Action(delegate()
                {
                    txt.Text += Environment.NewLine + s;
                    txt.ScrollToEnd();
                }));
            }
            else if (s.Contains("fax_send_file_ack"))
            {
                string txtResult = string.Empty;
                Hytera.Commom.Log.Logger.Debug("fax_send_file_ack : " + str);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(str);
                string result = xml.ChildNodes[0].SelectSingleNode("result").InnerText;
                int SendResult = string.IsNullOrEmpty(result) ? -1 : int.Parse(result);
                if (SendResult < 100)
                {
                    txtResult = "传真发送失败";
                }
                else
                {
                    if (SendResult == 103)
                    {
                        txtResult = "传真发送成功";
                    }
                    else if (SendResult == 104)
                    {
                        txtResult = "传真发送失败";
                    }
                    else
                    {
                        txtResult = "传真正在发送中...";
                    }
                }

                Dispatcher.Invoke(new Action(delegate()
                {
                    txt.Text += Environment.NewLine + txtResult;
                    txt.ScrollToEnd();
                }));
            }
            else
            {
                Dispatcher.Invoke(new Action(delegate()
                            {
                                txt.Text += Environment.NewLine + s;
                                txt.ScrollToEnd();
                            }));
            }
        }

        private void showdialog(string str)
        {
            Dispatcher.Invoke(new Action(delegate()
            {
                //MessageBox.Show("操作成功", "提示");
                txt.Text += str + Environment.NewLine;
                txt.ScrollToEnd();
            }));


        }


        bool isshow = false;
        string sendcmdguid = null;

        private string _cmdguid = null;
        string cmdguid
        {
            get
            {
                return _cmdguid;
            }
            set
            {
                _cmdguid = value;
                BusinessCenter.Instance.cmdguid = _cmdguid;
            }
        }


        private string _transfercmdguid = null;
        string transfercmdguid
        {
            get
            {
                return _transfercmdguid;
            }
            set
            {
                _transfercmdguid = value;
                BusinessCenter.Instance.transfercmdguid = _transfercmdguid;
            }
        }
        private void PTTDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmdguid != null || transfercmdguid != null)
                {
                    // BusinessCenter.Instance.RemoveFromCallList(cmdguid);
                    MessageBox.Show("请先挂断，请勿重复发起呼叫", "警告！");
                    return;
                }
                string callcmdguid = BusinessCenter.GUID;
                cmdguid = callcmdguid;
                cp.cmd_guid = callcmdguid;
                cp.callernumber = callnum.Text.ToString();
                if (numstyle.Text.ToString() == "Dispatcher")
                {
                    cp.callernumberstyle = "7";
                }

                cp.callednumber = callednum.Text.ToString();
                if (calledstyle.Text.ToString() == "个号")
                {
                    cp.callednumberstyle = "0";
                }
                else
                {
                    cp.callednumberstyle = "1";
                }
                //          cp.callednumberstyle = calledstyle.Text.ToString();
                cp.PUC_ID = txtPUC_ID.Text.Trim().ToString();
                cp.systemID = systemid.Text.ToString();
                cp.sapstyle = sapsty.Text.ToString();
                string sap_GUID = string.Empty;
                if (SapEntityList != null && SapEntityList.Count > 0)
                {
                    try
                    {
                        sap_GUID = SapEntityList.FirstOrDefault(sap => sap.System_id == systemid.Text.Trim()).Sap_guid;
                    }
                    catch(Exception ex)
                    {
                        Hytera.Commom.Log.Logger.Error("sapList is Null",ex);
                    }
                    if (sap_GUID.Equals("") || sap_GUID.Equals(string.Empty) || sap_GUID.Equals(null))
                    {
                        sap_GUID = "";
                    }
                }

                if (Isduplex.IsChecked == true)
                {
                    cp.IsDuplex = "1";
                }
                else
                {
                    cp.IsDuplex = "0";
                }
                if (IsEncryp.IsChecked == true)
                {
                    cp.IsEncryption = "1";
                }
                else
                {
                    cp.IsEncryption = "0";
                }
                cp.CallMode = callMode.Text.ToString();
                cp.pictrueboxHandle = pictureBox.Handle.ToInt32();
                //根据systemId获取sap_guid
                //string sap_guid = SapEntityList.First(sap => sap.System_id == cp.systemID).Sap_guid;

                BusinessCenter.Instance.PTTDown(cp, cmdguid, sap_GUID, txt);
                BusinessCenter.Instance.AddToCallList(cmdguid);

                isshow = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("呼叫异常："+ex.Message);
                Hytera.Commom.Log.Logger.Debug("PTTDown_Click:" + ex.ToString());
            }
       
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            isshow = false;
            BusinessCenter.Instance.Disconnect(cmdguid, txt);
            BusinessCenter.Instance.RemoveFromCallList(cmdguid);
            cmdguid = null;
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            sds.sendernumber = sendernum.Text.ToString();
            sds.sendernumberstyle = "7";
            sds.PUC_ID = txtPUC_ID.Text.Trim();
         //   sds.sendernumberstyle = sendernumstyle.Text.ToString();

            string sendmesscmdguid = BusinessCenter.GUID;
            sendcmdguid = sendmesscmdguid;

            sds.receivenumber = receivenum.Text.ToString();
            if (receivenumstyle.Text.ToString() == "个号")
            {
                sds.receivenumberstyle = "0";
            }
            else
                sds.receivenumberstyle = "1";
            //sds.receivenumberstyle = receivenum.Text.ToString();
            sds.systemID = sysid.Text.ToString();
            sds.sapstyle = sapstyle.Text.ToString();
            sds.sdscontent = sdscon.Text.ToString();

            BusinessCenter.Instance.sendmessage(sds, sendcmdguid,txt);

            //update("发送短信指令成功!");
        }

        private void demand_Click(object sender, RoutedEventArgs e)
        {
            BusinessCenter.Instance.demand(cmdguid,cp.PUC_ID, txt);
        }

        private void cease_Click(object sender, RoutedEventArgs e)
        {
            BusinessCenter.Instance.cease(cmdguid, cp.PUC_ID, txt);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                MediaManager.GetInstance().MediaTermDestroy();
            }
            catch (Exception ex)
            {
                //Logger("MediaTermDestroy error:", ex);
            }
        }

        private List<GroupEntry> GetCategories(XElement element)
        {
            return (from GroupEntry in element.Elements("dgna").Where(g => g.Element("dispatcher_account").Value == pc.user_name)
                    select new GroupEntry()
                    {
                        GUID = GroupEntry.Element("dgna_guid").Value,
                        Value = GroupEntry.Element("dgna_name").Value,
                        Name = GroupEntry.Element("dgna_name").Value,
                        SystemID = GroupEntry.Element("system_id").Value,
                        Number = GroupEntry.Element("number").Value,
                        Number_type = GroupEntry.Element("number_type").Value,
                        Account = GroupEntry.Element("dispatcher_account").Value,
                        SubGroupEntry = GetCategoriesDeviceEntry(GroupEntry.Element("member_list"), GroupEntry.Element("dgna_name").Value)
                        //SubGroupEntry = this.GetCategories(GroupEntry)

                    }).ToList();
        }

        private List<GroupEntry> GetCategoriesDeviceEntry(XElement element,string PID)
        {
            List<XElement> list = element.Elements("member").ToList<XElement>();
            return (from GroupEntry in element.Elements("member")
                    select new GroupEntry()
                    {
                        GUID = GroupEntry.Element("member_guid").Value,     //成员GUID
                        PID = PID,
                        Value = GroupEntry.Element("number").Value,
                        Name = GroupEntry.Element("number").Value,
                        //SubGroupEntry = GetCategoriesDeviceEntry(GroupEntry.Element("member_list"))
                    }).ToList();
        }

        private void GetDeviceList(XElement element)
        {
            //if (SapEntityList != null && SapEntityList.Count > 0)
            //{
            //    for (int i = 0; i < SapEntityList.Count; i++)
            //    {
            //        devicelist.AddRange((from DeviceEntry in element.Elements("device") //.Where(d => d.Element("system_id").Value == SapEntityList[i].System_id)
            //                             select new DeviceEntry()
            //                             {
            //                                 GUID = DeviceEntry.Element("guid").Value,
            //                                 Device_id = DeviceEntry.Element("device_id").Value,
            //                                 Device_alias = DeviceEntry.Element("device_alias").Value,
            //                                 Device_number = DeviceEntry.Element("device_number").Value,
            //                                 Number_type = DeviceEntry.Element("number_type").Value,
            //                                 SystemID = DeviceEntry.Element("system_id").Value,

            //                             }).ToList());
            //    }
            //}
            devicelist.AddRange((from DeviceEntry in element.Elements("device") 
                                 select new DeviceEntry()
                                 {
                                     GUID = DeviceEntry.Element("guid").Value,
                                     Device_id = DeviceEntry.Element("device_id").Value,
                                     Device_alias = DeviceEntry.Element("device_alias").Value,
                                     Device_number = DeviceEntry.Element("device_number").Value,
                                     Number_type = DeviceEntry.Element("number_type").Value,
                                     SystemID = DeviceEntry.Element("system_id").Value,

                                 }).ToList());
        }

        private List<SapEntity> GetSapEntity(XElement element)
        {
            //return (from SapEntity in element.Elements("sap")
            //        select new SapEntity()
            //        {
            //            System_id = SapEntity.Element("system_id").Value,
            //            Puc_id = SapEntity.Element("puc_id").Value,
            //            Sap_guid = SapEntity.Element("sap_guid").Value,
            //            Sap_alias = SapEntity.Element("sap_alias").Value,
            //            //Online = SapEntity.Element("number_type").Value,
            //            Sap_Type = SapEntity.Element("sap_type").Value,
            //        }).Where(sap => sap.Sap_Type == "3" || sap.Sap_Type == "5" || sap.Sap_Type == "6").ToList();
            return (from SapEntity in element.Elements("sap")
                    select new SapEntity()
                    {
                        System_id = SapEntity.Element("system_id").Value,
                        Puc_id = SapEntity.Element("puc_id").Value,
                        Sap_guid = SapEntity.Element("sap_guid").Value,
                        Sap_alias = SapEntity.Element("sap_alias").Value,
                        //Online = SapEntity.Element("number_type").Value,
                        Sap_Type = SapEntity.Element("sap_type").Value,
                    }).ToList();
        }

        /// <summary>
        /// 编辑动态重组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEditDgna_Click(object sender, RoutedEventArgs e)
        {
            string systemID = string.Empty;
            string groupName = string.Empty;
            var addGroup = new AddGroupWindow("EDIT");
            addGroup.Owner = this;
            addGroup.ShowDialog();
            if (addGroup.DialogResult == true)
            {

            }
        }

        /// <summary>
        /// 新增动态重组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDGNA_Click(object sender, RoutedEventArgs e)
        {
            string systemID = string.Empty;
            string groupName = string.Empty;
            var addGroup = new AddGroupWindow("ADD");
            addGroup.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addGroup.Owner = this;
            addGroup.ShowDialog();
            if (addGroup.DialogResult == true)
            {
                systemID = addGroup.txtSystemId.Text;
                groupName = addGroup.txtName.Text;
                //新增动态重组
                BusinessCenter.Instance.add_dgna(systemID, groupName,pc, txt);
                
                
            }
        }

        /// <summary>
        /// 删除动态重组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelDGNA_Click(object sender, RoutedEventArgs e)
        {
            if (cmbGroup.SelectedItem != null)
            {
                GroupEntry ge = (GroupEntry)cmbGroup.SelectedItem;
                MessageBoxResult result = MessageBox.Show("确定要删除名称‘" + ge.Name + "’DGNA组？", "确认删除", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    BusinessCenter.Instance.Del_dgna(ge,pc.LocalSipIP,pc.user_name, txt);
                    
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的DGNA组！");
            }
        }

        /// <summary>
        /// 新增动态重组成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {
            if (cmbGroup.SelectedItem != null)
            {
                GroupEntry ge = (GroupEntry)cmbGroup.SelectedItem;

                string groupValue = cmbGroup.SelectedValue.ToString();
                var addGroup = new AddGroupMemberWindow(devicelist);
                addGroup.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addGroup.Owner = this;
                addGroup.txtSystemId.Text = groupValue;
                addGroup.ShowDialog();
                if (addGroup.DialogResult == true)
                {
                    //string member1 = addGroup.txtName.Text;
                    //string member2 = addGroup.txtName2.Text;
                    //if (string.IsNullOrEmpty(addGroup.txtName2.Text))
                    //{
                    //    //新增动态重组成员
                    //    BusinessCenter.Instance.add_dgna_member(ge.GUID, member1);
                    //}
                    //else
                    //{
                    //    int startNum = int.Parse(member1);
                    //    int endNum = int.Parse(member2);
                    //    for (int i = startNum; i <= endNum; i++)
                    //    {
                    //        //新增动态重组成员
                    //        BusinessCenter.Instance.add_dgna_member(ge.GUID, i.ToString());
                    //    }
                    //}

                    for (int i = 0; i < addGroup.devicelist.Count; i++)
                    {
                        //新增动态重组成员
                        BusinessCenter.Instance.add_dgna_member(ge.GUID,txtPUC_ID.Text.Trim(), addGroup.devicelist[i], txt);
                    }


                }
            }
            else
            {
                MessageBox.Show("请选择要新增成员的DGNA组！");
            }
            
        }

        /// <summary>
        /// 删除动态重组成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelMenber_Click(object sender, RoutedEventArgs e)
        {
            if (lbGroupMenber.SelectedItem == null)
            {
                MessageBox.Show("请选择要删除的成员！");
            }
            else
            {
                GroupEntry ge = (GroupEntry)cmbGroup.SelectedItem;
                GroupEntry gedev = (GroupEntry)lbGroupMenber.SelectedItem;
                MessageBoxResult result = MessageBox.Show("确定要删除成员 " + gedev.Name + "？", "确认删除", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    BusinessCenter.Instance.delete_dgna_member(ge.GUID, gedev.GUID, txt);

                }
            }
        }

        private void cmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGroup.SelectedItem != null)
            {
                GroupEntry ge = (GroupEntry)cmbGroup.SelectedItem;
                lbGroupMenber.ItemsSource = ge.SubGroupEntry;
            }
            
            
        }

        private List<DeviceEntry> SelectDevicelist = new List<DeviceEntry>();
        List<string> listSelectedContent = new List<string>();

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            DeviceEntry deviceInfo = chk.DataContext as DeviceEntry;
            SelectDevicelist.Add(deviceInfo);

            listSelectedContent.Add(deviceInfo.Device_id);
            string selectContent = string.Join<string>(",", listSelectedContent);
            cmbDevice.Tag = selectContent;

            lbdevice.Items.Add(deviceInfo.Device_id);
            lbdevice.Items.Refresh();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            DeviceEntry deviceInfo = chk.DataContext as DeviceEntry;
            if (SelectDevicelist.Contains(deviceInfo))
            {
                SelectDevicelist.Remove(deviceInfo);
            }

            if (listSelectedContent.Contains(deviceInfo.Device_id))
            {
                listSelectedContent.Remove(deviceInfo.Device_id);
            }
            string selectContent = string.Join<string>(",", listSelectedContent);
            cmbDevice.Tag = selectContent;

            lbdevice.Items.Remove(deviceInfo.Device_id);
            lbdevice.Items.Refresh();
        }



        /// <summary>
        /// GPS订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTake_Click(object sender, RoutedEventArgs e)
        {
            if (lbdevice.Items.Count < 1)
            {
                MessageBox.Show("请选择订阅设备");
            }
            else if (string.IsNullOrEmpty(this.cmbTimeSpan.Text))
            {
                MessageBox.Show("请选择订阅间隔");
            }
            else
            {
                string timeSpan = this.cmbTimeSpan.Text.Replace(" s", "");
                string startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string endTime = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss");
                string subscriberList = string.Empty;
                for (int i = 0; i < SelectDevicelist.Count; i++)
                {
                    subscriberList += "<subscriberinfo>"
                                    + "<target_guid>" + SelectDevicelist[i].GUID + "</target_guid>"
                                    + "<interval_time>" + timeSpan + "</interval_time>"
                                    + "<start_time>" + startTime + "</start_time>"
                                    + "<end_time>" + endTime + "</end_time>"
                                    + "<and_or_flag>2</and_or_flag>"
                                    + "<distance>10</distance>"
                                    + "<system_id>001</system_id>"
                                    + "<target><number>" + SelectDevicelist[i].Device_id + "</number><number_type>" + SelectDevicelist[i].Number_type + "</number_type></target>"
                                    + "<puc_id>"+txtPUC_ID.Text.Trim()+"</puc_id>"
                                    + "</subscriberinfo>";
                }

                BusinessCenter.Instance.gps_batch_start(subscriberList, txt);
            }
        }

        /// <summary>
        /// 取消GPS订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnTake_Click(object sender, RoutedEventArgs e)
        {
            if (lbdevice.Items.Count < 1)
            {
                MessageBox.Show("请选择订阅设备");
            }
            else
            {
                string subscriberList = string.Empty;
                for (int i = 0; i < SelectDevicelist.Count; i++)
                {
                    subscriberList += "<unsubscriberinfo>"
                                    + "<puc_id>"+txtPUC_ID.Text.Trim()+"</puc_id>"
                                    + "<system_id>007</system_id>"
                                    + "<target>"
                                    + "<number>" + SelectDevicelist[i].Device_id + "</number><number_type>" + SelectDevicelist[i].Number_type + "</number_type>"
                                    + "</target>"
                                    + "</unsubscriberinfo>";
                }
                BusinessCenter.Instance.gps_batch_stop(subscriberList, txt);
            }

//            string testxml = @"";
//            DateTime time1 = DateTime.Now;
//            txt.Text = FormatXml(testxml);
//            DateTime time2 = DateTime.Now;
//            TimeSpan span = time2 - time1;
//            MessageBox.Show(span.TotalMilliseconds.ToString());
        }

        /// <summary>
        /// 格式化xml字符串
        /// </summary>
        /// <param name="sUnformattedXml"></param>
        /// <returns></returns>
        private string FormatXml(string sUnformattedXml)
        {
            StringBuilder sb = new StringBuilder();
            XmlTextWriter xtw = null;
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(sUnformattedXml);
                StringWriter sw = new StringWriter(sb);

                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            catch (Exception e)
            {
                sb.Append("xml格式错误，" + e.Message);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }

        /// <summary>
        /// 设备状态检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckStatus_Click(object sender, RoutedEventArgs e)
        {
            string DeviceId = txtDeviceNum.Text.Trim();
            if (CheckDeviceExits(DeviceId) == true)
            {
                string IsOnline = AnalysisXml.GetInstance().DeviceListDictionary[DeviceId].online;
                if (IsOnline == "1")
                {
                    MessageBox.Show("在线");
                }
                else
                {
                    MessageBox.Show("离线");
                }
            }
        }

        /// <summary>
        /// 摇晕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStun_Click(object sender, RoutedEventArgs e)
        {
            string DeviceId = txtDeviceNum.Text.Trim();
            if (CheckDeviceExits(DeviceId) == true)
            {
                string deviceGuid = AnalysisXml.GetInstance().DeviceListDictionary[DeviceId].guid;
                BusinessCenter.Instance.SetStun(pc.user_password, "", "", deviceGuid, txt);
            }
        }

        /// <summary>
        /// 摇醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnrevive_Click(object sender, RoutedEventArgs e)
        {
            string DeviceId = txtDeviceNum.Text.Trim();
            if (CheckDeviceExits(DeviceId) == true)
            {

            }
        }

        private void btnKill_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool CheckDeviceExits(string DeviceId)
        {
            //string DeviceId = txtDeviceNum.Text.Trim();
            if (string.IsNullOrEmpty(DeviceId))
            {
                MessageBox.Show("请输入设备号码");
                return false;
            }
            else if (AnalysisXml.GetInstance().DeviceListDictionary != null && AnalysisXml.GetInstance().DeviceListDictionary.ContainsKey(DeviceId))
            {
                return true;
            }
            else
            {
                MessageBox.Show("找不到该设备");
                return false;
            }
        }

        private void btnGpsRecord_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtGpsNumber.Text))
            {
                MessageBox.Show("请选择订阅设备");
            }
            else
            {
                string userId = pc.user_name;
                string number = txtGpsNumber.Text;
                string numberType = SelectDevicelist[0].Number_type;
                string systemId = systemid.Text;
                string systemType = AnalysisXml.GetInstance().SystemListDictionary[systemId].system_type;
                string startTime = dpStartTime.Text;
                string endTime = dpEndTime.Text;
                BusinessCenter.Instance.gps_record_query(userId, systemType, systemId, number, numberType, startTime, endTime, txt);
            }
        }

        /// <summary>
        /// 呼叫调度台
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CallDispach_Click(object sender, RoutedEventArgs e)
        {
            string callcmdguid = BusinessCenter.GUID;
            //BusinessCenter.Instance.SetupCall(callcmdguid,txt1.Text.Trim(), sayType.Text.Trim(), calledNum.Text.Trim(), "7");
            BusinessCenter.Instance.AddToCallList(callcmdguid);
        }

        bool IsConnect = false;
        /// <summary>
        /// 接听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevcCall_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CallGuid) && !string.IsNullOrEmpty(CallSapType))
            {
                if (IsConnect == false)
                {
                    BusinessCenter.Instance.RevcCall(CallGuid, CallSapType);
                    IsConnect = true;
                    btnRevcCall.Content = "挂断";
                }
                else
                {
                    BusinessCenter.Instance.Disconnect(CallGuid, txt);
                    IsConnect = false;
                    btnRevcCall.IsEnabled = false;
                    CallGuid = string.Empty;
                    CallSapType = string.Empty;
                    btnRevcCall.Content = "接听";
                }
            }
            else
            {
                MessageBox.Show("没有呼叫接入");
            }
        }

        TransferCallWin transferCallWin;

        /// <summary>
        /// 转接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTransferCall_Click(object sender, RoutedEventArgs e)
        {
            //转接前禁止当前通话发送语音
            if (!string.IsNullOrEmpty(CallGuid))
            {
                BusinessCenter.Instance.AddSysMutetMicList(CallGuid);
            }
            if (transferCallWin == null)
            {
                transferCallWin = new TransferCallWin();
                transferCallWin.TransferEvent += transferCallWin_TransferEvent;
                transferCallWin.Topmost = true;
                HwndSource winformWindow = (System.Windows.Interop.HwndSource.FromDependencyObject(this) as System.Windows.Interop.HwndSource);
                if (winformWindow != null)
                    new WindowInteropHelper(transferCallWin) { Owner = winformWindow.Handle };

                transferCallWin.Show();
            }
        }

        private void transferCallWin_TransferEvent(object obj,string systemId, string mode)
        {
            if (!string.IsNullOrEmpty(CallGuid))
            {
                BusinessCenter.Instance.RemoveSysMutetMicList(CallGuid);
            }
            if (!string.IsNullOrEmpty(mode) && mode != "Cancel")
            {
                TransferCall(obj,systemId, mode);
            }
            transferCallWin = null;
        }

        private void TransferCall(object obj,string systemId, string mode)
        {
            string number = obj.ToString();
            if (!string.IsNullOrEmpty(number) && number != "-1")
            {
                string callcmdguid = string.Empty;
                string devolveType = string.Empty;
                if (mode == "0")
                {
                    devolveType = mode;
                }
                else
                {
                    callcmdguid = mode;
                    devolveType = "2";
                }

                BusinessCenter.Instance.devolveCall(callcmdguid, systemId, number, CommonData.NUMBERTYPE_EXTERNAL, mode);
                //         <hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_call_devolve_request</cmd_name><mode>0</mode><destination><number>pstnTest</number><system_id>009</system_id><number_type>2</number_type></destination></hytera>
                //咨询转   <hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_call_devolve_request</cmd_name><mode>2</mode><destination><cmd_guid>{b2d22c99-cdfa-4144-b84d-a0e9a31a4458}</cmd_guid></destination></hytera>

            }
        }

        /// <summary>
        /// 发送传真
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFaxNum.Text) || string.IsNullOrEmpty(this.txtFaxNum.Text.Trim(';')))
            {
                MessageBox.Show("请输入传真号码");
                return;
            }
            else if (IsExistFilel() == false)
            {
                MessageBox.Show("请选择发送的传真文件");
                return;
            }
            else
            {
                List<string> fileList = new List<string>();
                fileList.Add(this.txtFile1.Text);
                fileList.Add(this.txtFile2.Text);
                fileList.Add(this.txtFile3.Text);
                fileList.Add(this.txtFile4.Text);
                fileList.Add(this.txtFile5.Text);
                //发送传真操作
                SendFax(this.txtFaxNum.Text, fileList);
            }
        }

        /// <summary>
        /// 选择附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string btnName = btn.Name;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Filter|*.TXT;*.DOC;*.DOCX;*.XLS;*.XLSX;*.JPG;*.PNG;*.TIF;*.PDF"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                switch (btnName)
                {
                    case "btnSel1":
                        this.txtFile1.Text = openFileDialog.FileName;
                        break;
                    case "btnSel2":
                        this.txtFile2.Text = openFileDialog.FileName;
                        break;
                    case "btnSel3":
                        this.txtFile3.Text = openFileDialog.FileName;
                        break;
                    case "btnSel4":
                        this.txtFile4.Text = openFileDialog.FileName;
                        break;
                    case "btnSel5":
                        this.txtFile5.Text = openFileDialog.FileName;
                        break;
                    default:
                        this.txtFile1.Text = openFileDialog.FileName;
                        break;
                }

            }
        }

        private void SendFax(string recv, List<string> fileList)
        {
//            string cmdXml = @"<hytera>
//<product_name>PUC</product_name>
//<version>10</version>
//<cmd_name>fax_send_file</cmd_name>
//<cmd_guid>{62934a92-9b81-495b-9e96-5df1f09730e5}</cmd_guid>
//<user_id>bocom5</user_id>
//<sendernum>bocom5</sendernum>
//<sendername>bocom5</sendername>
//<filename1>C:\Users\Administrator\Desktop\test.doc</filename1><filename2 /><filename3 /><filename4 /><filename5 />
//<recvinfo_list><recvinfo><recnum>0075582701709</recnum><recname>0075582701709</recname></recvinfo></recvinfo_list>
//</hytera>";

            if (!string.IsNullOrEmpty(recv) && fileList != null && fileList.Count > 0)
            {
                string[] recvArr = recv.Split(';');
                BusinessCenter.Instance.SendFax(pc.user_name, fileList, recvArr);
            }
        }

        public bool IsExistFilel()
        {
            if (string.IsNullOrEmpty(this.txtFile1.Text) &&
                string.IsNullOrEmpty(this.txtFile2.Text) &&
                string.IsNullOrEmpty(this.txtFile3.Text) &&
                string.IsNullOrEmpty(this.txtFile4.Text) &&
                string.IsNullOrEmpty(this.txtFile5.Text))
            {
                return false;
            }
            else
            {
                //if (File.Exists(""))
                //{

                //}
                return true;
            }
        }

        private void pushLocalCamera_Click(object sender, RoutedEventArgs e)
        {
            BusinessCenter.Instance.OpenLocalCamera(pictureBox.Handle.ToInt32());
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (cmdguid == null)
            {
                MessageBox.Show("请先呼叫再转发视频", "警告！");
                return;
            }
            if (transfercmdguid != null)
            {
                MessageBox.Show("请先挂断再呼叫", "警告！");
                return;
            }
            string callcmdguid = BusinessCenter.GUID;
            cp.callernumber = callnum.Text.ToString();
            cp.cmd_guid = callcmdguid;
            transfercmdguid = callcmdguid;
            if (numstyle.Text.ToString() == "Dispatcher")
            {
                cp.callernumberstyle = "7";
            }

            cp.callednumber = transferNumber.Text.ToString();
            if (transferNumberType.Text.ToString() == "个号")
            {
                cp.callednumberstyle = "0";
            }
            else if (transferNumberType.Text.ToString() == "组号")
            {
                cp.callednumberstyle = "1";
            }
            else
            {
                cp.callednumberstyle = "5";
            }
            cp.PUC_ID = txtPUC_ID.Text.Trim().ToString();
            cp.systemID = systemid.Text.ToString();
            cp.sapstyle = sapsty.Text.ToString();
            string sap_GUID = string.Empty;
            if (SapEntityList != null && SapEntityList.Count > 0)
            {
                try
                {
                    sap_GUID = SapEntityList.FirstOrDefault(sap => sap.System_id == systemid.Text.Trim()).Sap_guid;
                }
                catch (Exception ex)
                {
                    Hytera.Commom.Log.Logger.Error("sapList is Null", ex);
                }
                if (sap_GUID.Equals("") || sap_GUID.Equals(string.Empty) || sap_GUID.Equals(null))
                {
                    sap_GUID = "";
                }
            }



            cp.IsDuplex = "0";

            if (IsEncryp.IsChecked == true)
            {
                cp.IsEncryption = "1";
            }
            else
            {
                cp.IsEncryption = "0";
            }
            cp.CallMode = callMode.Text.ToString();
            cp.pictrueboxHandle = pictureBox.Handle.ToInt32();

            BusinessCenter.Instance.TransferFuction(cp, callcmdguid, sap_GUID, txt);
            BusinessCenter.Instance.AddToCallList(callcmdguid);

            isshow = true;

        }

        private void transferDisconnect_Click(object sender, RoutedEventArgs e)
        {
            isshow = false;
            BusinessCenter.Instance.Disconnect(transfercmdguid, txt);
            BusinessCenter.Instance.RemoveFromCallList(transfercmdguid);
            transfercmdguid = null;
        }

        #region 监听
        private void btnMonitor_Click(object sender, RoutedEventArgs e)
        {
            Monitor monitor=new Monitor();
            monitor.Puc_id = pc.PUC_ID;
            monitor.SystemID = systemId_monitor.Text;
            monitor.pseudo_trunking_id = pc.user_name;
            monitor.NumberType = numberType_monitor.Text == "个号" ? GlobalCommandName.NUMBERTYPE_INDIVIDUAL : GlobalCommandName.NUMBERTYPE_GROUP;
            int startNumber = 0;
            int endNumber=0;
            bool x = int.TryParse(txtStartNumber.Text,out startNumber);
            bool y = int.TryParse(txtEndNumber.Text, out endNumber);
            int monitorNumber = endNumber - startNumber;
            if (monitorNumber > 8)
            {
                MessageBox.Show("最多支持监听8路通话");
                return;
            }
            if (x && y)
            {
                for (int i = startNumber; i <= endNumber; i++)
                {
                    monitor.Number = i.ToString();                    
                    MonitorCenter.GetInstance.SetMonitor(monitor, txt);
                }
            }
        }

        private void btnCancelMonitor_Click(object sender, RoutedEventArgs e)
        {
            Monitor monitor = new Monitor();
            monitor.Puc_id = pc.PUC_ID;
            monitor.SystemID = systemId_monitor.Text;
            monitor.pseudo_trunking_id = pc.user_name;
            monitor.NumberType = numberType_monitor.Text == "个号" ? GlobalCommandName.NUMBERTYPE_INDIVIDUAL : GlobalCommandName.NUMBERTYPE_GROUP;
            int startNumber = 0;
            int endNumber = 0;
            bool x = int.TryParse(txtStartNumber.Text, out startNumber);
            bool y = int.TryParse(txtEndNumber.Text, out endNumber);
            if (x && y)
            {
                for (int i = startNumber; i <= endNumber; i++)
                {
                    monitor.Number = i.ToString();
                    MonitorCenter.GetInstance.CancelMonitor(monitor, txt);
                }
            }
        }
        #endregion

    }
}
