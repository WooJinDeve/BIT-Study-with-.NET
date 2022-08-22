#pragma once

struct SocketInfo
{
	SOCKET sock;
	WSAEVENT hEvent;
};

bool wbnet_InitLibrary();

bool wbnet_CreateSocket(const char* ip, int port);

unsigned int __stdcall ReciveThread(void* param);

int wbnet_SenData(char* buf, int length);

void wbnet_ExitLibrary();