using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
    public class Monitor
    {
        public string Puc_id { get; set; }
        public string CmdGuid { get; set; }

        public string SystemID { get; set; }

        public string SapGuid { get; set; }
        public string pseudo_trunking_id { get; set; }

        public string DeviceGuid { get; set; }

        public string SapType { get; set; }

        public string Number { get; set; }

        public string NumberType { get; set; }

        public int Count { get; set; }

        private string _monitorLevel;

        public string MonitorLevel
        {
            get { return _monitorLevel; }
            set
            {
                if (_monitorLevel != value)
                {
                    _monitorLevel = value;
                }
            }
        }
        public string xptsite_id { get; set; }

        private bool _IsSuccess;
        public bool IsSuccess
        {
            get { return _IsSuccess; }
            set
            {
                _IsSuccess = value;
            }
        }
    }
}
