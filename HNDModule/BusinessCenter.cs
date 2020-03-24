using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using System.ComponentModel;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using System.Reflection;
using System.Xml;
using System.Threading;
using ClientDemo.AnalysisData;
using ClientDemo.model;
using Hytera.Commom.Log;
using ClientDemo.CallBusiness;
using Hytera.I18N;


namespace ClientDemo
{

    public delegate void OnIncomingCall( string called, string caller );
    public class BusinessCenter
    {
        public bool Islogin = false;
        public string pXmlData22;
        public static BusinessCenter _pBusinessCenter = null;
        static ClientDemo.PUCApiAdapter.InitPUCAPIData PucApiParam = new ClientDemo.PUCApiAdapter.InitPUCAPIData();
        static event ClientDemo.PUCApiAdapter.PUCCallbackEventHandler InitializeCallbackEvent;

        public event OnIncomingCall OnIncomingCallEvent;
        public event Action<string> OnDataback;

        public event Action<string> showDialog;
        bool isStartedVoip = false;
        private event ClientDemo.PUCApiAdapter.MicCallBackFun micCallback;
        private event ClientDemo.PUCApiAdapter.VoipClientCallBackFun voipCallback;
        List<string> strCallList = new List<string>(10);
        hytera fengclass = new hytera();
        private BlockingCollection<string> _CallbackDataList = new BlockingCollection<string>();
        public string logincmdguid = null;
        string sapguid = null;
        string saptype = null;
        bool isOpenVoc = false;
        //禁止发送语音列表
        List<string> strSysMutetMicList = new List<string>();

        public string cmdguid;
        public string transfercmdguid;
        private const string fileName = "config/AutoConfig.xml";
        paramclass configpc;
        //
        public void InitPucApi(paramclass pc)
        {
            try
            {
                string StrServerIP = pc.ServerIP.Trim();
                int NServerPort = Convert.ToInt32(pc.ServerPort);//默认值是12000
                string StrServerSipIP = pc.ServerSipIP.Trim();
                string NServerSipPort = pc.ServerSipPort.Trim();
                string StrServerSipIP2 = pc.IP3.Trim();
                string NServerSipPort2 = pc.ID3.Trim();
                string StrServerIP2 = pc.IP3.Trim();
                int NServerPort2 = Convert.ToInt32(pc.ID3);

                string StrLocalIP = pc.LocalSipIP.Trim();
                string StrLocalPort = pc.LocalSipPort.Trim();

                int BOverNat = 0;
                string SIPLOCALPUCIP = pc.LocalSipIP.Trim(); ;
                string SIPLOCALPUCPORT = pc.LocalSipPort.Trim();
                string username1 = pc.user_name.Trim();

                PucApiParam._strLocalSipIP = Marshal.StringToHGlobalUni(SIPLOCALPUCIP);//api不再使用此字段，放在下面的属性中设置
                PucApiParam._nLocalSipPort = Convert.ToInt32(SIPLOCALPUCPORT);//api不再使用此字段，放在下面的属性中设置 5070

                PucApiParam._strServerIP = Marshal.StringToHGlobalUni(StrServerIP);//
                PucApiParam._nServerPort = Convert.ToInt32(NServerPort);//
                PucApiParam._strServerIP2 = Marshal.StringToHGlobalUni(StrServerIP2);//
                PucApiParam._nServerPort2 = NServerPort;//

                PucApiParam._strLocalIP = Marshal.StringToHGlobalUni(StrServerIP);//
                PucApiParam._strPUCID = Marshal.StringToHGlobalUni("001");//根据当前PUCID来设置，应设置为可配置项
                PucApiParam._bOverNat1 = BOverNat;//0
                PucApiParam._bOverNat2 = BOverNat;//0


                ClientDemo.PUCApiAdapter.InitPucApiDataProperty initProp = new ClientDemo.PUCApiAdapter.InitPucApiDataProperty();
                initProp._configname = Marshal.StringToHGlobalUni("SipLocalPucIp");
                initProp._configValue = Marshal.StringToHGlobalUni(SIPLOCALPUCIP);//
                initProp._strModule = Marshal.StringToHGlobalUni("PUCSipMessage");

                ClientDemo.PUCApiAdapter.InitPucApiDataProperty initProp2 = new ClientDemo.PUCApiAdapter.InitPucApiDataProperty();
                initProp2._configname = Marshal.StringToHGlobalUni("SipLocalPucPort");
                initProp2._configValue = Marshal.StringToHGlobalUni(SIPLOCALPUCPORT);//默认值5070
                initProp2._strModule = Marshal.StringToHGlobalUni("PUCSipMessage");

                ClientDemo.PUCApiAdapter.InitPucApiDataProperty initProp3 = new ClientDemo.PUCApiAdapter.InitPucApiDataProperty();
                initProp3._configname = Marshal.StringToHGlobalUni("serversipip1");
                initProp3._configValue = Marshal.StringToHGlobalUni(StrServerSipIP);//
                initProp3._strModule = Marshal.StringToHGlobalUni("Login");

                ClientDemo.PUCApiAdapter.InitPucApiDataProperty initProp4 = new ClientDemo.PUCApiAdapter.InitPucApiDataProperty();
                initProp4._configname = Marshal.StringToHGlobalUni("serversipport1");
                initProp4._configValue = Marshal.StringToHGlobalUni(NServerSipPort.ToString());//默认值6060
                initProp4._strModule = Marshal.StringToHGlobalUni("Login");

                ClientDemo.PUCApiAdapter.InitPucApiDataProperty initProp5 = new ClientDemo.PUCApiAdapter.InitPucApiDataProperty();
                initProp5._configname = Marshal.StringToHGlobalUni("serversipip2");
                initProp5._configValue = Marshal.StringToHGlobalUni(StrServerSipIP2);//备服务器IP
                initProp5._strModule = Marshal.StringToHGlobalUni("Login");

                ClientDemo.PUCApiAdapter.InitPucApiDataProperty initProp6 = new ClientDemo.PUCApiAdapter.InitPucApiDataProperty();
                initProp6._configname = Marshal.StringToHGlobalUni("serversipport2");
                initProp6._configValue = Marshal.StringToHGlobalUni("0");//备服务器端口
                initProp6._strModule = Marshal.StringToHGlobalUni("Login");

                ClientDemo.PUCApiAdapter.InitPucApiDataProperty[] initProps = new ClientDemo.PUCApiAdapter.InitPucApiDataProperty[6];
                initProps[0] = initProp;
                initProps[1] = initProp2;
                initProps[2] = initProp3;
                initProps[3] = initProp4;
                initProps[4] = initProp5;
                initProps[5] = initProp6;

                IntPtr propsPtr = MarshalArray(initProps);
                PucApiParam._nPropertyCount = initProps.Length;
                PucApiParam._property = propsPtr;
                PucApiParam._bOverNat1 = pc.BOverNat1?1:0;
                PucApiParam._bOverNat2 = pc.BOverNat2?1:0;
                PUCApiAdapter.PUCAPI_SetAPILog(4, 10);//日志级别由0到4,0基本不打日志
                try
                {
                    PUCApiAdapter.PUCAPI_Init(ref PucApiParam);
                    configpc = pc;
                }
                catch
                {
                    Logger.Debug("PUCAPI_Init failed.");
                }

            }
            catch(Exception ex)
            {

            }
        }


        public static string GUID
        {
            get
            {
                return Guid.NewGuid().ToString("B");
            }
        }
        public bool login(paramclass pc)
        {
            _loginPC = pc;
            //StartVOIP();

            bool result = true;
            if (Islogin)
            {

                fengclass.product_name = "PUC";
                fengclass.version = "10";
                fengclass.cmd_name = "puc_login";

                fengclass.cmd_guid = GUID;
                logincmdguid = fengclass.cmd_guid;
                fengclass.login_type = "Dispatcher";

                fengclass.user_name = pc.user_name.Trim();
                fengclass.password = EncryptDES(pc.user_password.Trim());


                string xml = Serialize(fengclass);
                //(Application.Current.MainWindow as MainWindow2).update(xml);
                IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
                PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
            }
            else
            {
                result = PUCApiAdapter.PUCAPI_Start();

                Thread.Sleep(5000);//等5秒，初始化成功后再登陆

                if (result)
                {
                    string cmdguid = GUID;        //这种取值会出现“{}”，如 {GUID}
                    //string cmdguid = Guid.NewGuid().ToString();
                    logincmdguid = cmdguid;
                    string name2 = pc.user_name.Trim();
                    string password = EncryptDES(pc.user_password.Trim());

                    string xml = ("<hytera><product_name>PUC</product_name><version>10</version><cmd_name>puc_login</cmd_name><cmd_guid>" + cmdguid + "</cmd_guid><login_type>Dispatcher</login_type><user_name>" + name2 + "</user_name><password>" + password + "</password></hytera>").Trim();
                    //(Application.Current.MainWindow as MainWindow2).update(xml);
                    IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
                    PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
                    Islogin = true;
                }
            }
            return Islogin;
        }
        public static string Serialize(object t)
        {
            try
            {
                Type tp = t.GetType();
                XmlSerializer ser = new XmlSerializer(tp);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Xml.XmlWriterSettings xws = new System.Xml.XmlWriterSettings();
                xws.Encoding = UnicodeEncoding.Unicode;
                xws.OmitXmlDeclaration = true;
                xws.IndentChars = "\t";
                xws.NewLineChars = "\r\n";
                xws.NewLineHandling = System.Xml.NewLineHandling.None;
                xws.NewLineOnAttributes = true;
                using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sw, xws))
                {
                    ser.Serialize(xw, t, ns);
                }
                return sw.ToString();
            }
            catch (Exception ex)
            {
                
            }

            return string.Empty;

        }


        public void PTTDown(callparam cp, string cmd_guid, string sap_Guid, System.Windows.Controls.TextBox txtMessage)
        {
            CC session = CallManager.GetInstance().CreateCCSession(cp,cmd_guid);
            string xml =string.Empty;
            switch (cp.sapstyle)
            {
                #region PDT
                case "5": {                
                    string name2 = cp.callernumber.Trim();
                    string sap_type = cp.sapstyle.Trim();
                    string puc_id = cp.PUC_ID;
                    string system_id = cp.systemID.Trim();
                    string call_type = cp.callednumberstyle.Trim();
                    string callednumber = cp.callednumber.Trim();
                    string caller_type = cp.callernumberstyle.Trim();
                    string callernumber = cp.callernumber.Trim();
                    string duplex = cp.IsDuplex.Trim();
                    string Encryption = cp.IsEncryption.Trim();
                    string cmdguid = cmd_guid;
                    string sap_guid = "";
                    if (string.IsNullOrEmpty(sap_Guid))
                    {
                        sapguid = "AD6F8B48-01A1-4A21-A84E-D9D3304E528B";
                    }
                    else
                    {
                        sapguid = sap_Guid;
                    }
                    sap_guid = "<sap_guid>" + sapguid + "</sap_guid>";
                    string g = "";
                    if (call_type == "0")
                    {
                        g = "<priority>1</priority><hook_signaling_flag>1</hook_signaling_flag><duplex_flag>" + duplex + "</duplex_flag>";
                    }
                     xml = string.Format(@"<hytera>
<product_name>PUC</product_name>
<version>10</version>
<cmd_name>cc_setup_call</cmd_name>
<cmd_guid>{0}</cmd_guid>
<puc_id>{1}</puc_id>
<system_id>{2}</system_id>
<user_id>{3}</user_id>
<media>
	<audio>
	<rtp_local_ip>{4}</rtp_local_ip>
	<rtp_local_port>{5}</rtp_local_port>
	<codec>8</codec>
	</audio>
</media>
<sap_type>5</sap_type>
<sap_guid>{10}</sap_guid>
<call_type>{6}</call_type>
<priority>1</priority>
<end2end_encryption_flag>0</end2end_encryption_flag>
<hook_signaling_flag>1</hook_signaling_flag>
<duplex_flag>{11}</duplex_flag>
<caller>
	<number>{7}</number>
	<number_type>7</number_type>
</caller>
<called>
	<number>{8}</number>
	<number_type>{9}</number_type>
</called>
</hytera>
", cmd_guid, puc_id, system_id, name2, session.Media.Audio.Rtp_local_ip, session.Media.Audio.Rtp_local_port, call_type, name2, callednumber, call_type, sapguid, cp.IsDuplex);
                    break;
                }
                #endregion

                #region LTE
                case "31":{
            string name2 = cp.callernumber.Trim();
            string sap_type = cp.sapstyle.Trim();
            string puc_id = cp.PUC_ID;
            string system_id = cp.systemID.Trim();
            string calledtype = cp.callednumberstyle.Trim();
            string callednumber = cp.callednumber.Trim();
            string caller_type = cp.callernumberstyle.Trim();
            string callernumber = cp.callernumber.Trim();
            string duplex = cp.IsDuplex.Trim();
            string Encryption = cp.IsEncryption.Trim();
            string cmdguid = cmd_guid;
            string sap_guid = "";
            if (string.IsNullOrEmpty(sap_Guid))
            {
                sapguid = "AD6F8B48-01A1-4A21-A84E-D9D3304E528B";
            }
            else
            {
                sapguid = sap_Guid;
            }
            sap_guid = "<sap_guid>" + sapguid + "</sap_guid>";

            string medie = string.Empty;
            string calltype=string.Empty;
            if (cp.CallMode == GlobalCommandName.CallMode.video)
            {
                StringBuilder vedioxml = new StringBuilder();
                vedioxml.Append("<media><video>");
                vedioxml.Append("<rtp_local_ip>" + session.Media.Video.Rtp_local_ip + "</rtp_local_ip>");
                vedioxml.Append("<rtp_local_port>" + session.Media.Video.Rtp_local_port + "</rtp_local_port>");
                vedioxml.Append("<codec>98</codec>");
                vedioxml.Append("<FrameRate>25</FrameRate>");
                vedioxml.Append("<FrameSize>2</FrameSize>");
                vedioxml.Append("<decode_level>3</decode_level>");
                vedioxml.Append("<encode_level>1</encode_level>");
                vedioxml.Append("<decode_framesize>7</decode_framesize>");
                vedioxml.Append("</video></media>");

                medie = vedioxml.ToString();
                if (cp.callednumberstyle == "0")
                {
                    calltype = GlobalCommandName.CallType_Individual_Video;
                }
                else if (cp.callednumberstyle == "1")
                {
                    calltype = GlobalCommandName.CallType_Group_Video;
                }
            }
            else if (cp.CallMode == GlobalCommandName.CallMode.Audio)
            {
                StringBuilder vedioxml = new StringBuilder();
                vedioxml.Append("<media><audio>");
                vedioxml.Append("<rtp_local_ip>" + session.Media.Audio.Rtp_local_ip + "</rtp_local_ip>");
                vedioxml.Append("<rtp_local_port>" + session.Media.Audio.Rtp_local_port + "</rtp_local_port>");
                vedioxml.Append("<codec>8</codec>");
                vedioxml.Append("</audio></media>");
                vedioxml.Append("<FrameRate>25</FrameRate>");
                vedioxml.Append("<FrameSize>3</FrameSize>");
                medie = vedioxml.ToString();
                 if (cp.callednumberstyle == "0")
                 {
                     calltype = GlobalCommandName.CallType_Individual;
                 }
                 else if (cp.callednumberstyle == "1")
                 {
                     calltype = GlobalCommandName.CallType_Group;
                 }
            }
            else if (cp.CallMode == GlobalCommandName.CallMode.AudioAndVideo)
            {
                StringBuilder vedioxml = new StringBuilder();
                vedioxml.Append("<media><audio>");
                vedioxml.Append("<rtp_local_ip>" + session.Media.Audio.Rtp_local_ip + "</rtp_local_ip>");
                vedioxml.Append("<rtp_local_port>" + session.Media.Audio.Rtp_local_port + "</rtp_local_port>");
                vedioxml.Append("<codec>8</codec>");
                vedioxml.Append("</audio>");
                vedioxml.Append("<video>");
                vedioxml.Append("<rtp_local_ip>" + session.Media.Video.Rtp_local_ip + "</rtp_local_ip>");
                vedioxml.Append("<rtp_local_port>" + session.Media.Video.Rtp_local_port + "</rtp_local_port>");
                vedioxml.Append("<codec>98</codec>");
                vedioxml.Append("<FrameRate>25</FrameRate>");
                vedioxml.Append("<FrameSize>2</FrameSize>");
                vedioxml.Append("<decode_level>3</decode_level>");
                vedioxml.Append("<encode_level>1</encode_level>");
                vedioxml.Append("<decode_framesize>7</decode_framesize>");
                vedioxml.Append("</video></media>");
                vedioxml.Append("<FrameRate>25</FrameRate>");
                vedioxml.Append("<FrameSize>2</FrameSize>");
                vedioxml.Append("<cameratype>0</cameratype>");
                vedioxml.Append("<videocall_type>0</videocall_type>");
                medie = vedioxml.ToString();
                 if (cp.callednumberstyle == "0")
                 {
                     calltype = GlobalCommandName.CallType_Individual_VideoAndVoc;
                 }
                 else if (cp.callednumberstyle == "1")
                 {
                     calltype = GlobalCommandName.CallType_Group_VideoAndVoc;
                 }
            }
        
             xml = string.Format(@"<hytera>
<product_name>PUC</product_name>
<version>10</version>
<cmd_name>cc_setup_call</cmd_name>
<cmd_guid>{0}</cmd_guid>
<puc_id>{1}</puc_id>
<system_id>{2}</system_id>
<user_id>{3}</user_id>
{14}
<call_mode>{15}</call_mode>
<sap_type>{6}</sap_type>
<sap_guid>{13}</sap_guid>
<call_type>{7}</call_type>
<end2end_encryption_flag>0</end2end_encryption_flag>
<priority>1</priority>
<hook_signaling_flag>1</hook_signaling_flag>
<duplex_flag>{8}</duplex_flag>
<caller>
	<number>{9}</number>
	<number_type>{10}</number_type>
</caller>
<called>
	<number>{11}</number>
	<number_type>{12}</number_type>
</called>
</hytera>
", cmd_guid, puc_id, system_id, name2, session.LocalIp, session.LocalPort, sap_type, calltype, duplex, callernumber, caller_type, callednumber, calledtype, sapguid, medie, cp.CallMode.GetHashCode());
                    break;
                    }
            }
            #endregion

            if (txtMessage != null)
            {
                txtMessage.Text += "\n发起呼叫 send xml：\n";
                txtMessage.Text += FormatXml(xml);
                txtMessage.ScrollToEnd();

            }
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            CallManager.GetInstance().CCSession.Add(cmd_guid, session);//添加到呼叫管理集合中，cc_connected_evt 会带有对端port信息，到时再进行Send句柄的申请与link
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }
        public void Disconnect(string str)
        {
            string cacmdguid = str;
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_disconnect</cmd_name><cmd_guid>" + cacmdguid + "</cmd_guid><sap_guid /></hytera>";

            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
            IntPtr cmdguid = Marshal.StringToHGlobalUni(cacmdguid);
            CallManager.GetInstance().CCSession.Remove(str);
            //PUCApiAdapter.VOC_StopPlay(cmdguid);
        }
        public void Disconnect(string str, System.Windows.Controls.TextBox txtMessage)
        {
            string cacmdguid = str;
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_disconnect</cmd_name><cmd_guid>" + cacmdguid + "</cmd_guid><sap_guid /></hytera>";
            //string xml2 = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_disconnect</cmd_name><cmd_guid>" + cacmdguid + "</cmd_guid><puc_id>00001</puc_id><sap_guid>1AC901B3-9CCF-4AA3-BA4F-D1B6AB3662B3</sap_guid></hytera>";
            txtMessage.Text += "\n挂断 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();

            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
            IntPtr cmdguid = Marshal.StringToHGlobalUni(cacmdguid);
            PUCApiAdapter.VOC_StopPlay(cmdguid);
            str = null;
        }
        public void sendmessage(SDSParam sds, string str, System.Windows.Controls.TextBox txtMessage)
        {
            string name2 = sds.sendernumber.Trim();
            string sender_type = sds.sendernumberstyle.Trim();
            string receivenumber = sds.receivenumber.Trim();
            string receivenumstyle = sds.receivenumberstyle.Trim();
            string puc_id = sds.PUC_ID;
            string systemID = sds.systemID.Trim();
            string sapstyle = sds.sapstyle.Trim();
            string sdscontent = sds.sdscontent.Trim();
            string sendcmdguid = str;
            string sap_guid = "";

            foreach (DeviceEntity device in AnalysisXml.GetInstance().GetDeviceListDictionary().Values)
            {
                if (string.IsNullOrEmpty(device.sap_guid)) continue;
                if (device.device_id == receivenumber && device.online == "1")//SAP类型一样，系统类型一样，且SAP上线的
                {
                    sap_guid = device.sap_guid;
                }
            }
            if(string.IsNullOrEmpty(sap_guid))
            {
                foreach (SapEntity sap in AnalysisXml.GetInstance().GetSapListDictionary().Values)
                {
                    if (sap.Sap_Type == sapstyle && sap.System_id == systemID && sap.Online == "1")//SAP类型一样，系统类型一样，且SAP上线的
                    {
                        sap_guid = sap.Sap_guid;
                    }
                }
            }
            
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>sds_send_text</cmd_name><cmd_guid>" + sendcmdguid +
                "</cmd_guid><puc_id>" + puc_id + "</puc_id><sap_type>5</sap_type><sds_content>" + sdscontent +
                " </sds_content><sap_guid>" + sap_guid + "</sap_guid><reception_flag>0</reception_flag><flash_flag>0</flash_flag><emergency_flag>0</emergency_flag><encode_type>7</encode_type><sender><number>" + name2 +
                "</number><number_type>" + sender_type + "</number_type></sender><recipient><number>" + receivenumber + "</number><number_type>" + receivenumstyle +
                "</number_type></recipient><system_id>" + systemID + "</system_id><end2end_encryption_flag>0</end2end_encryption_flag></hytera>";
            txtMessage.Text += "\n发送短信 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        public void sap_list_request(string puc_id)
        {
            string guid = Guid.NewGuid().ToString("B");
            string xml1 = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>sap_list_request</cmd_name><cmd_guid>" + guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id><version_seq>0</version_seq></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml1);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 加载设备列表
        /// </summary>
        /// <param name="puc_id"></param>
        public void device_list_request(string puc_id)
        {
            string guid = Guid.NewGuid().ToString("B");
            string xml1 = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>device_list_request</cmd_name><cmd_guid>" + guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml1);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        public void group_list_request(string puc_id)
        {
            string guid = Guid.NewGuid().ToString("B");
            string xml1 = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>group_list_request</cmd_name><cmd_guid>" + guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml1);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        public void organization_list_request(string puc_id)
        {
            string guid = Guid.NewGuid().ToString("B");
            string xml1 = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>organization_list_request</cmd_name><cmd_guid>" + guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml1);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }
        /// <summary>
        /// 加载设备列表
        /// </summary>
        /// <param name="puc_id"></param>
        public void system_list_request(string puc_id)
        {
            string guid = Guid.NewGuid().ToString("B");
            string xml1 = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>system_list_request</cmd_name><cmd_guid>" + guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml1);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 动态重组查询
        /// </summary>
        /// <param name="str"></param>
        public void dgna_request(string puc_id)
        {
            string guid = Guid.NewGuid().ToString("B");
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>dgna_request</cmd_name><cmd_guid>" + guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);

        }

        /// <summary>
        /// 新增动态重组
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="groupName"></param>
        public void add_dgna(string systemId, string groupName, paramclass pc, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString("B");
            string dgna_guid = Guid.NewGuid().ToString("B");
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>add_dgna</cmd_name><cmd_guid>" + cmd_guid + "</cmd_guid><puc_id>" + pc.PUC_ID + "</puc_id><dgna_guid>" + dgna_guid + "</dgna_guid><dgna_alias>" + groupName + "</dgna_alias><dispatcher_account>" + pc.user_name + "</dispatcher_account><system_id>" + systemId + "</system_id></hytera>";
            txtMessage.Text += "\n新增动态重组 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        public void update_dgna(string dgna_guid,string dgnaName)
        {
            string cmd_guid = Guid.NewGuid().ToString("B");
            string xml = "<hytera>"
	                    +"<product_name>puc</product_name>"
	                    +"<version>10</version>"
	                    +"<cmd_name>update_dgna</cmd_name>"
                        + "<cmd_guid>" + cmd_guid + "</cmd_guid>"
                        + "<dgna_guid>" + dgna_guid + "</dgna_guid>"
	                    +"<dgna_number></dgna_number>"
                        + "<dgna_alias>" + dgnaName + "</dgna_alias>"
	                    +"<system_id/>"
                        + "<dispatcher_account>" + fengclass.user_name + "</dispatcher_account>"
                        +"</hytera>";
            //string xml2 = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>update_dgna</cmd_name><cmd_guid>" + cmd_guid + "</cmd_guid><dgna_guid>" + dgna_guid + "</dgna_guid><number>70030914</number><dgna_alias>g2</dgna_alias><system_id>007</system_id><dispatcher_account>t18</dispatcher_account></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalAnsi(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 删除动态重组
        /// </summary>
        public void Del_dgna(GroupEntry ge, string LocalIp, string User_Name, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString("B");            
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>delete_dgna</cmd_name><cmd_guid>" + cmd_guid + "</cmd_guid><dgna_guid>" + ge.GUID + "</dgna_guid><system_id>" + ge.SystemID + "</system_id></hytera>";
                          
            txtMessage.Text += "\n删除动态重组 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 新增动态重组成员
        /// </summary>
        public void add_dgna_member(string dgna_guid,string puc_id, DeviceEntry deviceEntry, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString("B");
            string member_guid = Guid.NewGuid().ToString("B");
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>add_dgna_member</cmd_name><cmd_guid>" + cmd_guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id><dgna_guid>" + dgna_guid + "</dgna_guid><device_guid>" + deviceEntry.GUID + "</device_guid><member_guid>" + member_guid + "</member_guid><number>" + deviceEntry.Device_id + "</number><number_type>" + deviceEntry.Number_type + "</number_type></hytera>";
            //string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>add_dgna_member</cmd_name><cmd_guid>" + cmd_guid + "</cmd_guid><puc_id>" + puc_id + "</puc_id><dgna_guid>" + dgna_guid + "</dgna_guid><device_guid>A8B55BDA-C214-4846-8715-38CCC795AE03</device_guid><member_guid>" + member_guid + "</member_guid><number>70020200</number><number_type>0</number_type></hytera>";
            txtMessage.Text += "\n新增动态重组成员 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 删除动态重组成员
        /// </summary>
        public void delete_dgna_member(string dgna_guid, string number_guid, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString("B");
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>delete_dgna_member</cmd_name><cmd_guid>" + cmd_guid + "</cmd_guid><member_guid>" + number_guid + "</member_guid><dgna_guid>" + dgna_guid + "</dgna_guid></hytera>";
            txtMessage.Text += "\n删除动态重组成员 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// GPS订阅
        /// </summary>
        /// <param name="subscriberinfo"></param>
        public void gps_batch_start(string subscriberinfo, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = "<hytera>"
                        +"<product_name>PUC</product_name>"
                        +"<version>10</version><cmd_name>gps_batch_start_gps_info_report</cmd_name><cmd_guid>"+cmd_guid+"</cmd_guid>"
                        + "<subscriberinfo_list>" + subscriberinfo + "</subscriberinfo_list>"
                        +"</hytera>";
            txtMessage.Text += "\n GPS订阅 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 停止GPS订阅
        /// </summary>
        /// <param name="subscriberinfo"></param>
        public void gps_batch_stop(string subscriberinfo, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = "<hytera>"
                        +"<product_name>PUC</product_name>"
                        +"<version>10</version>"
                        +"<cmd_name>gps_batch_stop_gps_info_report</cmd_name>"
                        + "<cmd_guid>" + cmd_guid + "</cmd_guid>"
                        + "<unsubscriberinfo_list>" + subscriberinfo + "</unsubscriberinfo_list>"
                        +"</hytera>";
            txtMessage.Text += "\n 停止GPS订阅 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        public void gps_record_query(string userId, string systemType, string systemId, string number, string numberType, string startTime, string endTime, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = string.Format(@"<hytera>
<product_name>PUC</product_name>
<user_id>{0}</user_id>
<version>10</version>
<cmd_name>gps_record_query</cmd_name>
<cmd_guid>{1}</cmd_guid>
<system_type>{2}</system_type>
<system_id>{3}</system_id>
<target>
<number>{4}</number>
<number_type>{5}</number_type>
</target>
<start_time>{6}</start_time>
<end_time>{7}</end_time>
</hytera>", userId, cmd_guid, systemType, systemId, number, numberType, startTime, endTime);
            txtMessage.Text += "\n GPS历史轨迹 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 抢占话权
        /// </summary>
        /// <param name="str"></param>
        public void demand(string str, string puc_id, System.Windows.Controls.TextBox txtMessage)
        {
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_tx_demand</cmd_name><cmd_guid>"+str+"</cmd_guid><puc_id>"+puc_id+"</puc_id><sap_type>"+ saptype+ "</sap_type><sap_guid>"+ sapguid +"</sap_guid></hytera>";
            if (txtMessage != null)
            {
                txtMessage.Text += "\n抢占话权 send xml：\n";
                txtMessage.Text += FormatXml(xml);
                txtMessage.ScrollToEnd();
            }
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 释放话权
        /// </summary>
        /// <param name="str"></param>
        public void cease(string str,string puc_id, System.Windows.Controls.TextBox txtMessage)
        {
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_tx_cease</cmd_name><cmd_guid>" + str + "</cmd_guid><puc_id>" + puc_id + "</puc_id><sap_type>" + saptype + "</sap_type><sap_guid>" + sapguid + "</sap_guid></hytera>";
            txtMessage.Text += "\n释放话权 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 设备状态检测（上下线）
        /// </summary>
        /// <param name="DeviceGuid"></param>
        /// <param name="puc_id"></param>
        /// <param name="UserID"></param>
        /// <param name="systemType"></param>
        public void DeviceCheckStatus(string DeviceGuid, string puc_id, string UserID, string systemType, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>device_status_check</cmd_name><cmd_guid>" + cmd_guid +
                "</cmd_guid><puc_id>" + puc_id + "</puc_id><device_guid>" + DeviceGuid + "</device_guid><user_id>" + UserID + "</user_id><system_type>" + systemType + "</system_type><device_ssi /><sap_type>" + saptype + "<sap_type/></hytera>";
            txtMessage.Text += "\n状态检测 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 摇晕
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="systemType"></param>
        /// <param name="puc_id"></param>
        /// <param name="DeviceGuid"></param>
        /// <param name="txtMessage"></param>
        public void SetStun(string UserID, string systemType, string puc_id, string DeviceGuid, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = "<hytera>"
                        + "<product_name>PUC</product_name>"
                        + "<user_id>" + UserID + "</user_id>"
                        + "<version>10</version>"
                        + "<cmd_name>device_stun_request</cmd_name>"
                        + "<cmd_guid>" + cmd_guid + "</cmd_guid>"
                        + "<system_type>" + systemType + "</system_type>"
                        + "<puc_id>" + puc_id + "</puc_id>"
                        + "<device_guid>" + DeviceGuid + "</device_guid>"
                        + "<dispatcher_guid>调度员GUID</dispatcher_guid>"
                        + "<dispatcher_name>调度员名字</dispatcher_name>"
                        + "</hytera>";
            txtMessage.Text += "\n状态检测 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 摇醒
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="systemType"></param>
        /// <param name="puc_id"></param>
        /// <param name="DeviceGuid"></param>
        /// <param name="txtMessage"></param>
        public void SetRevive(string UserID, string systemType, string puc_id, string DeviceGuid, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = "<hytera>"
                        + "<product_name>PUC</product_name>"
                        + "<user_id>" + UserID + "</user_id>"
                        + "<version>10</version>"
                        + "<cmd_name>device_revive_request</cmd_name>"
                        + "<cmd_guid>" + cmd_guid + "</cmd_guid>"
                        + "<system_type>" + systemType + "</system_type>"
                        + "<puc_id>" + puc_id + "</puc_id>"
                        + "<device_guid>" + DeviceGuid + "</device_guid>"
                        + "<dispatcher_guid>调度员GUID</dispatcher_guid>"
                        + "<dispatcher_name>调度员名字</dispatcher_name>"
                        + "</hytera>";
            txtMessage.Text += "\n状态检测 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 摇毙
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="systemType"></param>
        /// <param name="puc_id"></param>
        /// <param name="DeviceGuid"></param>
        /// <param name="txtMessage"></param>
        public void DeviceKill(string UserID, string systemType, string puc_id, string DeviceGuid, System.Windows.Controls.TextBox txtMessage)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = "<hytera>"
                        + "<product_name>puc</product_name>"
                        + "<user_id>" + UserID + "</user_id>"
                        + "<version>10</version>"
                        + "<puc_id>" + puc_id + "</puc_id>"
                        + "<cmd_name>device_kill_request</cmd_name>"
                        + "<cmd_guid>" + cmd_guid + "</cmd_guid>"
                        + "<system_type>" + systemType + "</system_type>"
                        + "<device_guid>" + DeviceGuid + "</device_guid>"
                        + "<dispatcher_guid>调度员GUID<dispatcher_guid>"
                        + "<dispatcher_name>调度员名称<dispatcher_name>"
                        + "</hytera>";
            txtMessage.Text += "\n状态检测 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="pc"></param>
        public void loginout(paramclass pc)
        {
            string name2 = pc.user_name.Trim();
            string password = EncryptDES(pc.user_password.Trim());
            string xml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>puc_logout</cmd_name><cmd_guid>"+ logincmdguid +"</cmd_guid><user_name>" + name2 + "</user_name></hytera>";
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 呼叫
        /// </summary>
        /// <param name="pc"></param>
//        public void SetupCall(string cmd_guid, string caller, string sapType, string called, string calledNumberType)
//        {
//            CC session = CallManager.GetInstance().CreateCCSession(cmd_guid);
//            string puc_id = MainWindow2.GetLocalParam().PUC_ID;
////            string xml = string.Format(@"<hytera>
////<product_name>PUC</product_name>
////<version>10</version>
////<cmd_name>cc_setup_call</cmd_name>
////<cmd_guid>{0}</cmd_guid>                                                                                                                                       
////<puc_id>00001</puc_id>
////<sap_type>{1}</sap_type>
////<call_type>9</call_type>
////<priority>1</priority>
////<end2end_encryption_flag>0</end2end_encryption_flag>
////<hook_signaling_flag>1</hook_signaling_flag><ambience_listening_flag>0</ambience_listening_flag><duplex_flag>1</duplex_flag>
////<caller><number>{2}</number><number_type>7</number_type></caller>
////<called><number>{3}</number><number_type>{4}</number_type></called>
////</hytera>", cmd_guid, sapType, caller, called, calledNumberType);
//            string xml = string.Format(@"<hytera>
//<product_name>PUC</product_name>
//<version>10</version>
//<cmd_name>cc_setup_call</cmd_name>
//<cmd_guid>{0}</cmd_guid>
//<puc_id>{1}</puc_id>
//<system_id>001</system_id>
//<user_id>{2}</user_id>
//<media>
//	<audio>
//	<rtp_local_ip>{3}</rtp_local_ip>
//	<rtp_local_port>{4}</rtp_local_port>
//	<codec>8</codec>
//	</audio>
//</media>
//<sap_type>5</sap_type>
//<sap_guid>C2B21909-0076-49A8-8D7B-D502F2A8CBA1</sap_guid>
//<call_type>{5}</call_type>
//<priority>1</priority>
//<end2end_encryption_flag>0</end2end_encryption_flag>
//<hook_signaling_flag>1</hook_signaling_flag>
//<duplex_flag>0</duplex_flag>
//<caller>
//	<number>{6}</number>
//	<number_type>7</number_type>
//</caller>
//<called>
//	<number>{7}</number>
//	<number_type>{8}</number_type>
//</called>
//</hytera>
//", cmd_guid, puc_id, caller, session.LocalIp, session.LocalPort, calledNumberType, caller, called, calledNumberType);
//            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
//            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
//            CallManager.GetInstance().CCSession.Add(cmd_guid, session);//添加到呼叫管理集合中，cc_connected_evt 会带有对端port信息，到时再进行Send句柄的申请与link
//        }

        /// <summary>
        /// 转接
        /// </summary>
        /// <param name="cmd_guid"></param>
        /// <param name="system_id"></param>
        /// <param name="called"></param>
        /// <param name="calledNumberType"></param>
        /// <param name="devolveType"></param>
        public void devolveCall(string cmd_guid, string system_id, string called, string calledNumberType, string devolveType)
        {
            string xml = string.Empty;
            string cmdXml = "<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_call_devolve_request</cmd_name><destination>";
            if (devolveType == "0")
            {
                //盲转
                cmdXml += "<number>{0}</number><system_id>{1}</system_id><number_type>{2}</number_type><mode>0</mode></destination></hytera>";
                xml = string.Format(cmdXml, called, system_id, calledNumberType);
            }
            else
            {
                //咨询转
                cmdXml += "<cmd_guid>{0}</cmd_guid><mode>2</mode></destination></hytera>";
                xml = string.Format(cmdXml, cmd_guid);
            }
            
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 发送传真
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="faxFiles"></param>
        /// <param name="recNames"></param>
        public void SendFax(string userId, List<string> faxFiles, string[] recNames)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            StringBuilder cmdXml = new StringBuilder();
            cmdXml.Append("<hytera><product_name>PUC</product_name><version>10</version><cmd_name>fax_send_file</cmd_name><cmd_guid>cmd_guid</cmd_guid><user_id>" + userId + "</user_id><sendernum>" + userId + "</sendernum><sendername>" + userId + "</sendername>");
            for (int i = 0; i < faxFiles.Count; i++)
            {
                cmdXml.Append("<filename1>" + faxFiles[i] + "</filename1>");
            }
            cmdXml.Append( "<recvinfo_list>");
            for (int i = 0; i < recNames.Length; i++)
            {
                cmdXml.Append( "<recvinfo><recnum>" + recNames[i] + "</recnum><recname>" + recNames[i] + "</recname></recvinfo>");
            }
            cmdXml.Append("</recvinfo_list></hytera>");

            IntPtr xmlPtr = Marshal.StringToHGlobalUni(cmdXml.ToString());
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        /// <summary>
        /// 接听
        /// </summary>
        /// <param name="pc"></param>
        public void RevcCall(string callGuid, string sapType)
        {
            string cmd_guid = Guid.NewGuid().ToString();
            string xml = string.Format(@"<hytera><product_name>PUC</product_name><version>10</version><cmd_name>cc_connect</cmd_name><cmd_guid>{0}</cmd_guid><puc_id>00001</puc_id><sap_type>{1}</sap_type></hytera>", callGuid, sapType);
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        }

        public BusinessCenter()
        {
            InitializeCallbackEvent = new ClientDemo.PUCApiAdapter.PUCCallbackEventHandler(Instance_InitializeCallbackEvent);
            PucApiParam.OnResponse = InitializeCallbackEvent;

        }

        private IntPtr MarshalArray(ClientDemo.PUCApiAdapter.InitPucApiDataProperty[] _properties)
        {
            int size = Marshal.SizeOf(typeof(ClientDemo.PUCApiAdapter.InitPucApiDataProperty));
            int iTotalSize = size * _properties.Length;
            IntPtr pUnmagePtr = Marshal.AllocHGlobal(iTotalSize);
            for (int i = 0; i < _properties.Length; i++)
            {
                Marshal.StructureToPtr(_properties[i], pUnmagePtr + size * i, false);
            }

            return pUnmagePtr;
        }

        public static BusinessCenter Instance
        {
            get
            {
                if (_pBusinessCenter == null)
                {
                    _pBusinessCenter = new BusinessCenter();
                }
                return _pBusinessCenter;
            }
        }

        /// <summary>
        /// 协议返回接口
        /// </summary>
        /// <param name="strPtr"></param>
        public void Instance_InitializeCallbackEvent(IntPtr strPtr)
        {
            string pXmlData = Marshal.PtrToStringUni(strPtr);
            _CallbackDataList.Add(pXmlData);
            pXmlData22 = pXmlData;

            hytera feng = Deserialize<hytera>(pXmlData22);

            if (feng.result == "0" || feng.online == "1")
            {
                //if (showDialog != null)
                //    showDialog("1");
            }
            sapguid = feng.sap_guid;
            saptype = feng.sap_type;
            AnalysisXml.GetInstance().ReciveData(pXmlData);
            Console.Write(pXmlData);

            if (OnDataback != null)
                OnDataback(pXmlData22);

            if (pXmlData.Contains("cc_connected_evt"))//接通后要打开语音
            {
                //if (!isOpenVoc)//语音接口打开成功后不用再打开第二次

                // StartVoice();
                // StartVOIP();
             
                if (MediaManager.resultAudioStartCap > 1)
                {
                }
                else
                {
                    MessageBox.Show("打开麦克风失败");
                }


                XmlDocument xml = new XmlDocument();
                xml.LoadXml(pXmlData);
                XmlNode root = xml.SelectSingleNode("hytera");
                string cmd_guid = root.SelectSingleNode("cmd_guid").InnerText;

                XmlNode XmlOfMedia = root.SelectSingleNode("media");
                XmlNode XmlOfAudio = XmlOfMedia == null ? null : XmlOfMedia.SelectSingleNode("audio");
                string VoicedestIp = "";
                int VoicedestPort = 0;
                if (XmlOfAudio != null)
                {
                     VoicedestIp = XmlOfAudio.SelectSingleNode("rtp_remote_ip").InnerText;//从evt中获取服务器端 rtp接收ip , port
                     VoicedestPort = int.Parse(XmlOfAudio.SelectSingleNode("rtp_remote_port").InnerText);
                }
                XmlNode XmlOfVideo = XmlOfMedia == null ? null : XmlOfMedia.SelectSingleNode("video");
                string VideodestIp = "";
                int VideodestPort = 0;
                if (XmlOfVideo != null)
                {
                    VideodestIp = XmlOfVideo.SelectSingleNode("rtp_remote_ip").InnerText;//从evt中获取服务器端 rtp接收ip , port
                    VideodestPort = int.Parse(XmlOfVideo.SelectSingleNode("rtp_remote_port").InnerText);
                }

               
                CC cc = CallManager.GetInstance().CCSession[cmd_guid];//从session集合中获取对应session

                if (cc != null)
                {
                    //if (transfercmdguid == null && cmdguid != null)
                    if (transfercmdguid == null)
                    {
                        if (cc.CallMode == GlobalCommandName.CallMode.Audio || cc.CallMode == GlobalCommandName.CallMode.AudioAndVideo)
                        {
                            //if (cc.VoicePtrLinkPlay > 0)
                            //    MediaManager.GetInstance().StopLinkByPtrLink(cc.VoicePtrLinkPlay);
                            //cc.VoicePtrPlay = MediaManager.GetInstance().GetAudioPlayPtr();//播放语音

                            //if (cc.VoiceSendSipPort > 0)
                            //{
                            //    cc.VoicePtrSendSip = MediaManager.GetInstance().GetPtrSendBycmdGuid(cmd_guid, configpc.LocalSipIP, "20.0.0.99", cc.VoiceSendSipPort, 2);//发送语音
                            //    cc.VoicePtrLinkPlay = MediaManager.GetInstance().StartLinkByPtr(cc.VoicePtrRecv, cc.VoicePtrSendSip);//链接语音  接收-->播放
                            //    //cc.VoicePtrLinkPlay = MediaManager.GetInstance().StartLinkByPtr(cc.VoicePtrRecv, cc.VoicePtrPlay);//链接语音  接收-->播放
                            //}
                            //cc.VoicePtrLinkPlay = MediaManager.GetInstance().StartLinkByPtr(cc.VoicePtrRecv, cc.VoicePtrPlay);//链接语音  接收-->播放
                        }
                        if (cc.CallMode == GlobalCommandName.CallMode.video || cc.CallMode == GlobalCommandName.CallMode.AudioAndVideo)
                        {
                            if (cc.VideoPtrLinkPlay > 0)
                                MediaManager.GetInstance().StopLinkByPtrLink(cc.VideoPtrLinkLocalPlay);
                            cc.VideoPtrPlay = MediaManager.GetInstance().GetVedioPlayPtr(cc.pictrueboxHandle);//播放视频
                            cc.VideoPtrLinkPlay = MediaManager.GetInstance().StartLinkByPtr(cc.VideoPtrRecv, cc.VideoPtrPlay);//链接视频  接收-->播放
                        }
                    }
                    cc.VoicePtrSend = MediaManager.GetInstance().GetPtrSendBycmdGuid(cmd_guid, configpc.LocalSipIP, VoicedestIp, VoicedestPort, 2);//发送语音
                    cc.VideoPtrSend = MediaManager.GetInstance().GetPtrSendBycmdGuid(cmd_guid, configpc.LocalSipIP, VideodestIp, VideodestPort, 1);//发送视频


                    MediaManager.GetInstance().StartLinkByPtr(cc.VoicePrtRecvSip, cc.VoicePtrSend);
                    demand(cmdguid, _loginPC.PUC_ID , null);


                    if (cc.IsDuplex)
                    {

                        if(MediaManager.resultAudioStartCap<1)
                            MediaManager.GetInstance().StartCapAudio();
                        if (cc.VoicePtrLinkSend < 1)
                        {
                            cc.VoicePtrLinkSend = MediaManager.GetInstance().StartLinkByPtr(MediaManager.resultAudioStartCap, cc.VoicePtrSend);//链接语音 采集-->发送  
                        }

                      
                         if(MediaManager.resultVideoStartCap<1)
                            MediaManager.GetInstance().StartCapVideo();
                         if (cc.VideoPtrLinkSend < 1)
                         {
                             cc.VideoPtrLinkSend = MediaManager.GetInstance().StartLinkByPtr(MediaManager.resultVideoStartCap, cc.VideoPtrSend);//链接视频 采集-->发送   
                         }
                    }

                    if (transfercmdguid == cc.CallID)
                    {
                        if (cc.VideoPtrSend < 1)
                        {
                            cc.VideoPtrSend = MediaManager.GetInstance().GetPtrSendBycmdGuid(cmd_guid, configpc.LocalSipIP, VideodestIp, VideodestPort, 1);//发送视频
                        }
                        if (cmdguid != null)
                        {
                            cc.VideoPtrLinkSend = MediaManager.GetInstance().StartLinkByPtr(CallManager.GetInstance().CCSession[cmdguid].VideoPtrRecv, cc.VideoPtrSend);//链接视频 采集-->发送 
                        }
                    }
                    /*
                     *对于demo，一路呼叫接通，直接开启流的发送，不根据话权的更替做变化。
                     *由PUCServer端根据对应的sip来决策流的转发与接收与否，建议正常版本不要这么做 
                     *对于接收，session结束前不停
                     */

                }
            }
            if (pXmlData.Contains("cc_disconnected_evt"))//挂断后要关闭语音
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(pXmlData);
                XmlNode root = xml.SelectSingleNode("hytera");
                string cmd_guid = root.SelectSingleNode("cmd_guid").InnerText;

                //StopVoc(cmd_guid);
                BusinessCenter.Instance.StopHndWhenDisconnected(cmd_guid);
                BusinessCenter.Instance.Disconnect(cmd_guid);
                BusinessCenter.Instance.RemoveFromCallList(cmd_guid);//移出语音发送列表
            }
            if (pXmlData.Contains("cc_tx_granted_evt"))
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(pXmlData);
                XmlNode root = xml.SelectSingleNode("hytera");
                string grant_status = root.SelectSingleNode("grant_status").InnerText;
                string cmd_guid = root.SelectSingleNode("cmd_guid").InnerText;

                CC cc = CallManager.GetInstance().CCSession[cmd_guid];//从session集合中获取对应session
                if (grant_status == "0")
                {
                    if (cc.VoicePtrLinkSend > 0)
                    {
                        MediaManager.MediaTerm_CtrlSendPause(cc.VoicePtrSend, 1);
                    }
                    if (cc.VideoPtrLinkSend > 0)
                    {
                        MediaManager.MediaTerm_StopSend(cc.VideoPtrLinkSend);
                    }
                }
                else if (grant_status == "1")
                {
                    if (cc.VoicePtrLinkSend > 0)// if (sessionptr.VoicePtrLinkSend > 0 && !sessionptr.IsMuteMic && sessionptr.CallType != CallTypes.PushVedioCall)
                    {
                        MediaManager.MediaTerm_CtrlSendPause(cc.VoicePtrSend, 0);
                    }
                    else
                    {
                        if (!cc.IsDuplex)
                        {
                            if (MediaManager.resultAudioStartCap < 1)
                                MediaManager.GetInstance().StartCapAudio();
                            cc.VoicePtrLinkSend = MediaManager.GetInstance().StartLinkByPtr(MediaManager.resultAudioStartCap, cc.VoicePtrSend);//链接语音 采集-->发送  
                        }
                    }

                    if (MediaManager.resultVideoStartCap < 1)
                        MediaManager.GetInstance().StartCapVideo();
                    if (cc.VideoPtrLinkSend < 1 && cc.VideoPtrSend > 0)
                    {
                        cc.VideoPtrLinkSend = MediaManager.GetInstance().StartLinkByPtr(MediaManager.resultVideoStartCap, cc.VideoPtrSend);//链接视频 采集-->发送   
                    }

                }
            }
            if (pXmlData.Contains("cc_incoming_evt"))
            { 
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(pXmlData);
                string cmd_guid = xml.ChildNodes[0].SelectSingleNode("cmd_guid").InnerText;
                string puc_id = xml.ChildNodes[0].SelectSingleNode("puc_id").InnerText;
                string system_id = xml.ChildNodes[0].SelectSingleNode("system_id").InnerText;
                string user_id = xml.ChildNodes[0].SelectSingleNode("user_id").InnerText;

                string sap_type = xml.ChildNodes[0].SelectSingleNode("sap_type").InnerText;
                string sap_guid = xml.ChildNodes[0].SelectSingleNode("sap_guid").InnerText;
                callparam cp=new callparam();
                cp.CallMode= GlobalCommandName.CallMode.Audio;
                CC session = CallManager.GetInstance().CreateCCSession(cp, cmd_guid);
                string sendXML = string.Format(@"<hytera>
  <product_name>PUC</product_name>
  <version>10</version>
  <cmd_name>cc_connect</cmd_name>
  <cmd_guid>{0}</cmd_guid>
  <puc_id>{1}</puc_id>
  <system_id>{2}</system_id>
  <user_id>{3}</user_id>
  <media>
    <audio>
      <rtp_local_ip>{4}</rtp_local_ip>
      <rtp_local_port>{5}</rtp_local_port>
      <codec>{6}</codec>
    </audio>
  </media>
  <sap_type>{7}</sap_type>
  <sap_guid>{8}</sap_guid>
</hytera>", cmd_guid, puc_id, system_id, user_id, session.Media.Audio.Rtp_local_ip, session.Media.Audio.Rtp_local_port, "8", sap_type, sap_guid);
                //txtMessage.Text += "\n接通监听呼叫 send xml：\n";
                //txtMessage.Text += FormatXml(xml);
                //txtMessage.ScrollToEnd();
                IntPtr xmlPtr = Marshal.StringToHGlobalUni(sendXML);
                CallManager.GetInstance().CCSession.Add(cmd_guid, session);//添加到呼叫管理集合中，cc_connected_evt 会带有对端port信息，到时再进行Send句柄的申请与link
                cmdguid = cmd_guid;
                PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);

                var caller = xml.ChildNodes[0].SelectSingleNode("caller").SelectSingleNode("number").InnerText;
                var called = xml.ChildNodes[0].SelectSingleNode("called").SelectSingleNode("number").InnerText;
                if (OnIncomingCallEvent != null)
                {
                    OnIncomingCallEvent(called, caller);
                }
            }

           


            if (OnDataback != null)
                OnDataback(pXmlData22);
        }

        private bool StopHndWhenDisconnected(string cmdGuid)
        {
            CC session = null;
            if (CallManager.GetInstance().CCSession.Count > 0)
            {
                session = CallManager.GetInstance().CCSession[cmdGuid];
            }
            if (session != null)
            {
                //停止相关媒体库事件




                int stopLinkPlay = MediaManager.GetInstance().StopLinkByPtrLink(session.VoicePtrLinkPlay);
                int stopLinkSend = MediaManager.GetInstance().StopLinkByPtrLink(session.VoicePtrLinkSend);
                int stopSend = MediaManager.GetInstance().StopSendByPtrSend(session.VoicePtrSend);
                int stopPlayPtr = MediaManager.GetInstance().StopPlayByPtrPlay(session.VoicePtrPlay);
                int stopRecvPtr = MediaManager.GetInstance().StopRecvByPtrRecv(session.VoicePtrRecv);


                int stopVideoPtrLinkPlay = MediaManager.GetInstance().StopLinkByPtrLink(session.VideoPtrLinkPlay);
                int stopVideoPtrLinkLocalPlay = MediaManager.GetInstance().StopLinkByPtrLink(session.VideoPtrLinkLocalPlay);
                int stopVideoPtrLinkSend = MediaManager.GetInstance().StopLinkByPtrLink(session.VideoPtrLinkSend);
                int stopVideoPtrRecv = MediaManager.GetInstance().StopRecvByPtrRecv(session.VideoPtrRecv);
                int stopVideoPtrPlay = MediaManager.GetInstance().StopPlayByPtrPlay(session.VideoPtrPlay);
                int stopVideoPtrSend = MediaManager.GetInstance().StopSendByPtrSend(session.VideoPtrSend);

                

                if((stopLinkPlay+stopLinkSend+stopPlayPtr+stopRecvPtr)== 0)
                {
                    return true;
                }
                else
                {
                    Logger.Debug("PUC_Disconnected_evt   Response: Release Resource failed.");
                    return false;
                }
            }
            else
            {
                Logger.Debug("PUC_Disconnected_evt   Response:  GetSession failed.");
                return false;
            }
        }

        public static T Deserialize<T>(string contentXml) where T : class
        {
            try
            {
                using (StringReader reader = new StringReader(contentXml))
                {
                    Type tp = typeof(T);
                    XmlSerializer ser = new XmlSerializer(tp);
                    return ser.Deserialize(reader) as T;
                }
            }
            catch (Exception ex)
            {
             
            }

            return null;
        }

        #region 加密

        public enum Module
        {
            ENCRYPT, // ENCRYPT:加密
            DECRYPT //DECRYPT:解密
        };
        /// <summary>
        /// DEC加密接口
        /// </summary>
        /// <param name="pData">需要加密的文本指針</param>
        /// <param name="iLen">需要加密的文本長度</param>
        /// <param name="pIV">向量</param>
        /// <param name="bModule">加密或解密</param>
        /// <param name="pDst">密文輸出指針</param>
        /// <param name="iDstlen">密文長度</param>
        /// <returns>返回實際長度</returns>
        [DllImport("Encrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static int Des_CBC_crypt(IntPtr pData, int iLen, IntPtr pIV, Module bModule, IntPtr pDst, int iDstlen);


        //默认密钥向量
        private static byte[] _keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string _encryptKey = "HytBSoft";
        private paramclass _loginPC;

        public static string EncryptDES(string encryptString)
        {
            try
            {

                return ClientDemo.Util.DECEncryptHelper.EncryptDES(encryptString);
            }
            catch(Exception ex)
            {
                Logger.Error("密码加密失败",ex);
                return "";

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static IntPtr ByteToIntptrEx(byte[] bytes)
        {
            GCHandle hObj = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr pObj = hObj.AddrOfPinnedObject();
            hObj.Free();
            return pObj;
        }

#endregion



        public void AddSysMutetMicList(string callId)
        {
            lock (strSysMutetMicList)
            {
                if (!this.strSysMutetMicList.Contains(callId))
                {
                    this.strSysMutetMicList.Add(callId);
                }
            }
        }
        public void RemoveSysMutetMicList(string callId)
        {
            lock (strSysMutetMicList)
            {
                this.strSysMutetMicList.Remove(callId);
            }
        }

        /// <summary>
        /// 发送语音到服务器
        /// </summary>
        /// <param name="uMsg"></param>
        /// <param name="nCodecType"></param>
        /// <param name="nLen"></param>
        /// <param name="pSrcVocWaveBuf"></param>
        /// <param name="dwAppInstance"></param>
        /// <returns></returns>
        public uint processMicCallback(uint uMsg, int nCodecType, int nLen, IntPtr pSrcVocWaveBuf, uint dwAppInstance)
        {
            {
                lock (strCallList)
                {
                    foreach (var item in strCallList)
                    {
                        if (this.strSysMutetMicList.Contains(item)) continue;   //转接之后静音呼叫
                        IntPtr cmdguid = Marshal.StringToHGlobalUni(item);
                        PUCApiAdapter.VOIP_SendVocData(cmdguid, nCodecType, pSrcVocWaveBuf, nLen);
                        Marshal.FreeHGlobal(cmdguid);
                    }
                }
            }
            //解锁
            return 0;
        }



        public void AddToCallList(string callId)
        {

            if (!this.strCallList.Contains(callId))
            {
                this.strCallList.Add(callId);
            }
        }

        public void RemoveFromCallList(string callId)
        {
            lock (strCallList)
            {
                this.strCallList.Remove(callId);
            }
        }

        /// <summary>
        /// 格式化xml字符串
        /// </summary>
        /// <param name="sUnformattedXml"></param>
        /// <returns></returns>
        private string FormatXml(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            catch(Exception e)
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

        public void OpenLocalCamera(int pictrueboxHandle)
        {
            MediaManager.GetInstance().OpenLocalCamera(pictrueboxHandle);
        }

        public void TransferFuction(callparam cp, string cmd_guid, string sap_Guid, System.Windows.Controls.TextBox txtMessage)
        {
            cp.istransferCall = true;
            CC session = CallManager.GetInstance().CreateCCSession(cp, cmd_guid);
            string xml = string.Empty;
            switch (cp.sapstyle)
            {
                #region PDT
                case "5":
                    {
                        string name2 = cp.callernumber.Trim();
                        string sap_type = cp.sapstyle.Trim();
                        string puc_id = cp.PUC_ID;
                        string system_id = cp.systemID.Trim();
                        string call_type = cp.callednumberstyle.Trim();
                        string callednumber = cp.callednumber.Trim();
                        string caller_type = cp.callernumberstyle.Trim();
                        string callernumber = cp.callernumber.Trim();
                        string duplex = cp.IsDuplex.Trim();
                        string Encryption = cp.IsEncryption.Trim();
                        string cmdguid = cmd_guid;
                        string sap_guid = "";
                        if (string.IsNullOrEmpty(sap_Guid))
                        {
                            sapguid = "AD6F8B48-01A1-4A21-A84E-D9D3304E528B";
                        }
                        else
                        {
                            sapguid = sap_Guid;
                        }
                        sap_guid = "<sap_guid>" + sapguid + "</sap_guid>";
                        string g = "";
                        if (call_type == "0")
                        {
                            g = "<priority>1</priority><hook_signaling_flag>1</hook_signaling_flag><duplex_flag>" + duplex + "</duplex_flag>";
                        }
                        xml = string.Format(@"<hytera>
<product_name>PUC</product_name>
<version>10</version>
<cmd_name>cc_setup_call</cmd_name>
<cmd_guid>{0}</cmd_guid>
<puc_id>{1}</puc_id>
<system_id>{2}</system_id>
<user_id>{3}</user_id>
<media>
	<audio>
	<rtp_local_ip>{4}</rtp_local_ip>
	<rtp_local_port>{5}</rtp_local_port>
	<codec>8</codec>
	</audio>
</media>
<sap_type>5</sap_type>
<sap_guid>C2B21909-0076-49A8-8D7B-D502F2A8CBA1</sap_guid>
<call_type>{6}</call_type>
<priority>1</priority>
<end2end_encryption_flag>0</end2end_encryption_flag>
<hook_signaling_flag>1</hook_signaling_flag>
<duplex_flag>0</duplex_flag>
<caller>
	<number>{7}</number>
	<number_type>7</number_type>
</caller>
<called>
	<number>{8}</number>
	<number_type>{9}</number_type>
</called>
</hytera>
", cmd_guid, puc_id, system_id, name2, session.LocalIp, session.LocalPort, call_type, name2, callednumber, call_type);
                        break;
                    }
                #endregion

                #region LTE
                case "31":
                    {
                        string name2 = cp.callernumber.Trim();
                        string sap_type = cp.sapstyle.Trim();
                        string puc_id = cp.PUC_ID;
                        string system_id = cp.systemID.Trim();
                        string calledtype = cp.callednumberstyle.Trim();
                        string callednumber = cp.callednumber.Trim();
                        string caller_type = cp.callernumberstyle.Trim();
                        string callernumber = cp.callernumber.Trim();
                        string duplex = cp.IsDuplex.Trim();
                        string Encryption = cp.IsEncryption.Trim();
                        string cmdguid = cmd_guid;
                        string sap_guid = "";
                        if (string.IsNullOrEmpty(sap_Guid))
                        {
                            sapguid = "AD6F8B48-01A1-4A21-A84E-D9D3304E528B";
                        }
                        else
                        {
                            sapguid = sap_Guid;
                        }
                        sap_guid = "<sap_guid>" + sapguid + "</sap_guid>";

                        string medie = string.Empty;
                        string calltype = string.Empty;
                        if (cp.CallMode == GlobalCommandName.CallMode.video)
                        {
                            StringBuilder vedioxml = new StringBuilder();
                            vedioxml.Append("<media><video>");
                            vedioxml.Append("<rtp_local_ip>" + session.Media.Video.Rtp_local_ip + "</rtp_local_ip>");
                            vedioxml.Append("<rtp_local_port>" + session.Media.Video.Rtp_local_port + "</rtp_local_port>");
                            vedioxml.Append("<codec>98</codec>");
                            vedioxml.Append("<FrameRate>25</FrameRate>");
                            vedioxml.Append("<FrameSize>2</FrameSize>");
                            vedioxml.Append("<decode_level>3</decode_level>");
                            vedioxml.Append("<encode_level>1</encode_level>");
                            vedioxml.Append("<decode_framesize>7</decode_framesize>");
                            vedioxml.Append("</video></media>");
                            vedioxml.Append("<videocall_type>2</videocall_type>");
                            vedioxml.Append("<videosource_type>2</videosource_type>");
                            vedioxml.Append("<FrameRate>25</FrameRate>");
                            vedioxml.Append("<FrameSize>3</FrameSize>");
                            medie = vedioxml.ToString();
                            if (cp.callednumberstyle == "0")
                            {
                                calltype = GlobalCommandName.CallType_PushVedioCall;//视频转发都是22;GlobalCommandName.CallType_PushVedioCall
                            }
                            else if (cp.callednumberstyle == "1")
                            {
                                calltype = GlobalCommandName.CallType_PushVedioCall;
                            }
                        }
                        else if (cp.CallMode == GlobalCommandName.CallMode.Audio)
                        {
                            StringBuilder vedioxml = new StringBuilder();
                            vedioxml.Append("<media><audio>");
                            vedioxml.Append("<rtp_local_ip>" + session.Media.Audio.Rtp_local_ip + "</rtp_local_ip>");
                            vedioxml.Append("<rtp_local_port>" + session.Media.Audio.Rtp_local_port + "</rtp_local_port>");
                            vedioxml.Append("<codec>8</codec>");
                            vedioxml.Append("</audio></media>");
                            vedioxml.Append("<FrameRate>25</FrameRate>");
                            vedioxml.Append("<FrameSize>3</FrameSize>");
                            medie = vedioxml.ToString();
                            if (cp.callednumberstyle == "0")
                            {
                                calltype = GlobalCommandName.CallType_PushVedioCall;
                            }
                            else if (cp.callednumberstyle == "1")
                            {
                                calltype = GlobalCommandName.CallType_PushVedioCall;
                            }
                        }
                        else if (cp.CallMode == GlobalCommandName.CallMode.AudioAndVideo)
                        {
                            StringBuilder vedioxml = new StringBuilder();
                            vedioxml.Append("<media><audio>");
                            vedioxml.Append("<rtp_local_ip>" + session.Media.Audio.Rtp_local_ip + "</rtp_local_ip>");
                            vedioxml.Append("<rtp_local_port>" + session.Media.Audio.Rtp_local_port + "</rtp_local_port>");
                            vedioxml.Append("<codec>8</codec>");
                            vedioxml.Append("</audio>");
                            vedioxml.Append("<video>");
                            vedioxml.Append("<rtp_local_ip>" + session.Media.Video.Rtp_local_ip + "</rtp_local_ip>");
                            vedioxml.Append("<rtp_local_port>" + session.Media.Video.Rtp_local_port + "</rtp_local_port>");
                            vedioxml.Append("<codec>98</codec>");
                            vedioxml.Append("<FrameRate>25</FrameRate>");
                            vedioxml.Append("<FrameSize>2</FrameSize>");
                            vedioxml.Append("<decode_level>3</decode_level>");
                            vedioxml.Append("<encode_level>1</encode_level>");
                            vedioxml.Append("<decode_framesize>7</decode_framesize>");
                            vedioxml.Append("</video></media>");
                            vedioxml.Append("<FrameRate>25</FrameRate>");
                            vedioxml.Append("<FrameSize>2</FrameSize>");
                            vedioxml.Append("<videocall_type>2</videocall_type>");
                            vedioxml.Append("<videosource_type>2</videosource_type>");
                            medie = vedioxml.ToString();
                            if (cp.callednumberstyle == "0")
                            {
                                calltype = GlobalCommandName.CallType_PushVedioCall;
                            }
                            else if (cp.callednumberstyle == "1")
                            {
                                calltype = GlobalCommandName.CallType_PushVedioCall;
                            }
                        }

                        xml = string.Format(@"<hytera>
<product_name>PUC</product_name>
<version>10</version>
<cmd_name>cc_setup_call</cmd_name>
<cmd_guid>{0}</cmd_guid>
<puc_id>{1}</puc_id>
<system_id>{2}</system_id>
<user_id>{3}</user_id>
{14}
<call_mode>{15}</call_mode>
<sap_type>{6}</sap_type>
<sap_guid>{13}</sap_guid>
<call_type>{7}</call_type>
<end2end_encryption_flag>0</end2end_encryption_flag>
<priority>1</priority>
<hook_signaling_flag>1</hook_signaling_flag>
<duplex_flag>0</duplex_flag>
<caller>
	<number>{9}</number>
	<number_type>{10}</number_type>
</caller>
<called>
	<number>{11}</number>
	<number_type>{12}</number_type>
</called>
</hytera>
", cmd_guid, puc_id, system_id, name2, session.LocalIp, session.LocalPort, sap_type, calltype, duplex, callernumber, caller_type, callednumber, calledtype, sapguid, medie, cp.CallMode.GetHashCode());
                        break;
                    }
            }
                #endregion

            txtMessage.Text += "\n发起呼叫 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            CallManager.GetInstance().CCSession.Add(cmd_guid, session);//添加到呼叫管理集合中，cc_connected_evt 会带有对端port信息，到时再进行Send句柄的申请与link
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
        } 
    }
}