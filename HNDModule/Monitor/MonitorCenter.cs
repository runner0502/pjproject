using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClientDemo
{
    public class MonitorCenter
    {
        private static object _Lock = new object();
        private static MonitorCenter _MonitorCenter;

        public static MonitorCenter GetInstance
        {
            get
            {
                if (_MonitorCenter == null)
                {
                    lock (_Lock)
                    {
                        if (_MonitorCenter == null)
                        {
                            _MonitorCenter = new MonitorCenter();
                        }
                    }
                }
                return _MonitorCenter;
            }
        }

        public bool SetMonitor(Monitor monitor, System.Windows.Controls.TextBox txtMessage)
        {
            string monitorLevel = string.Empty;
            if (string.IsNullOrEmpty(monitor.MonitorLevel))
            {
                monitorLevel = (GlobalCommandName.PUCMonitorLevelEvent |
                                    GlobalCommandName.PUCMonitorLevelMedia |
                                    GlobalCommandName.PUCMonitorLevelOnline |
                                    GlobalCommandName.PUCMonitorLevelSDSStatus |
                                    GlobalCommandName.PUCMonitorLevelSDSText
                                    ).ToString();
            }
            else
            {
                monitorLevel = monitor.MonitorLevel;
            }


            string xml = string.Format(@"<hytera>
  <product_name>PUC</product_name>
  <version>10</version>
  <cmd_name>mon_monitor</cmd_name>
  <cmd_guid>{0}</cmd_guid>
  <puc_id>{1}</puc_id>
  <system_id>{2}</system_id>
  <user_id>{3}</user_id>
  <monitor_level>{4}</monitor_level>
  <target>
    <number>{5}</number>
    <number_type>{6}</number_type>
  </target>
</hytera>", Guid.NewGuid(), monitor.Puc_id, monitor.SystemID, monitor.pseudo_trunking_id, monitorLevel, monitor.Number, monitor.NumberType);

            txtMessage.Text += "\n发起监听 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
            return true;
        }

        public bool CancelMonitor(Monitor monitor, System.Windows.Controls.TextBox txtMessage)
        {
            string xml = string.Format(@"<hytera>
  <product_name>PUC</product_name>
  <version>10</version>
  <cmd_name>mon_monitor</cmd_name>
  <cmd_guid>{0}</cmd_guid>
  <puc_id>{1}</puc_id>
  <system_id>{2}</system_id>
  <user_id>{3}</user_id>
  <monitor_level>{4}</monitor_level>
  <target>
    <number>{5}</number>
    <number_type>{6}</number_type>
  </target>
</hytera>", Guid.NewGuid(), monitor.Puc_id, monitor.SystemID, monitor.pseudo_trunking_id, (GlobalCommandName.PUCMonitorLevelCancelAll).ToString(), monitor.Number, monitor.NumberType);

            txtMessage.Text += "\n发起取消监听 send xml：\n";
            txtMessage.Text += FormatXml(xml);
            txtMessage.ScrollToEnd();
            IntPtr xmlPtr = Marshal.StringToHGlobalUni(xml);
            PUCApiAdapter.PUCAPI_ProcessRequest(xmlPtr);
            return true;
        }





        #region 公用方法
        /// <summary>
        /// 格式化xml字符串
        /// </summary>
        /// <param name="sUnformattedXml"></param>
        /// <returns></returns>
        private string FormatXml(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            catch (Exception e)
            {
                sb.Append("xml格式错误，" + e.Message);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }
        #endregion
    }
}
