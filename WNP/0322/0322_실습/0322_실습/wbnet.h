#pragma once

int wbnet_LibraryInit();

void wbnet_DeleteSocket();
void wbnet_LibraryExit();

void wbnet_CreateSocket(int port, const TCHAR* ip);


DWORD WINAPI RecvThread(void* p);