
// HNDDemo.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CHNDDemoApp: 
// �йش����ʵ�֣������ HNDDemo.cpp
//

class CHNDDemoApp : public CWinApp
{
public:
	CHNDDemoApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CHNDDemoApp theApp;