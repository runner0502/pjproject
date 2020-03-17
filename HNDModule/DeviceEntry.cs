using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
    public class DeviceEntry : INotifyPropertyChanged
    {
        public DeviceEntry()
        {
            this.DeviceList = new List<DeviceEntry>();
        }
        private string guid;
        public string GUID { get { return guid; } set { guid = value; this.OnPropertyChanged("GUID"); } }

        //private string id;
        //public string ID { get { return id; } set { id = value; this.OnPropertyChanged("ID"); } }

        private string device_id;
        public string Device_id { get { return device_id; } set { device_id = value; this.OnPropertyChanged("Device_id"); } }

        private string device_alias;
        public string Device_alias { get { return device_alias; } set { device_alias = value; this.OnPropertyChanged("Device_alias"); } }

        private string device_number;
        public string Device_number { get { return device_number; } set { device_number = value; this.OnPropertyChanged("Device_number"); } }

        private string number_type;
        public string Number_type { get { return number_type; } set { number_type = value; this.OnPropertyChanged("Number_type"); } }

        private string member_guid;
        public string Nember_guid { get { return member_guid; } set { member_guid = value; this.OnPropertyChanged("Nember_guid"); } }
       
        private string systemId;
        public string SystemID
        {
            get { return systemId; }
            set
            {
                systemId = value;
                this.OnPropertyChanged("SystemID");
            }
        }

        List<DeviceEntry> deviceList;
        public List<DeviceEntry> DeviceList
        {
            get { return deviceList; }
            set
            {
                deviceList = value;
                this.OnPropertyChanged("SubGroupEntry");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
            // System.Windows.MessageBox.Show(prop);
        }
    }
}
