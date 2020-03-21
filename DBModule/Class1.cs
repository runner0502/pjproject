using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModule
{
    public class Class1
    {
        public void getsome()
        {
            DBModule.lhdbEntities1 dbs = new DBModule.lhdbEntities1();
            hnd_device device = new hnd_device();
            device.deviceId = "111";
            device.Alias = "22";
            dbs.hnd_device.Add(device);
            dbs.SaveChanges();
            
        }
    }
}
