using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModule
{
    public class Class1
    {
        DBModule.lhdbEntities1 _dbs = new DBModule.lhdbEntities1();

        public void getsome()
        {
            hnd_device device = new hnd_device();
            device.deviceId = "111";
            device.Alias = "22";
            _dbs.hnd_device.Add(device);
            _dbs.SaveChanges();
            
        }

        public void InitConfig()
        {
            hnd_params user = new hnd_params { TheKey = ConfigKey.HyteraUserName, TheValue = "anzheng" };
            _dbs.hnd_params.RemoveRange(_dbs.hnd_params);
            _dbs.hnd_params.Add(user);
            _dbs.hnd_params.Add( new hnd_params { TheKey=ConfigKey.HyteraPassword, TheValue="123456" } );
            _dbs.hnd_params.Add( new hnd_params { TheKey=ConfigKey.PUCId, TheValue="00001" } );
            //_dbs.hnd_params.Add(new hnd_params { TheKey=ConfigKey.SystemId, TheValue="001" });
            _dbs.hnd_params.Add(new hnd_params { TheKey = ConfigKey.LocalSipIP, TheValue = "20.0.0.99" });
            _dbs.hnd_params.Add(new hnd_params { TheKey=ConfigKey.LocalSipPort, TheValue="7060" });
            _dbs.hnd_params.Add( new hnd_params {TheKey=ConfigKey.MainServerIP, TheValue="20.0.0.11" } );
            _dbs.hnd_params.Add( new hnd_params { TheKey = ConfigKey.MainServerPort, TheValue="12000" } );
            _dbs.hnd_params.Add( new hnd_params { TheKey=ConfigKey.SecondServerIP, TheValue=""} );
            _dbs.hnd_params.Add( new hnd_params {TheKey=ConfigKey.SecondServerPort, TheValue = ""  } );
            _dbs.hnd_params.Add( new hnd_params {TheKey = ConfigKey.SecondSipServerIP, TheValue="" } );
            _dbs.hnd_params.Add( new hnd_params {TheKey=ConfigKey.SecondSipServerPort, TheValue="" } );
            _dbs.hnd_params.Add( new hnd_params { TheKey=ConfigKey.UseMainNATServer, TheValue = "0" } );
            _dbs.hnd_params.Add( new hnd_params { TheKey=ConfigKey.UseSecondNATServer, TheValue = "0" } );
            _dbs.SaveChanges();
        }

        static class ConfigKey
        {
            public static string HyteraUserName = "HyteraUserName";        //海能达用户名
            public static string HyteraPassword = "HyteraPassword";        //海能达密码
            public static string PUCId = "PUCId";                          //PUC服务器ID
            public static string SystemId = "SystemId";                    //系统ID
            public static string LocalSipIP = "LocalSipIP";                //本地SIP IP
            public static string LocalSipPort = "LocalSipPort";           //本地SIP 端口 
            public static string MainServerIP = "MainServerIP";          //主服务器IP
            public static string MainServerPort = "MainServerPort";      //主服务器端口
            public static string SecondServerIP = "SecondServerIP";      //备用服务器IP
            public static string SecondServerPort = "SecondServerPort";  //备用服务器端口
            public static string SecondSipServerIP = "SecondSipServerIP";      //备用SIP服务器IP
            public static string SecondSipServerPort = "SecondSipServerPort";  //备用SIP服务器端口
            public static string UseMainNATServer = "UseMainNATServer";       //是否使用主NAT服务器 0为不使用，1位使用
            public static string UseSecondNATServer = "UseSecondNATServer";   //是否使用备用NAT服务器 0为不使用，1位使用


        }
    }
}
