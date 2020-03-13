using Hytera.Commom.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml;
using System.Xml.Linq;

namespace ClientDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        paramclass pc;
        callparam  cp;
        SDSParam sds;
        List<GroupEntry> categories;     //声明动态分组对象
        private List<DeviceEntry> devicelist;
        public MainWindow()
        {
            InitializeComponent();
            pc = new paramclass();
            cp = new callparam();
            sds = new SDSParam();
            BusinessCenter.Instance.OnDataback += update;
            BusinessCenter.Instance.showDialog += showdialog;

            //string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //Directory.SetCurrentDirectory(path);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pc.user_name = txt1.Text.ToString();
            pc.user_password = psword.Password.ToString();
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

          
            bool flag = BusinessCenter.Instance.login(pc);

            //update(flag ? "登录成功" : "登录失败");
            

           //   MessageBox.Show("hello");
            callnum.Text = txt1.Text;
            numstyle.Text = "Dispatcher";
            sendernum.Text = txt1.Text;
            sendernumstyle.Text = "Dispatcher";

            Thread.Sleep(8000);
            //加载设备列表
            BusinessCenter.Instance.device_list_request(txtPUC_ID.Text.Trim());

            //Thread.Sleep(8000);
            //登录成功加载动态重组
            BusinessCenter.Instance.dgna_request(txtPUC_ID.Text.Trim());

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

            //加载动态分组
            if (s.Contains("dgna_request_ack"))
            {
                categories = new List<GroupEntry>();

                //byte[] array = Encoding.ASCII.GetBytes(str);
                Stream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(s));

                XDocument categoriesXML = XDocument.Load(stream);
                XElement element = categoriesXML.Element("hytera");

                categories.AddRange(this.GetCategories(element.Element("dgna_list")));
                categories = categories.Where(o => o.Account == pc.user_name).ToList();
                Dispatcher.Invoke(new Action(delegate()
                {
                    cmbGroup.ItemsSource = categories;
                    cmbGroup.Items.Refresh();
                }));
                //Action action1 = () =>
                //{
                //    cmbGroup.ItemsSource = categories;
                //    cmbGroup.Items.Refresh();
                //};
            }
            else if (s.Contains("device_list_request_ack"))
            {
                if (devicelist == null)
                    devicelist = new List<DeviceEntry>();
                Stream stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(s));

                XDocument categoriesXML = XDocument.Load(stream);
                XElement element = categoriesXML.Element("hytera");

                devicelist.AddRange(this.GetDeviceList(element.Element("device_list")));
                Dispatcher.Invoke(new Action(delegate()
                {
                    cmbDevice.ItemsSource = devicelist;
                    cmbDevice.Items.Refresh();
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


        string cmdguid = null;
        bool isshow = false;
        string sendcmdguid = null;
        private void PTTDown_Click(object sender, RoutedEventArgs e)
        {
            if(isshow)
            {
               // BusinessCenter.Instance.RemoveFromCallList(cmdguid);
                MessageBox.Show("请先挂断，请勿重复发起呼叫","警告！");
                return;
            }
            string callcmdguid = BusinessCenter.GUID;
            cmdguid = callcmdguid;
            cp.callernumber = callnum.Text.ToString();
            if (numstyle.Text.ToString()=="Dispatcher")
            {
                cp.callernumberstyle = "7";
            }

            cp.callednumber = callednum.Text.ToString();
            if (calledstyle.Text.ToString() == "个号")
            {
                cp.callednumberstyle = "0";
            }
  //          cp.callednumberstyle = calledstyle.Text.ToString();
            cp.systemID = systemid.Text.ToString();
            cp.sapstyle = sapsty.Text.ToString();
            if(Isduplex.IsChecked == true)
            {
              cp.IsDuplex = "1" ;
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


            BusinessCenter.Instance.PTTDown(cp, cmdguid,"", txt);
            BusinessCenter.Instance.AddToCallList(cmdguid);

            isshow = true;

       
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            isshow = false;
            BusinessCenter.Instance.Disconnect(cmdguid,txt);
            BusinessCenter.Instance.RemoveFromCallList(cmdguid);
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            sds.sendernumber = sendernum.Text.ToString();
            sds.sendernumberstyle = "7";
         //   sds.sendernumberstyle = sendernumstyle.Text.ToString();

            string sendmesscmdguid = BusinessCenter.GUID;
            sendcmdguid = sendmesscmdguid;

            sds.receivenumber = receivenum.Text.ToString();
            //if (receivenumstyle.Text.ToString() == "个号")
            //{
            //    sds.receivenumberstyle = "0";
            //}
            //else
            //    sds.receivenumberstyle = "0";
            sds.receivenumberstyle = receivenum.Text.ToString();
            sds.systemID = sysid.Text.ToString();
            sds.sapstyle = sapstyle.Text.ToString();
            sds.sdscontent = sdscon.Text.ToString();

            BusinessCenter.Instance.sendmessage(sds, sendcmdguid,txt);

            update("发送短信指令成功!");
        }

        private void demand_Click(object sender, RoutedEventArgs e)
        {
            BusinessCenter.Instance.demand(cmdguid,cp.PUC_ID,txt);
        }

        private void cease_Click(object sender, RoutedEventArgs e)
        {
            BusinessCenter.Instance.cease(cmdguid,cp.PUC_ID,txt);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Dispatcher.Invoke(() =>
            //{
            //    this.Dispose(true);//关闭后腰注销接口
            //});
        }

        private List<GroupEntry> GetCategories(XElement element)
        {
            return (from GroupEntry in element.Elements("dgna")
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

        private List<DeviceEntry> GetDeviceList(XElement element)
        {
            return (from DeviceEntry in element.Elements("device")
                    select new DeviceEntry()
                    {
                        GUID = DeviceEntry.Element("guid").Value,
                        Device_id = DeviceEntry.Element("device_id").Value,
                        Device_alias = DeviceEntry.Element("device_alias").Value,
                        Device_number = DeviceEntry.Element("device_number").Value,
                        Number_type = DeviceEntry.Element("number_type").Value,
                        SystemID = DeviceEntry.Element("system_id").Value,

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
                //Thread.Sleep(3000);
                ////重新加载动态重组
                //BusinessCenter.Instance.dgna_request();
                
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

                    //Thread.Sleep(3000);
                    ////重新加载动态重组
                    //BusinessCenter.Instance.dgna_request();

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
                                    + "<puc_id>00001</puc_id>"
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
                                    + "<puc_id>00001</puc_id>"
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

        //private void btnLoadDevice_Click(object sender, RoutedEventArgs e)
        //{
        //    //加载设备列表
        //    BusinessCenter.Instance.device_list_request();
        //}

    }
}
