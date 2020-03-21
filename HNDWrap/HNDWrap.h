// HNDWrap.h

#pragma once

typedef void(*onDataCallback)(char* xml);

extern "C" __declspec(dllexport) int __stdcall testcx(int i);
extern "C" __declspec(dllexport) void __stdcall setDataCallback( onDataCallback cb );
extern "C" __declspec(dllexport) bool __stdcall HNDInit();
extern "C" __declspec(dllexport) bool __stdcall HNDDispose();
extern "C" __declspec(dllexport) bool __stdcall HNDMakeCall(char* number);
extern "C" __declspec(dllexport) int __stdcall HNDGetLocalPort();
extern "C" __declspec(dllexport) void __stdcall HNDSetRemoteSipPort(int port);



