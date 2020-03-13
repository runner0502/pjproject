using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{
    public class GroupEntry : INotifyPropertyChanged
    {
        public GroupEntry()
        {
            this.SubGroupEntry = new List<GroupEntry>();
            this.PID = null;
        }
        private string guid;
        public string GUID { get { return guid; } set { guid = value; this.OnPropertyChanged("GUID"); } }

        private string pID;
        public string PID { get { return pID; } set { pID = value; this.OnPropertyChanged("PID"); } }

        private string name;
        public string Name { get { return name; } set { name = value; this.OnPropertyChanged("Name"); } }

        private string valueStr;
        public string Value { get { return valueStr; } set { valueStr = value; this.OnPropertyChanged("Value"); } }

        private string number;
        public string Number { get { return number; } set { number = value; this.OnPropertyChanged("Number"); } }

        private string number_type;
        public string Number_type { get { return number_type; } set { number_type = value; this.OnPropertyChanged("Number_type"); } }

        private string account;
        public string Account { get { return account; } set { account = value; this.OnPropertyChanged("Account"); } }

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

        List<GroupEntry> subGroupEntry;
        public List<GroupEntry> SubGroupEntry
        {
            get { return subGroupEntry; }
            set
            {
                subGroupEntry = value;
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

    //public class DeviceEntry 
    //{
    //    public string PID { get; set; }
    //    public string Name { get; set; }
    //    public string Value { get; set; }

    //    //List<DeviceEntry> nodeEntrys;
    //    //public List<DeviceEntry> SubGroupEntry
    //    //{
    //    //    get { return nodeEntrys; }
    //    //    set
    //    //    {
    //    //        nodeEntrys = value;
    //    //        this.OnPropertyChanged("DeviceEntry");
    //    //    }
    //    //}

    //    //public event PropertyChangedEventHandler PropertyChanged;
    //    //private void OnPropertyChanged(string prop)
    //    //{
    //    //    if (this.PropertyChanged != null)
    //    //        this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
    //    //    // System.Windows.MessageBox.Show(prop);
    //    //}
    //}
}
