using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientDemo.CallBusiness
{
    public class Media
    {
        public AudioEntity Audio
        {
            get;
            set;
        }
        
        public AudioEntity Video
        {
            get;
            set;
        }
    }
    public class AudioEntity
    {
        public string Rtp_local_ip
        {
            get;
            set;
        }

        public string Rtp_local_port
        {
            get;
            set;
        }

        public string Codec
        {
            get;
            set;
        }

        public string FrameRate
        {
            get;
            set;
        }
        
        public string FrameSize
        {
            get;
            set;
        }
        
        public string Rtp_remote_ip
        {
            get;
            set;
        }
       
        public string Rtp_remote_port
        {
            get;
            set;
        }

        public string Video_Max_Profile
        {
            get;
            set;
        }

        public string Video_Real_Profile
        {
            get;
            set;
        }
    }
}
