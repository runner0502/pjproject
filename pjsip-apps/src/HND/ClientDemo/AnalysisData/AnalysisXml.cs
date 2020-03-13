using ClientDemo.model;
using Hytera.Commom.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClientDemo.AnalysisData
{

    public class AnalysisXml
    {
        static AnalysisXml _AnalysisXml;
        static object _lock = new object();
        public Dictionary<string, DeviceEntity> DeviceListDictionary;//<device_id,>
        public Dictionary<string, SystemEntity> SystemListDictionary;
        public Dictionary<string, SapEntity> SapListDictionary;
        //hash表中装载着数据内容
        private Dictionary<string, LngLat> _rectifyCoor = new Dictionary<string, LngLat>();
        Dictionary<string, string> BaseStationInfoDictionary;//基站ID和别名
        List<string> GroundOnlineForcesBaseStationList;//地面警力基站ID
        string Gps_interval_time = "20";

        public static AnalysisXml GetInstance()
        {
            lock (_lock)
            {
                if (_AnalysisXml == null)
                {
                    _AnalysisXml = new AnalysisXml();
                }
            }
            return _AnalysisXml;
        }

        private AnalysisXml()
        {

            DeviceListDictionary = new Dictionary<string, DeviceEntity>();
            SystemListDictionary = new Dictionary<string, SystemEntity>();
            SapListDictionary = new Dictionary<string, SapEntity>();
            BaseStationInfoDictionary = new Dictionary<string, string>();
            GroundOnlineForcesBaseStationList = new List<string>();
            //InitBaseStationIGps();
        }

        public Dictionary<string, DeviceEntity> GetDeviceListDictionary()
        {
            return DeviceListDictionary;
        }

        public Dictionary<string, SapEntity> GetSapListDictionary()
        {
            return SapListDictionary;
        }

        public void ReciveData(string RequestAckXML)
        {
            try
            {
                if (RequestAckXML.Contains(CommonData.GetInstance().sap_list_request_ack))
                {
                    this.AnalysisSapListXML(RequestAckXML);
                }
                if (RequestAckXML.Contains(CommonData.GetInstance().system_list_request_ack))
                {
                    this.AnalysisSystemListXML(RequestAckXML);
                }
                if (RequestAckXML.Contains(CommonData.GetInstance().device_list_request_ack))
                {
                    this.AnalysisDeviceListXML(RequestAckXML);
                }
                if (RequestAckXML.Contains(CommonData.GetInstance().get_gps_report_evt))
                {
                    this.AnalysisGpsReportEventXML(RequestAckXML);
                }
                if (RequestAckXML.Contains(CommonData.GetInstance().device_list_status_check_evt))
                {
                    this.AnalysisDeviceListStatusCheckEvtXML(RequestAckXML);
                }
                if (RequestAckXML.Contains(CommonData.GetInstance().mgr_get_uegroups_ack))
                {
                    //this.AnalysisDeviceListStatusCheckEvtXML(RequestAckXML);
                }
                if (RequestAckXML.Contains(CommonData.GetInstance().group_list_request_ack))
                {
                    //this.AnalysisDeviceListStatusCheckEvtXML(RequestAckXML);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ClearAllData()
        {
            try
            {
                DeviceListDictionary.Clear();
                SystemListDictionary.Clear();
                SapListDictionary.Clear();
                BaseStationInfoDictionary.Clear();
                GroundOnlineForcesBaseStationList.Clear();
            }
            catch (Exception ex)
            {
            }

        }

        public void AnalysisDeviceListXML(string DeviceListXML)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(DeviceListXML);
                XmlNodeList device_list = xml.ChildNodes[0].SelectSingleNode("device_list").ChildNodes;
                if (device_list != null && device_list.Count > 0)
                    foreach (XmlNode deviceInfo in device_list)
                    {
                        //XmlNode deviceInfo = deviceNode.SelectSingleNode("device");
                        string system_id = deviceInfo.SelectSingleNode("system_id").InnerText;
                        string puc_id = deviceInfo.SelectSingleNode("puc_id").InnerText;
                        string guid = deviceInfo.SelectSingleNode("guid").InnerText;
                        string device_id = deviceInfo.SelectSingleNode("device_id").InnerText;
                        string device_alias = deviceInfo.SelectSingleNode("device_alias").InnerText;
                        string staff_guid = deviceInfo.SelectSingleNode("staff_guid").InnerText;
                        string device_type = deviceInfo.SelectSingleNode("device_type").InnerText;
                        string basestation_id = "";
                        string online = "";
                        string sap_guid = "";
                        string sap_Type = "";
                        string staff_id = "";
                        string basestation_name = "";

                        XmlNode statusNode = deviceInfo.SelectSingleNode("status");
                        if (statusNode != null)
                        {
                            if (statusNode.SelectSingleNode("basestation_id") != null)
                            {
                                basestation_id = statusNode.SelectSingleNode("basestation_id").InnerText;
                            }
                            if (statusNode.SelectSingleNode("online") != null)
                            {
                                online = statusNode.SelectSingleNode("online").InnerText;
                            }
                            if (statusNode.SelectSingleNode("sap_guid") != null)
                            {
                                sap_guid = statusNode.SelectSingleNode("sap_guid").InnerText;
                            }
                        }

                        DeviceEntity device_Row = new DeviceEntity();
                        device_Row.system_id = system_id;
                        device_Row.puc_id = puc_id;
                        device_Row.guid = guid;
                        device_Row.device_id = device_id;
                        device_Row.staff_guid = staff_guid;
                        device_Row.device_alias = device_alias;
                        device_Row.device_type = device_type;
                        device_Row.basestation_id = basestation_id;
                        device_Row.online = online;
                        device_Row.sap_guid = sap_guid;
                        device_Row.sap_Type = sap_Type;
                        device_Row.staff_id = staff_id;
                        device_Row.basestation_name = basestation_name;
                        device_Row.latitude = "";
                        device_Row.longitude = "";
                        device_Row.receivedatetime = "";

                        if (!DeviceListDictionary.ContainsKey(device_id))
                            DeviceListDictionary.Add(device_id, device_Row);
                        else
                            DeviceListDictionary[device_id] = device_Row;
                    }
                SetDeviceListValue();
            }
            catch (Exception ex)
            {
            }
        }


        public void AnalysisSystemListXML(string systemListXML)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(systemListXML);
                //  XmlNodeList system_list = xml.GetElementsByTagName("system_list");
                XmlNodeList system_list = xml.ChildNodes[0].SelectSingleNode("system_list").ChildNodes;
                if (system_list != null && system_list.Count > 0)
                    foreach (XmlNode systemInfo in system_list)
                    {
                        //    XmlNode systemInfo = systemNode.SelectSingleNode("system");
                        string guid = systemInfo.SelectSingleNode("guid").InnerText;
                        string puc_id = systemInfo.SelectSingleNode("puc_id").InnerText;
                        string system_alias = systemInfo.SelectSingleNode("system_alias").InnerText;
                        string system_id = systemInfo.SelectSingleNode("system_id").InnerText;
                        string system_type = systemInfo.SelectSingleNode("system_type").InnerText;

                        SystemEntity system_Row = new SystemEntity();
                        system_Row.system_id = system_id;
                        system_Row.puc_id = puc_id;
                        system_Row.guid = guid;
                        system_Row.system_alias = system_alias;
                        system_Row.system_type = system_type;

                        if (!SystemListDictionary.ContainsKey(system_id))
                            SystemListDictionary.Add(system_id, system_Row);
                        else
                            SystemListDictionary[system_id] = system_Row;
                    }
                SetDeviceListValue();
            }
            catch (Exception ex)
            {
            }
        }


        public void AnalysisSapListXML(string sapListXML)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(sapListXML);
                //   XmlNodeList sap_list = xml.GetElementsByTagName("sap_list");
                XmlNodeList sap_list = xml.ChildNodes[0].SelectSingleNode("sap_list").ChildNodes;
                if (sap_list != null && sap_list.Count > 0)
                    foreach (XmlNode sapInfo in sap_list)
                    {
                        //    XmlNode sapInfo = sapNode.SelectSingleNode("sap");
                        string sap_guid = sapInfo.SelectSingleNode("sap_guid").InnerText;
                        string puc_id = sapInfo.SelectSingleNode("puc_id").InnerText;
                        string sap_alias = sapInfo.SelectSingleNode("sap_alias").InnerText;
                        string system_id = sapInfo.SelectSingleNode("system_id").InnerText;
                        string online = "";
                        string sap_type = sapInfo.SelectSingleNode("sap_type").InnerText;

                        XmlNode statusNode = sapInfo.SelectSingleNode("status");
                        if (statusNode != null)
                        {
                            if (statusNode.SelectSingleNode("online") != null)
                            {
                                online = statusNode.SelectSingleNode("online").InnerText;
                            }
                        }

                        SapEntity sap_Row = new SapEntity();
                        sap_Row.System_id = system_id;
                        sap_Row.Puc_id = puc_id;
                        sap_Row.Sap_guid = sap_guid;
                        sap_Row.Sap_alias = sap_alias;
                        sap_Row.Online = online;
                        sap_Row.Sap_Type = sap_type;


                        if (!SapListDictionary.ContainsKey(sap_guid))
                            SapListDictionary.Add(sap_guid, sap_Row);
                        else
                            SapListDictionary[sap_guid] = sap_Row;
                    }
                SetDeviceListValue();
            }
            catch (Exception ex)
            {
            }
        }

        public void AnalysisGpsReportEventXML(string GpsReportXML)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(GpsReportXML);
                XmlNode root = xml.SelectSingleNode("hytera");
                XmlNode targetNode = root.SelectSingleNode("target");
                string number = targetNode.SelectSingleNode("number").InnerText;
                string latitude = root.SelectSingleNode("latitude").InnerText;
                string longitude = root.SelectSingleNode("longitude").InnerText;

                if (DeviceListDictionary[number] != null)
                {
                    DeviceListDictionary[number].latitude = latitude;
                    DeviceListDictionary[number].longitude = longitude;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 更新设备上下线状态
        /// </summary>
        /// <param name="statusXML"></param>
        public void AnalysisDeviceListStatusCheckEvtXML(string statusXML)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(statusXML);
                XmlNode root = xml.SelectSingleNode("hytera");
                XmlNodeList device_list = xml.ChildNodes[0].SelectSingleNode("device_list").ChildNodes;
                string requestXML = "";
                if (device_list != null && device_list.Count > 0)

                    foreach (XmlNode deviceInfo in device_list)
                    {
                        //  XmlNode deviceInfo = deviceNode.SelectSingleNode("device");
                        string guid = deviceInfo.SelectSingleNode("device_guid").InnerText;
                        string online = "";
                        string sapguid = "";
                        string basestationId = string.Empty;
                        XmlNode statusNode = deviceInfo.SelectSingleNode("status");
                        if (statusNode != null)
                        {
                            if (statusNode.SelectSingleNode("online") != null)
                            {
                                online = statusNode.SelectSingleNode("online").InnerText;
                            }
                            if (statusNode.SelectSingleNode("sap_guid") != null)
                            {
                                sapguid = statusNode.SelectSingleNode("sap_guid").InnerText;
                            }
                            if (statusNode.SelectSingleNode("basestation_id") != null)
                            {
                                basestationId = statusNode.SelectSingleNode("basestation_id").InnerText;
                            }
                        }
                        string number = "";
                        //修改设备状态
                        foreach (DeviceEntity deviceRow in DeviceListDictionary.Values)
                        {
                            if (deviceRow.guid == guid)
                            {
                                deviceRow.online = online;
                                deviceRow.sap_guid = sapguid;
                                //获取基站定位坐标
                                GetBaseStationIGps(basestationId, deviceRow);
                                break;
                            }
                        }
                        if (DeviceListDictionary.ContainsKey(number) && DeviceListDictionary[number] != null)
                        {
                            DeviceListDictionary[number].online = online;
                        }
                    }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetBaseStationIGps(string basestationId, DeviceEntity deviceRow)
        {
            if ((_rectifyCoor != null) && (!_rectifyCoor.ContainsKey(basestationId) && !string.IsNullOrEmpty(basestationId)))
            {
                deviceRow.longitude_bs = _rectifyCoor[basestationId].Longitude.ToString();
                deviceRow.latitude_bs = _rectifyCoor[basestationId].Latitude.ToString();
            }
        }

        private bool InitBaseStationIGps()
        {
            try
            {

                string dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                dllPath = dllPath.Substring(0, dllPath.LastIndexOf("\\"));
                string sXMLPath = String.Format("{0}\\GoogleMetroConfig.xml", dllPath);
                if (System.IO.File.Exists(sXMLPath))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(sXMLPath);
                    XmlNodeList nodes = doc.SelectSingleNode("CorrectGooglePosition").ChildNodes;
                    Logger.Debug("CorrectGooglePosition count：" + nodes.Count);
                    foreach (var item in nodes)
                    {
                        if (item as XmlElement == null) continue;
                        XmlElement node = item as XmlElement;
                        string sID = node.Attributes["ID"].Value;
                        string sLongitude = node.Attributes["Longitude"].Value;
                        string sLatitude = node.Attributes["Latitude"].Value;
                        if ((_rectifyCoor != null) && (!_rectifyCoor.ContainsKey(sID) && !string.IsNullOrEmpty(sID)))
                        {
                            LngLat c_cityInfo = new LngLat();
                            c_cityInfo.Longitude = StringToDouble(sLongitude);
                            c_cityInfo.Latitude = StringToDouble(sLatitude);
                            _rectifyCoor.Add(sID, c_cityInfo);
                        }
                    }
                    return true;
                }
                else
                {
                    Logger.Debug("The exist not config file:" + sXMLPath);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 修改成本地语言设置
        /// </summary>
        /// <param name="strDouble"></param>
        /// <returns></returns>
        public double StringToDouble(string strDouble)
        {
            try
            {
                if (String.IsNullOrEmpty(strDouble))
                {
                    return 0;
                }
                else
                {
                    System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                    nfi.NumberDecimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                    return Double.Parse(strDouble, System.Globalization.NumberStyles.Any, nfi);
                }
            }
            catch
            {
                return 0;
            }
        }

        public void AnalysisBaseStationInfoXML()
        {
            try
            {
                string path = @".\GoogleMetroConfig.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(path);    //加载Xml文件  
                XmlElement rootElem = doc.DocumentElement;   //获取根节点 
                foreach (XmlNode xn in rootElem)
                {
                    try
                    {
                        if (xn.Attributes != null && xn.Attributes["ID"] != null)
                        {
                            if (!string.IsNullOrEmpty(xn.Attributes["ID"].Value))
                            {
                                if (BaseStationInfoDictionary[xn.Attributes["ID"].Value] == null)
                                    BaseStationInfoDictionary.Add(xn.Attributes["ID"].Value, xn.Attributes["Desc"].Value);
                                else
                                    BaseStationInfoDictionary[xn.Attributes["ID"].Value] = xn.Attributes["Desc"].Value;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// 给设备列表的staff_id  staff_name sap_type  basestation_name phone_num赋值
        /// </summary>
        public void SetDeviceListValue()
        {
            try
            {
                foreach (DeviceEntity deviceRow in DeviceListDictionary.Values)
                {
                    try
                    {
                        string basestation_id = deviceRow.basestation_id;
                        string staff_guid = deviceRow.staff_guid;
                        string system_id = deviceRow.system_id;
                        foreach (SapEntity sapRow in SapListDictionary.Values)
                        {
                            if (sapRow.System_id == system_id)
                            {
                                deviceRow.sap_Type = sapRow.Sap_Type;
                                break;
                            }
                        }
                        foreach (string baseStationID in BaseStationInfoDictionary.Keys)
                        {
                            if (baseStationID == basestation_id)
                            {
                                deviceRow.basestation_name = BaseStationInfoDictionary[baseStationID].ToString();
                                break;
                            }
                        }
                        if (BaseStationInfoDictionary.ContainsKey(basestation_id) && BaseStationInfoDictionary[basestation_id] == null)
                        {
                            GroundOnlineForcesBaseStationList.Add(basestation_id);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
    partial class LngLat
    {
        private double _Longitude;
        public double Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                _Longitude = value;
            }
        }

        private double _Latitude;
        public double Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                _Latitude = value;
            }
        }

    }
}
