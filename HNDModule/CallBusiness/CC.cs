using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo.CallBusiness
{
    public class CC
    {
        public string CallID { get; set; }
        public string LocalIp { get; set; }

        public int LocalPort { get; set; }

        public string DestIp { get; set; }

        public int DestPort { get; set; }

        public bool IsDuplex { get;set; }

        public GlobalCommandName.CallMode CallMode { get; set; }

        /// <summary>
        /// 接收句柄
        /// </summary>
        public int VoicePtrRecv { get; set; }

        public int VoicePtrSendSip { get; set; }

        public int VoiceSendSipPort { get; set; }

        /// <summary>
        /// 播放句柄
        /// </summary>
        public int VoicePtrPlay { get; set; }

        /// <summary>
        /// 发送句柄
        /// </summary>
        public int VoicePtrSend { get; set; }

        /// <summary>
        /// 发送链接句柄
        /// </summary>
        public int VoicePtrLinkSend { get; set; }

        /// <summary>
        /// 播放链接句柄
        /// </summary>
        public int VoicePtrLinkPlay { get; set; }


        /// <summary>
        /// 接收句柄
        /// </summary>
        public int VideoPtrRecv { get; set; }

        /// <summary>
        /// 发送句柄
        /// </summary>
        public int VideoPtrSend { get; set; }

        /// <summary>
        /// 发送链接句柄
        /// </summary>
        public int VideoPtrLinkSend { get; set; }

        /// <summary>
        /// 播放链接句柄
        /// </summary>
        public int VideoPtrLinkPlay { get; set; }


        /// <summary>
        /// 本地播放链接句柄
        /// </summary>
        public int VideoPtrLinkLocalPlay { get; set; }


        /// <summary>
        /// 播放句柄
        /// </summary>
        public int VideoPtrPlay { get; set; }

        /// <summary>
        /// 视频窗口句柄1
        /// </summary>
        public int pictrueboxHandle { get; set; }
        public Media Media { get; set; }
    }
}
