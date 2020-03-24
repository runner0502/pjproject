// 这是主 DLL 文件。

#include "stdafx.h"

#include "HNDWrap.h"
#include "HNDWrapInternal.h"

void OnOnIncomingCallEvent(System::String ^called, System::String ^caller);
void OnOnDataback(System::String ^obj);
void *testnative122(int i)
{
	return __nullptr;
}

onDataCallback g_onDataCallback;
onIncomingCallback g_onIncomingCallback;

using namespace HNDWrap;
using namespace HNDModule;
using namespace ClientDemo;

extern "C" __declspec(dllexport) int __stdcall testcx(int i)
{
	testnative122(1);
	/*HNDWrapInternal ^item = gcnew HNDWrapInternal();
	return item->test();*/



	ClientDemo::BusinessCenter ^bs = gcnew ClientDemo::BusinessCenter();
	bs->OnDataback += gcnew System::Action<System::String ^>(&OnOnDataback);
	return 0;
	
}

extern "C" __declspec(dllexport) void __stdcall setDataCallback(onDataCallback cb)
{
	g_onDataCallback = cb;	
}

extern "C" __declspec(dllexport) void __stdcall setIncomingCallEvent(onIncomingCallback cb)
{
	g_onIncomingCallback = cb;
}


extern "C" __declspec(dllexport) bool __stdcall HNDInit()
{
	ClientDemo::BusinessCenter::Instance->OnDataback += gcnew System::Action<System::String ^>(&OnOnDataback);
	ClientDemo::BusinessCenter::Instance->OnIncomingCallEvent += gcnew ClientDemo::OnIncomingCall(&OnOnIncomingCallEvent);
	Client::GetInstance()->Init();
	Client::GetInstance()->Login();
	return true;
}

extern "C" __declspec(dllexport) bool __stdcall HNDDispose()
{
	Client::GetInstance()->Dispose1();
	return true;
}

extern "C" __declspec(dllexport) bool __stdcall HNDMakeCall( char* number )
{
	System::String^ numberStr = System::Runtime::InteropServices::Marshal::PtrToStringAuto(System::IntPtr(number));
	Client::GetInstance()->MakeCall("80020202");
	return true;
}


extern "C" __declspec(dllexport) int __stdcall HNDGetLocalPort()
{
	return Client::GetInstance()->GetLocalPort();
}

extern "C" __declspec(dllexport) void __stdcall HNDSetRemoteSipPort(int port)
{
	Client::GetInstance()->SetRemoteSipPort(port);
}

void OnOnDataback(System::String ^obj)
{
	testnative122(1);
	if (g_onDataCallback != nullptr)
	{
		char *para1d = "22";
		g_onDataCallback(para1d);
	}
	
}

void OnOnIncomingCallEvent(System::String ^called, System::String ^caller)
{
	if (g_onIncomingCallback != nullptr)
	{
		g_onIncomingCallback("called", "caller");
	}
}
