#pragma once

struct SocketInfo
{
	SOCKET sock;
	WSAEVENT hEvent;
};

bool wbnet_InitLibrary();

bool wbnet_CreateListenSocket(int port);

//소켓 추가
bool AddSocket(SOCKET sock, int networkflag);
bool DeleteSocket(SOCKET sock);

HANDLE wbnet_Run();

unsigned int WINAPI WorkerTrhread(void* pParam);

bool GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents);

bool Accept(SOCKET sock, WSANETWORKEVENTS NetworkEvents);

bool Read(SOCKET sock, WSANETWORKEVENTS NetworkEvents);

bool Close(SOCKET sock, WSANETWORKEVENTS NetworkEvents);