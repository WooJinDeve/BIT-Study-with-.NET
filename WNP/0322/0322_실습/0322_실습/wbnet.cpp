#include "std.h"

HANDLE g_hThread;
SOCKET sock;
#define BUF_SIZE 4096


int wbnet_LibraryInit()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)  //버전 초기화(2.2버전)
		return -1;

	sock = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);  // SOCK_DGRAM --> IPPROTO_UDP
	if (sock == INVALID_SOCKET)
		return -1;

	// SO_REUSEADDR 옵션 설정
	int retval;

	BOOL optval = TRUE;
	retval = setsockopt(sock, SOL_SOCKET, SO_REUSEADDR,
		(char*)&optval, sizeof(optval));
	if (retval == SOCKET_ERROR)
		return -1;

	return 0;
}

void wbnet_DeleteSocket()
{
	TerminateThread(g_hThread, 0);
	CloseHandle(g_hThread);

	//closesocket(sock);
}

void wbnet_LibraryExit()
{
	WSACleanup();
}

void wbnet_CreateSocket(int port, const TCHAR* ip)
{
	int retval;

	SOCKADDR_IN			localaddr;
	ZeroMemory(&localaddr, sizeof(localaddr));
	localaddr.sin_family = AF_INET;
	localaddr.sin_port = htons(port);
	localaddr.sin_addr.s_addr = htonl(INADDR_ANY);
	retval = bind(sock, (SOCKADDR*)&localaddr,sizeof(localaddr));

	struct ip_mreq mreq;
	mreq.imr_multiaddr.s_addr = inet_addr((const char*)ip);
	mreq.imr_interface.s_addr = htonl(INADDR_ANY);
	retval = setsockopt(sock, IPPROTO_IP, IP_ADD_MEMBERSHIP, (char*)&mreq, sizeof(mreq));
	if (retval == SOCKET_ERROR)
		return;


	g_hThread = CreateThread(0, 0, RecvThread, (LPVOID)sock, 0, 0);

}

DWORD WINAPI RecvThread(void* p)
{
	SOCKET s = (SOCKET)p;

	//수신
	while (true)
	{
		SOCKADDR_IN c_addr;
		int sz = sizeof(c_addr);

		char buf[BUF_SIZE] = { 0 };

		recvfrom(s, buf, BUF_SIZE, 0, (SOCKADDR*)&c_addr, &sz);
	}
	closesocket(s);
	return 0;
}
