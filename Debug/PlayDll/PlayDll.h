#ifndef __PLAYDLL_INTERFACE_H_
#define __PLAYDLL_INTERFACE_H_

#include <string.h>
#include <vector>
using namespace std;
#ifdef _WIN32
#include <windows.h>
#ifdef PLAYDLL_EXPORTS
#define PLAYDLL_API __declspec(dllexport)
#else
#define PLAYDLL_API __declspec(dllimport)
#endif
#else
#define PLAYDLL_API
#if !defined(__ANDROIDARM__)
#define DWORD unsigned int
#define UINT  unsigned int
#define BYTE  unsigned char

#define WINAPI
#endif
#endif

#ifdef __cplusplus
extern "C" {
#endif
	const int PLAY_STREAM_TYPE_RTP = 0;     // RTP 流类型
	const int PLAY_STREAM_TYPE_H264 = 1;    // H264 流类型
	const int PLAY_STREAM_TYPE_PCM = 2;    // pcm 流类型
	// 获取流宽高的回调函数

	typedef int(WINAPI *stream_width_height_t)(int nWidth, int nHeight, int nWndHandle);

	// =======================================================================================
	// ------------------------------- 播放控件接口 --------------------------------------
	// =======================================================================================

	//////////////////////////////////////////////////////////////////////////////////////////
	//功能说明：启动播放功能
	//函数原型：int PlayStart(int nWndHandle, bool bSave = false, stream_width_height_t pStreamWidthHeight = NULL); 
	//输入参数：nWndHandle	        播放窗口的句柄
	//          bSave               是否保存流为文件标志，默认为不保存
	//          pStreamWidthHeight  获取流宽高的回调函数
	//          nEncodeId           编码类型(如: AV_CODEC_ID_NONE:0, AV_CODEC_ID_H264:28, AV_CODEC_ID_H265：X, ...)
	//输出参数：无
	//返回值：	>=0		成功
	//			负数	失败
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayStart(int nWndHandle, int bSave = 0, stream_width_height_t pStreamWidthHeight = NULL, int nEncodeId = 0);

	//////////////////////////////////////////////////////////////////////////////////////////
	//功能说明：切换播放窗口
	//函数原型：bool PlayChangeWin(int nWndHandle, int nPlayWndHandle); 
	//输入参数：nWndHandle	   主窗口的句柄
	//输入参数：nPlayWndHandle 切换到播放的窗口句柄
	//输出参数：无
	//返回值：	true	成功
	//			false	失败
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayChangeWin(int nWndHandle, int nPlayWndHandle);

	//////////////////////////////////////////////////////////////////////////////////////////
	//功能说明：设定视频旋转方向
	//函数原型：int PlaySetDirection(int nWndHandle, int nDirection); 
	//输入参数：nWndHandle	        播放窗口的句柄
	//          nDirection          视频旋转的方向值（要是90的倍数：如-270，-180，-90，0，90，180，270，360）
	//输出参数：无
	//返回值：	>=0		成功
	//			负数	失败
	//注        该函数在启动播放功能函数后设置才有用。
	//////////////////////////////////////////////////////////////////////////////////////////
	//int PLAYDLL_API PlaySetDirection(int nWndHandle, int nDirection);

	//////////////////////////////////////////////////////////////////////////////////////////
	//功能说明：停止播放功能
	//函数原型：int PlayStop(int nWndHandle); 
	//输入参数：nWndHandle	播放窗口的句柄
	//输出参数：无
	//返回值：	>=0		成功
	//			负数	失败
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayStop(int nWndHandle);

	//////////////////////////////////////////////////////////////////////////////////////////
	//功能说明：停止全部播放功能
	//函数原型：int PlayStopAll(); 
	//输入参数：无
	//输出参数：无
	//返回值：	>=0		成功
	//			负数	失败
	//备注:     程序退出前，要用来清除所有的播放资源
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayStopAll();

	//////////////////////////////////////////////////////////////////////////////////////////
	//功能说明：响应接收到流数据功能函数
	//函数原型：int PlayOnRecvData(unsigned char *buffer, int nBufLen, int nWndHandle, int nStreamType = PLAY_STREAM_TYPE_RTP);
	//输入参数：buffer	    流的缓存区
	//          nBufLen     流的长度
	//输入参数：nWndHandle	播放窗口的句柄
	//          nStreamType 流的类型 0：为rtp流, 1:为h264流 ,默认为rtp流
	//输出参数：无
	//返回值：	>=0		成功
	//			负数	失败
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayOnRecvData(unsigned char *buffer, int nBufLen, int nWndHandle, int nStreamType = PLAY_STREAM_TYPE_RTP);

#ifdef __cplusplus
}
#endif

#endif





