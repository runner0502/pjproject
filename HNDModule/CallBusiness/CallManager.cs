using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientDemo.CallBusiness
{
    public class CallManager
    {
        public static string _localSipIP;
        private static CallManager _manager;

        /// <summary>
        /// cmdGuid
        /// 对应cmdguid实体的CC
        /// </summary>
        public Dictionary<string, CC> CCSession = new Dictionary<string,CC>();
        private CallManager()
        {

        }

        public static CallManager GetInstance()
        {
            if(_manager != null)
                return _manager;

            else
            {
                _manager = new CallManager();
                return _manager;
            }
        }

        public CC CreateCCSession(callparam cp,string cmdGuid) 
        {
            CC session = new CC();
            session.pictrueboxHandle = cp.pictrueboxHandle;
            session.LocalIp = _localSipIP;
            session.IsDuplex = cp.IsDuplex == "1" ? true : false;
            session.CallID = cp.cmd_guid;
            if (cp.CallMode == "Voice")
                session.CallMode = "0";
            if (cp.CallMode == "Vedio")
                session.CallMode = "1";
            if (cp.CallMode == "Vedio+Voice")
                session.CallMode = "2";


            if (session.LocalIp != null)
            {
                try
                {
                    switch (cp.CallMode)
                    {
                        case "Vedio":
                            {
                                session.VideoPtrRecv = MediaManager.GetInstance().GetVedioPtrRecvBycmdGuid(session.LocalIp, cmdGuid);
                                IntPtr ptr = MediaManager.MediaTerm_GetHndPara(session.VideoPtrRecv);
                                ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo result = (ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo)Marshal.PtrToStructure(ptr, typeof(ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo));
                                if (result.localPort != 0)
                                {
                                    session.Media = new Media();
                                    session.Media.Video = new AudioEntity();
                                    session.Media.Video.Rtp_local_ip = result.ipAddr;
                                    session.Media.Video.Rtp_local_port = result.localPort.ToString();
                                    //CurrentCCSession = session;
                                    return session;//此时已经规定本Session的本地端口
                                }
                                else
                                {
                                    return null;
                                }
                            }
                          
                        case "Voice":
                            {
                                session.VoicePtrRecv = MediaManager.GetInstance().GetVoicePtrRecvBycmdGuid(session.LocalIp, cmdGuid);
                                IntPtr ptr = MediaManager.MediaTerm_GetHndPara(session.VoicePtrRecv);
                                ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo result = (ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo)Marshal.PtrToStructure(ptr, typeof(ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo));
                                if (result.localPort != 0)
                                {
                                    session.Media = new Media();
                                    session.Media.Audio = new AudioEntity();
                                    session.Media.Audio.Rtp_local_ip = result.ipAddr;
                                    session.Media.Audio.Rtp_local_port = result.localPort.ToString();
                                   // CurrentCCSession = session;
                                    return session;//此时已经规定本Session的本地端口
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        case "Vedio+Voice":
                            {
                                session.VideoPtrRecv = MediaManager.GetInstance().GetVedioPtrRecvBycmdGuid(session.LocalIp, cmdGuid);
                                IntPtr ptr1 = MediaManager.MediaTerm_GetHndPara(session.VideoPtrRecv);
                                ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo result1 = (ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo)Marshal.PtrToStructure(ptr1, typeof(ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo));
                                session.Media = new Media();
                                if (result1.localPort != 0)
                                {
                                    session.Media.Video = new AudioEntity();
                                    session.Media.Video.Rtp_local_ip = result1.ipAddr;
                                    session.Media.Video.Rtp_local_port = result1.localPort.ToString();
                                }
                                else
                                {
                                    return null;
                                }

                                session.VoicePtrRecv = MediaManager.GetInstance().GetVoicePtrRecvBycmdGuid(session.LocalIp, cmdGuid);
                                IntPtr ptr2 = MediaManager.MediaTerm_GetHndPara(session.VoicePtrRecv);
                                ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo result2 = (ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo)Marshal.PtrToStructure(ptr2, typeof(ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo));
                                if (result2.localPort != 0)
                                {
                                    session.Media.Audio = new AudioEntity();
                                    session.Media.Audio.Rtp_local_ip = result2.ipAddr;
                                    session.Media.Audio.Rtp_local_port = result2.localPort.ToString();
                                }
                                else
                                {
                                    return null;
                                }
                                //CurrentCCSession = session;
                                return session;
                            }

                        default: { return null; }
                            
                    }






                }
                catch(Exception ex)
                {
                    MessageBox.Show("创建本地rtp接收端口失败，请确认语音设备正常运行。");
                    Hytera.Commom.Log.Logger.Error("session.VoicePtrRecv："+session.VoicePtrRecv, ex);
                    return null;
                }

            }
            else
            {
                MessageBox.Show("请先登录，获取本地ip失败");
                return null;
            }
        }

    }
}
