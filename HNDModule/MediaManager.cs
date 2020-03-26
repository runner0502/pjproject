using ClientDemo.CallBusiness;
using ClientDemo.Util;
using Hytera.Commom.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ClientDemo
{
    public class MediaManager
    {
        private static MediaManager manager;
        private bool isInitMediaTerm;

        public static bool MediaTermRuning = false;

        private object _lock = new object();

        /// <summary>
        /// 是否已经打开了摄像头，并且采集视频流
        /// </summary>
        //static bool isOpenCamera = false;
        public static string cameraName = "NoCamera";
        public static int resultAudioStartCap = 0;
        public static int resultVideoStartCap = 0;
        public static bool isAudioCapNormal = false;
        public static bool isVideoCapNormal = false;


        /// <summary>
        /// 音视频接口返回的设备信息，用于打开语音设备
        /// </summary>
        public static List<ClientDemo.PUCApiAdapter.mediaTermDeviceInfo> mediaTermDeviceInfoList = new List<ClientDemo.PUCApiAdapter.mediaTermDeviceInfo>();


        public static int videoWidth = 352;
        public static int videoHeight = 288;
        public static int videoDecodeProfile = 1;
        public static int videoEncodeProfile = 1;
        public static int videoDecodeFrameRate = 25;
        public static int videoEncodeFrameRate = 25;
        public static int videoDecodeFrameSize = VideoFrameSize.enVedioFrameSizeCIF.GetHashCode();
        public static int videoEncodeFrameSize = VideoFrameSize.enVedioFrameSizeCIF.GetHashCode();
        public static int videoPlayFrameSize = VideoFrameSize.enVedioFrameSize1080P.GetHashCode();
        public static int videoMaxResolution = VideoFrameSize.enVedioFrameSizeCIF.GetHashCode();

        /// <summary>
        /// 视频像素枚举
        /// </summary>
        public enum VideoFrameSize
        {
            enVedioFrameSizeQCIF = 0,
            enVedioFrameSizeQVGA = 1,
            enVedioFrameSizeCIF = 2,
            enVedioFrameSizeVGA = 3,
            enVedioFrameSize4CIF = 4,
            enVedioFrameSizeD1 = 5,
            enVedioFrameSize720P = 6,
            enVedioFrameSize1080P = 7
        };





        /// <summary>
        /// 媒体库设备信息回调
        /// </summary>
        static event ClientDemo.PUCApiAdapter.MediaTermDeviceInfoCallBackFun MediaTermDeviceInfoCallBackEvent;
        /// <summary>
        /// 音频事件回调
        /// </summary>
        static event ClientDemo.PUCApiAdapter.MediaTermEventReportCallBackFun MediaTermEventReportCallBackEvent;
        /// <summary>
        /// 本地视频数据采集回调
        /// </summary>
        static event ClientDemo.PUCApiAdapter.MediaTermDataBackFunCallBackFun VideoTermDataBackFunCallBackEvent;

        /// <summary>
        /// 本地音频数据采集回调
        /// </summary>
        static event ClientDemo.PUCApiAdapter.MediaTermDataBackFunCallBackFun MediaTermDataBackFunCallBackEvent;

        private MediaManager()
        {
            MediaTermDeviceInfoCallBackEvent = new ClientDemo.PUCApiAdapter.MediaTermDeviceInfoCallBackFun(MediaTermDeviceInfoCallBack);
            MediaTermEventReportCallBackEvent = new ClientDemo.PUCApiAdapter.MediaTermEventReportCallBackFun(MediaTermEventReportCallBack);
        }
        /// <summary>
        /// 媒体设备信息回调
        /// </summary>
        /// <param name="mediaTermDeviceInfo"></param>
        /// <param name="appIns"></param>
        /// <returns></returns>
        public int MediaTermDeviceInfoCallBack(IntPtr mediaTermDeviceInfo_Ptr, Int64 appIns)
        {
            try
            {
                ClientDemo.PUCApiAdapter.mediaTermDeviceInfo mediaTermDeviceInfoTemp = (ClientDemo.PUCApiAdapter.mediaTermDeviceInfo)Marshal.PtrToStructure(mediaTermDeviceInfo_Ptr, typeof(ClientDemo.PUCApiAdapter.mediaTermDeviceInfo));
                lock (_lock)
                {
                    mediaTermDeviceInfoList.Add(mediaTermDeviceInfoTemp);

                    Logger.Debug(string.Format("@!!!mediaTermDeviceInfoList.Add  mediaType = {0}, devCh = {1}, name = {2},videoMaxProfile = {3},videoMaxResolution = {4}", mediaTermDeviceInfoTemp.mediaType, mediaTermDeviceInfoTemp.devCh,
                        System.Text.Encoding.Default.GetString(mediaTermDeviceInfoTemp.name).TrimEnd('\0'), mediaTermDeviceInfoTemp.videoMaxProfile, mediaTermDeviceInfoTemp.videoMaxResolution));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTermDeviceInfoCallBack error", ex);
            }
            return 0;
        }


        public event Action<int, int> MediaTermEventCallBack;

        /// <summary>
        /// 媒体事件信息回调
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="mediaTermDeviceInfo"></param>
        /// <param name="appIns"></param>
        /// <returns></returns>
        public int MediaTermEventReportCallBack(int handle, IntPtr mediaTermEventInfo_Ptr, Int64 appIns)
        {
            try
            {
                lock (_lock)
                {
                    ClientDemo.PUCApiAdapter.mediaTermEventInfo pMediaTermEventInfo = (ClientDemo.PUCApiAdapter.mediaTermEventInfo)Marshal.PtrToStructure(mediaTermEventInfo_Ptr, typeof(ClientDemo.PUCApiAdapter.mediaTermEventInfo));

                    Logger.Debug("MediaTermEventReportCallBack pMediaTermEventInfo.type:" + pMediaTermEventInfo.type + "pMediaTermEventInfo.subType:" + pMediaTermEventInfo.subType);

                    //if (pMediaTermEventInfo.type == 6 && pMediaTermEventInfo.subType == 9)
                    //{
                    //    isAudioCapNormal = false;
                    //    if (OnMediaCapChanged != null)
                    //    {
                    //        OnMediaCapChanged(1, false);
                    //    }

                    //    isVideoCapNormal = false;
                    //    if (OnMediaCapChanged != null)
                    //    {
                    //        OnMediaCapChanged(2, false);
                    //    }
                    //}

                    //本地媒体服务重启
                    if (pMediaTermEventInfo.type == 6 && pMediaTermEventInfo.subType == 8)
                    {
                        MediaTermRuning = true;
                        resultAudioStartCap = 0;
                        resultVideoStartCap = 0;
                        InitMediaTermDevInfo(true);
                    }
                    //设备列表更新
                    if (pMediaTermEventInfo.type == 1 && pMediaTermEventInfo.subType == 1)
                    {
                        InitMediaTermDevInfo(true);
                    }
                    //音频异常
                    if (pMediaTermEventInfo.type == 1 && pMediaTermEventInfo.subType == 3)
                    {
                        isAudioCapNormal = false;
                        //if (OnMediaCapChanged != null)
                        //{
                        //    OnMediaCapChanged(1, false);
                        //}
                    }
                    //音频正常
                    if (pMediaTermEventInfo.type == 1 && pMediaTermEventInfo.subType == 5)
                    {
                        isAudioCapNormal = true;
                        //if (OnMediaCapChanged != null)
                        //{
                        //    OnMediaCapChanged(1, true);
                        //}
                    }
                    //视频异常 
                    if (pMediaTermEventInfo.type == 1 && pMediaTermEventInfo.subType == 2)
                    {
                        isVideoCapNormal = false;
                        //if (OnMediaCapChanged != null)
                        //{
                        //    OnMediaCapChanged(2, false);
                        //}
                    }
                    //视频正常
                    if (pMediaTermEventInfo.type == 1 && pMediaTermEventInfo.subType == 4)
                    {
                        isVideoCapNormal = true;
                        //if (OnMediaCapChanged != null)
                        //{
                        //    OnMediaCapChanged(2, true);
                        //}
                    }
                    //本地播放结束
                    if (pMediaTermEventInfo.type == 5 && pMediaTermEventInfo.subType == 7)
                    {
                        //if (OnMediaCapChanged != null)
                        //{
                        //    OnMediaCapChanged(pMediaTermEventInfo.detailId, true);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTermEventReportCallBack error:", ex);
            }
            return 0;
        }
        public static MediaManager GetInstance()
        {
            if (manager != null)
            {
                return manager;
            }
            else
            {
                manager = new MediaManager();
                return manager;
            }
        }

        public bool InitMediaTerm()
        {
            if (!this.isInitMediaTerm )
            {
                PUCApiAdapter.mediaTermInitCfg mc = new PUCApiAdapter.mediaTermInitCfg();
                mc.saveLog = 1;
                mc.localIpAddr = "192.168.72.8";

                IntPtr ptr = MemoryControl.StructToIntPtr(mc);
                this.isInitMediaTerm = MediaTerm_Init(ptr, MediaTermEventReportCallBackEvent, 0);//设置1打印媒体库自身中的日志
                Logger.Info("MediaTerm_Init:" + this.isInitMediaTerm);
                MemoryControl.FreeHGlobalPtr(ptr);

                //获取所有设备列表
                if (this.isInitMediaTerm)
                {
                    InitMediaTermDevInfo(true);
                }

            }
            return this.isInitMediaTerm;
        }


        delegate int MediaTerm_InitDelegate(IntPtr ptr, ClientDemo.PUCApiAdapter.MediaTermEventReportCallBackFun eventCallBack, Int64 appIns);
        public static bool MediaTerm_Init(IntPtr ptr, PUCApiAdapter.MediaTermEventReportCallBackFun eventCallBack, Int64 appIns)
        {
            try
            {
                Logger.Debug(string.Format("MediaTerm_Init start  ptr = {0}  saveLog: 保存日志标志isLog=0不保存日志， 1为保存日志 ", ptr));
                MediaTerm_InitDelegate mid = PUCApiAdapter.MediaTerm_Init;
                IAsyncResult ar = mid.BeginInvoke(ptr, eventCallBack, appIns, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 3500)
                    {
                        Logger.Info("MediaTerm timeout");
                        return false;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mid.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_Init(ptr, eventCallBack, appIns);
                Logger.Debug(string.Format("MediaTerm_Init end  result = {0} ,0为成功，其他为失败", result));
                MediaTermRuning = result == 0 ? true : false;
                return result == 0 ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTem_Init", ex);
            }
            return false;
        }

        delegate IntPtr MediaTerm_GetDevInfoDelegate(int index);
        public static IntPtr MediaTerm_GetDevInfo(int index)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return IntPtr.Zero;
                }

                Logger.Debug("MediaTerm_GetDevInfo start ");
                MediaTerm_GetDevInfoDelegate mgdid = PUCApiAdapter.MediaTerm_GetDevInfo;
                IAsyncResult ar = mgdid.BeginInvoke(index, null, null);
                int index1 = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return IntPtr.Zero;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index1 = index1 + 1;
                    }

                }
                IntPtr result = mgdid.EndInvoke(ar);
                //IntPtr result = PUCApiAdapter.MediaTerm_GetDevInfo(index);
                Logger.Debug(string.Format("MediaTerm_GetDevInfo end result = {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_GetDevInfo", ex);
            }
            return new IntPtr();
        }

        public int CapturePtr { get; private set; }

        private void InitMediaTermDevInfo(bool isStartCap = false)
        {
            try
            {
                mediaTermDeviceInfoList.Clear();
                int devNum = MediaTerm_QueryDevNum();

                if (devNum > 0)
                {
                    for (int i = 0; i < devNum; i++)
                    {
                        IntPtr mediaTermDeviceInfo_Ptr = MediaTerm_GetDevInfo(i);
                        ClientDemo.PUCApiAdapter.mediaTermDeviceInfo mediaTermDeviceInfoTemp = (ClientDemo.PUCApiAdapter.mediaTermDeviceInfo)Marshal.PtrToStructure(mediaTermDeviceInfo_Ptr, typeof(ClientDemo.PUCApiAdapter.mediaTermDeviceInfo));
                        lock (_lock)
                        {
                            mediaTermDeviceInfoList.Add(mediaTermDeviceInfoTemp);

                            if (mediaTermDeviceInfoTemp.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.videoPlayDev)
                            {
                                if (mediaTermDeviceInfoTemp.videoMaxResolution > videoDecodeFrameSize || mediaTermDeviceInfoTemp.videoMaxProfile > videoDecodeProfile)
                                {
                                    videoDecodeFrameSize = mediaTermDeviceInfoTemp.videoMaxResolution;
                                    videoDecodeProfile = mediaTermDeviceInfoTemp.videoMaxProfile;

                                    Logger.Debug("StartOpenVideoCapturDevice videoDecodeProfile = " + videoDecodeProfile + "    videoDecodeFrameSize =" + videoDecodeFrameSize);
                                }
                            }

                            if (isStartCap && resultAudioStartCap < 1 && mediaTermDeviceInfoTemp.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.audioCaptureDev)
                                    StartCapAudio();
                            Logger.Debug("isStartCap = " + isStartCap + "resultAudioStartCap = " + resultAudioStartCap);
                            //MessageBox.Show("GET MIC :" + CapturePtr);

                            if (isStartCap && resultVideoStartCap < 1 && mediaTermDeviceInfoTemp.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.videoCaptureDev) { }
                              //  StartOpenDevice(mediaTermDeviceInfoTemp);

                            Logger.Debug(string.Format("--mediaTermDeviceInfoList.Add  mediaType = {0}, devCh = {1}, name = {2},videoMaxProfile = {3},videoMaxResolution = {4}", mediaTermDeviceInfoTemp.mediaType, mediaTermDeviceInfoTemp.devCh,
                                System.Text.Encoding.Default.GetString(mediaTermDeviceInfoTemp.name).TrimEnd('\0'), mediaTermDeviceInfoTemp.videoMaxProfile, mediaTermDeviceInfoTemp.videoMaxResolution));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("InitMediaTermDevInfo error", ex);
            }
        }



        delegate int MediaTerm_QueryDevNumDelegate();
        public static int MediaTerm_QueryDevNum()
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                Logger.Debug("MediaTerm_QueryDevNum start ");
                MediaTerm_QueryDevNumDelegate mqdnd = PUCApiAdapter.MediaTerm_QueryDevNum;
                IAsyncResult ar = mqdnd.BeginInvoke(null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mqdnd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_QueryDevNum();
                Logger.Debug(string.Format("MediaTerm_QueryDevNum end result = {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_QueryDevNum", ex);
            }
            return -1;
        }
        public int StartCapAudio()
        {
            Logger.Debug("StartCapAudio()");
            int result = -1;
            try
            {//从配置文件中读取是否已经设置过，没有默认取第一个
                var sSCardIndex = "";
                int iSCardIndex;
                bool cret = int.TryParse(sSCardIndex, out iSCardIndex);
                if (!cret)
                {
                    Logger.System("Get Audio Mic Fail:" + sSCardIndex.ToString());
                    iSCardIndex = 0;
                }
                //mediaTermDeviceInfoList
                List<ClientDemo.PUCApiAdapter.mediaTermDeviceInfo> auDev = new List<ClientDemo.PUCApiAdapter.mediaTermDeviceInfo>();
                foreach (ClientDemo.PUCApiAdapter.mediaTermDeviceInfo item in mediaTermDeviceInfoList)
                {
                    if (item.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.audioCaptureDev)
                    {
                        auDev.Add(item);
                    }
                }
                //string devName = System.Text.Encoding.Default.GetString(mediaTermDeviceInfoTemp.name);
                //打开选择的设备并采集回调
                if (auDev.Count > 0)
                {
                    if (iSCardIndex < auDev.Count)
                    {
                        result = StartOpenDevice(auDev[iSCardIndex]);
                        Logger.Debug("StartOpenAudioCaptureDevice result = " + result);
                    }
                    else
                    {
                        result = StartOpenDevice(auDev[0]);
                        Logger.Debug("StartOpenAudioCaptureDevice iSCardIndex:" + iSCardIndex + "    auDev.Count" + auDev.Count + "StartOpenDevice:" + auDev[0].devCh);
                    }
                }
                else
                {
                    result = -1;
                    Logger.Debug("StartOpenAudioCaptureDevice failed! Count:" + auDev.Count.ToString() + " iSCardIndex:" + iSCardIndex.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Error("StartVoice error:", ex);
            }
            return result;
        }


        public int StartOpenDevice(ClientDemo.PUCApiAdapter.mediaTermDeviceInfo device)
        {
            try
            {
                if (device.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.audioCaptureDev)//开始采集本地语言，有呼叫的时候直接将该语音发送给服务器
                {
                    int mediaTermCapAudioNsLevelint = 2;
                    string mediaTermCapAudioNsLevel = "";
                    if (!string.IsNullOrEmpty(mediaTermCapAudioNsLevel))
                    {
                        try
                        {
                            int.TryParse(mediaTermCapAudioNsLevel, out mediaTermCapAudioNsLevelint);
                        }
                        catch
                        {
                            mediaTermCapAudioNsLevelint = 2;
                        }
                    }
                    ClientDemo.PUCApiAdapter.mediaTermCapCfg pMediaTermCapCfg = new ClientDemo.PUCApiAdapter.mediaTermCapCfg();
                    pMediaTermCapCfg.mediaType = ClientDemo.PUCApiAdapter.mediaTermType.audioCaptureDev;
                    pMediaTermCapCfg.capDevCh = device.devCh;

                    //视频参数设置  ----   此处无效
                    pMediaTermCapCfg.videoType = 0;
                    pMediaTermCapCfg.videoWidth = 0;
                    pMediaTermCapCfg.videoHeight = 0;
                    pMediaTermCapCfg.videoBitRate = 0;
                    pMediaTermCapCfg.videoFrameRate = 0;
                    pMediaTermCapCfg.videoFullScreenCapFlag = 0;
                    pMediaTermCapCfg.videoScreenOffsetX = 0;
                    pMediaTermCapCfg.videoScreenOffsetY = 0;

                    //音频参数设置
                    pMediaTermCapCfg.audioType = 0;//g711a
                    pMediaTermCapCfg.audioSampingRate = 8000;//采样率
                    pMediaTermCapCfg.audioChannel = 1;
                    pMediaTermCapCfg.audioBitRate = 1411200;
                    pMediaTermCapCfg.audioNsLevel = mediaTermCapAudioNsLevelint;//降噪级别，0:不降噪(默认)，1、2、3三个等级值越大，降噪强度越大
                    pMediaTermCapCfg.audioAgcLevel = 0;//增益级别，0:不增益(默认)，1:自动增益，2以上的值为固定增益的分贝数
                    pMediaTermCapCfg.audioAecLevel = 0;//回声消除级别，0:不做回声消除(默认)，1、2、3三个等级值越大，消除强度越大
                    pMediaTermCapCfg.audioAecDelayTime = 60;//回声消除参数：回声延迟时间，要求大于等于60(默认)且为20的整数倍

                    pMediaTermCapCfg.reserved = 0;


                    IntPtr ptr = MemoryControl.StructToIntPtr(pMediaTermCapCfg);

                    if (resultAudioStartCap < 1)
                        resultAudioStartCap = MediaTerm_StartCap(ptr);

                    Logger.Debug("MediaTerm_StartCap mediaTermType.audioCaptureDev result  " + resultAudioStartCap.ToString() + "    pMediaTermCapCfg.capDevCh " + pMediaTermCapCfg.capDevCh);

                    MemoryControl.FreeHGlobalPtr(ptr);

                    //if (OnMediaCapChanged != null)
                    //{
                    //    if (resultAudioStartCap > 0)
                    //    {
                    //        isAudioCapNormal = true;
                    //        OnMediaCapChanged(1, true);
                    //       // AppConfig.GetInstance.SetAppConfigValue("MicCardNameBak", System.Text.Encoding.Default.GetString(device.name).TrimEnd('\0'));
                    //    }
                    //    else
                    //    {
                    //        isAudioCapNormal = false;
                    //        OnMediaCapChanged(1, false);
                    //    }
                    //}

                    return resultAudioStartCap;
                }
                else if (device.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.videoCaptureDev)//开始采集本地视频，有视频请求的时候直接将本地视频发送到服务器
                {
                    cameraName = System.Text.Encoding.Default.GetString(device.name).TrimEnd('\0');

                    videoMaxResolution = device.videoMaxResolution;

                    string videoMaxResolutionstr = "";
                    if (!string.IsNullOrEmpty(videoMaxResolutionstr))
                    {
                        int maxResolution = 0;
                        if (int.TryParse(videoMaxResolutionstr, out maxResolution))
                        {
                            if (videoMaxResolution > maxResolution)
                                videoEncodeFrameSize = maxResolution;
                            else
                                videoEncodeFrameSize = videoMaxResolution;
                        }
                    }
                    else
                    {
                        if (device.gpuAbility == 1)
                        {
                            videoEncodeFrameSize = videoMaxResolution;
                            //AppConfig.GetInstance.SetAppConfigValue("HeadsetVideoResolution", videoEncodeFrameSize.ToString());
                        }
                    }

                    videoEncodeProfile = device.videoMaxProfile;

                    Logger.Debug("StartOpenVideoCapturDevice videoEncodeProfile = " + videoEncodeProfile + "    videoEncodeFrameSize =" + videoEncodeFrameSize);

                    ClientDemo.PUCApiAdapter.mediaTermCapCfg pMediaTermCapCfg = new ClientDemo.PUCApiAdapter.mediaTermCapCfg();
                    pMediaTermCapCfg.mediaType = ClientDemo.PUCApiAdapter.mediaTermType.videoCaptureDev;
                    pMediaTermCapCfg.capDevCh = device.devCh;

                    //视频参数设置
                    pMediaTermCapCfg.videoType = 0;//0:h264

                    videoEncodeFrameRate = device.videoMaxFrameRate;
                    pMediaTermCapCfg.videoFrameRate = device.videoMaxFrameRate;
                    pMediaTermCapCfg.videoResolution = videoEncodeFrameSize;

                    string headsetVideoBitRateGradestr = "600";
                    int headsetVideoBitRateGradeint = 0;
                    string headsetVideoBitRateGrade = "";
                    if (string.IsNullOrEmpty(headsetVideoBitRateGrade))
                    {
                        if (videoEncodeFrameSize == VideoFrameSize.enVedioFrameSizeCIF.GetHashCode())
                            headsetVideoBitRateGrade = "7";
                        else if (videoEncodeFrameSize == VideoFrameSize.enVedioFrameSizeVGA.GetHashCode())
                            headsetVideoBitRateGrade = "6";
                        else if (videoEncodeFrameSize == VideoFrameSize.enVedioFrameSizeD1.GetHashCode())
                            headsetVideoBitRateGrade = "5";
                        else if (videoEncodeFrameSize == VideoFrameSize.enVedioFrameSize720P.GetHashCode())
                            headsetVideoBitRateGrade = "3";
                        else if (videoEncodeFrameSize == VideoFrameSize.enVedioFrameSize1080P.GetHashCode())
                            headsetVideoBitRateGrade = "1";
                        else
                            headsetVideoBitRateGrade = "7";
                        //AppConfig.GetInstance.SetAppConfigValue("HeadsetVideoBitRateGrade", headsetVideoBitRateGrade);
                    }

                    string videoBitRateStr = "";
                    char[] delimiterChars = { ',' };
                    string[] videoBitRates = videoBitRateStr.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                    if (videoBitRates.Length > 0)
                    {
                        try
                        {
                            int speakerIndex = 0;
                            if (int.TryParse(headsetVideoBitRateGrade, out speakerIndex))
                            {
                                if (speakerIndex >= 0 && videoBitRates.Length >= speakerIndex)
                                {
                                    headsetVideoBitRateGradestr = videoBitRates[speakerIndex];
                                }
                            }
                        }
                        catch
                        {
                            headsetVideoBitRateGradestr = "600";
                        }
                    }
                    int.TryParse(headsetVideoBitRateGradestr, out headsetVideoBitRateGradeint);

                    pMediaTermCapCfg.videoBitRate = headsetVideoBitRateGradeint;//比特率,默认400000

                    pMediaTermCapCfg.videoFullScreenCapFlag = 0;//屏幕采集标志默认为0，为1时屏幕采集
                    pMediaTermCapCfg.videoScreenOffsetX = 0;//屏幕采集时的起始点x坐标，默认为0
                    pMediaTermCapCfg.videoScreenOffsetY = 0;//屏幕采集时的起始点x坐标，默认为0

                    //音频参数设置  ----   此处无效
                    pMediaTermCapCfg.audioType = 0;//g711a
                    pMediaTermCapCfg.audioSampingRate = 8000;//采样率
                    pMediaTermCapCfg.audioNsLevel = 0;//降噪级别，0:不降噪(默认)，1、2、3三个等级值越大，降噪强度越大
                    pMediaTermCapCfg.audioAgcLevel = 1;//增益级别，0:不增益(默认)，1:自动增益，2以上的值为固定增益的分贝数
                    pMediaTermCapCfg.audioAecLevel = 0;//回声消除级别，0:不做回声消除(默认)，1、2、3三个等级值越大，消除强度越大
                    pMediaTermCapCfg.audioAecDelayTime = 60;//回声消除参数：回声延迟时间，要求大于等于60(默认)且为20的整数倍

                    pMediaTermCapCfg.reserved = 0;

                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(pMediaTermCapCfg));
                    Marshal.StructureToPtr(pMediaTermCapCfg, ptr, true);

                    if (resultVideoStartCap < 1)
                        resultVideoStartCap = MediaTerm_StartCap(ptr);

                    Logger.Debug("MediaTerm_StartCap mediaTermType.videoCaptureDev result  " + resultVideoStartCap.ToString()
                        + "    pMediaTermCapCfg.capDevCh " + pMediaTermCapCfg.capDevCh
                        + "    pMediaTermCapCfg.videoResolution" + pMediaTermCapCfg.videoResolution
                        + "    pMediaTermCapCfg.videoBitRate" + pMediaTermCapCfg.videoBitRate);
                    Marshal.FreeHGlobal(ptr);
                    //if (OnMediaCapChanged != null)
                    //{
                    //    if (resultVideoStartCap > 0)
                    //    {
                    //        isVideoCapNormal = true;
                    //        OnMediaCapChanged(2, true);
                    //    }
                    //    else
                    //    {
                    //        isVideoCapNormal = false;
                    //        OnMediaCapChanged(2, false);
                    //    }
                    //}

                    return resultVideoStartCap;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("StartOpenDevice", ex);
                return -1;
            }
        }


        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="capCfg"></param>
        /// <param name="lpCallBackFun"></param>
        /// <param name="appIns"></param>
        /// <returns></returns>
        delegate int MediaTerm_StartCapDelegate(IntPtr capCfg);
        public static int MediaTerm_StartCap(IntPtr capCfg)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                Logger.Debug("MediaTerm_StartCap start");
                MediaTerm_StartCapDelegate mshd = PUCApiAdapter.MediaTerm_StartCap;
                IAsyncResult ar = mshd.BeginInvoke(capCfg, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mshd.EndInvoke(ar);

                //int result = PUCApiAdapter.MediaTerm_StartCap(capCfg);
                Logger.Debug("MediaTerm_StartCap end");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StartCap", ex);
            }
            return -1;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <param name="capHnd"></param>
        /// <returns></returns>
        delegate int MediaTerm_StopHndDelegate(int capHnd);
        public static int MediaTerm_StopCap(int capHnd)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                Logger.Debug(string.Format("MediaTerm_StopCap start capHnd = {0}", capHnd));
                MediaTerm_StopHndDelegate mshd = PUCApiAdapter.MediaTerm_StopHnd;
                IAsyncResult ar = mshd.BeginInvoke(capHnd, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }

                int result = mshd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_StopHnd(capHnd);
                Logger.Debug(string.Format("MediaTerm_StopCap end capHnd = {0}", capHnd));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StopCap", ex);
            }
            return -1;
        }


        /// <summary>
        /// 开始播放
        /// </summary>
        /// <param name="playCfg"></param>
        /// <returns></returns>
        delegate int MediaTerm_StartPlayDelegate(IntPtr playCfg);
        public static int MediaTerm_StartPlay(IntPtr playCfg)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                Logger.Debug("MediaTerm_StartPlay start");
                MediaTerm_StartPlayDelegate mspd = PUCApiAdapter.MediaTerm_StartPlay;
                IAsyncResult ar = mspd.BeginInvoke(playCfg, null, null);

                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }

                int result = mspd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_StartPlay(playCfg);
                Logger.Debug(string.Format("MediaTerm_StartPlay end result = {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StartPlay", ex);
            }
            return -1;
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="playHnd"></param>
        /// <returns></returns>
        delegate int MediaTerm_StopPlayDelegate(int playHnd);
        public static int MediaTerm_StopPlay(int playHnd)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                Logger.Debug(string.Format("MediaTerm_StopPlay start playHnd = {0}", playHnd));
                MediaTerm_StopPlayDelegate mspd = PUCApiAdapter.MediaTerm_StopHnd;
                IAsyncResult ar = mspd.BeginInvoke(playHnd, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }

                int result = mspd.EndInvoke(ar);
                //int result =  PUCApiAdapter.MediaTerm_StopHnd(playHnd);
                Logger.Debug(string.Format("MediaTerm_StopPlay end playHnd = {0}", playHnd));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StopPlay", ex);
            }
            return -1;
        }

        /// <summary>
        /// 设置链接，可多次调用
        /// 可链接的方式:
        /// 采集-->播放、采集-->发送、采集-->写文件
        /// 接收-->播放、接收-->发送、接收-->写文件
        /// 读文件-->播放、读文件-->发送
        /// </summary>
        /// <param name="startHnd">开始链接点句柄</param>
        /// <param name="endHnd">结束链接点句柄</param>
        /// <returns>链接结果句柄</returns>
        delegate int MediaTerm_SetLinkDelegate(int startHnd, int endHnd);
        public static int MediaTerm_SetLink(int startHnd, int endHnd)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }
                MediaTerm_SetLinkDelegate md = PUCApiAdapter.MediaTerm_SetLink;
                Logger.Debug(string.Format("MediaTerm_SetLink start"));
                IAsyncResult ar = md.BeginInvoke(startHnd, endHnd, null, null);

                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = md.EndInvoke(ar);
                //PUCApiAdapter.MediaTerm_SetLink(startHnd, endHnd);
                Logger.Debug(string.Format("MediaTerm_SetLink end startHnd = {1} endHnd = {2} result = {0}", result, startHnd, endHnd));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_SetLink error:", ex);
            }
            return 0;
        }

        /// <summary>
        /// 开始接收
        /// </summary>
        /// <param name="ptr">接收配置参数</param>
        /// <returns>接收句柄 非0</returns>
        delegate int MediaTerm_StartRecvDelegate(IntPtr ptr);
        public static int MediaTerm_StartRecv(IntPtr ptr)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                MediaTerm_StartRecvDelegate msrd = MediaTerm_StartRecvS;
                Logger.Debug(string.Format("MediaTerm_StartRecv start"));
                IAsyncResult ar = msrd.BeginInvoke(ptr, null, null);

                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }

                int result = msrd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_StartRecv(ptr);
                Logger.Debug(string.Format("MediaTerm_StartRecv end ptr = {1} result = {0}", result, ptr));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StartRecv error:", ex);
            }
            return 0;
        }

        public static int MediaTerm_StartRecvS(IntPtr ptr)
        {
            try
            {
                Logger.Debug(string.Format("MediaTerm_StartRecvS start"));
                int result = PUCApiAdapter.MediaTerm_StartRecv(ptr);
                Logger.Debug(string.Format("MediaTerm_StartRecvS end ptr = {1} result = {0}", result, ptr));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StartRecv error:", ex);
            }
            return 0;
        }
        /// <summary>
        /// 停止接收
        /// </summary>
        /// <param name="hnd">接收句柄</param>
        /// <returns>0成功，-1失败</returns>
        delegate int MediaTerm_StopRecvDelegate(int hnd);
        public static int MediaTerm_StopRecv(int hnd)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                MediaTerm_StopRecvDelegate msrd = PUCApiAdapter.MediaTerm_StopHnd;
                Logger.Debug(string.Format("MediaTerm_StopRecv start"));
                IAsyncResult ar = msrd.BeginInvoke(hnd, null, null);
                int index = 0;
                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = msrd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_StopHnd(hnd);
                Logger.Debug(string.Format("MediaTerm_StopRecv end result = {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StopRecv error:", ex);
            }
            return 0;
        }


        /// <summary>
        /// 开始发送
        /// </summary>
        /// <param name="ptr">接收配置参数</param>
        /// <returns>接收句柄 非0</returns>
        delegate int MediaTerm_StartSendDelegate(IntPtr ptr);
        public static int MediaTerm_StartSend(IntPtr ptr)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                MediaTerm_StartSendDelegate mssd = PUCApiAdapter.MediaTerm_StartSend;
                Logger.Debug(string.Format("MediaTerm_StartSend start"));
                IAsyncResult ar = mssd.BeginInvoke(ptr, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mssd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_StartSend(ptr);
                Logger.Debug(string.Format("MediaTerm_StartSend end ptr = {1} result = {0}", result, ptr));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StartSend error:", ex);
            }
            return 0;
        }

        /// <summary>
        /// 停止发送
        /// </summary>
        /// <param name="hnd">接收句柄</param>
        /// <returns>0成功，-1失败</returns>
        delegate int MediaTerm_StopSendDelegate(int hnd);
        public static int MediaTerm_StopSend(int hnd)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }

                MediaTerm_StopSendDelegate mssd = PUCApiAdapter.MediaTerm_StopHnd;
                Logger.Debug(string.Format("MediaTerm_StopSend start"));
                IAsyncResult ar = mssd.BeginInvoke(hnd, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mssd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_StopHnd(hnd);
                Logger.Debug(string.Format("MediaTerm_StopSend end result = {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_StopSend error:", ex);
            }
            return 0;
        }


        /// <summary>
        /// 销毁库
        /// </summary>
        /// <returns></returns>
        delegate int MediaTerm_DestroyDelegate();
        public static int MediaTerm_Destroy()
        {
            try
            {

                MediaTermRuning = false;
                MediaTerm_DestroyDelegate mdd = PUCApiAdapter.MediaTerm_Destroy;
                Logger.Debug("MediaTerm_Destroy start");
                IAsyncResult ar = mdd.BeginInvoke(null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mdd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_Destroy();
                Logger.Debug(string.Format("MediaTerm_Destroy end result = {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_Destroy", ex);
            }
            return -1;
        }

        public int MediaTermDestroy()
        {
            int result= MediaTerm_Destroy();
            Logger.Debug("CallDataCenter MediaTermDestroy result:" + result);
            return result;
        }

        public string[] GetSoundCard()
        {
            lock (_lock)
            {
                List<string> outDevices = new List<string>();
                foreach (ClientDemo.PUCApiAdapter.mediaTermDeviceInfo item in mediaTermDeviceInfoList)
                {
                    if (item.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.audioPlayDev)
                    {
                        outDevices.Add(System.Text.Encoding.Default.GetString(item.name).TrimEnd('\0'));
                    }
                }
                if (outDevices.Count > 0)
                {
                    return outDevices.ToArray();
                }
                return new string[] { };
            }
        }

        public List<ClientDemo.PUCApiAdapter.mediaTermDeviceInfo> GetMediaTermDeviceInfoList()
        {
            lock (_lock)
            {
                return mediaTermDeviceInfoList;
            }
        }
        /// <summary>
        /// 获取播放句柄（默认声卡）
        /// </summary>
        /// <returns></returns>
        public int GetAudioPlayPtr()
        {
            int ptrPlay;

            string audioAgcLevel = "";
            int audioAgcLevelInt = 0;

            if (!string.IsNullOrEmpty(audioAgcLevel))
            {
                try
                {
                    audioAgcLevelInt = int.Parse(audioAgcLevel);
                }
                catch (Exception ex)
                {
                    Logger.Error("AudioAgcLevel is not number. Use default 0.", ex);
                }
            }

            string rdspeaker = "";
            int scardIndex = 0;
            string scardName = string.Empty;

            if (!string.IsNullOrEmpty(rdspeaker))
            {
                try
                {
                    scardIndex = int.Parse(rdspeaker);
                }
                catch (Exception ex)
                {
                    Logger.Error("RecordSoundCardName is not number. Use default 0.", ex);
                }
            }

            string[] soundCards = GetSoundCard();
            if (string.IsNullOrEmpty(rdspeaker))
            {
                if (soundCards.Length > 0)
                {
                    scardName = soundCards[0];
                }
            }
            else
            {
                if (soundCards.Length > 0)
                {
                    try
                    {
                        int speakerIndex = 0;
                        if (int.TryParse(rdspeaker, out speakerIndex))
                        {
                            if (speakerIndex >= 0 && soundCards.Length >= speakerIndex)
                            {
                                scardName = soundCards[speakerIndex];
                            }
                        }
                        else
                        {
                            scardName = soundCards[0];
                        }
                    }
                    catch
                    {
                        scardName = soundCards[0];
                    }
                }
            }

            ClientDemo.PUCApiAdapter.mediaTermPlayCfg _mediaTermPlayCfg = new ClientDemo.PUCApiAdapter.mediaTermPlayCfg();
            _mediaTermPlayCfg.mediaType = ClientDemo.PUCApiAdapter.mediaTermType.audioPlayDev;
            _mediaTermPlayCfg.playDevCh = GetMediaTermDeviceInfoList().Where(p => { return p.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.audioPlayDev && System.Text.Encoding.Default.GetString(p.name).TrimEnd('\0') == scardName; }).FirstOrDefault().devCh;
            _mediaTermPlayCfg.mixTempletFilePath = "Test";

            _mediaTermPlayCfg.videoHeight = 0;
            _mediaTermPlayCfg.videoType = 0;
            _mediaTermPlayCfg.videoWidth = 0;
            _mediaTermPlayCfg.videoWinHnd = 0;

            _mediaTermPlayCfg.audioVolume = Convert.ToByte(128);
            _mediaTermPlayCfg.audioPlayChannel = Convert.ToByte(1);//播放声道,0立体声(默认),1:右声道2:左声道
            _mediaTermPlayCfg.audioType = 0;

            _mediaTermPlayCfg.audioSampingRate = 8000;
            _mediaTermPlayCfg.audioChannels = 1;
            _mediaTermPlayCfg.audioNsLevel = 0;
            _mediaTermPlayCfg.audioAgcLevel = audioAgcLevelInt;
            _mediaTermPlayCfg.audioMixTempletIndex = 0;
            _mediaTermPlayCfg.reserved = 0;

            Logger.Debug("CallDataCenter mediaTermPlayCfg  _mediaTermPlayCfg.playDevCh" + _mediaTermPlayCfg.playDevCh);

            IntPtr ptr = MemoryControl.StructToIntPtr(_mediaTermPlayCfg);
            ptrPlay = MediaTerm_StartPlay(ptr);
            MemoryControl.FreeHGlobalPtr(ptr);
            Logger.Debug("CallDataCenter GetAudioPlayPtr:  ptrPlay" + ptrPlay);
            return ptrPlay;
        }

        /// <summary>
        /// 链接  接收-->播放， 采集-->发送,  接收-->发送 等过程
        /// </summary>
        /// <param name="ptrStart"></param>
        /// <param name="ptrEnd"></param>
        /// <returns></returns>
        public int StartLinkByPtr(int ptrStart, int ptrEnd)
        {
            int result;

            result = MediaTerm_SetLink(ptrStart, ptrEnd);
            Logger.Debug("CallDataCenter StartLinkByPtr:ptrStart:" + ptrStart + "   ptrEnd:" + ptrEnd + "   result:" + result);
            return result;
        }
        /// <summary>
        /// 通过cmdGuid获取通话的发送句柄
        /// </summary>
        /// <param name="cmdGuid"></param>
        /// <returns></returns>
        public int GetPtrSendBycmdGuid(string cmdGuid, string localIp, string destIp, int destPort, int sendMediaType = 2, int frameSize = 0)
        {
            int ptrSend = 0;
            lock (_lock)
            {
                if (CallManager.GetInstance().CCSession.ContainsKey(cmdGuid))
                {


                    CC cc = CallManager.GetInstance().CCSession[cmdGuid];

                    if (sendMediaType == 2)
                    {
                        if (cc == null || cc.VoicePtrSend == 0)
                        {
                            ClientDemo.PUCApiAdapter.mediaPlgTermSendCfg msc = new ClientDemo.PUCApiAdapter.mediaPlgTermSendCfg();
                            msc.localIp = localIp;//本地ip
                            msc.localPort = 0;
                            msc.destIp = destIp;
                            msc.destPort = destPort;
                            msc.localSsrc = 0;
                            msc.sendMediaType = sendMediaType;
                            msc.bindRecvChannelId = cc != null ? cc.VoicePtrRecv : 0;
                            msc.reserved = 0;
                            IntPtr ptr = MemoryControl.StructToIntPtr(msc);
                            ptrSend = MediaTerm_StartSend(ptr);
                            MemoryControl.FreeHGlobalPtr(ptr);
                            Logger.Debug("CallDataCenter GetPtrSendBycmdGuid:cmdGuid:" + cmdGuid + "   ptrSend" + ptrSend + "    VoicePtrRecv" + (cc != null ? cc.VoicePtrRecv : 0));
                        }
                        else
                        {
                            ptrSend = cc.VoicePtrSend;
                        }
                    }

                    if (sendMediaType == 1)
                    {
                        if (cc == null || cc.VideoPtrSend == 0)
                        {
                            ClientDemo.PUCApiAdapter.mediaPlgTermSendCfg msc = new ClientDemo.PUCApiAdapter.mediaPlgTermSendCfg();
                            msc.localIp = localIp;// local ip
                            msc.localPort = 0;
                            msc.destIp = destIp;
                            msc.destPort = destPort;
                            msc.localSsrc = 0;
                            msc.sendMediaType = sendMediaType;
                            msc.bindRecvChannelId = cc != null ? cc.VideoPtrRecv : 0;
                            msc.reserved = 0;

                            IntPtr ptr = MemoryControl.StructToIntPtr(msc);
                            ptrSend = MediaTerm_StartSend(ptr);
                            Logger.Debug("CallDataCenter GetPtrSendBycmdGuid:cmdGuid:" + cmdGuid + "   ptrSend" + ptrSend + "     VideoPtrRecv" + (cc != null ? cc.VideoPtrRecv : 0));

                            //ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo mpthi = new ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo();
                            //mpthi.videoResolution = videoEncodeFrameSize;
                            //mpthi.videoResolution = frameSize;
                            //mpthi.videoProfile = videoEncodeProfile;
                            //IntPtr mpthiptr = MemoryControl.StructToIntPtr(mpthi);
                            //MediaTerm_SetHndPara(ptrSend, mpthiptr);

                            ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo mpthi = new ClientDemo.PUCApiAdapter.mediaPlgTermHndInfo();
                            //mpthi.videoResolution = DataCenter.BusinessCenter.videoEncodeFrameSize;
                            if (frameSize == 4 || frameSize == 5)
                                frameSize = 3;
                            mpthi.videoResolution = frameSize > 0 ? frameSize : videoEncodeFrameSize;
                            mpthi.videoProfile = videoEncodeProfile;
                            IntPtr mpthiptr = MemoryControl.StructToIntPtr(mpthi);
                            MediaTerm_SetHndPara(ptrSend, mpthiptr);

                            Logger.Debug("CallDataCenter GetPtrSendBycmdGuid MediaTerm_SetHndPara:cmdGuid:" + cmdGuid + "   ptrSend" + ptrSend);

                            MemoryControl.FreeHGlobalPtr(mpthiptr);

                            MemoryControl.FreeHGlobalPtr(ptr);
                            Logger.Debug("CallDataCenter GetPtrSendBycmdGuid:cmdGuid:" + cmdGuid + "   ptrSend" + ptrSend);
                        }
                        else
                            ptrSend = cc.VideoPtrSend;
                    }
                }
                else
                {
                    MessageBox.Show("此路呼叫未初始化");
                }
            }
            return ptrSend;
        }

        int _initLocalport = 6677;

        public int GetVoicePtrRecvBycmdGuid(string localIp, string cmdGuid)
        {
            int ptrRecv = 0;
            ClientDemo.PUCApiAdapter.mediaPlgTermRecvCfg mrc = new ClientDemo.PUCApiAdapter.mediaPlgTermRecvCfg();
            mrc.localIp = localIp;
            mrc.localPort = 0;
            mrc.recvMediaType = 2;
            mrc.bindSendChannelId = -1;
            IntPtr ptr = MemoryControl.StructToIntPtr(mrc);
            ptrRecv = MediaTerm_StartRecv(ptr);
            MemoryControl.FreeHGlobalPtr(ptr);
            Logger.Debug("CallDataCenter GetVoicePtrRecvBycmdGuid:cmdGuid:" + cmdGuid + "   ptrRecv" + ptrRecv);
            return ptrRecv;
        }

        public int GetVedioPtrRecvBycmdGuid(string localIp, string cmdGuid)
        {
            int ptrRecv = 0;
            ClientDemo.PUCApiAdapter.mediaPlgTermRecvCfg mrc = new ClientDemo.PUCApiAdapter.mediaPlgTermRecvCfg();
            mrc.localIp = localIp;
            mrc.localPort = 0;
            mrc.recvMediaType = 1;
            mrc.bindSendChannelId = -1;
            IntPtr ptr = MemoryControl.StructToIntPtr(mrc);
            ptrRecv = MediaTerm_StartRecv(ptr);
            MemoryControl.FreeHGlobalPtr(ptr);
            Logger.Debug("CallDataCenter GetVoicePtrRecvBycmdGuid:cmdGuid:" + cmdGuid + "   ptrRecv" + ptrRecv);
            return ptrRecv;

        }



        /// <summary>
        /// 获取句柄参数(采集、播放、发送、接收(暂时只支持)、读文件、写文件、链接)
        /// </summary>
        /// <param name="hnd">句柄值</param>
        /// <returns>获取到的具体的信息</returns>
        delegate IntPtr MediaTerm_GetHndParaDelegate(int hnd);
        public static IntPtr MediaTerm_GetHndPara(int hnd)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return IntPtr.Zero;
                }

                Logger.Debug(string.Format("MediaTerm_GetHndPara start hnd = {0}", hnd));
                if (hnd != 0)
                {
                    MediaTerm_GetHndParaDelegate mtgpd = PUCApiAdapter.MediaTerm_GetHndPara;
                    IAsyncResult ar = mtgpd.BeginInvoke(hnd, null, null);
                    int index = 0;

                    while (!ar.IsCompleted)
                    {
                        if (index > 200)
                        {
                            Logger.Info("MediaTerm timeout");
                            return IntPtr.Zero;
                        }
                        else
                        {
                            Thread.Sleep(10);
                            index = index + 1;
                        }

                    }
                    IntPtr result = mtgpd.EndInvoke(ar);
                    //IntPtr result = PUCApiAdapter.MediaTerm_GetHndPara(hnd);
                    Logger.Debug(string.Format("MediaTerm_GetHndPara end hnd = {0}, result = {1}", hnd, result));
                    return result;
                }
                return new IntPtr();
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_GetHndPara error:", ex);
            }
            return new IntPtr();
        }
        #region 根据句柄关闭相关资源

        public int StopRecvByPtrRecv(int ptrRecv)
        {
            int result;
            result = MediaTerm_StopRecv(ptrRecv);
            Logger.Debug("CallDataCenter StopRecvByPtrRecv:ptrRecv:" + ptrRecv + "   result:" + result);
            return result;
        }

        public int StopPlayByPtrPlay(int ptrRecv)
        {
            int result;
            result = MediaTerm_StopPlay(ptrRecv);
            Logger.Debug("CallDataCenter StopPlayByPtrPlay:ptrPlay:" + ptrRecv + "   result:" + result);
            return result;
        }

        public int StopSendByPtrSend(int ptrRecv)
        {
            int result;
            result = MediaTerm_StopSend(ptrRecv);
            Logger.Debug("CallDataCenter StopSendByPtrSend:ptrSend:" + ptrRecv + "   result:" + result);
            return result;

        }

        public int StopLinkByPtrLink(int ptrRecv)
        {
            int result;
            result = MediaTerm_UnsetLink(ptrRecv);
            Logger.Debug("CallDataCenter StopLinkByPtrLink:ptrSend:" + ptrRecv + "   result:" + result);
            return result;
        }
        #endregion

        /// <summary>
        /// 停止链接
        /// </summary>
        /// <param name="linkHnd">接收句柄</param>
        /// <returns>0成功，-1失败</returns>
        delegate int MediaTerm_UnsetLinkDelegate(int linkHnd);
        public static int MediaTerm_UnsetLink(int linkHnd)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }
                MediaTerm_UnsetLinkDelegate mud = PUCApiAdapter.MediaTerm_UnsetLink;
                Logger.Debug(string.Format("MediaTerm_UnsetLink start"));
                IAsyncResult ar = mud.BeginInvoke(linkHnd, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mud.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_UnsetLink(linkHnd);
                Logger.Debug(string.Format("MediaTerm_UnsetLink end linkHnd = {0} result = {1}", linkHnd, result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_UnsetLink error:", ex);
            }
            return 0;
        }

        public int GetVedioPlayPtr(int pictrueboxHandle)
        {
            ClientDemo.PUCApiAdapter.mediaTermPlayCfg mtc = new ClientDemo.PUCApiAdapter.mediaTermPlayCfg();
            mtc.mediaType = ClientDemo.PUCApiAdapter.mediaTermType.videoPlayDev;
            mtc.playDevCh = GetMediaTermDeviceInfoList().Where(p => { return p.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.videoPlayDev; }).FirstOrDefault().devCh;
            mtc.videoType = 0;
            mtc.videoWinHnd = pictrueboxHandle;
            mtc.reserved = 0;
            IntPtr mtcPtr = Marshal.AllocHGlobal(Marshal.SizeOf(mtc));
            Marshal.StructureToPtr(mtc, mtcPtr, false);
            int playHandle = MediaTerm_StartPlay(mtcPtr);
            MemoryControl.FreeHGlobalPtr(mtcPtr);
            return playHandle;
        }

        public void OpenLocalCamera(int pictrueboxHandle)
        {
            ClientDemo.PUCApiAdapter.mediaTermPlayCfg mtp = new ClientDemo.PUCApiAdapter.mediaTermPlayCfg();
            mtp.mediaType = ClientDemo.PUCApiAdapter.mediaTermType.videoPlayDev;
            mtp.playDevCh = GetMediaTermDeviceInfoList().Where(p => { return p.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.videoPlayDev; }).FirstOrDefault().devCh;
            mtp.reserved = 0;
            mtp.videoWinHnd = pictrueboxHandle;
            mtp.videoType = 0;
            mtp.videoWidth = 0;
            mtp.videoHeight = 0;
            mtp.videoResolution = videoPlayFrameSize;

            IntPtr ptr = MemoryControl.StructToIntPtr(mtp);
            int playLocalHandle = MediaTerm_StartPlay(ptr);


            if (resultVideoStartCap < 1)
                StartCapVideo();

            int playLocalLinkHandle = MediaTerm_SetLink(resultVideoStartCap, playLocalHandle);
            MemoryControl.FreeHGlobalPtr(ptr);

        }
        public void StartCapVideo()
        {
            foreach (var item in mediaTermDeviceInfoList)
            {
                if (item.mediaType == ClientDemo.PUCApiAdapter.mediaTermType.videoCaptureDev)
                {
                    int resultOpenLocaCamera = StartOpenDevice(item);
                    Logger.Debug("StartOpenVideoCapturDevice result = " + resultOpenLocaCamera);
                    break;
                }
            }
        }

        delegate int MediaTerm_CtrlSendPauseDelegate(int playHnd, IntPtr ptr);
        public static int MediaTerm_CtrlSendPause(int playHnd, int iPauseFlag)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }
                Logger.Debug(string.Format("MediaTerm_CtrlPlayPause start"));
                ClientDemo.PUCApiAdapter.mediaTermCtrlCfg mtCtrlCfg = new ClientDemo.PUCApiAdapter.mediaTermCtrlCfg();
                mtCtrlCfg.ctrlType = ClientDemo.PUCApiAdapter.mediaTermCtrlType.CtrlSendPause;
                mtCtrlCfg.par1 = iPauseFlag;
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(mtCtrlCfg));
                Marshal.StructureToPtr(mtCtrlCfg, ptr, true);

                MediaTerm_CtrlSendPauseDelegate mcpd = PUCApiAdapter.MediaTerm_CtrlHnd;
                IAsyncResult ar = mcpd.BeginInvoke(playHnd, ptr, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = mcpd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_CtrlHnd(playHnd, ptr);
                Marshal.FreeHGlobal(ptr);
                Logger.Debug(string.Format("MediaTerm_CtrlSendPause end linkHnd = {0} result = {1}  iPauseFlag = {2}", playHnd, result, iPauseFlag));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_CtrlSendPause error:", ex);
            }
            return 0;
        }


        delegate int MediaTerm_SetHndParaDelegate(int setHnd, IntPtr info);
        public static int MediaTerm_SetHndPara(int setHnd, IntPtr info)
        {
            try
            {
                if (!MediaTermRuning)
                {
                    Logger.Debug("MediaTerm is stop ");
                    return 0;
                }
                MediaTerm_SetHndParaDelegate msd = PUCApiAdapter.MediaTerm_SetHndPara;
                Logger.Debug(string.Format("MediaTerm_SetHndPara start"));
                IAsyncResult ar = msd.BeginInvoke(setHnd, info, null, null);
                int index = 0;

                while (!ar.IsCompleted)
                {
                    if (index > 200)
                    {
                        Logger.Info("MediaTerm timeout");
                        return 0;
                    }
                    else
                    {
                        Thread.Sleep(10);
                        index = index + 1;
                    }

                }
                int result = msd.EndInvoke(ar);
                //int result = PUCApiAdapter.MediaTerm_SetHndPara(setHnd, info);
                Logger.Debug(string.Format("MediaTerm_SetHndPara end setHnd = {0} result = {1}", setHnd, result));
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("MediaTerm_SetHndPara error:", ex);
            }
            return 0;
        }
    }
}
