using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
    public static class PUCApiAdapter
    {
        #region PUCAPIInterface

        //public unsafe delegate void 
        [DllImport("PUCAPI.dll", EntryPoint = "PUCAPI_Init", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PUCAPI_Init(ref InitPUCAPIData _responseback);

        [DllImport("PUCAPI.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool PUCAPI_Start();

        [DllImport("PUCAPI.dll", EntryPoint = "PUCAPI_ProcessRequest", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static void PUCAPI_ProcessRequest(IntPtr reqXml);

        [DllImport("PUCAPI.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static void PUCAPI_Stop();

        [DllImport("PUCAPI.dll", SetLastError = true, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static void PUCAPI_Destroy();

        [DllImport("PUCAPI.dll", SetLastError = true, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool VOIP_Start(uint unBindIP,
		UInt16 usLocalPort,
		uint dwAppInstance, //应用层this指针
        VoipClientCallBackFun lpProcCallBackFun //应用层回调函数,通过回调函数向应用层返回消息
		);

        [DllImport("PUCAPI.dll", SetLastError = true, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool VOIP_SendVocData(IntPtr lpCallID, int nCodecType, IntPtr pVocData, int nVocDataLen);

        [DllImport("PUCAPI.dll", SetLastError = true, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public extern static void VOIP_Stop();
        [DllImport("PUCAPI.dll", EntryPoint = "PUCAPI_ResetInitData", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PUCAPI_ResetInitData(ref InitPUCAPIData _responseback);

        [DllImport("PUCAPI.dll", EntryPoint = "PUCAPI_SetAPILog", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PUCAPI_SetAPILog(int nLevel, int nMaxSize);
        #endregion

        #region PUCClientVocInterface
        /// <summary>
        /// 打开语音设备
        /// </summary>
        /// <param name="nMicDeviceID">MIC设备ID</param>
        /// <param name="nCodecType">编码类型</param>
        /// <param name="dwAppInstance">可以填应用层this指针</param>
        /// <param name="lpMicCallBackFun">应用层回调函数,通过回调函数向应用层返回MIC采集到的语音数据</param>
        /// <returns></returns>
         [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_OpenAudioDevice(int nMicDeviceID,VocCodecTypes nCodecType,
             uint dwAppInstance, //应用层this指针
	         MicCallBackFun lpMicCallBackFun);

         /// <summary>
         /// 关闭语音设备
         /// </summary>
        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static void VOC_CloseAudioDevice(); 
        
        /// <summary>
        /// 启动一路语音流播放
        /// </summary>
        /// <param name="strCallID"></param>
        /// <param name="nSpreakerID">播放语音的声卡ID</param>
        /// <param name="nPlayChannel">语音播放声道，0:立体声,1:右声道,2:左声道</param>
        /// <param name="nGainLevel">增益级别</param>
        /// <returns></returns>
        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_StartPlay(IntPtr strCallID, int nSpreakerID, int nPlayChannel, int nGainLevel);
        
        /// <summary>
        /// 停止一路语音流播放
        /// </summary>
        /// <param name="strCallID"></param>
        /// <returns></returns>
        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_StopPlay(IntPtr strCallID);

        /// <summary>
        /// 播放语音流数据
        /// </summary>
        /// <param name="strCallID"></param>
        /// <param name="nCodecType">语音编码类型</param>
        /// <param name="pVocData">语音数据</param>
        /// <param name="nVocDataLen">语音数据长度</param>
        /// <returns>返回true成功,false失败.</returns>
        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_Playing(IntPtr strCallID, int nCodecType, byte[] pVocData, int nVocDataLen);

        /// <summary>
        /// 播放语音流数据
        /// </summary>
        /// <param name="lpstrID"></param>
        /// <param name="pVocData">语音数据结构体指针tagVoipVoiceData</param>
        /// <returns></returns>
        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_PlayingEx(IntPtr lpstrID, IntPtr pVocData);

        /// <summary>
        /// 设置当前语音播放增益
        /// </summary>
        /// <param name="strCallID"></param>
        /// <param name="nGainLevel">增益级别</param>
        /// <returns>返回true成功,false失败.</returns>
        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_SetPlayGain(IntPtr strCallID, int nGainLevel);
        
        /// <summary>
        /// 设置当前播放语音的声道
        /// </summary>
        /// <param name="strCallID"></param>
        /// <param name="nPlayChannel">语音播放声道ID，0:表示立体声</param>
        /// <returns>返回true成功,false失败.</returns>
        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_SetPlayChannel(IntPtr strCallID, int nPlayChannel);

        [DllImport("PucClientVocLib.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int VOC_GetMicDeviceCount();

        [DllImport("PucClientVocLib.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int VOC_GetPlayDeviceCount();

        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_GetPlayDeviceName(int nID, out IntPtr strName);

        [DllImport("PucClientVocLib.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static bool VOC_GetMicDeviceName(int nID, out IntPtr strName);

        #endregion

        #region PUCAPIInterface

        public delegate void PUCCallbackEventHandler(IntPtr strPtr);
    
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct InitPUCAPIData
        {
            public PUCCallbackEventHandler OnResponse;//回调接口函数
            public IntPtr _strLocalSipIP;//本地SIP地址
            public int _nLocalSipPort;
            public IntPtr _strServerIP;//服务器IP
            public int _nServerPort;
            public IntPtr _strServerIP2;//服务器IP2 双击热备
            public int _nServerPort2;
            public IntPtr _strLocalIP;//本地IP地址
            public IntPtr _strPUCID;
            public int _bOverNat1;//是否跨网段连接服务器
            public int _bOverNat2;//是否跨网段连接服务器
            public int _nPropertyCount;
            public IntPtr _property;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct InitPucApiDataProperty
        {
            public IntPtr _strModule;
            public IntPtr _configname;
            public IntPtr _configValue;
        }
        public enum CallBackMsg_Enum
        {
            CBACK_CLIENTMSG_VOCDATA = 0,//接收VOC数据消息

            CBACK_CLIENTMSG_VOCRECV_START = 1,//单工，开始接收语音流
            CBACK_CLIENTMSG_VOCSEND_START = 2,//单工，开始发送语音流
            CBACK_CLIENTMSG_VOCDUPLEX_START = 3,//双工，开始双工语音流传输
            CBACK_CLIENTMSG_VOICEOVER = 4,//语音流传输结束

            CBACK_CLIENTMSG_MAX
        }


        public enum VocCodecTypes
        {
            CODECTYPE_G711U = 0,
            CODECTYPE_G711A = 8,
            CODETYPE_H264RTP_PL = 98, 		//Mark = false
            CODECYPE_H264RTP_PLEX = 99,   //Mark = true
            CODECYPE_H264RTP_PACK = 100, //rtp包，freecomm
            CODECYPE_H264_FRAME = 101//视频H264类型（98+512），帧数据
        };


        public delegate uint VoipClientCallBackFun(CallBackMsg_Enum uMsg, //回调函数返回的消息,定义如CallBackMsg_Enum
            uint dwAppInstance,//应用层This指针,可以为空
            IntPtr lpCallID, //呼叫ID
            IntPtr pVocWaveBuf  //数据,当uMsg=CBACK_CLIENTMSG_VOCDATA该参数有效，数据结构定义如_tagVoipVoiceData
            );

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct tagVoipVoiceData
        {
            public byte codec; //编码类型,0xff 表示为wav格式
            public Int32 nLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 960)]
            public byte[] DataBuf;
        }


        #endregion

        #region PUCClientVocInterface

        /// <summary>
        /// 本地语音采集及发送回调函数
        /// </summary>
        /// <param name="uMsg">暂未使用</param>
        /// <param name="nCodecType">编码类型</param>
        /// <param name="nLen">语音数据长度</param>
        /// <param name="pSrcVocWaveBuf">语音数据</param>
        /// <param name="dwAppInstance">应用层this指针，可为null</param>
        /// <returns></returns>
        public delegate uint MicCallBackFun(uint uMsg, int nCodecType, int nLen, IntPtr pSrcVocWaveBuf, uint dwAppInstance);

        /// <summary>
        /// 响应接收到流数据功能函数
        /// </summary>
        /// <param name="buffer">流的缓存区</param>
        /// <param name="nBufLen">流的长度</param>
        /// <param name="nWndHandle">播放窗口的句柄</param>
        /// <param name="nStreamType">流的类型 0为rtp流, 1为h264流 ,默认为rtp流</param>
        /// <returns></returns>
        [DllImport("PlayDll\\PlayDll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public extern static int PlayOnRecvData(IntPtr buffer, int nBufLen, int nWndHandle, int nStreamType);

        public delegate int OnGetStreamWidthHeight
        (
            int nWidth,
            int nHeight,
            int nWndHandle
    );
        #endregion

        #region 音视频接口 MediaTermLib.dll

        //初始化库
        //输入参数saveLog: 保存日志标志
        //返回值：0成功，-1失败
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_Init", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_Init(IntPtr ptr, MediaTermEventReportCallBackFun eventCallBack, Int64 appIns);

        //查询设备数量
        //返回值：可用的设备个数，小于0为错误码
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_QueryDevNum", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_QueryDevNum();

        //查询设备信息
        //输入参数index: 设备的序号，从0开始
        //返回值：一个设备的设备信息
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_GetDevInfo", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr MediaTerm_GetDevInfo(int index);

        //停止句柄
        //输入参数hnd: 句柄值(采集、播放、发送、接手、写文件、读文件)
        //返回值：0成功，其他错误码
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_StopHnd", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_StopHnd(int playHnd);

        //开始播放
        //输入参数playCfg: mediaTermPlayCfg 播放配置参数
        //返回值：播放句柄  控制音量   左右声道  呼叫
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_StartPlay", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static int MediaTerm_StartPlay(IntPtr playCfg);

        //开始采集
        //输入参数capCfg: mediaTermCapCfg 采集配置参数
        //输入参数lpCallBackFun: 采集回调函数
        //输入参数appIns: 用户数据
        //返回值：采集句柄
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_StartCap", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_StartCap(IntPtr capCfg);

        //开始发送
        //输入参数sendCfg: mediaPlgTermSendCfg发送配置参数
        //返回值：发送句柄 非0
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_StartSend", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_StartSend(IntPtr sendCfg);

        //开始接收
        //输入参数recvCfg: mediaPlgTermRecvCfg接收配置参数
        //返回值：接收句柄 非0
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_StartRecv", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_StartRecv(IntPtr recvCfg);

        //开始操作文件
        //输入参数fileCfg: mediaPlgTermOpFileCfg文件配置参数
        //返回值：文件句柄 非0
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_StartOpFile", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_StartOpFile(IntPtr fileCfg);

        //设置链接，可多次调用
        //输入参数startHnd: 开始链接点句柄
        //输入参数endHnd: 结束链接点句柄
        //返回值：链接结果句柄
        //可链接的方式:
        //采集-->播放、采集-->发送、采集-->写文件
        //接收-->播放、接收-->发送、接收-->写文件
        //读文件-->播放、读文件-->发送
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_SetLink", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_SetLink(int startHnd, int endHnd);

        //停止链接
        //输入参数linkHnd: 链接结果句柄
        //返回值：0成功，-1失败
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_UnsetLink", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_UnsetLink(int linkHnd);

        //---------------------------------------------------------------------------------------------------------
        //设置句柄参数(采集、播放、发送、接收、读文件、写文件、链接)暂未用
        //输入参数hnd: 句柄值
        //输入数参info:需要设置的信息
        //返回值：0成功，-1失败
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_SetHndPara", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_SetHndPara(int hnd, IntPtr info);

        //获取句柄参数(采集、播放、发送、接收(暂时只支持)、读文件、写文件、链接)
        //输入参数hnd: 句柄值
        //返回值：获取到的具体的信息
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_GetHndPara", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr MediaTerm_GetHndPara(int hnd);

        //控制句柄
        //输入参数hnd: 句柄值(播放)
        //输入参数ctrlCfg：控制参数
        //返回值：0成功，其他错误码
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_CtrlHnd", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_CtrlHnd(int hnd, IntPtr ctrlCfg);

        //销毁库
        //返回值：0成功，-1失败
        [DllImport(@"Media\MediaTermLibV2.dll", EntryPoint = "MediaTermV2_Destroy", CallingConvention = CallingConvention.Cdecl)]
        public extern static int MediaTerm_Destroy();
        #endregion

        #region  音视频接口枚举和结构体

        //获取播放媒体的宽高
        public delegate int GetStreamWidthHeight(int nWidth, int nHeight, int nWndHandle);

        //媒体设备信息回调
        public delegate int MediaTermDeviceInfoCallBackFun(IntPtr mediaTermDeviceInfo, Int64 appIns);

        //媒体事件信息回调
        public delegate int MediaTermEventReportCallBackFun(int handle, IntPtr mediaTermDeviceInfo, Int64 appIns);

        //媒体数据信息回调
        public delegate int MediaTermDataBackFunCallBackFun(int handle, IntPtr pBuffer, int bufferLen, MediaTermBufferType MediaTermBufferType, Int64 appIns);

        public delegate int MediaPlgTermFileInfoCallBackFun(IntPtr fileInfo, Int64 appIns);


        public enum mediaTermType
        {//设备媒体类型
            videoCaptureDev, //操作视频采集设备
            videoPlayDev,    //操作视频播放设备
            audioCaptureDev, //操作音频采集设备
            audioPlayDev,    //操作音频播放设备

            screenCapSoftDev,//屏幕采集 
            loudSpeakerCapSoftDev,//扬声器采集
        };

        public enum mediaTermDataType
        {
            MEDIA_FRAME_DATA,//帧数据数据
            MEDIA_RTP_WITH_HEAD_DATA,//包含rtp头的数据
            MEDIA_RTP_WITHOUT_HEAD_DATA,//不包含rtp头的数据
        };

        public enum MediaTermBufferType
        {
            //帧的类型
            MEDIA_BUFFER_UNKOW = 0,//不清楚
            MEDIA_FRAME_G711ADATA = 1,		//g711a语音帧数据
            MEDIA_FRAME_H264DATA = 2,		//H264帧数据

            //包的类型，用于填充rtp头部的M位与timestamp
            MEDIA_RTP_G711A_PACKET = 3,  //g711a语音包数据，一帧全部在一个包中，rtp的markbite填写为1，timestamp增加
            // MEDIA_RTP_SPS_PACKET = 4,		//h264帧的SPS包，一帧全部在一个包中，rtp的markbite填写为1，timestamp增加
            // MEDIA_RTP_PPS_PACKET = 5,		//h264帧的PPS包，一帧全部在一个包中，rtp的markbite填写为1，timestamp增加
            MEDIA_RTP_SINGLE_PACKET = 4,//拆h264帧时，一帧全部在一个包中，rtp的markbite填写为1，timestamp增加
            MEDIA_RTP_FIRST_PACKET = 5,//拆h264帧时，第一包，rtp的markbite填写为0，timestamp增加
            MEDIA_RTP_MIDLE_PACKET = 6,//拆h264帧时，中间包，rtp的markbite填写为0，timestamp不增加
            MEDIA_RTP_LAST_PACKET = 7,//拆h264帧时，最后一包，rtp的markbite填写为1，timestamp不增加
        };

        public enum mediaTermEventType
        {
            MEDIA_EVENT_CAPTURE,//采集事件
            MEDIA_EVENT_PLAY,//播放事件
            MEDIA_EVENT_SENDFILE,//发送文件事件
            MEDIA_EVENT_SAVEFILE,//保存文件事件
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct mediaTermDeviceInfo
        {//媒体输入输出设备信息--服务器返回事件
            public mediaTermType mediaType; //媒体类型 
            public int devCh;              //编号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] name; //名称,gb2312编码  System.Text.Encoding.Default.GetString(mediaTermDeviceInfoTemp.name).TrimEnd('\0')
            public int videoMaxWidth; //视频采集设备最大采集宽度 add
            public int videoMaxHeight; //视频采集设备最大采集高度 add
            public int videoMaxFrameRate;//视频采集设备最大帧率 add
            public int videoMaxProfile;//视频采集设备最大profile 1:base 2:main 3:high
            public int videoMaxResolution;//0:QCIF,1:QVGA,2:CIF,3:VGA,4:4CIF,5:D1,6:720P,7:1080P

            public int audioMaxSampleRate;//音频采集设备最大采样率 add
            public int audioMaxChanel;//音频采集设备最大声道 add    
            public int gpuAbility;//gpu编解码能力:0:没有gpu编解码能力,1:有gpu编解码能力 
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct mediaTermInitCfg
        {//初始化配置信息
            public int saveLog;//保持日志标志，0:不保存(默认) 1:保存
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string localIpAddr;
            public MediaTermEventReportCallBackFun eventCallBack;
            public Int64 appIns;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct mediaTermEventInfo
        {//媒体事件信息--服务器返回事件
            public int type;//类型，1:采集事件，2:播放事件，3:接收事件
            public int subType;//子类型，1:设备列表变化，2:视频设备异常，3:音频设备异常，4:接收事件超时，5:发送事件超时
            public int detailId;//具体id，设备异常时为设备编号id，接收或发送事件超时为接收或发送id
            public int reserve;
        };

        public enum mediaTermCtrlType
        {//改变类型
            CtrlAudioPlayVolume, //更改音频播放音量
            CtrlAudioPlayChannel,    //更改音频播放声道
            CtrlVideoPlayAngle, //更改视频播放角度
            CtrlVideoPlayWindowSize,    //更改视频播放窗口大小
            CtrlPlayPause,  //更改播放暂停恢复播放
            CtrlVideoPlayFullWindow, //更改视频播放铺满窗口	
            CtrlSendPause,  //更改发送暂停恢复发送
            CtrlRecvPause	//更改接收暂停恢复接收	
        };

        public struct mediaTermCtrlCfg
        {//媒体句柄控制
            public mediaTermCtrlType ctrlType; //控制类型 
            public int par1;//控制参数1，取值范围如下
            //音量volume：0-128;
            //声道channel:0立体声(默认),1:右声道2:左声道
            //角度angle:画面顺时钟旋转角度0 / 90 / 180 / 270
            //窗口大小width:窗口宽
            //暂停pauseFlag:暂停标志//0:运行(默认) 1:暂停

            public int par2;//控制参数2，取值范围如下
            //height:窗口高

            public int par3;//控制参数3
            public int reserved;
        };

        //------------采集配置------------------------------------------
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct mediaTermCapCfg
        {
            //-------通用配置-----------
            public mediaTermType mediaType; //媒体类型
            public int capDevCh;//采集设备编号

            //------视频采集配置-----------------
            public int videoType;//0:h264(默认)
            public int videoWidth;        //宽度 默认(352),屏幕范围采集时宽度
            public int videoHeight;       //高度 默认(288),屏幕范围采集时高度
            public int videoBitRate;     //比特率,默认400000
            public int videoFrameRate;    //帧率(25/30)默认25 
            public int videoResolution;//0:QCIF,1:QVGA,2:CIF,3:VGA,4:4CIF,5:D1,6:720P,7:1080P

            public int videoFullScreenCapFlag;//屏幕全屏幕采集标志默认为0,为1时屏幕采集   add 
            public int videoScreenOffsetX;//屏幕采集时的起始点x坐标，默认为0
            public int videoScreenOffsetY;//屏幕采集时的起始点x坐标，默认为0
            public Int64 videoScreenCapWindHnd; //屏幕窗口采集时采集时的屏幕窗口句柄，默认为空 add

            //----------音频采集配置-----------------
            public int audioType;//0:g711a(默认)
            public int audioSampingRate; //采样率，默认8KHz
            public int audioChannel;//声道数目，默认单声道，1:单声道，2:双声道   add
            public int audioBitRate;     //比特率,默认1411200	
            public int audioNsLevel;//降噪级别，0:不降噪(默认)，1、2、3三个等级值越大，降噪强度越大
            public int audioAgcLevel;//增益级别，0:不增益(默认)，1:自动增益，2以上的值为固定增益的分贝数
            public int audioAecLevel;//回声消除级别，0:不做回声消除(默认)，1、2、3三个等级值越大，消除强度越大
            public int audioAecDelayTime;//回声消除参数：回声延迟时间，要求大于等于60(默认)且为20的整数倍

            //-------------其他数据配置-----------
            public int reserved;
        };

        //--------------------播放配置----------------------------------
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct mediaTermPlayCfg
        {//媒体播放配置
            //-------通用配置-----------
            public mediaTermType mediaType; //媒体类型
            public int playDevCh;//播放设备编号

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string mixTempletFilePath;


            //---视频播放配置-----------------------
            public Int64 videoWinHnd;  //窗口句柄
            public int videoType;//0:h264(默认)
            public int videoWidth;        //宽度 默认(352)
            public int videoHeight;       //高度 默认(288)
            public int videoResolution;//0:QCIF,1:QVGA,2:CIF,3:VGA,4:4CIF,5:D1,6:720P,7:1080P

            public int videoJoinTempletIndex;     //融屏模板编号
            public int videoJoinSubWinPos;    //融屏时的子位置号
            public int videoFullWindow;//视频播放是否铺满窗口，0:不铺满(默认)，按窗口宽高和视频流宽高比列播放，1:铺满

            //-------音频播放配置-------
            public int audioVolume;//音量(0-128) 默认128
            public int audioPlayChannel;//播放声道,0立体声(默认),1:右声道2:左声道

            public int audioType;//0:g711a(默认)
            public int audioChannels;//声音流的声道数目，1:单声道声音流，2:双声道声音流  //add
            public int audioSampingRate; //采样率，默认8KHz
            public int audioNsLevel;//降噪级别，0:不降噪(默认)，1、2、3三个等级值越大，降噪强度越大
            public int audioAgcLevel;//增益级别，0:不增益(默认)，1:自动增益，2以上的值为固定增益的分贝数
            public int audioMixTempletIndex;//混音模板编号

            //-------------其他数据配置-----------
            public int reserved;
        };

        //--------------------发送配置----------------------------------
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct mediaPlgTermSendCfg
        {//媒体发送配置

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string localIp;//本地IP
            public int localPort;//本地端口，如果填写为0,则库内部管理端口

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string destIp;//目的IP地址,可以是组播地址
            public int destPort;//目的端口,可以是组播端口

            public int localSsrc;//本地标识
            public int sendMediaType;//发送媒体类型,1:视频，2:音频

            public int bindRecvChannelId;//绑定接收通道Id，默认-1
            public int reserved;
        };

        //--------------------接收配置----------------------------------
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct mediaPlgTermRecvCfg
        {//媒体接收配置

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string localIp;//本地IP，可以是组播地址
            public int localPort;//本地端口，如果填写为0,则库内部管理端口

            public int destSsrc;//远端标识
            public int recvMediaType;//发送媒体类型,1:视频，2:音频

            public int bindSendChannelId;//绑定发送通道Id，默认-1
            public int reserved;
        };

        //--------------------操作文件配置----------------------------------
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct mediaPlgTermFileCfg
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string fileName;//文件名称，不包含路径
            public int fileType;//文件媒体类型,1:视频，2:音频
            public int fileSize;//文件大小
            public int fileStartTime;//文件起始时间
            public int fileEndTime;//文件结束时间
            public int reserved;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct mediaPlgTermOpFileCfg
        {//操作文件
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string fileName;//文件名称，不包含路径
            public int operateType;//操作类型，1:录制，2:回放
            public int operateMediaType;//文件媒体类型,1:视频，2:音频
            public int reserved;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct mediaPlgTermQueryFileCfg
        {//查询文件条件	
            public int reserved;
        };

        //--------------------句柄信息结构----------------------------------
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct mediaPlgTermHndInfo
        {//句柄信息，可扩展
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string ipAddr;
            public int localPort;

            public int videoType;//0:h264(默认)
            public int videoWidth;        //宽度 默认(352)
            public int videoHeight;       //高度 默认(288)
            public int videoFrameRate;//帧率 默认(25)
            public int videoProfile; //profile 1:(默认)base 2:main 3:high
            public int videoResolution;//0:QCIF,1:QVGA,2:CIF,3:VGA,4:4CIF,5:D1,6:720P,7:1080P

            public int audioType;//0:g711a(默认)
            public int audioChannels;//声音流的声道数目，1:单声道声音流，2:双声道声音流  
            public int audioSampingRate; //采样率，默认8KHz
            public int audioPacketTime;//包时长，默认60ms

            public int reserved;
        };

        //--------------------本地文件配置--------------------
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public struct mediaTermLocalFileCfg
        {
            public mediaTermType mediaType; //媒体类型
            public mediaTermDataType fileDataType;//获取帧还是包，默认不带rtp包头
            //   char fileName[256];//本地文件名称
            // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            //  public char[] name; //本地文件名称 System.Text.Encoding.Default.GetString(mediaTermDeviceInfoTemp.name).TrimEnd('\0')
            // public string name;
            // public byte[] name;
            //[MarshalAs(UnmanagedType.ByValTStr,SizeConst=256)]
            //public string name;
            public int nFrameRate;//发送本地文件时的帧率
        };

        #endregion
    }
    
}
