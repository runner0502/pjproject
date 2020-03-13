using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDemo
{

    public class GlobalCommandName
    {
        public const string Version = "10";
        public const string Product_Name = "PUC";
        public const string PUC_LoginTypeDispatcher = "Dispatcher";
        public const string ResultSuccess = "0";
        public const string OutOfLicence = "-5";

        #region CmdName
        public const string PUC_LogIn = "puc_login";
        public const string PUC_LogIn_Ack = "puc_login_ack";
        public const string PUC_Logout = "puc_logout";
        public const string PUC_Logout_Ack = "puc_logout_ack";
        public const string Data_Version_Request = "data_version_request";
        public const string Data_Version_Request_Ack = "data_version_request_ack";
        //public const string Data_Version_Update_Evt = "data_version_update_evt"; //没有找到使用
        public const string Puc_Add_Evt = "puc_add_evt";
        public const string Puc_Remove_Evt = "puc_remove_evt";//由于服务器业务要求，下级不会主动与服务器连接(性能考虑），只有在上级申请下级时才会连接，连接了就不会删除下级该节点；
        public const string Puc_Server_Connect_Evt = "puc_server_connect_evt"; //下级服务器与上级服务器连接
        public const string Puc_Server_Disconnect_Evt = "puc_server_disconnect_evt";//下级服务器与上级服务器连接断开
        public const string PUC_List_Request = "puc_list_request";
        public const string PUC_List_Request_Ack = "puc_list_request_ack";
        public const string Puc_License_Info_Request = "puc_license_info_request";
        public const string Puc_License_Info_Request_Ack = "puc_license_info_request_ack";
        public const string Account_Info_Edit_Ack = "account_info_edit_ack";//没有使用 wdz
        public const string Role_Info_Edit_Ack = "edit_role_ack";//没有使用 wdz

        public const string Role_Request = "role_request";
        public const string Role_Request_Ack = "role_request_ack";

        public const string Account_List_Request = "account_list_request";
        public const string Account_List_Request_Ack = "account_list_request_ack";
        public const string Account_List_Request_Evt = "account_list_request_evt";//没有使用 wdz
        public const string Organization_List_Request = "organization_list_request";
        public const string Organization_List_Request_Ack = "organization_list_request_ack";
        public const string Custom_Organization_List_Request = "custom_organization_list_request";
        public const string Custom_Organization_List_Request_Ack = "custom_organization_list_request_ack";
        public const string Organization_List_Request_Evt = "organization_list_request_evt";
        public const string Organization_Edit_Ack = "organization_edit_ack";
        public const string Custom_Organization_Edit_Ack = "custom_organization_edit_ack";
        public const string Add_Organization_InfoProc = "add_organization_info";
        public const string Add_Custom_OrganizationProc = "add_custom_organization";
        public const string Update_Organization_InfoProc = "update_organization_info";
        public const string Update_Custom_OrganizationProc = "update_custom_organization";
        public const string System_List_Request = "system_list_request";
        public const string Ability_List_Request = "ability_list_request";
        public const string Ability_List_Request_Ack = "ability_list_request_ack";

        public const string Update_Dispatch_Request = "update_dispatch_ability";
        public const string Update_Dispatch_Request_Ack = "update_dispatch_ability_ack";

        public const string System_List_Request_Ack = "system_list_request_ack";
        public const string System_Edit_Ack = "system_edit_ack";
        public const string CC_Disconnect = "cc_disconnect";
        public const string CC_DisConnected_Evt = "cc_disconnected_evt";
        public const string CC_Member_DisConnected_Evt = "cc_member_disconnect_evt"; //cc_member_disconnected_evt 
        public const string CC_Member_Connected_Evt = "cc_member_connect_evt";
        public const string CC_Connected_Evt = "cc_connected_evt";
        public const string CC_Tx_Granted_Evt = "cc_tx_granted_evt";
        public const string CC_Remain_Time_Report_Evt = "cc_remain_time_report_evt";
        public const string Portable_Alias_Report_Evt = "portable_alias_report_evt";
        public const string CC_Notify_Calltype_Change_evt = "cc_notify_calltype_change_evt";
        public const string CC_Setup_Call = "cc_setup_call";
        public const string CC_Setup_Call_Ack = "cc_setup_call_ack";
        public const string CC_Setup_RsPlay = "cc_setup_rsplay";
        public const string CC_Connect_RsPlay = "cc_connect_RsPlay";
        public const string CC_Incoming_Evt = "cc_incoming_evt";
        public const string CC_Alert = "cc_alert";
        public const string CC_Alerting_Evt = "cc_alerting_evt";
        public const string CC_Connect = "cc_connect";
        public const string CC_Tx_Cease = "cc_tx_cease";
        public const string CC_Tx_Demand = "cc_tx_demand";
        public const string CC_Tx_Ceased_Evt = "cc_tx_cease_evt";
        public const string CC_RecordPlay_Span_Set = "cc_recordplay_span_set";
        public const string CC_RecordPlay_Span_Set_Ack = "cc_recordplay_span_set_ack";
        public const string CC_RecordPlay_Pause_Set = "cc_recordplay_pause_set";
        public const string CC_RecordPlay_Pause_Set_Ack = "cc_recordplay_pause_set_ack";
        public const string CC_Voicecall_With_Video = "cc_voicecall_with_video";
        public const string CC_Voicecall_With_Video_Ack = "cc_voicecall_with_video_ack";
        public const string CC_Voicecall_With_Video_Evt = "cc_voicecall_with_video_evt";

        public const string CC_Force_Disconnect = "cc_force_disconnect";
        public const string Device_List_Request = "device_list_request";
        public const string Device_List_Request_Ack = "device_list_request_ack";
        public const string Device_List_Request_Evt = "device_list_request_evt";
        public const string Device_Request = "device_request";
        public const string Device_Request_Ack = "device_request_ack";
        public const string Device_Edit_Ack = "device_edit_ack";
        public const string Group_List_Request = "group_list_request";
        public const string Group_List_Request_Ack = "group_list_request_ack";
        public const string Group_List_Request_Evt = "group_list_request_evt";
        public const string Group_Request = "group_request";
        public const string Group_Request_Ack = "group_request_ack";
        public const string Group_Info_Edit_Ack = "group_info_edit_ack";
        public const string Staff_List_Request = "staff_list_request";
        public const string Staff_List_Request_Ack = "staff_list_request_ack";
        public const string Staff_List_Request_Evt = "staff_list_request_evt";
        public const string Staff_Request = "staff_request";
        public const string Staff_Request_Ack = "staff_request_ack";
        public const string Staff_Info_Edit_Ack = "staff_info_edit_ack";
        public const string Sap_List_Request = "sap_list_request";
        public const string Sap_List_Request_Ack = "sap_list_request_ack";
        public const string Sap_Edit_Ack = "sap_edit_ack";
        public const string Sds_Status_List_Request = "sds_status_list_request";
        public const string Sds_Status_List_Request_Ack = "sds_status_list_request_ack";
        public const string Sds_Status_Edit_Ack = "sds_status_edit_ack";

        public const string Favorite_Request = "favorite_request";
        public const string Favorite_Request_Ack = "favorite_request_ack";
        public const string Add_Favorite = "add_favorite";
        public const string Update_Favorite = "update_favorite";
        public const string Delete_Favorite = "delete_favorite";
        public const string Favorite_Edit_Ack = "favorite_edit_ack";
        public const string Favorite_Basic_Data_Request_Start = "favorite_basic_data_request_start";
        public const string Favorite_Basic_Data_Request_Start_Ack = "favorite_basic_data_request_start_ack";

        public const string Device_Attach_GrpList_Request = "device_attach_grp_list_request";
        public const string Device_Attach_GrpList_Request_Ack = "device_attach_grp_list_ack";
        public const string Device_Attach_Grp_List_Evt = "device_attach_grp_list_evt";


        public const string Crosspatch_Request = "crosspatch_request";
        public const string Crosspatch_Request_Ack = "crosspatch_request_ack";

        public const string MGR_Channel_Zone_Change = "mgr_channel_zone_change";
        public const string MGR_Channel_Zone_Change_Ack = "mgr_channel_zone_change_ack";

        public const string Phone_Client_Bind_Info_Request = "phone_client_bind_info_request";
        public const string Phone_Client_Bind_Info_Request_Ack = "phone_client_bind_info_request_ack";
        public const string Add_Phone_Client_Bind_Info = "add_phone_client_bind_info";
        public const string Modify_Phone_Client_Bind_Info = "modify_phone_client_bind_info";
        public const string Remove_Phone_Client_Bind_Info = "remove_phone_client_bind_info";
        public const string Edit_Phone_Client_Bind_Info_Ack = "edit_phone_client_bind_info_ack";

        public const string Client_Group_Request = "client_group_request";
        public const string Client_Group_Request_Ack = "client_group_request_ack";

        public const string Add_Client_Group = "add_client_group";
        public const string Modify_Client_Group = "modify_client_group";
        public const string Remove_Client_Group = "remove_client_group";
        public const string Edit_Client_Group_Ack = "edit_client_group_ack";

        public const string Route_Add_Request = "route_add_request";
        public const string Route_Delete_Request = "route_delete_request";
        public const string Route_Update_Request = "route_update_request";
        public const string Route_Query_Request_Ack = "route_query_request_ack";
        public const string Route_Add_Request_Ack = "route_add_request_ack";
        public const string Route_Delete_Request_Ack = "route_delete_request_ack";
        public const string Route_Update_Request_Ack = "route_update_request_ack";
        public const string Route_Update_Priority_Request = "route_update_priority_request";
        public const string Route_Update_Priority_Request_Ack = "route_update_priority_request_ack";


        public const string Add_Device = "add_device";
        public const string Add_Group_Info = "add_group_info";
        public const string Add_Organization_Info = "add_organization_info";
        public const string Add_Custom_Organization = "add_custom_organization";
        public const string Add_Staff_Info = "add_staff_info";
        public const string Add_System = "add_system";
        public const string Add_Account = "add_account";
        public const string Add_Sds_Status = "add_sds_status";
        public const string Add_Crosspatch = "add_crosspatch";
        public const string Add_Crosspatch_Member = "add_crosspatch_member";
        public const string Add_Sap = "add_sap";
        public const string Add_Role = "add_role";
       

        public const string Include_call_Request = "cc_include_call_request";
        public const string Include_call_Request_ACK = "cc_include_call_request_ack";

        public const string Delete_Device = "delete_device";
        public const string Delete_Group_Info = "delete_group_info";
        public const string Delete_Organization_Info = "delete_organization_info";
        public const string Delete_Custom_Organization = "delete_custom_organization";
        public const string Delete_Staff_Info = "delete_staff_info";
        public const string Delete_System = "delete_system";
        public const string Delete_Account = "delete_account";
        public const string Delete_Sds_Status = "delete_sds_status";
        public const string Delete_Role = "delete_role";

        public const string Delete_Crosspatch = "delete_crosspatch";
        public const string Delete_Crosspatch_Member = "delete_crosspatch_member";
        public const string Delete_Sap = "delete_sap";

        public const string Update_Role = "update_role";
        public const string Update_Device = "update_device";
        public const string Update_Group_Info = "update_group_info";
        public const string Update_Organization_Info = "update_organization_info";
        public const string Update_Custom_Organization = "update_custom_organization";
        public const string Update_Staff_Info = "update_staff_info";
        public const string Update_System = "update_system";
        public const string Update_Account = "update_account";
        public const string Update_Sds_Status = "update_sds_status";
        public const string Update_Sap = "update_sap";
        public const string Update_Crosspatch = "update_crosspatch";
        public const string Edit_Crosspatch_Ack = "edit_crosspatch_ack";
        public const string Edit_Crosspatch_Member_Ack = "edit_crosspatch_member_ack";
        public const string Set_Crosspatch_Active = "set_crosspatch_active";

        public const string Dispatcher_Status_Check_Evt = "dispatcher_status_check_evt";

        public const string Simulselect_Request = "simulselect_request";
        public const string Simulselect_Request_Ack = "simulselect_request_ack";
        public const string Add_Simulselect = "add_simulselect";
        public const string Delete_Simulselect = "delete_simulselect";
        public const string Update_Simulselect = "update_simulselect";
        public const string Simulselect_Edit_Ack = "simulselect_edit_ack";

        public const string CC_Send_DTMF = "cc_send_dtmf";

        public const string Add_Poi = "add_poi";
        public const string Delete_Poi = "delete_poi";
        public const string Update_Poi = "update_poi";
        public const string Poi_Request = "poi_request";
        public const string Poi_Edit_Ack = "poi_edit_ack";
        public const string Poi_Request_Ack = "poi_request_ack";

        public const string Mgr_Get_Groupmembers_Ack = "mgr_get_groupmembers_ack";
        public const string Mgr_Startsub_Syscall_Ack = "mgr_startsub_syscall_ack";
        public const string Mgr_Stopsub_Syscall_Ack = "mgr_stopsub_syscall_ack";
        public const string Mgr_Get_Groupmembers = "mgr_get_groupmembers";
        public const string Mgr_Get_Uegroups_Ack = "mgr_get_uegroups_ack";
        public const string Mgr_Get_Uegroups = "mgr_get_uegroups";
        public const string Mgr_Startsub_Syscall = "mgr_startsub_syscall";
        public const string Mgr_Stopsub_Syscall = "mgr_stopsub_syscall";
        public const string PUC_Gb_Ipc_Control = "puc_gb_ipc_control";

        #region record_download_request
        public const string Record_Download_Request = "record_download_request";
        public const string Record_Download_Request_Ack = "record_download_request_ack";
        #endregion

        #region Fax
        public const string Fax_Num_Query = "fax_num_query";
        public const string Fax_Num_Query_Ack = "fax_num_query_ack";
        public const string Add_Fax_Num = "fax_num_add";
        public const string Add_Fax_Num_Ack = "fax_num_add_ack";
        public const string Fax_Num_Modify = "fax_num_modify";
        public const string Fax_Num_Modify_Ack = "fax_num_modify_ack";
        public const string Fax_Num_Del = "fax_num_del";
        public const string Fax_Num_Del_Ack = "fax_num_del_ack";

        public const string Fax_Send_File = "fax_send_file";
        public const string Fax_Send_File_Ack = "fax_send_file_ack";
        public const string Fax_Query_Send_Log_Count = "fax_query_send_log_count";
        public const string Fax_Query_Send_Log_Count_Ack = "fax_query_send_log_count_ack";
        public const string Fax_Query_Send_Log = "fax_query_send_log";
        public const string Fax_Query_Send_Log_Ack = "fax_query_send_log_ack";

        public const string Fax_Download_File = "fax_download_file";
        public const string Fax_Download_File_Ack = "fax_download_file_ack";
        public const string Fax_Query_Recv_Log_Count = "fax_query_recv_log_count";
        public const string Fax_Query_Recv_Log_Count_Ack = "fax_query_recv_log_count_ack";
        public const string Fax_Query_Recv_Log = "fax_query_recv_log";
        public const string Fax_Query_Recv_Log_Ack = "fax_query_recv_log_ack";
        public const string Fax_Notify = "fax_notify";
        #endregion


        #region 无线备份Virtual Crosspatch
        public const string Virtual_Crosspatch_Request = "virtual_crosspatch_request";
        public const string Virtual_Crosspatch_Request_ack = "virtual_crosspatch_request_ack";//下面这个virtual_crosspatch_list相当于virtual_crosspatch_request_ack 
        public const string Add_Virtual_Crosspatch = "add_virtual_crosspatch";
        public const string Modify_Virtual_Crosspatch = "modify_virtual_crosspatch";
        public const string Remove_Virtual_Crosspatch = "remove_virtual_crosspatch";
        public const string Edit_Virtual_Crosspatch_Ack = "edit_virtual_crosspatch_ack";


        #endregion
        #region DGNA
        public const string Add_DGNA = "add_dgna";
        public const string Add_Predefined_DGNA = "add_predefined_dgna";
        public const string Delete_DGNA = "delete_dgna";
        public const string Delete_Predefined_DGNA = "delete_predefined_dgna";
        public const string Edit_DGNA_Ack = "edit_dgna_ack";
        public const string DGNA_Request = "dgna_request";
        public const string DGNA_Request_Ack = "dgna_request_ack";
        public const string Edit_Predefined_DGNA_Ack = "edit_predefined_dgna_ack";
        public const string Predefined_DGNA_Request = "predefined_dgna_request";
        public const string Predefined_DGNA_Request_Ack = "predefined_dgna_request_ack";
        public const string Update_DGNA = "update_dgna";
        public const string Update_Predefined_DGNA = "update_predefined_dgna";
        public const string Add_DGNA_Member = "add_dgna_member";
        public const string Edit_DGNA_Member_Ack = "edit_dgna_member_ack";
        public const string Delete_DGNA_Member = "delete_dgna_member";
        #endregion

        #region  Report
        public const string Query_Call_Log_Count = "query_call_log_count";
        public const string Query_Call_Log_Count_Ack = "query_call_log_count_ack";
        public const string Query_Call_Log = "query_call_log";
        public const string Query_Call_Log_Ack = "query_call_log_ack";

        public const string Query_SDS_Log_Count = "query_sds_log_count";
        public const string Query_SDS_Log_Count_Ack = "query_sds_log_count_ack";
        public const string Query_SDS_Log = "query_sds_log";
        public const string Query_SDS_Log_Ack = "query_sds_log_ack";

        public const string Query_RRS_Count_Info = "query_rrs_count_info";
        public const string Query_RRS_Count_Info_Ack = "query_rrs_count_info_ack";
        public const string Query_RRS_Info = "query_rrs_info";
        public const string Query_RRS_Info_Ack = "query_rrs_info_ack";

        #endregion

        #region Disconnect or Kickout

        public const string PUC_Kickout_Evt = "puc_kickout_evt";
        public const string PUC_Disconnect_Evt = "puc_disconnect_evt";

        #endregion

        #region RRS
        public const string Device_Status_Check = "device_status_check";
        public const string Device_Call_Alert = "device_call_alert";
        public const string Device_Call_Alert_Ack = "device_call_alert_ack";
        public const string Device_Status_List_Request = "device_status_list_request";
        public const string Device_Status_Check_Ack = "device_status_check_ack";
        public const string Device_List_Status_Check_Evt = "device_list_status_check_evt";
        public const string Device_Lock_List_Check_Evt = "device_lock_list_check_evt";
        public const string Device_Status_Check_Evt = "device_status_check_evt";
        public const string Sap_Status_Check_Evt = "sap_status_check_evt";
        public const string RRS_Online = "1";
        public const string RRS_Offline = "0";
        #endregion

        #region Safety
        public const string Device_Stun_Request = "device_stun_request";
        public const string Device_Stun_Request_Ack = "device_stun_request_ack";
        public const string Update_Device_Lock_Status_Evt = "update_device_lock_status_evt";
        public const string Device_Lock_Status_Check = "device_lock_status_check";
        public const string Device_Kill_Request = "device_kill_request";
        public const string Device_Revive_Request = "device_revive_request";
        public const string Device_Revive_Request_Ack = "device_revive_request_ack";
        public const string Device_Kill_Request_Ack = "device_kill_request_ack";
        public const string Device_Lock_Status_Check_Ack = "device_lock_status_check_ack";
        public const string Mgr_Sub_Syscall_Evt = "mgr_sub_syscall_evt";
        public const string Device_Capability_Change_Evt = "device_capability_change_evt";
        public const string Device_Capability_List_Request = "device_capability_list_request";
        public const string Device_Capability_List_Evt = "device_capability_list_evt";
        public const string Pistol_Holster_Status_EvtProc = "pistol_holster_status_evt";
        public const string Sub_CallStatus_Info = "sub_callstatus_info";
        public const string Sub_CallStatus_Info_Ack = "sub_callstatus_info_ack";
        public const string Sub_Callstatus_Info_EvtProc = "sub_callstatus_info_evt";
        #endregion

        #region SDS
        public const string Sds_Text_Send_Ack = "sds_text_send_ack";
        public const string Sds_Status_Send_Ack = "sds_status_send_ack";
        public const string Sds_Text_Receive_Evt = "sds_text_receive_evt";
        public const string Sds_Status_Receive_Evt = "sds_status_receive_evt";
        public const string Sds_Send_Text = "sds_send_text";
        public const string Sds_Send_Status = "sds_send_status";
        public const string Sds_Send_Receive_State = "sds_send_receive_state";

        #endregion

        #region GPS
        public const string Gps_Get_Gps_Info = "gps_get_gps_info";
        public const string Get_Gps_Report_Timeout = "get_gps_report_timeout";
        public const string Gps_Start_Gps_Info_Report = "gps_start_gps_info_report";
        public const string Gps_Stop_Gps_Info_Report = "gps_stop_gps_info_report";
        public const string Gps_Quick_Start = "gps_quick_start";
        public const string Gps_Quick_Stop = "gps_quick_stop";
        public const string Device_List_Status_Quickgps_Evt = "device_list_status_quickgps_evt";

        public const string Gps_Get_Gps_Info_Ack = "gps_get_gps_info_ack";
        public const string Gps_Start_Gps_Info_Report_Ack = "gps_start_gps_info_report_ack";
        public const string Gps_Stop_Gps_Info_Report_Ack = "gps_stop_gps_info_report_ack";
        public const string Gps_Quick_Start_Ack = "gps_quick_start_ack";
        public const string Gps_Quick_Stop_Ack = "gps_quick_stop_ack";
        public const string Get_Gps_Report_Evt = "get_gps_report_evt";
        public const string Get_Cycle_Report_State_Evt = "get_cycle_report_state_evt";

        public const string Gps_Record_Query = "gps_record_query";
        public const string Gps_Record_Query_Ack = "gps_record_query_ack";

        public const string Gps_Query_Gps_Subscribeinfo_Count = "gps_query_gps_subscribeinfo_count";
        public const string Gps_Start_Gps_Info_Report_List = "gps_start_gps_info_report_list";
        public const string Gps_Query_Gps_Subscribeinfo_Count_Ack = "gps_query_gps_subscribeinfo_count_ack";
        public const string Gps_Start_Gps_Info_Report_List_Ack = "gps_start_gps_info_report_list_ack";

        public const string Gps_Batch_Start_Gps_Info_Report = "gps_batch_start_gps_info_report";
        public const string Gps_Batch_Start_Gps_Info_Report_Ack = "gps_batch_start_gps_info_report_ack";
        public const string Gps_Batch_Stop_Gps_Info_Report = "gps_batch_stop_gps_info_report";
        public const string Gps_Batch_Stop_Gps_Info_Report_Ack = "gps_batch_stop_gps_info_report_ack";
        public const string Gps_Notify_Gps_Info_Active_Report = "gps_notify_gps_info_active_report";
        public const string Gps_Notify_Gps_Info_Active_Report_Ack = "gps_notify_gps_info_active_report_ack";

        //越区告警
        public const string Rule_List_Request = "rule_list_request";
        public const string Rule_List_Request_Ack = "rule_list_request_ack";
        public const string Update_Rule_List = "update_rule_list";
        public const string Update_Rule_List_Ack = "update_rule_list_ack";
        public const string Update_Rule_List_Evt = "update_rule_list_evt";
        public const string Add_Region = "add_region";
        public const string Add_Region_Ack = "add_region_ack";
        public const string Delete_Region = "delete_region";
        public const string Delete_Region_Ack = "delete_region_ack";
        public const string Update_Region = "update_region";
        public const string Update_Region_Ack = "update_region_ack";
        public const string Region_List_Request = "region_list_request";
        public const string Region_List_Request_Ack = "region_list_request_ack";
        public const string Region_Edit_Evt = "region_edit_evt";
        public const string Geofencing_Alarm_Query = "geofencing_alarm_query";
        public const string Geofencing_Alarm_Query_Ack = "geofencing_alarm_query_ack";
        public const string Geofencing_Alarm_List_Request = "geofencing_alarm_list_request";
        public const string Geofencing_Alarm_List_Request_Ack = "geofencing_alarm_list_request_ack";
        public const string Geofencing_Alarm_Count_Query = "geofencing_alarm_count_query";
        public const string Geofencing_Alarm_Count_Query_Ack = "geofencing_alarm_count_query_ack";
        public const string Geofencing_Alarm_Report_Evt = "geofencing_alarm_report_evt";

        //GPS Control
        public const string gps_start_admin_req = "gps_start_gps_admin_req";
        public const string gps_start_admin_req_ack = "gps_start_gps_admin_ack";

        public const string gps_get_radio_sub_para_req = "gps_get_radio_sub_para_req";
        public const string gps_get_radio_sub_para_ack = "gps_get_radio_sub_para_ack";

        public const string gps_get_sub_relation_req = "gps_get_sub_relation_req";
        public const string gps_get_sub_relation_req_ack = "gps_get_sub_relation_ack";

        public const string gps_set_radio_min_cycle_req = "gps_set_radio_min_cycle_req";
        public const string gps_set_radio_min_cycle_req_ack = "gps_set_radio_min_cycle_ack";

        public const string gps_cancel_radio_min_cycle_req = "gps_cancel_radio_min_cycle_req";
        public const string gps_cancel_radio_min_cycle_req_ack = "gps_cancel_radio_min_cycle_ack";

        public const string gps_cancel_radio_sub_req = "gps_cancel_radio_sub_req";
        public const string gps_cancel_radio_sub_req_ack = "gps_cancel_radio_sub_ack";

        public const string gps_stop_gps_info_report_syn = "gps_stop_gps_info_report_syn";
        public const string gps_cancel_dispatcher_sub_syn = "gps_cancel_dispatcher_sub_syn";

        public const string gps_cancel_dispatcher_sub_req = "gps_cancel_dispatcher_sub_req";
        public const string gps_cancel_dispatcher_sub_ack = "gps_cancel_dispatcher_sub_ack";

        public const string gps_set_whole_net_req = "gps_set_whole_net_req";
        public const string gps_set_whole_net_ack = "gps_set_whole_net_ack";

        public const string gps_set_unify_min_cycle_req = "gps_set_unify_min_cycle_req";
        public const string gps_set_unify_min_cycle_req_ack = "gps_set_unify_min_cycle_ack";

        public const string gps_cancel_unify_min_cycle_req = "gps_cancel_unify_min_cycle_req";
        public const string gps_cancel_unify_min_cycle_req_ack = "gps_cancel_unify_min_cycle_ack";

        #endregion

        #region Monitor
        public const string Mon_Monitor = "mon_monitor";
        public const string Mon_Monitor_Ack = "mon_monitor_ack";
        public const string Mon_Monitor_Evt = "mon_monitor_evt";
        public const string Mon_Monitor_Register_Evt = "mon_monitor_register_evt";
        public const string Mon_Status_Receive_Evt = "mon_status_receive_evt";
        public const string Mon_Text_Rreceive_Evt = "mon_text_receive_evt";

        //将同时要监听项对应的数值做或（|）运算。各监听项定义如下：
        //例子：要同时监听上下线和监听呼叫事件，则对0x01和0x20做或运算 0x01|0x20 = 0x21
        /// <summary>
        /// 取消所有监听
        /// </summary>
        public const int PUCMonitorLevelCancelAll = 0;
        /// <summary>
        /// 监听上下线
        /// </summary>
        public const int PUCMonitorLevelOnline = 0x01;
        /// <summary>
        /// 监听文本短信
        /// </summary>
        public const int PUCMonitorLevelSDSText = 0x04;
        /// <summary>
        /// 监听状态短信
        /// </summary>
        public const int PUCMonitorLevelSDSStatus = 0x08;
        /// <summary>
        /// 监听呼叫事件
        /// </summary>
        public const int PUCMonitorLevelEvent = 0x20;
        /// <summary>
        /// 监听呼叫语音
        /// </summary>
        public const int PUCMonitorLevelMedia = 0x40;

        #endregion

        #region BaseStationMonitor
        public const string Base_station_info_list_request = "base_station_info_list_request";
        public const string Base_station_info_list_request_ack = "base_station_info_list_request_ack";
        public const string Cm_channel_info_list_sub = "cm_channel_info_list_sub";
        public const string Cm_channel_info_list_stop_sub = "cm_channel_info_list_stop_sub";
        public const string update_base_station_info_list = "update_base_station_info_list";
        public const string Cm_channel_info_list_sub_ack = "cm_channel_info_list_sub_ack";
        public const string Cm_channel_info_list_stop_sub_ack = "cm_channel_info_list_stop_sub_ack";
        public const string Cm_channel_info_list_report_evt = "cm_channel_info_list_report_evt";
        public const string Cm_channel_ptt_info_list_report_evt = "cm_ptt_info_list_report_evt";
        public const string drag_position_info_report = "drag_position_info_report";
        public const string channel_monitor = "channel_monitor";
        public const string MonitorBaseStation = "MonitorBaseStation";
        public const string cc_bs_force_disconnect = "cc_bs_force_disconnect";
        public const string cc_interrupt = "cc_interrupt";
        public const string SubBaseStationInfo = "SubBaseStationInfo";
        public const string voc_control = "voc_control";
        public const string ice_keep_alive = "ice_keep_alive";
        public const string base_station_online_devices_request = "base_station_online_devices_request";
        public const string cc_tx_granted_evt = "cc_tx_granted_evt1";
        public const string client_logout_evt = "client_logout_evt";

        #endregion


        #region System Control
        public const string System_Ctrl = "system_ctrl";
        public const string System_Ctrl_Ack = "system_ctrl_ack";
        #endregion

        #region RollCall
        public const string Add_Roll_Call_Init = "add_roll_call_init";
        public const string Add_Roll_Call_Records = "add_roll_call_records";
        public const string Query_Roll_Call_Record = "query_roll_call_record";
        public const string Query_Roll_Call_Record_Count = "query_roll_call_record_count";
        public const string Request_Roll_Call_Init = "request_roll_call_init";

        public const string Add_Roll_Call_Init_Ack = "add_roll_call_init_ack";
        public const string Add_Roll_Call_Record_Ack = "add_roll_call_records_ack";
        public const string Query_Roll_Call_Record_Count_Ack = "query_roll_call_record_count_ack";
        public const string Query_Roll_Call_Record_Ack = "query_roll_call_record_ack";
        public const string Request_Roll_Call_Init_Ack = "request_roll_call_init_ack";

        #endregion

        public const string Update_Account_Password = "update_account_password";

        #region ICC
        public const string Sync_ICC_Login = "sync_icc_login";
        public const string ICC_Dictcode_List_Request = "icc_dictcode_list_request";
        public const string Sync_ICC_Dictcode = "sync_icc_dictcode";
        public const string ICC_Dictcode_List_Request_Ack = "icc_dictcode_list_request_ack";

        public const string ICC_Dictcodedesc_List_Request = "icc_dictcodedesc_list_request";
        public const string Add_ICC_Dictcodedesc = "add_icc_dictcodedesc";
        public const string Update_ICC_Dictcodedesc = "update_icc_dictcodedesc";
        public const string Delete_ICC_Dictcodedesc = "delete_icc_dictcodedesc";
        public const string ICC_Dictcodedesc_List_Request_Ack = "icc_dictcodedesc_list_request_ack";
        public const string ICC_Dictcodedesc_Edit_Ack = "icc_dictcodedesc_edit_ack";

        public const string ICC_Alarm_Case_List_Request = "icc_alarm_case_list_request";
        public const string Sync_ICC_Alarm_Case = "sync_icc_alarm_case";
        public const string ICC_Alarm_Case_List_Request_Ack = "icc_alarm_case_list_request_ack";

        public const string ICC_Alarm_Process_List_Request = "icc_alarm_process_list_request";
        public const string ICC_Alarm_Process_List_Request_Ack = "icc_alarm_process_list_request_ack";

        public const string ICC_Alarm_Dispatch_List_Request = "icc_alarm_dispatch_list_request";
        public const string ICC_Alarm_Dispatch_List_Request_Ack = "icc_alarm_dispatch_list_request_ack";

        public const string ICC_Keydep_List_Request = "icc_keydep_list_request";
        public const string Sync_ICC_Keydep = "sync_icc_keydep";
        public const string Delete_ICC_Keydep = "delete_icc_keydep";
        public const string ICC_Keydep_List_Request_Ack = "icc_keydep_list_request_ack";

        public const string ICC_Monitor_List_Request = "icc_monitor_list_request";
        public const string Sync_ICC_Monitor = "sync_icc_monitor";
        public const string Delete_ICC_Monitor = "delete_icc_monitor";
        public const string ICC_Monitor_List_Request_Ack = "icc_monitor_list_request_ack";

        public const string ICC_ColorWarningRelation_List_Request = "icc_colorwarningrelation_list_request";
        public const string Add_ICC_ColorWarningRelation = "add_icc_colorwarningrelation";
        public const string Update_ICC_ColorWarningRelation = "update_icc_colorwarningrelation";
        public const string Delete_ICC_ColorWarningRelation = "delete_icc_colorwarningrelation";
        public const string ICC_ColorWarningRelation_List_Request_Ack = "icc_colorwarningrelation_list_request_ack";
        public const string ICC_ColorWarningRelation_Edit_Ack = "icc_colorwarningrelation_edit_ack";

        public const string ICC_GridAreaStatistic_List_Request = "icc_gridareastatistic_list_request";
        public const string ICC_GridAreaStatistic_List_Request_Ack = "icc_gridareastatistic_list_request_ack";

        public const string ICC_Region_Associate_List_Request = "icc_region_associate_list_request";
        public const string Add_ICC_Region_Associate = "add_icc_region_associate";
        public const string Update_ICC_Region_Associate = "update_icc_region_associate";
        public const string Delete_ICC_Region_Associate = "delete_icc_region_associate";
        public const string ICC_Region_Associate_List_Request_Ack = "icc_region_associate_list_request_ack";
        public const string ICC_Region_Associate_Edit_Ack = "icc_region_associate_edit_ack";

        public const string ICC_PDTState_List_Request = "icc_pdtstate_list_request";
        public const string Sync_ICC_PDTState = "sync_icc_pdtstate";
        public const string ICC_PDTState_List_Request_Ack = "icc_pdtstate_list_request_ack";

        public const string ICC_Car_List_Request = "icc_car_list_request";
        public const string Add_ICC_Car = "add_icc_car";
        public const string Update_ICC_Car = "update_icc_car";
        public const string Delete_ICC_Car = "delete_icc_car";
        public const string ICC_Car_List_Request_Ack = "icc_car_list_request_ack";
        public const string ICC_Car_Edit_Ack = "icc_car_edit_ack";

        public const string Resouce_Request = "resource_request";
        public const string Resouce_Request_Ack = "resource_request_ack";

        public const string CaseGridAnalysis_Request = "casegridanalysis_request";
        public const string CaseGridAnalysis_Request_Ack = "casegridanalysis_request_ack";

        public const string GridResource_Request = "gridresource_request";
        public const string GridResource_Request_Ack = "gridresource_request_ack";

        public const string FuzzyQuery_Request = "fuzzyquery_request";
        public const string FuzzyQuery_Request_Ack = "fuzzyquery_request_ack";
        #endregion

        #region Emergency
        public const string Emergency_Alarm_Report_Evt = "emergency_alarm_report_evt";
        public const string Emergency_Alarm_Handle = "emergency_alarm_handle";
        public const string Emergency_Alarm_Handle_Ack = "emergency_alarm_handle_ack";
        public const string Emergency_Alarm_Update = "emergency_alarm_update";
        public const string Emergency_Alarm_List_Request = "emergency_alarm_list_request";
        public const string Emergency_Alarm_List_Request_Ack = "emergency_alarm_list_request_ack";
        public const string Emergency_Alarm_Count_Query = "emergency_alarm_count_query";
        public const string Emergency_Alarm_Count_Query_Ack = "emergency_alarm_count_query_ack";
        public const string Emergency_Alarm_Query = "emergency_alarm_query";
        public const string Emergency_Alarm_Query_Ack = "emergency_alarm_query_ack";

 
        public const string Callback_Alarm_Report_Evt = "callback_list_report_evt";
        public const string Callback_List_Handle = "callback_list_handle";
        public const string Callback_List_Handle_Ack = "callback_list_handle_ack";
        public const string Callback_List_Update = "callback_list_update";
        public const string Callback_List_Request = "callback_list_request";
        public const string Callback_List_Request_Ack = "callback_list_request_ack";
        public const string Callback_List_Count_Query = "callback_list_count_query";
        public const string Callback_List_Count_Query_Ack = "callback_list_count_query_ack";

        public const string Callback_List_Query = "callback_list_query";
        public const string Callback_List_Query_Ack = "callback_list_query_ack";



        #endregion

        #region IndoorLocation add by xjf 2016年9月13日
        public const string Add_Indoor_Org = "add_indoor_org";
        public const string Add_Indoor_Org_Ack = "add_indoor_org_ack";
        public const string Add_Indoor_Point = "add_indoor_point";
        public const string Add_Indoor_Point_Ack = "add_indoor_point_ack";
        public const string Add_Indoor_Monitor = "add_indoor_monitor";
        public const string Delete_Indoor_Org = "delete_indoor_org";
        public const string Delete_Indoor_Org_Ack = "delete_indoor_org_ack";
        public const string Delete_Indoor_Point = "delete_indoor_point";
        public const string Delete_Indoor_Point_Ack = "delete_indoor_point_ack";
        public const string Edit_Indoor_Monitor_Ack = "edit_indoor_monitor_ack";
        public const string Delete_Indoor_Monitor = "delete_indoor_monitor";
        public const string Device_Indoor_Location_Report_Evt = "device_indoor_location_report_evt";
        public const string Get_Indoor_Map = "get_indoor_map";
        public const string Get_Indoor_Map_Ack = "get_indoor_map_ack";
        public const string Get_Indoor_Org_List = "get_indoor_org_list";
        public const string Get_Indoor_Org_List_Ack = "get_indoor_org_list_ack";
        public const string Get_Indoor_Points_List = "get_indoor_points_list";
        public const string Get_Indoor_Points_List_Ack = "get_indoor_points_list_ack";
        public const string Get_Indoor_Monitors_List = "get_indoor_monitors_list";
        public const string Get_Indoor_Monitors_List_Ack = "get_indoor_monitors_list_ack";
        public const string Updata_Indoor_Org = "updata_indoor_org";
        public const string Updata_Indoor_Org_Ack = "updata_indoor_org_ack";
        public const string Updata_Indoor_Point = "updata_indoor_point";
        public const string Updata_Indoor_Point_Ack = "updata_indoor_point_ack";
        public const string Update_Indoor_Monitor = "update_indoor_monitor";
        public const string Upload_Indoor_Map = "upload_indoor_map";
        public const string Upload_Indoor_Map_Ack = "upload_indoor_map_ack";
        public const string Query_Indoor_Record = "query_indoor_record";
        public const string Query_Indoor_Record_Ack = "query_indoor_record_ack";
        #endregion IndoorLocation

        #region GroupMemberSelect
        public const string Get_Current_Group_Mem_Request = "get_current_group_mem_request";
        public const string Get_Current_Group_Mem_Request_Ack = "get_current_group_mem_request_ack";
        public const string Get_Current_Group_Mem_Cancel_Request = "get_current_group_mem_cancel_request";
        public const string Get_Current_Group_Mem_Cancel_Request_Ack = "get_current_group_mem_cancel_request_ack";
        public const string Get_Current_Group_Mem_Evt = "get_current_group_mem_evt";
        public const string Terminal_Current_Attach_Updata_Evt = "terminal_current_attach_updata_evt";
        #endregion

        #region conference

        public const string Conference_Request = "conference_request";
        public const string Conference_Request_Ack = "conference_request_ack";
        public const string Add_Conference = "add_conference";
        public const string Delete_Conference = "delete_conference";
        public const string Update_Conference = "update_conference";
        public const string Edit_Conference_Ack = "edit_conference_ack";
        public const string Add_Conference_Member = "add_conference_member";
        public const string Update_Conference_Member_Videomix = "update_conference_member_videomix";
        public const string Delete_Conference_Member = "delete_conference_member";
        public const string Edit_Conference_Member_Ack = "edit_conference_member_ack";
        public const string Apply_Conference_Chairman = "apply_conference_chairman";
        public const string Apply_Conference_Chairman_Ack = "apply_conference_chairman_ack";
        public const string Specify_Conference_Chairman = "specify_conference_chairman";
        public const string Specify_Conference_Chairman_Ack = "specify_conference_chairman_ack";
        public const string Apply_Conference_Speaker = "apply_conference_speaker";
        public const string Apply_Conference_Speaker_Ack = "apply_conference_speaker_ack";
        public const string Specify_Conference_Speaker = "specify_conference_speaker";
        public const string Specify_Conference_Speaker_Ack = "specify_conference_speaker_ack";
        public const string Set_Conference_Freetalk = "set_conference_freetalk";
        public const string Set_Conference_Freetalk_Ack = "set_conference_freetalk_ack";
        public const string Turn_Conference_Member_Device = "turn_conference_member_device";
        public const string Turn_Conference_Member_Device_Ack = "turn_conference_member_device_ack";
        public const string Broadcast_Conference_Screen = "broadcast_conference_screen";
        public const string Broadcast_Conference_Screen_Ack = "broadcast_conference_screen_ack";

        #endregion
        #endregion CmdName

        #region DisconnectReason

        #region HyteraDMR2DFSI
        /// <summary>
        /// Unknown:0
        /// </summary>
        public const string DisconnectReason_HyteraDMR2DFSI_Unknown = "0";
        /// <summary>
        /// hang up:1
        /// </summary>
        public const string DisconnectReason_HyteraDMR2DFSI_HangUp = "1";
        /// <summary>
        /// Tiemout:2
        /// </summary>
        public const string DisconnectReason_HyteraDMR2DFSI_CallTimeout = "2";
        #endregion

        #region HyteraDMR3SSI
        /// <summary>
        /// 未知原因:0
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_Unknown = "0";

        /// <summary>
        /// 用户请求挂断:1
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_HangUp = "1";

        /// <summary>
        /// 呼叫控制服务未开启:2
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_CCServiceNotOpen = "2";

        /// <summary>
        /// 未登录:3
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_NotLogin = "3";

        /// <summary>
        /// 呼叫失败:4
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_FailToSetupcall = "4";

        /// <summary>
        /// 无权限:5
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_NoPermission = "5";

        /// <summary>
        /// 该号码不存在:6
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_InvalidSsi = "6";

        /// <summary>
        /// 对方忙:7
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_OtherPartyBusy = "7";

        /// <summary>
        /// 对方拒绝:8
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_OtherPartyRefuse = "8";

        /// <summary>
        /// 对方不支持加密:9
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_OtherPartyNotSupportEncryption = "9";

        /// <summary>
        /// 系统呼叫模块忙:10
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_SystemCCBusy = "10";

        /// <summary>
        /// 呼叫超时:11
        /// </summary>
        public const string DisconnectReason_HyteraDMR3SSI_CallTimeout = "11";
        #endregion HyteraDMR3SSI

        #region HyteraTetraDFSI
        public const string HyteraTetraDFSI_NotDefinedOrUnknown = "0";
        public const string HyteraTetraDFSI_UserRequest = "1";
        public const string HyteraTetraDFSI_CalledPartyIsBusy = "2";
        public const string HyteraTetraDFSI_CalledPartyCanNotBeReached = "3";
        public const string HyteraTetraDFSI_CalledPartyDoesNotSupportEncryption = "4";
        public const string HyteraTetraDFSI_NetworkIsBusy = "5";
        public const string HyteraTetraDFSI_TheTrafficIsNotAllowed = "6";
        public const string HyteraTetraDFSI_TheTrafficIsIncompatible = "7";
        public const string HyteraTetraDFSI_TheServiceIsUnavailable = "8";
        public const string HyteraTetraDFSI_EitherPartyIsPreempted = "9";
        public const string HyteraTetraDFSI_InvalidCallIdentifier = "10";
        public const string HyteraTetraDFSI_CalledPartyRejectsTheCall = "11";
        public const string HyteraTetraDFSI_NoCCEntity = "12";
        public const string HyteraTetraDFSI_TimerExpiry = "13";
        public const string HyteraTetraDFSI_SwMIDisconnect = "14";
        public const string HyteraTetraDFSI_NoAcknowledgement = "15";
        public const string HyteraTetraDFSI_UnknownTETRAIdentity = "16";
        public const string HyteraTetraDFSI_SupplementaryServiceDependent = "17";
        public const string HyteraTetraDFSI_UnknownExternalSubscriberNumber = "18";
        public const string HyteraTetraDFSI_CallRestorationFailure = "19";
        public const string HyteraTetraDFSI_CalledPartyRequiresEncryption = "20";
        public const string HyteraTetraDFSI_ConcurrentSetupNotSupported = "21";
        public const string HyteraTetraDFSI_CalledPartyIsUnderTheSameDMGATEOfTheCallingParty = "22";
        #endregion HyteraTetraDFSI

        #region Server
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_309 = "309";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_415 = "415";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_701 = "701";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_702 = "702";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_703 = "703";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_704 = "704";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_705 = "705";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_706 = "706";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_707 = "707";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_708 = "708";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_709 = "709";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_710 = "710";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_711 = "711";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_712 = "712";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_713 = "713";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_714 = "714";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_715 = "715";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_716 = "716";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_717 = "717";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_718 = "718";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_751 = "751";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_752 = "752";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_753 = "753";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_754 = "754";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_755 = "755";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_756 = "756";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_757 = "757";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_801 = "801";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_802 = "802";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_803 = "803";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_804 = "804";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_805 = "805";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_806 = "806";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_807 = "807";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_808 = "808";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_809 = "809";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_821 = "821";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_822 = "822";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_823 = "823";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_824 = "824";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_825 = "825";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_826 = "826";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_827 = "827";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_828 = "828";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_829 = "829";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_830 = "830";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_831 = "831";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_832 = "832";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_833 = "833";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_834 = "834";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_835 = "835";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_836 = "836";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_837 = "837";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_838 = "838";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_839 = "839";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_840 = "840";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_841 = "841";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_842 = "842";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_843 = "843";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_844 = "844";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_845 = "845";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_846 = "846";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_847 = "847";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_848 = "848";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_901 = "901";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_902 = "902";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_903 = "903";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_904 = "904";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_905 = "905";
        public const string DISPATCH_DISCONNECTED_REASON_SERVER_906 = "906";

        #endregion

        #endregion DisconnectReason

        #region SapType

        /// <summary>
        /// 调度员sap:53
        /// </summary>
        public const string SAPType_DISPATCHER = "53";
        /// <summary>
        /// Hytera DMR数字集群车台:1
        /// </summary>
        public const string SAPType_HYTERA_DMR3_DFSI = "1";
        /// <summary>
        /// Hytera DMR常规（模拟/数字）车台:2
        /// </summary>
        public const string SAPType_HYTERA_DMR2_DFSI = "2";
        /// <summary>
        /// Hytera Tetra数字车台:3
        /// </summary>
        public const string SAPType_HYTERA_TETRA_DFSI = "3";
        /// <summary>
        /// Hytera MPT模拟常规车台:4
        /// </summary>
        public const string SAPType_HYTERA_MPT_CFSI = "4";
        /// <summary>
        /// Hytera PDT数字集群系统:5
        /// </summary>
        public const string SAPType_HYTERA_DMR3_SSI = "5";
        /// <summary>
        /// HMF Tetra数字集群系统:6
        /// </summary>
        public const string SAPType_HMF_Tetra_SSI = "6";
        /// <summary>
        /// Hytera MPT模拟集群系统:7
        /// </summary>
        public const string SAPType_HYTERA_MPT_CSSI = "7";
        /// <summary>
        /// Motorola MPT 模拟集群系统:8
        /// </summary>
        public const string SAPType_MOTO_Tetra_SSI = "8";
        /// <summary>
        /// 公共电话系统:9
        /// </summary>
        public const string SAPType_IPPBX = "9";
        /// <summary>
        /// agat tetra ssi:10
        /// </summary>
        public const string SAPType_AGAT_Tetra_SSI = "10";

        /// <summary>
        /// CrossPatch为了便于处理，规定一个SAPType:11
        /// </summary>
        public const string SAPType_CrossPatch = "11";
        /// <summary>
        /// 12  
        /// </summary>
        public const string SAPType_Hytera_DMR3_ISSI = "12";
        /// <summary>
        /// 调度台之间的呼叫:13
        /// </summary>
        public const string SAPType_HYTERA_Intercom = "13";

        /// <summary>
        /// 录音回放：14
        /// </summary>
        public const string SAPType_HYTERA_RecordReplay = "14";

        /// <summary>
        /// EADS系统网关：15
        /// </summary>
        public const string SAPType_EADS_Tetra_SSI = "15";

        /// <summary>
        /// DMR常规中转台:16
        /// </summary>
        public const string SAPType_HYTERA_DMR2_Repeater = "16";

        /// <summary>
        /// TM800 MPT模拟车台:17
        /// </summary>
        public const string SAPType_HYTERA_TM800_MPT_AFSI = "17";
        /// <summary>
        /// TM800 常规模拟车台：18
        /// </summary>
        public const string SAPType_HYTERA_TM800_Conventional_AFSI = "18";
        /// <summary>
        /// DMR常规同播系统有线网关：19
        /// </summary>
        public const string SAPType_HYTERA_DMR2_Simulcast_CSSI = "19";
        /// <summary>
        /// DMR常规同播系统子网网关：20
        /// </summary>
        public const string SAPType_HYTERA_DMR2_Simulcast_Subnet = "20";
        public const string SAPType_SQ = "21";
        /// <summary>
        /// DMR常规XPT网关：22
        /// </summary>
        public const string SAPType_DMR2_XPT = "22";
        public const string SAPType_Hytera_DMR2_AIS = "25";

        public const string SAPType_Hytera_DMR3_AIS = "26";
        public const string SAPType_Custom2TetraSSI = "24";

        public const string SAPType_HYTERA_MPT_ISSI = "23";


        /// <summary>
        /// 哈尔滨北斗接入
        /// </summary>
        public const string SAPType_Custom3_BeiDou = "27";

        /// <summary>
        /// PSTN系统对接,FXO网关接入
        /// </summary>
        public const string SAPType_Hytera_FXO_Trunking = "28";

        /// <summary>
        /// PSTN系统对接,FXS网关接入
        /// </summary>
        public const string SAPType_Hytera_FXS_PostsPort = "29";

        /// <summary>
        /// GSM 接入
        /// </summary>
        public const string SAPType_Hytera_GSM = "30";
        /// <summary>
        /// Hytera LTE 模拟集群系统：31
        /// </summary>
        public const string SAPType_Hytera_LTE_CSSI = "31";

        /// <summary>
        /// 华为eLte
        /// </summary>
        public const string SAPType_HuaWei_LTE = "32";

        /// <summary>
        ///Dias 系统：33
        /// </summary>
        public const string SAPType_Hytera_DS_CSSI = "33";

        /// <summary>
        /// HUIMINGJIE公司
        /// </summary>
        public const string SAPType_Hytera_HMJ340M = "34";

        /// <summary>
        ///DC智慧警车系统SAP：44
        /// </summary>
        public const string SAPType_Hytera_Police_LED = "44";
        /// <summary>
        ///DC智慧警车系统SAP：45
        /// </summary>
        public const string SAPType_Hytera_Police_Alert = "45";
        /// <summary>
        ///DC智慧警车系统SAP：46
        /// </summary>
        public const string SAPType_Hytera_Police_CarKey = "46";
        /// 大华公司
        /// </summary>
        public const string SAPType_Hytera_DaHua_GPS = "35";
        /// <summary>
        /// 科达公司
        /// </summary>
        public const string SAPType_Hytera_KeDa_GPS = "36";

        /// <summary>
        /// 英福泰科
        /// </summary>
        public const string SAPType_Hytera_YingFuTaiKe_GPS = "37";

        public const string SAPType_Hytera_DMR2_VirtualDC = "38";

        public const string SAPType_Codan_HF = "39";
        public const string SAPType_FengHuo_HF = "40";
        public const string SAPType_Haige_HF = "41";

        public const string SAPType_Hytera_DMR3_ISSI_DISP = "42";

        public const string SAPType_Video_GB28181_1 = "52";

        /// <summary>
        /// Conference为了便于处理，规定一个SAPType:55
        /// </summary>
        public const string SAPType_Conference = "55";
        public const string SAPType_Video_GB28181_57 = "57";

        #endregion

        #region EncodeType
        /// <summary>
        /// 2进制数据
        /// </summary>
        public const string ENCODE_BIN_FORMAT = "0";
        /// <summary>
        /// MS地址
        /// </summary>
        public const string ENCODE_MSN_FORMAT = "1";
        /// <summary>
        /// 4BIT BCD码
        /// </summary>
        public const string ENCODE_BCD_FORMAT = "2";
        /// <summary>
        /// ISO 7 BIT 字符集
        /// </summary>
        public const string ENCODE_ISO7_FORMAT = "3";
        /// <summary>
        /// ISO 8 BIT 字符集
        /// </summary>
        public const string ENCODE_ISO8_FORMAT = "4";
        /// <summary>
        /// NMEA定位编码
        /// </summary>
        public const string ENCODE_NMEA_FORMAT = "5";
        /// <summary>
        /// IP地址
        /// </summary>
        public const string ENCODE_IP_ADDRESS = "6";
        /// <summary>
        /// 16BIT unicode
        /// </summary>
        public const string ENCODE_16BIT_UNICODE = "7";
        /// <summary>
        /// 8BIT 字节
        /// </summary>
        public const string ENCODE_8BIT_FORMAT = "8";

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
        /// 全网:3
        /// </summary>
        public const string NUMBERTYPE_BROADCAST = "3";
        /// <summary>
        /// DGNA动态组:4
        /// </summary>
        public const string NUMBERTYPE_DGNA_GROUP = "4";
        /// <summary>
        /// 工作站号码（含调度台）:5
        /// </summary>
        public const string NUMBERTYPE_WORKSTATION = "5";
        /// <summary>
        /// fleet 号码:6
        /// </summary>
        public const string NUMBERTYPE_FLEET = "6";
        /// <summary>
        /// 调度台用户:7
        /// </summary>
        public const string NUMBERTYPE_DISPATCHER = "7";

        /// <summary>
        /// CrossPatch 号码:8,直接使用CrossPatch的GUID作为号码
        /// </summary>
        public const string NUMBERTYPE_CROSSPATCH = "8";

        /// <summary>
        /// SimulSelect号码:9，仅用来标识面板类型
        /// </summary>
        public const string NUMBERTYPE_SIMULSELECT = "9";
        /// <summary>
        /// VirtualCrosspatch 
        /// </summary>
        public const string NUMBERTYPE_VirtualCROSSPATCH = "10";

        /// <summary>
        /// 包容呼叫 
        /// </summary>
        public const string NUMBERTYPE_Include = "12";
        /// <summary>
        /// Conference 
        /// </summary>
        public const string NUMBERTYPE_CONFERENCE = "11";
        /// <summary>
        /// SAP 
        /// </summary>
        public const string NUMBERTYPE_SAP = "9999";
        #endregion

        #region SystemType

        public const string SystemType_HyteraDMRConventional = "1";
        public const string SystemType_HyteraDMRTrunking = "2";
        public const string SystemType_HyteraPDT = "3";
        public const string SystemType_HyteraMPT = "4";
        public const string SystemType_HyteraAnalogConventional = "5";
        public const string SystemType_HMFTetra = "6";
        public const string SystemType_EADSTetra = "7";
        public const string SystemType_MOTOTetra = "8";
        public const string SystemType_AGATTetra = "9";
        public const string SystemType_PSTN = "10";
        public const string SystemType_Reserved = "11";
        public const string SystemType_HyteraT2 = "12";
        public const string SystemType_HyteraIntercom = "13";
        public const string SystemType_HyteraDMR2Simulcast = "14";
        public const string SystemType_HyteraDMR2XPT = "15";
        public const string SystemType_Custom2Tetra = "16";
        public const string SystemType_Custom3_BeiDou = "17";//北斗接入
        public const string SystemType_HyteraFXO = "18";
        public const string SystemType_HyteraFXS = "19";
        public const string SystemType_Hytera_GSM = "20";
        public const string SystemType_HyteraLTE = "21";
        public const string SystemType_HuaWeiLTE = "22";
        public const string SystemType_DS_System = "23";
        public const string SystemType_DC_System = "30";

        public const string SystemType_HuiMingJie = "24";
        public const string SystemType_DaHua = "25";
        public const string SystemType_KeDa = "26";
        public const string SystemType_YingFuTaiKe = "27";
        public const string SystemType_HF = "28";
        public const string SystemType_HIKVISION = "29";

        public const string SystemType_Video_GB28181_1 = "33";
        public const string SystemType_Video_GB28181_37 = "37";
        #endregion

        #region GroupType
        //  public const string GroupType_Digital_Group = "100";
        public const string NUMBERTYPE_Simulation_Group_CTCSS = "101";
        public const string NUMBERTYPE_Simulation_Group_CDCSS = "102";
        public const string NUMBERTYPE_Simulation_Group_CDCSS_Invert = "103";
        //由于同播系统数字组与亚音频组可能存在相同的GroupID,所以要加入一个字母以区别
        public const string SimulationIdentify = "9999";
        #endregion
        #region CallType

        /// <summary>
        /// "0"：individual call      个呼 point to point call
        /// </summary>
        public const string CallType_Individual = "0";
        /// <summary>
        /// "1"：group call          组呼 point to multipoint call
        /// </summary>
        public const string CallType_Group = "1";
        /// <summary>
        /// "2"：system call         系统全呼
        /// </summary>
        public const string CallType_System = "2";
        /// <summary>
        /// "3"：broadcast call       广播呼叫 broadcast call
        /// </summary>
        public const string CallType_Broadcast = "3";
        /// <summary>
        /// "4"：external call 外部呼叫
        /// </summary>
        public const string CallType_External = "4";
        /// <summary>
        /// "5"：remote monitor call  远程监听呼叫
        /// </summary>
        //public const string CallType_RemoteMonitor = "5";
        /// <summary>
        /// "6"：analog call 模拟呼叫
        /// </summary>
        public const string CallType_Analog = "6";
        /// <summary>
        /// "7": emergency call  紧急呼叫
        /// </summary>
        public const string CallType_Emergency = "7";
        /// <summary>
        /// "8": CrossPatch
        /// </summary>
        public const string CallType_CrossPatch = "8";

        /// <summary>
        /// "9": Intercom
        /// </summary>
        public const string CallType_Intercom = "9";
        /// <summary>
        /// "10":RecordReplay
        /// </summary>
        public const string CallType_RecordReplay = "10";

        /// <summary>
        /// 11: individual call with vedio and voice //可视单呼
        /// </summary>
        public const string CallType_Individual_VideoAndVoc = "11";

        /// <summary>
        /// 12: individual call only vedio        //视频单呼(无语音)
        /// </summary>
        public const string CallType_Individual_Video = "12";
        /// <summary>
        /// 13: group call with vedio and voice   //视频组呼(带语音)
        /// </summary>
        public const string CallType_Group_VideoAndVoc = "13";
        /// <summary>
        /// 14: group call only vedio           //仅视频组呼(无语音)
        /// </summary>
        public const string CallType_Group_Video = "14";
        /// <summary>
        /// 15: group call with different source   //不同源可视组呼
        /// </summary>
        public const string CallType_Group_DifferentSource = "15";
        /// <summary>
        /// 16: 语音组播 
        /// </summary>
        public const string CallType_Broadcast_Voc = "16";
        /// <summary>
        /// 17: broadcast call with vedio  //视频组播
        /// </summary>
        public const string CallType_Broadcast_Video = "17";
        /// <summary>
        /// 18：broadcast call only vedio //仅视频组播
        /// </summary>
        public const string CallType_Broadcast_OnlyVideo = "18";
        /// <summary>
        /// 19: pull vedio call //视频上拉
        /// </summary>
        public const string CallType_PullVedioCall = "19";
        /// <summary>
        /// 20:视频下拉
        /// </summary>
        //public const string CallType_PullVedioCallByRadio = "20";
        /// <summary>
        /// 21:视频回传
        /// </summary>
        public const string CallType_VideoCallBack = "21";
        /// <summary>
        /// 22: push vedio call视频推送
        /// </summary>
        public const string CallType_PushVedioCall = "22";
        /// <summary>
        /// 23:pull vedio call with voice //视频上拉带语音,供eLTE使用和hytera使用
        /// </summary>
        public const string CallType_PullVedioCallWithVoice = "23";
        /// <summary>
        /// 24:视频回传, 供eLTE使用
        /// </summary>
        public const string CallType_VedioFeedbackWithVoice = "24";
        /// <summary>
        /// 25: 调度台呼叫调度台_视频和语音
        /// </summary>
        public const string CallType_IntercomVedioAndVoc = "25";

        /// <summary>
        /// 26: 调度台呼叫调度台_仅视频
        /// </summary>
        public const string CallType_IntercomVedioCall = "26";

        /// <summary>
        /// 27: 会议呼叫_仅音
        /// </summary>
        public const string CallType_ConferenceCallWithVoice = "27";

        /// <summary>
        /// 28: 会议呼叫_视频和语音
        /// </summary>
        public const string CallType_ConferenceCallWithVideoAndVoice = "28";

        /// <summary>
        /// 29: 包容呼叫
        /// </summary>
        public const string CallType_IncludeCall = "29";
        #endregion

        #region SDSType
        public const string SDSType_SDS1 = "1";
        public const string SDSType_SDS2 = "2";
        public const string SDSType_SDS3 = "3";
        public const string SDSType_SDS4 = "4";
        public const string SDSType_StatusMesage = "5";
        public const string SDSType_CommonMesage = "6";
        public const string SDSType_Emergency = "7";
        public const string SDSType_MediaMessage = "8";//彩信wdz20161130
        public const string SDSType_BinaryMessage = "9";//二进制短信 add by malina
        public const string SDSSend_Status1 = "1";
        public const string SDSSend_Status2 = "2";
        public const string SDSSend_Status3 = "3";
        #region SDSAckResult
        public const string SDSAckResult_SendSucess = "0";
        public const string SDSAckResult_SendSucess_lds_device_receive = "1";
        public const string SDSAckResult_SendSucess_ms_device_receive = "2";
        public const string SDSAckResult_SendSucess_system_receive = "3";
        public const string SDSAckResult_Unknown = "-1";
        public const string SDSAckResult_Number_error = "-2";
        public const string SDSAckResult_Recipient_offline = "-3";
        public const string SDSAckResult_Number_disable = "-4";
        public const string SDSAckResult_Permission_denied = "-5";
        public const string SDSAckResult_Channel_busy = "-6";
        public const string SDSAckResult_Rx_only = "-7";
        public const string SDSAckResult_Low_bettery = "-8";
        public const string SDSAckResult_Pll_unlock = "-9";
        public const string SDSAckResult_Call_no_ack = "-10";
        public const string SDSAckResult_Repeater_wakeup_fail = "-11";
        public const string SDSAckResult_No_contact = "-12";
        public const string SDSAckResult_Tx_deny = "-13";
        public const string SDSAckResult_Tx_interrupted = "-14";
        #endregion

        #endregion

        #region CallMode
        /// <summary>
        /// "0"：音频呼叫
        /// </summary>
        public const string CallMode_Audio = "0";
        /// <summary>
        /// "1"：视频呼叫
        /// </summary>
        public const string CallMode_Video = "1";
        /// <summary>
        /// "2"：音视频呼叫
        /// </summary>
        public const string CallMode_AudioAndVideo = "2";
        #endregion

        #region VideoCall_Type
        /// <summary>
        /// "0"：视频上拉
        /// </summary>
        public const string VideoCall_Type_Request = "0";
        /// <summary>
        /// "2"：视频下推
        /// </summary>
        public const string VideoCall_Type_Send = "2";
        /// <summary>
        /// "3"：视频全双工
        /// </summary>
        public const string VideoCall_Type_Duplex = "3";
        #endregion

        #region VideoSource_Type
        /// <summary>
        /// "0"：本地（将本地摄像头的视频或者视频文件下推到指定设备或者终端）
        /// </summary>
        public const string VideoSource_Type_Local = "0";
        /// <summary>
        /// "2"：其他呼叫（videosource_callid指定对于的呼叫ID）:将客户端当前接收的某一路呼叫的视频下推到指定设备或者终端
        /// </summary>
        public const string VideoSource_Type_Other = "2";
        #endregion

        #region  CallPriority
        public static string CallPriority_Common = "0";  //默认级别
        public static string CallPriority_First = "14";  //优先级别
        public static string CallPriority_Emergency = "15";//紧急级别
        #endregion

        #region  RingMode
        public static string RingMode_vibration = "0";  //同振
        public static string RingMode_Directvibration = "1";  //顺振
        #endregion

        #region Lock_State  锁定状态

        /// <summary>
        /// 未被锁定
        /// </summary>
        public const string Lock_State_None = "0";
        /// <summary>
        /// 遥晕
        /// </summary>
        public const string Lock_State_Stun = "1";
        /// <summary>
        /// 遥醒
        /// </summary>
        public const string Lock_State_Revive = "2";
        /// <summary>
        /// 遥毙
        /// </summary>
        public const string Lock_State_Kill = "3";
        /// <summary>
        /// 预遥晕
        /// </summary>
        public const string Lock_State_Stun_Preview = "4";
        /// <summary>
        /// 预遥醒
        /// </summary>
        public const string Lock_State_Revive_Preview = "5";
        /// <summary>
        /// 预遥毙
        /// </summary>
        public const string Lock_State_Kill_Preview = "6";
        #endregion

        #region GrantStatus

        /// <summary>
        /// PTT空闲:0
        /// </summary>
        public static string GrantStatus_PTTIDLE = "0";
        /// <summary>
        /// 得到授权：1
        /// </summary>
        public static string GrantStatus_GRANTED = "1";
        /// <summary>
        /// 另一方得到授权:2
        /// </summary>
        public static string GrantStatus_GRANTTOANOTHERUSER = "2";
        /// <summary>
        /// 未得到授权：3
        /// </summary>
        public static string GrantStatus_NOTGRANTED = "3";
        /// <summary>
        /// 请求排队中  (不使用)：4
        /// </summary>
        public static string GrantStatus_QUEUED = "4";

        #endregion

        #region Duplexs
        public const string DuplexMode_AllPlex = "1";
        public const string DuplexMode_HalfPlex = "0";
        #endregion

        #region Call Subjects
        public static readonly List<string> CallSubjects_Duplexs = new List<string>() { "0", "1" };
        public static readonly List<string> CallSubjects_AmbListenings = new List<string>() { "0", "1" };
        /// <summary>
        /// 0为直通、1为非直通
        /// </summary>
        public static readonly List<string> CallSubjects_HookSignalings = new List<string>() { "0", "1" };
        public static readonly List<string> CallSubjects_E2EEncryptions = new List<string>() { "0", "1" };
        public const string CallListingFlag = "1";//大于等于1为监听
        public const string CallNotListing = "0";//null和0都代表非监听
        public const string CallSubjects_EmgPri = "15";
        public const string CallSubjects_HighPri = "14";
        public const string CallSubjects_AmbListenDemandTxPri = "3";
        public const string CallSubjects_DefautPri = "1";
        public const string CallSubjects_ForInterPri = "15";
        public const string TxDemandPriority_Low = "0";
        public const string TxDemandPriority_High = "1";
        public const string TxDemandPriority_PreEmptive = "2";
        public const string TxDemandPriority_Emergency = "3";

        #endregion

        #region EmergencyAlarm
        public const string AlarmStatus_Cancel = "0";//取消告警
        public const string AlarmStatus_Mark = "1";//收到告警
        public const string AlarmStatus_Receive = "2";//接收告警

        public const string AlarmType_DMR2_Alarm = "1";//告警(无呼叫，无Gps)
        public const string AlarmType_DMR2_AlarmAndCall = "2";//告警带呼叫(无Gps)
        public const string AlarmType_DMR2_AlarmAndGps = "3";//告警带Gps(无呼叫)
        public const string AlarmType_DMR2_AlarmAndCallAndGps = "4";//告警带Gps和呼叫

        public const string AlarmType_Trunking_AlarmAndCall = "5";//紧急呼叫
        public const string AlarmType_Trunking_ObviousAlarm = "6";//显式告警
        public const string AlarmType_Trunking_SecretAlarm = "7";//隐式告警
        #endregion

        #region RRSStatus
        public const string RRSStatus_Offline = "0";//取消告警
        public const string RRSStatus_Online = "1";//收到告警
        #endregion

        #region GeofencingAlarm

        public const string RegionAlarmType_leavealarm = "0";//离开告警
        public const string RegionAlarmType_enteralarm = "1";//进入告警
        public const string RegionAlarmStatus_resolvealarm = "0";//取消告警
        public const string RegionAlarmStatus_activealarm = "1";//产生告警

        #endregion

        #region XmlNodeName
        public const string XML_PAYLOAD = "payload";
        public const string XML_SYSTEM_TYPE = "systemtype";
        public const string XML_CMD_GUID = "cmdguid";
        public const string XML_CMD_NAME = "cmdname";
        #endregion

        #region Command Mode
        /// <summary>
        /// 命令处理模式为发送时处理
        /// </summary>
        public const string CommandMode_Out = "Out";

        /// <summary>
        /// 命令处理模式为接收时处理
        /// </summary>
        public const string CommandMode_In = "In";
        #endregion

        #region PluginLicenseName
        public const string PluginLicenseName_AgatTetraSSI = "Hytera.PUC.AgatTetraSSI";
        public const string PluginLicenseName_AVL = "Hytera.PUC.AVL";
        public const string PluginLicenseName_AVLForICC = "Hytera.PUC.AVLForICC";
        public const string PluginLicenseName_CAPF = "Hytera.PUC.CAPF";
        public const string PluginLicenseName_Browser = "Hytera.PUC.Browser";
        public const string PluginLicenseName_Clock = "Hytera.PUC.Clock";
        public const string PluginLicenseName_GPSControl = "Hytera.PUC.GPSControl";
        public const string PluginLicenseName_CrossPatch = "Hytera.PUC.CrossPatch";
        public const string PluginLicenseName_Conference = "Hytera.PUC.Conference";
        public const string PluginLicenseName_IncludeCall = "Hytera.PUC.IncludeCall";
        public const string PluginLicenseName_DGNA = "Hytera.PUC.DGNA";
        public const string PluginLicenseName_DialPad = "Hytera.PUC.DialPad";
        public const string PluginLicenseName_EADSTetraSSI = "Hytera.PUC.EADSTetraSSI";
        public const string PluginLicenseName_HistoryTrack = "Hytera.PUC.HistoryTrack";
        public const string PluginLicenseName_HyteraDMR2DFSI = "Hytera.PUC.HyteraDMR2DFSI";
        public const string PluginLicenseName_HyteraMPTCFSI = "Hytera.PUC.HyteraMPTCFSI";
        public const string PluginLicenseName_HyteraMPTCSSI = "Hytera.PUC.HyteraMPTCSSI";
        public const string PluginLicenseName_HyteraMPTISSI = "Hytera.PUC.HyteraMPTISSI";
        public const string PluginLicenseName_HyteraMPT800AFSI = "Hytera.PUC.HyteraTM800MPTAFSI";
        public const string PluginLicenseName_HyteraPDTTrunking = "Hytera.PUC.HyteraPDTTrunking";
        public const string PluginLicenseName_HyteraTetraDFSI = "Hytera.PUC.HyteraTetraDFSI";
        public const string PluginLicenseName_Intercom = "Hytera.PUC.Intercom";
        public const string PluginLicenseName_MultiGridVideo = "Hytera.PUC.MultiGridVideo";
        public const string PluginLicenseName_HyteraGB28181 = "Hytera.PUC.HyteraGB28181";
        public const string PluginLicenseName_Media = "Hytera.PUC.Media";
        public const string PluginLicenseName_Pic = "Hytera.PUC.Pic";
        public const string PluginLicenseName_BaseStationMonitor = "Hytera.PUC.HyteraBaseStationMonitor";
        public const string PluginLicenseName_PSTN = "Hytera.PUC.PSTN";
        public const string PluginLicenseName_Report = "Hytera.PUC.Report";
        public const string PluginLicenseName_RichText = "Hytera.PUC.RichText";
        public const string PluginLicenseName_SDS = "Hytera.PUC.SDS";
        public const string PluginLicenseName_SimulSelect = "Hytera.PUC.SimulSelect";
        public const string PluginLicenseName_Video = "Hytera.PUC.Video";
        public const string PluginLicenseName_Freecomm = "Hytera.PUC.Freecomm";
        public const string PluginLicenseName_HyteraDMR2Simulcast = "Hytera.PUC.HyteraDMR2Simulcast";
        public const string PluginLicenseName_HyteraCORMDFSI = "Hytera.PUC.EMSignal";
        public const string PluginLicenseName_HyteraDMR2XPT = "Hytera.PUC.HyteraDMR2XPT";
        public const string PluginLicenseName_Custom2TetraSSI = "Hytera.PUC.Custom2TetraSSI";
        public const string PluginLicenseName_HyteraDMR3AIS = "Hytera.PUC.HyteraDMR3AIS";
        public const string PluginLicenseName_HyteraGSM = "Hytera.PUC.HyteraGSM";
        public const string PluginLicenseName_HyteraPSTNFXS = "Hytera.PUC.HyteraFXSPotsPort";
        public const string PluginLicenseName_HyteraPSTNFXO = "Hytera.PUC.HyteraFXOTrunking";
        public const string PluginLicenseName_HyteraLTECSSI = "Hytera.PUC.HyteraLTECSSI";
        public const string PluginLicenseName_IndoorLocation = "Hytera.PUC.IndoorLocation";//add by xjf 2016年9月7日 室内定位
        public const string PluginLicenseName_HMJ340M = "Hytera.PUC.HMJ340M";
        public const string PluginLicenseName_Custom3HFDFSI = "Hytera.PUC.Custom3HFDFSI";
        public const string PluginLicenseName_HyteraBaseStationMonitor = "Hytera.PUC.HyteraBaseStationMonitor";
        public const string PluginLicenseName_COR_MD_FSI = "COR_MD_FSI";
        public const string PluginLicenseName_SystemMonitor = "Hytera.PUC.SystemMonitor";// add by wdz 2016-12-12 lte系统呼叫信息订阅
        public const string PluginLicenseName_Fax = "Hytera.PUC.FaxManage"; //传真
        public const string PluginLicenseName_HuaWeiLTE = "Hytera.PUC.HuaWeiLTE";
        public const string PluginLicenseName_CallRouteManager = "CallRouteManager";
        public const string PluginLicenseName_HIKVISIONIPC = "Hytera.PUC.HIKVISIONIPC"; // 海康威视
        public const string PluginLicenseName_HyteraEVK = "Hytera.PUC.HyteraEVK"; // 实时流视频（执法记录仪）
        public const string PluginLicenseName_RollCall = "RollCallPlugin";//add by xjf 2018年4月17日 RollCall整合
        #endregion

        #region FunctionLicenseName
        public const string FunctionLicenseName_Individualcall = "individualcall";
        public const string FunctionLicenseName_Groupcall = "Groupcall";
        public const string FunctionLicenseName_Systemcall = "systemcall";
        public const string FunctionLicenseName_Encryptcall = "EncryptCall";
        public const string FunctionLicenseName_Intercom = "intercom";
        public const string FunctionLicenseName_Pstn = "pstn";
        public const string FunctionLicenseName_Simulselect = "simulselect";
        public const string FunctionLicenseName_Broadcastcall = "Broadcastcall";
        public const string FunctionLicenseName_Emergencycall = "emergencycall";
        public const string FunctionLicenseName_Crosspatchcall = "crosspatchcall";
        public const string FunctionLicenseName_Conferencecall = "Conference";
        public const string FunctionLicenseName_Duplexcall = "duplexcall";
        public const string FunctionLicenseName_CallTransfer = "CallTransfer";
        public const string FunctionLicenseName_Multipartycall = "multipartycall";
        public const string FunctionLicenseName_CallHoldCallRestore = "CallHoldCallRestore";
        public const string FunctionLicenseName_GroupPatch = "GroupPatch";
        public const string FunctionLicenseName_Dynamicgroup = "Dynamicgroup";
        public const string FunctionLicenseName_Monitor = "monitor";
        public const string FunctionLicenseName_Environmentmonitor = "Environmentmonitor";
        public const string FunctionLicenseName_Callonchannel = "Callonchannel";
        public const string FunctionLicenseName_GPS = "GPS";
        public const string FunctionLicenseName_Googlemap = "Googlemap";
        public const string FunctionLicenseName_OfflineGooglemap = "OfflineGooglemap";
        public const string FunctionLicenseName_MetroGooglemap = "GoogleMapMetro";
        public const string FunctionLicenseName_PGIS = "PGIS";
        public const string FunctionLicenseName_Mapinfo = "Mapinfo";
        public const string FunctionLicenseName_OpenStreetmap = "OpenStreetmap";//jdm OpenStreet
        public const string FunctionLicenseName_CustomizedMap = "CustomizedMap";//jdm CustomizedMap
        public const string FunctionLicenseName_AMap = "AMap";//jdm AMap
        public const string FunctionLicenseName_HeavenAndEarthMap = "HeavenAndEarthMap";//jdm HeavenAndEarthMap
        public const string FunctionLicenseName_Geographicalarm = "GeoDefenceAlarm";
        public const string License_Function_InterestPoint = "InterestPoint";
        public const string FunctionLicenseName_Singlegpspull = "singlegpspull";
        public const string FunctionLicenseName_Callonmap = "callonmap";
        public const string FunctionLicenseName_Dgnaonmap = "dgnaonmap";
        public const string FunctionLicenseName_Gpspullmain = "gpspullmain";
        public const string FunctionLicenseName_Gpspullslave = "gpspullslave";
        public const string FunctionLicenseName_Systemgpspull = "systemgpspull";
        public const string FunctionLicenseName_Partgpspull = "partgpspull";
        public const string FunctionLicenseName_Historytrack = "historytrack";
        public const string FunctionLicenseName_SDS = "SDS";
        public const string FunctionLicenseName_Favorite = "Favorite";
        public const string FunctionLicenseName_Textmessage = "Textmessage";
        public const string FunctionLicenseName_Statusmessage = "Statusmessage";
        public const string FunctionLicenseName_EmergencyAlarm = "EmergencyAlarm";
        public const string FunctionLicenseName_EmergencyAlarmQuery = "EmergencyAlarmQuery";
        public const string FunctionLicenseName_Callbackrequest = "Callbackrequest";
        public const string FunctionLicenseName_SecurityService = "SecurityService";
        public const string FunctionLicenseName_Stun = "Stun";
        public const string FunctionLicenseName_Revive = "Revive";
        public const string FunctionLicenseName_Kill = "Kill";
        public const string FunctionLicenseName_SystemCtrl = "SystemCtrl";
        public const string FunctionLicenseName_Modeset = "Modeset";
        public const string FunctionLicenseName_Defgroupset = "Defgroupset";
        public const string FunctionLicenseName_Switchchannel = "Switchchannel";
        public const string FunctionLicenseName_ReportQuery = "ReportQuery";
        public const string FunctionLicenseName_CallhistoryQuery = "callhistoryQuery";
        public const string FunctionLicenseName_SDShistoryQuery = "SDShistoryQuery";
        public const string FunctionLicenseName_RRShistoryQuery = "RRShistoryQuery";
        public const string FunctionLicenseName_GeographicalarmQuery = "geographicalarmQuery";
        public const string FunctionLicenseName_AllQuery = "allQuery";
        public const string FunctionLicenseName_RRSServer = "RRSServer";
        public const string FunctionLicenseName_DialPad = "DialPad";
        public const string FunctionLicenseName_Pic = "Pic";
        public const string FunctionLicenseName_Browser = "Browser";
        public const string FunctionLicenseName_RichText = "RichText";
        public const string FunctionLicenseName_Media = "Media";
        public const string FunctionLicenseName_Clock = "Clock";
        public const string FunctionLicenseName_BaseStationMonitor = "BaseStationMonitor";
        public const string FunctionLicenseName_Video = "video";
        public const string FunctionLicenseName_Allgpscyclereport = "Allgpscyclereport";
        public const string FunctionLicenseName_Overtheairprogramming = "overtheairprogramming";
        public const string FunctionLicenseName_End2EndEncryption = "end2end_encryption";
        public const string FunctionLicenseName_RecordPlay = "RecordPlay";
        public const string FunctionLicenseName_RecordFileDownload = "RecordFileDownload";
        public const string FunctionLicenseCount_Custombackup1 = "Custombackup1";
        public const string FunctionLicenseName_IndoorLocation = "IndoorLocation";//add by xjf 2016年9月9日 室内定位
        public const string FunctionLicenseName_FaxManage = "Fax"; //传真
        public const string FunctionLicenseName_MediaMessage = "MediaMessage";//多媒体短信
        public const string FunctionLicenseName_GPSControl = "GPSControl";
        public const string FunctionLicenseName_MultiGridVideo = "MultiGridVideo";
        public const string FunctionLicenseName_HIKVISIONIPC = "HIKVISIONIPC"; // 海康威视
        public const string FunctionLicenseName_HyteraEVK = "HyteraEVK"; // 实时流视频（执法记录仪）

        /// <summary>
        /// 捷视飞通公司视频接口
        /// </summary>
        public const string FunctionLicenseName_IFreecommVideo = "iFreecommVideo";
        /// <summary>
        /// 强拆
        /// </summary>
        public const string FunctionLicenseName_OverrideCall = "override";
        /// <summary>
        /// 强插
        /// </summary>
        public const string FunctionLicenseName_InterruptCall = "Interrupt";

        public const string FunctionLicenseName_RollCall = "Roll_Call";//add by xjf 2018年4月17日 RollCall整合

        public const string FunctionLicenseCount_Monitor_Num = "monitor_num";
        public const string FunctionLicenseCount_DGNA_Num = "dgna_num";
        public const string FunctionLicenseCount_DGNAMember_Num = "dgnamember_num";
        public const string FunctionLicenseCount_Simulselect_Num = "simulselect";
        public const string FunctionLicenseCount_SimulselectMember_Num = "simulselect_member";
        public const string FunctionLicenseCount_CrossPatch_Num = "crosspatch";
        public const string FunctionLicenseCount_CrossPatchMember_Num = "crosspatch_member";
        public const string FunctionLicenseCount_Conference_Num = "onference";
        public const string FunctionLicenseCount_ConferenceMember_Num = "conference_member";
        public const string FunctionLicenseCount_Client_Num = "client_num";
        #endregion

        #region Panel Name
        public const string Panel_Name_BaseStationMonitorPanel = "BaseStationMonitorPanel";

        #endregion
        public static string GUID
        {
            //default:dbbfa8e3-9add-4240-bdb7-489ef4b3c9d7,
            //N:fff3b3c62a894301b0938f70f5e57456,
            //D:3efa512b-2600-44a5-8546-ea46c6c04774,
            //B:{1b3c36be-5654-4594-ab62-52aa5c66be41},
            //P:(37153f5b-b637-419e-9193-b8c06f47851c),
            //x:{0xc2ea6fe7,0xb4dc,0x41d8,{0xbc,0x3f,0xaa,0xf2,0x22,0xae,0x32,0x31}}
            get
            {
                return Guid.NewGuid().ToString("B");
            }
        }

        public static string PluginPrefix = "Plugin.";
        public static string Permission_Valid = "1";
        public static string Permission_Invalid = "0";

        public const string DispatcherHierarchy = "[Dispatcher{CBB10F0B-A280-4AD1-B599-54D7F527B167}]";
        public const string OthersHierarchy = "[Others{BBFC5D5B-1D61-44D0-9C68-6423A09A7A93}]";
        public const string FreecommHierarchy = "[Freecomm{C79E8C02-F2EE-48B5-8E21-BE9BBFB00C64}]";
        public const string FreecommDeviceHierarchy = "[Freecomm{97109B69-CFDF-4A76-A3B9-B422AE45E992}]";
        public const string FreecommGroupHierarchy = "[Freecomm{9AC992E1-3B0C-4B42-9B2B-EFA55745021A}]";

        public const string FavoriteOrganizationHierarchyId = "[FavoriteOrg]";
        public const string FavoriteObjectHierarchyId = "[FavoriteObject]";
        public const string FavoriteGroupHierarchyId = "[FavoriteGroup]";
        public const string FavoriteDeviceHierarchyId = "[FavoriteDevice]";
        public const string FavoriteCustomOrgHierarchyId = "[CustomOrganization]";

        public const int DataType_Data = 1;
        public const int DataType_Command = 0;

        public const string Administrator = "admin";
        public const string Root_Organization = "00";

        #region FavoriteType
        public const string FavoriteType_Organization = "0";
        public const string FavoriteType_Device = "1";
        public const string FavoriteType_Group = "2";
        public const string FavoriteType_Dispatcher = "3";
        public const string FavoriteType_Sap = "4";
        public const string FavoriteType_CarStationGroup = "5";
        public const string FavoriteType_Crosspatch = "6";
        public const string FavoriteType_Conference = "7";
        public const string FavoriteType_DGNA = "8";
        public const string FavoriteType_Simulselect = "9";
        #endregion

        # region OperateType base data
        public const string Operate_Add = "Add";
        public const string Operate_Update = "Update";
        public const string Operate_Delete = "Delete";
        #endregion

        #region  ResourceType
        public const string Resource_Organization = "Organization";
        public const string Resource_Device = "Device";
        public const string Resource_Group = "Group";
        public const string Resource_Staff = "Staff";
        #endregion

        public const string CC_Call_Devolve_Request = "cc_call_devolve_request";

        public const string CC_Call_Devolve_Evt = "cc_call_devolve_evt";    //呼叫转移返回

        #region PlaySoundType
        public const string Emgerency = "Emgerency";
        public const string Incomming = "Incomming";
        public const string Connect = "Connect";
        public const string Granted = "Granted";
        public const string SetupCall = "SetupCall";
        public const string Disconnect = "Disconnect";
        public const string ReceiveMsg = "ReceiveMsg";
        public const string CallbackAlert = "CallbackAlert";
#endregion

    }
}
