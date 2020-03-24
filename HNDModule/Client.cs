using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientDemo;

namespace HNDModule
{
    public class Client
    {

        static Client g_instance;
        private LoginPara _loginpara;

        public static Client GetInstance()
        {
            if (g_instance == null)
            {
                g_instance = new Client();
            }
            return g_instance;
        }

        public static paramclass pc;
        callparam _cp;
        SDSParam _sds;
        List<GroupEntry> _categories;     //声明动态分组对象
        private List<DeviceEntry> _devicelist;
        List<SapEntity> SapEntityList;
        string CallGuid = string.Empty;
        string CallSapType = string.Empty;
        private const string _fileName = "config/AutoConfig.xml";
        string _pucId = "00001";

        public event OnIncomingCall OnIncomingCallEvent;

        public bool Init()
        {
            pc = new paramclass();
            Cp = new callparam();
            Sds = new SDSParam();
            SapEntityList1 = new List<ClientDemo.SapEntity>();
            Categories = new List<GroupEntry>();
            Devicelist = new List<DeviceEntry>();
            //BusinessCenter.Instance.OnDataback += update;
            //BusinessCenter.Instance.showDialog += showdialog;
            Hytera.Commom.Log.Logger.configLogger();
            //string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //Directory.SetCurrentDirectory(path);
            MediaManager.GetInstance().InitMediaTerm();

            //LoginPara loginpara = Hytera.I18N.XMLHelper.DeSerializeFromFile<LoginPara>(fileName);

            Loginpara = new LoginPara();
            Loginpara.LoginName = "anzheng";
            Loginpara.Password = "123456";
            Loginpara.LocolSipIP = "20.0.0.99";
            Loginpara.LocolSipPort = "7060";
            Loginpara.ServerIP = "20.0.0.11";
            Loginpara.ServerPort = "12000";
            Loginpara.ServerSipIP = "20.0.0.11";
            Loginpara.ServerSipPort = "6060";

            BusinessCenter.Instance.OnIncomingCallEvent += Instance_OnIncomingCallEvent;

            return true;
        }

        private void Instance_OnIncomingCallEvent(string called, string caller)
        {
            if (OnIncomingCallEvent != null)
            {
                OnIncomingCallEvent(called, caller);
            }
        }

        public void Login()
        {
            pc.user_name = Loginpara.LoginName;
            pc.user_password = Loginpara.Password;
            // pc.user_password = "NlGrPtPl";
            pc.PUC_ID = _pucId;
            pc.LocalSipIP = Loginpara.LocolSipIP;
            pc.ServerIP = Loginpara.ServerIP;
            pc.IP3 = "";
            pc.ServerSipIP = Loginpara.ServerSipIP;

            pc.LocalSipPort = Loginpara.LocolSipPort;
            pc.ServerPort = Loginpara.ServerPort;
            pc.ID3 = "1";
            pc.ServerSipPort = Loginpara.ServerSipPort;
            //pc.BOverNat1 = false;
            //pc.BOverNat2 = false;

            Login(pc);
        }

        public void Login(paramclass pc)
        {
            ClientDemo.CallBusiness.CallManager._localSipIP = pc._localSipIP;

            BusinessCenter.Instance.InitPucApi(pc);

            Hytera.Commom.Log.Logger.Debug("Login Start");
            bool flag = BusinessCenter.Instance.login(pc);

            //update(flag ? "登录成功" : "登录失败");


            //   MessageBox.Show("hello");
            //callnum.Text = txt1.Text;
            //numstyle.Text = "Dispatcher";
            //sendernum.Text = txt1.Text;
            //sendernumstyle.Text = "Dispatcher";
            //Thread.Sleep(5000);

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

        public void Logout()
        {
            //pc.user_name = txt1.Text.ToString();
            //pc.user_password = psword.Password.ToString();
            //pc.LocalSipIP = localSipIP.Text.ToString();
            //pc.ServerIP = serverIP.Text.ToString();
            //pc.IP3 = IP3.Text.ToString();
            //pc.ServerSipIP = serverSipIP.Text.ToString();

            //pc.LocalSipPort = localSipPort.Text.ToString();
            //pc.ServerPort = serverPort.Text.ToString();
            //pc.ID3 = ID3.Text.ToString();
            //pc.ServerSipPort = serverSipPort.Text.ToString();

            //LoginSubmitEvent();
            //Dispatcher.Invoke(new Action(delegate ()
            //{
            //    txt.Text = "";
            //}));

            BusinessCenter.Instance.loginout(pc);

        }

        public void Dispose1()
        {
            try
            {
                Logout();
                PUCApiAdapter.VOC_CloseAudioDevice();
                PUCApiAdapter.PUCAPI_Stop();
                PUCApiAdapter.VOIP_Stop();
            }
            catch (Exception ex)
            {

            }
        }

        private string _cmdguid = null;
        public string cmdguid
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

        public callparam Cp
        {
            get
            {
                return _cp;
            }

            set
            {
                _cp = value;
            }
        }

        public SDSParam Sds
        {
            get
            {
                return _sds;
            }

            set
            {
                _sds = value;
            }
        }

        public LoginPara Loginpara
        {
            get
            {
                return _loginpara;
            }

            set
            {
                _loginpara = value;
            }
        }

        public List<GroupEntry> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                _categories = value;
            }
        }

        public List<DeviceEntry> Devicelist
        {
            get
            {
                return _devicelist;
            }

            set
            {
                _devicelist = value;
            }
        }

        public List<SapEntity> SapEntityList1
        {
            get
            {
                return SapEntityList;
            }

            set
            {
                SapEntityList = value;
            }
        }

        enum Calltype
        {
            device = 0, group = 1, other = 5
        }


        public string MakeCall(string number)
        {
            try
            {
                string callcmdguid = BusinessCenter.GUID;
                cmdguid = callcmdguid;
                Cp.cmd_guid = callcmdguid;
                Cp.callernumber = Loginpara.LoginName;
                //if (numstyle.Text.ToString() == "Dispatcher")
                //{
                Cp.callernumberstyle = "7";
                //}

                Cp.callednumber = number;
                //if (calledstyle.Text.ToString() == "个号")
                //{
                Cp.callednumberstyle = "0";
                //}
                //else
                //{
                //cp.callednumberstyle = "1";
                //}
                //          cp.callednumberstyle = calledstyle.Text.ToString();
                Cp.PUC_ID = _pucId;
                Cp.systemID = "001";
                Cp.sapstyle = "5";
                string sap_GUID = string.Empty;
                if (SapEntityList != null && SapEntityList.Count > 0)
                {
                    try
                    {
                        sap_GUID = SapEntityList.FirstOrDefault(sap => sap.System_id == Cp.systemID).Sap_guid;
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

                //cp.IsDuplex = "1";
                Cp.IsDuplex = "0";
                // cp.IsEncryption = "1";
                Cp.IsEncryption = "0";
                Cp.CallMode = GlobalCommandName.CallMode.Audio;
                //cp.pictrueboxHandle = pictureBox.Handle.ToInt32();
                //根据systemId获取sap_guid
                //string sap_guid = SapEntityList.First(sap => sap.System_id == cp.systemID).Sap_guid;
                BusinessCenter.Instance.PTTDown(Cp, cmdguid, sap_GUID, null);
                BusinessCenter.Instance.AddToCallList(cmdguid);

                //isshow = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("呼叫异常：" + ex.Message);
                Hytera.Commom.Log.Logger.Debug("PTTDown_Click:" + ex.ToString());
            }

            return cmdguid;

        }

        public int GetLocalPort()
        {
            return ClientDemo.CallBusiness.CallManager.GetInstance().CCSession[BusinessCenter.Instance.cmdguid].VoiceRecvSipPort;
        }

        public void SetRemoteSipPort(int port)
        {
            ClientDemo.CallBusiness.CC session = ClientDemo.CallBusiness.CallManager.GetInstance().CCSession[cmdguid];
            session.VoiceSendSipPort = port;
            //if (session.VoicePtrSend >0)
            //{
                if (session.VoicePtrLinkPlay > 0)
                    MediaManager.GetInstance().StopLinkByPtrLink(session.VoicePtrLinkPlay);
                //cc.VoicePtrPlay = MediaManager.GetInstance().GetAudioPlayPtr();//播放语音
                  session.VoicePtrSendSip = MediaManager.GetInstance().GetPtrSendBycmdGuid(cmdguid, "20.0.0.99", "20.0.0.99", session.VoiceSendSipPort, 2);//发送语音
                  session.VoicePtrLinkPlay = MediaManager.GetInstance().StartLinkByPtr(session.VoicePtrRecv, session.VoicePtrSendSip);//链接语音  接收-->播放
            //}
        }
    }
}
