using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
    public class callparam
    {
        public string cmd_guid;
        public string _callernumber;
        public string callernumber
        {
            get
            {
                return this._callernumber;
            }
            set
            {
                this._callernumber = value;
            }
        }
        public string _callernumberstyle;
        public string callernumberstyle
        {
            get
            {
                return this._callernumberstyle;
            }
            set
            {
                this._callernumberstyle = value;
            }
        }
        public string _callednumber;
        public string callednumber
        {
            get
            {
                return this._callednumber;
            }
            set
            {
                this._callednumber = value;
            }
        }

        public string _callednumberstyle;
        public string callednumberstyle
        {
            get
            {
                return this._callednumberstyle;
            }
            set
            {
                this._callednumberstyle = value;
            }
        }
        private string _puc_id;
        public string PUC_ID
        {
            get
            {
                return this._puc_id;
            }
            set
            {
                this._puc_id = value;
            }
        }
        public string _systemID;
        public string systemID
        {
            get
            {
                return this._systemID;
            }
            set
            {
                this._systemID = value;
            }
        }
        public string _sapstyle;
        public string sapstyle
        {
            get
            {
                return this._sapstyle;
            }
            set
            {
                this._sapstyle = value;
            }
        }
        public string _IsDuplex;
        public string IsDuplex
        {
            get
            {
                return this._IsDuplex;
            }
            set
            {
                this._IsDuplex = value;
            }
        }
        public string _IsEncryption;
        public string IsEncryption
        {
            get
            {
                return this._IsEncryption;
            }
            set
            {
                this._IsEncryption = value;
            }
        }
        public GlobalCommandName.CallMode _CallMode;
        public GlobalCommandName.CallMode CallMode
        {
            get
            {
                return this._CallMode;
            }
            set
            {
                this._CallMode = value;
            }
        }
        private int _pictrueboxHandle;
        public int pictrueboxHandle
        {
            get
            {
                return this._pictrueboxHandle;
            }
            set
            {
                this._pictrueboxHandle = value;
            }
        }

        private bool _istransferCall;
        public bool istransferCall
        {
            get
            {
                return this._istransferCall;
            }
            set
            {
                this._istransferCall = value;
            }
        }
    }
}
