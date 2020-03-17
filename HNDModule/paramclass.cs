using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
   public class paramclass
    {
        public string _user_name;
        public string user_name
        {
            get
            {
                return this._user_name;
            }
            set
            {
                this._user_name = value;
            }
        }
        public string _user_password;
        public string user_password
        {
            get
            {
                return this._user_password;
            }
            set
            {
                this._user_password = value;
            }
        }
        private string _puc_Id;
        public string PUC_ID
        {
            get {
                return this._puc_Id;
            }
            set
            {
                this._puc_Id = value;
            }
        }
        public string _localSipIP;
        public string LocalSipIP
        {
            get
            {
                return this._localSipIP;
            }
            set
            {
                this._localSipIP = value;
            }
        }

        public string _IP2;
        public string IP2
        {
            get
            {
                return this._IP2;
            }
            set
            {
                this._IP2 = value;
            }
        }
        public string _IP3;
        public string IP3
        {
            get
            {
                return this._IP3;
            }
            set
            {
                this._IP3 = value;
            }
        }
        public string _IP4;
        public string IP4
        {
            get
            {
                return this._IP4;
            }
            set
            {
                this._IP4 = value;
            }
        }

        public string _serverIP;
        public string ServerIP
        {
            get
            {
                return this._serverIP;
            }
            set
            {
                this._serverIP = value;
            }
        }

        public string _serverPort;
        public string ServerPort
        {
            get
            {
                return this._serverPort;
            }
            set
            {
                this._serverPort = value;
            }
        }

        public string _serverSipIP;
        public string ServerSipIP
        {
            get
            {
                return this._serverSipIP;
            }
            set
            {
                this._serverSipIP = value;
            }
        }

        public string _serverSipPort;
        public string ServerSipPort
        {
            get
            {
                return this._serverSipPort;
            }
            set
            {
                this._serverSipPort = value;
            }
        }

        public string _localSipPort;
        public string LocalSipPort
        {
            get
            {
                return this._localSipPort;
            }
            set
            {
                this._localSipPort = value;
            }
        }

        public string _ID2;
        public string ID2
        {
            get
            {
                return this._ID2;
            }
            set
            {
                this._ID2 = value;
            }
        }
        public string _ID3;
        public string ID3
        {
            get
            {
                return this._ID3;
            }
            set
            {
                this._ID3 = value;
            }
        }

        public string _ID4;
        public string ID4
        {
            get
            {
                return this._ID4;
            }
            set
            {
                this._ID4 = value;
            }
        }

        private bool _bOverNat1;
        private bool _bOverNat2;

        public bool BOverNat2
        {
            get { return _bOverNat2; }
            set { _bOverNat2 = value; }
        }

        public bool BOverNat1
        {
            get { return _bOverNat1; }
            set { _bOverNat1 = value; }
        }
    }

   public class VideoData
   {
       /// <summary>
       /// 呼叫callid
       /// </summary>
       public string callId;

       /// <summary>
       /// 上拉视频显示窗口句柄
       /// </summary>
       public int nHwnd;

       /// <summary>
       /// 预览视频显示窗口句柄
       /// </summary>
       public int nHwndMe;

       /// <summary>
       /// 是否需要转发当前视频
       /// </summary>
       public bool bIsTransfer;

       /// <summary>
       /// 转发视频时第一路呼叫的callid
       /// </summary>
       public string sourceCallId;
   }
}
