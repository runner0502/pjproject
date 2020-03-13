using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace ClientDemo
{
    public class CommonData
    {
        static object _lock= new object();
        static CommonData _CommonData;
        List<string> reportList = new List<string>();

        public string CurrDispatch { get; set; }

        #region Sap_Type
        /// <summary>
        /// Hytera PDT数字集群系统:5
        /// </summary>
        public const string SAPType_HYTERA_DMR3_SSI = "5";

        /// <summary>
        /// 公共电话系统:9
        /// </summary>
        public const string SAPType_IPPBX = "9";

        /// <summary>
        /// 调度台之间的呼叫:13
        /// </summary>
        public const string SAPType_HYTERA_Intercom = "13";
        #endregion

        #region NumberType
        /// <summary>
        /// 个号:0
        /// </summary>
        public const string NUMBERTYPE_INDIVIDUAL = "0";

        /// <summary>
        /// 组号:1
        /// </summary>
        public const string NUMBERTYPE_GROUP = "1";

        /// <summary>
        /// 外线:2
        /// </summary>
        public const string NUMBERTYPE_EXTERNAL = "2";

        /// <summary>
        /// 调度台用户:7
        /// </summary>
        public const string NUMBERTYPE_DISPATCHER = "7";
        #endregion

        public static CommonData GetInstance()
        { 
            lock(_lock)
            {
                if (_CommonData == null)
                {
                    _CommonData = new CommonData();
                }
            }
            return _CommonData;
        }

        public CommonData()
        {
            reportList.Add(organization_list_request_ack);
            reportList.Add(staff_list_request_ack);
            reportList.Add(device_list_request_ack);
            reportList.Add(group_list_request_ack);
            reportList.Add(system_list_request_ack);
            reportList.Add(gps_get_gps_info_ack);
            reportList.Add(sap_list_request_ack);
            reportList.Add(gps_batch_start_gps_info_report_ack);
            reportList.Add(gps_batch_stop_gps_info_report_ack);
            reportList.Add(gps_stop_gps_info_report_ack);
            reportList.Add(get_gps_report_evt);
            reportList.Add(gps_record_query_ack);
            reportList.Add(sds_text_send_ack);
            reportList.Add(sds_status_receive_evt);
            reportList.Add(sds_status_list_request_ack);
            reportList.Add(edit_dgna_ack);
            reportList.Add(edit_dgna_member_ack);
            reportList.Add(update_organization_info);
            reportList.Add(update_staff_info);
            reportList.Add(update_device);
            reportList.Add(update_group_info);
            reportList.Add(device_list_status_check_evt);
            reportList.Add(sds_text_receive_evt);
        }


        public List<string> AckList
        {
            get { return  reportList; }
        }

        #region  protocol
        public string puc_login = "puc_login";
        public string puc_logout = "puc_logout";
        public string puc_login_ack = "puc_login_ack";
        public string puc_logout_ack = "puc_logout_ack";
        public string successResult = "<result>0</result>";
        public string puc_disconnect_evt = "puc_disconnect_evt";
        public string device_list_status_check_evt = "device_list_status_check_evt";
        public string gps_get_gps_info = "gps_get_gps_info";
        public string gps_start_gps_info_report = "gps_start_gps_info_report";
        public string gps_stop_gps_info_report = "gps_stop_gps_info_report";
        public string gps_quick_start = "gps_quick_start";
        public string gps_quick_stop = "gps_quick_stop";
        public string gps_record_query = "gps_record_query";
        public string ps_query_gps_subscribeinfo_count = "ps_query_gps_subscribeinfo_count";
        public string gps_start_gps_info_report_list = "gps_start_gps_info_report_list";
        public string gps_batch_start_gps_info_report = "gps_batch_start_gps_info_report";
        public string gps_batch_stop_gps_info_report = "gps_batch_stop_gps_info_report";
        public string sds_send_text = "sds_send_text";
        public string sds_send_status = "sds_send_status";
        public string predefined_sds_request = "predefined_sds_request";
        public string add_sds_info = "add_sds_info";
        public string delete_sds_info = "delete_sds_info";
        public string update_sds_info = "update_sds_info";
        public string sds_status_list_request = "sds_status_list_request";
        public string add_sds_status = "add_sds_status";
        public string organization_list_request = "organization_list_request";
        public string add_organization_info = "add_organization_info";
        public string delete_organization_info = "delete_organization_info";
        public string update_organization_info = "update_organization_info";
        public string staff_list_request = "staff_list_request";
        public string add_staff_info = "add_staff_info";
        public string delete_staff_info = "delete_staff_info";
        public string update_staff_info = "update_staff_info";
        public string device_list_request = "device_list_request";
        public string add_device = "add_device";
        public string delete_device = "delete_device";
        public string update_device = "update_device";
        public string group_list_request = "group_list_request";
        public string add_group_info = "add_group_info";
        public string delete_group_info = "delete_group_info";
        public string update_group_info = "update_group_info";
        public string group_member_list_request = "group_member_list_request";
        public string add_dgna = "add_dgna";
        public string delete_dgna = "delete_dgna";
        public string add_dgna_member = "add_dgna_member";
        public string delete_dgna_member = "delete_dgna_member";
        public string sap_list_request = "sap_list_request";
        public string system_list_request = "system_list_request";
        public string mgr_get_uegroups = "mgr_get_uegroups";
        #endregion

        #region   ack
        public string organization_list_request_ack = "organization_list_request_ack";
        public string staff_list_request_ack = "staff_list_request_ack";
        public string device_list_request_ack = "device_list_request_ack";
        public string group_list_request_ack = "group_list_request_ack";
        public string system_list_request_ack = "system_list_request_ack";
        public string sap_list_request_ack = "sap_list_request_ack";
        public string gps_get_gps_info_ack = "gps_get_gps_info_ack";
        public string gps_start_gps_info_report_ack = "gps_start_gps_info_report_ack";
        public string gps_batch_start_gps_info_report_ack = "gps_batch_start_gps_info_report_ack";
        public string gps_batch_stop_gps_info_report_ack = "gps_batch_stop_gps_info_report_ack";
        public string gps_stop_gps_info_report_ack = "gps_stop_gps_info_report_ack";
        public string get_gps_report_evt = "get_gps_report_evt";
        public string gps_record_query_ack = "gps_record_query_ack";
        public string sds_text_send_ack = "sds_text_send_ack";
        public string sds_status_receive_evt = "sds_status_receive_evt";
        public string sds_status_list_request_ack = "sds_status_list_request_ack";
        public string edit_dgna_ack = "edit_dgna_ack";
        public string edit_dgna_member_ack = "edit_dgna_member_ack";
        public string sds_text_receive_evt = "sds_text_receive_evt";
        public string mgr_get_uegroups_ack = "mgr_get_uegroups_ack";
        #endregion

		public string GetNewGuid
        {
            get { return "{" + System.Guid.NewGuid().ToString() + "}"; }
        }

    }
}