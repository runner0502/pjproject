using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
    public class SDSParam
    {
        public string _sendernumber;
        public string sendernumber
        {
            get
            {
                return this._sendernumber;
            }
            set
            {
                this._sendernumber = value;
            }
        }
        public string _sendernumberstyle;
        public string sendernumberstyle
        {
            get
            {
                return this._sendernumberstyle;
            }
            set
            {
                this._sendernumberstyle = value;
            }
        }
        public string _receivenumber;
        public string receivenumber
        {
            get
            {
                return this._receivenumber;
            }
            set
            {
                this._receivenumber = value;
            }
        }

        public string _receivenumberstyle;
        public string receivenumberstyle
        {
            get
            {
                return this._receivenumberstyle;
            }
            set
            {
                this._receivenumberstyle = value;
            }
        }
        public string _sdscontent;
        public string sdscontent
        {
            get
            {
                return this._sdscontent;
            }
            set
            {
                this._sdscontent = value;
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
        public string _sapGuid = "";
        public string SapGuid
        {
            get
            {
                return this.SapGuid;
            }
            set
            {
                this.SapGuid = value;
            }
        }
    }
}
