using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientDemo
{
    [Serializable]
    [XmlRoot("Globalization")]
    public class LoginPara
    {
        string _LoginName = "test1";
        string _Password = "";
        string _LocolSipIP = "10.110.12.89";
        string _LocolSipPort = "5070";
        string _ServerSipIP = "10.110.12.90";
        string _ServerSipPort = "6060";
        string _ServerIP = "10.110.12.90";
        string _ServerPort = "12000";

        public string LoginName
        {
            get
            {
                return _LoginName;
            }
            set
            {
                _LoginName = value;
            }
        }
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        public string LocolSipIP
        {
            get
            {
                return _LocolSipIP;
            }
            set
            {
                _LocolSipIP = value;
            }
        }
        public string LocolSipPort
        {
            get
            {
                return _LocolSipPort;
            }
            set
            {
                _LocolSipPort = value;
            }
        }
        public string ServerSipIP
        {
            get
            {
                return _ServerSipIP;
            }
            set
            {
                _ServerSipIP = value;
            }
        }
        public string ServerSipPort
        {
            get
            {
                return _ServerSipPort;
            }
            set
            {
                _ServerSipPort = value;
            }
        }
        public string ServerIP
        {
            get
            {
                return _ServerIP;
            }
            set
            {
                _ServerIP = value;
            }
        }
        public string ServerPort
        {
            get
            {
                return _ServerPort;
            }
            set
            {
                _ServerPort = value;
            }
        }
    }
}
