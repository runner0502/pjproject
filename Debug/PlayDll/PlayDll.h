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
	const int PLAY_STREAM_TYPE_RTP = 0;     // RTP ������
	const int PLAY_STREAM_TYPE_H264 = 1;    // H264 ������
	const int PLAY_STREAM_TYPE_PCM = 2;    // pcm ������
	// ��ȡ����ߵĻص�����

	typedef int(WINAPI *stream_width_height_t)(int nWidth, int nHeight, int nWndHandle);

	// =======================================================================================
	// ------------------------------- ���ſؼ��ӿ� --------------------------------------
	// =======================================================================================

	//////////////////////////////////////////////////////////////////////////////////////////
	//����˵�����������Ź���
	//����ԭ�ͣ�int PlayStart(int nWndHandle, bool bSave = false, stream_width_height_t pStreamWidthHeight = NULL); 
	//���������nWndHandle	        ���Ŵ��ڵľ��
	//          bSave               �Ƿ񱣴���Ϊ�ļ���־��Ĭ��Ϊ������
	//          pStreamWidthHeight  ��ȡ����ߵĻص�����
	//          nEncodeId           ��������(��: AV_CODEC_ID_NONE:0, AV_CODEC_ID_H264:28, AV_CODEC_ID_H265��X, ...)
	//�����������
	//����ֵ��	>=0		�ɹ�
	//			����	ʧ��
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayStart(int nWndHandle, int bSave = 0, stream_width_height_t pStreamWidthHeight = NULL, int nEncodeId = 0);

	//////////////////////////////////////////////////////////////////////////////////////////
	//����˵�����л����Ŵ���
	//����ԭ�ͣ�bool PlayChangeWin(int nWndHandle, int nPlayWndHandle); 
	//���������nWndHandle	   �����ڵľ��
	//���������nPlayWndHandle �л������ŵĴ��ھ��
	//�����������
	//����ֵ��	true	�ɹ�
	//			false	ʧ��
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayChangeWin(int nWndHandle, int nPlayWndHandle);

	//////////////////////////////////////////////////////////////////////////////////////////
	//����˵�����趨��Ƶ��ת����
	//����ԭ�ͣ�int PlaySetDirection(int nWndHandle, int nDirection); 
	//���������nWndHandle	        ���Ŵ��ڵľ��
	//          nDirection          ��Ƶ��ת�ķ���ֵ��Ҫ��90�ı�������-270��-180��-90��0��90��180��270��360��
	//�����������
	//����ֵ��	>=0		�ɹ�
	//			����	ʧ��
	//ע        �ú������������Ź��ܺ��������ò����á�
	//////////////////////////////////////////////////////////////////////////////////////////
	//int PLAYDLL_API PlaySetDirection(int nWndHandle, int nDirection);

	//////////////////////////////////////////////////////////////////////////////////////////
	//����˵����ֹͣ���Ź���
	//����ԭ�ͣ�int PlayStop(int nWndHandle); 
	//���������nWndHandle	���Ŵ��ڵľ��
	//�����������
	//����ֵ��	>=0		�ɹ�
	//			����	ʧ��
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayStop(int nWndHandle);

	//////////////////////////////////////////////////////////////////////////////////////////
	//����˵����ֹͣȫ�����Ź���
	//����ԭ�ͣ�int PlayStopAll(); 
	//�����������
	//�����������
	//����ֵ��	>=0		�ɹ�
	//			����	ʧ��
	//��ע:     �����˳�ǰ��Ҫ����������еĲ�����Դ
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayStopAll();

	//////////////////////////////////////////////////////////////////////////////////////////
	//����˵������Ӧ���յ������ݹ��ܺ���
	//����ԭ�ͣ�int PlayOnRecvData(unsigned char *buffer, int nBufLen, int nWndHandle, int nStreamType = PLAY_STREAM_TYPE_RTP);
	//���������buffer	    ���Ļ�����
	//          nBufLen     ���ĳ���
	//���������nWndHandle	���Ŵ��ڵľ��
	//          nStreamType �������� 0��Ϊrtp��, 1:Ϊh264�� ,Ĭ��Ϊrtp��
	//�����������
	//����ֵ��	>=0		�ɹ�
	//			����	ʧ��
	//////////////////////////////////////////////////////////////////////////////////////////
	PLAYDLL_API int  PlayOnRecvData(unsigned char *buffer, int nBufLen, int nWndHandle, int nStreamType = PLAY_STREAM_TYPE_RTP);

#ifdef __cplusplus
}
#endif

#endif





