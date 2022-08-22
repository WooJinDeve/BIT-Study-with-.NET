//wbfilenet.cpp

#include "std.h"

#define BUF_SIZE 4096
HANDLE g_hThread;

char g_multiIP[6][20] = { "", "234.5.5.1","234.5.5.2",
					"234.5.5.3","234.5.5.4" ,"234.5.5.5" };

RecvFunc RecvData;

//전역변수로 수정한 이유
//로그아웃시 소켓을 종료하기 위함
//로그아웃시 소켓을 종료하지 않으면 재 로그인이 불가능함
SOCKET g_recvSocket, g_sendSocket;  

bool wbnet_LibraryInit()
{
	WSADATA wsadata;

	if (WSAStartup(MAKEWORD(2, 2), &wsadata) != 0)
		return false;
	else
		return true;
}
  
void wbnet_LibraryExit()
{
	WSACleanup();
}

bool wbnet_CreateSocket(int port, int groupid, RecvFunc fun)
{
	RecvData = fun;

	g_recvSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	g_sendSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

	// SO_REUSEADDR 옵션 설정
	BOOL optval = TRUE;
	int retval = setsockopt(g_recvSocket, SOL_SOCKET, SO_REUSEADDR,
		(char*)&optval, sizeof(optval));
	if (retval == SOCKET_ERROR)
		return false;

	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);
	addr.sin_addr.s_addr = INADDR_ANY;

	if (bind(g_recvSocket, (SOCKADDR*)&addr, sizeof(addr)) == -1)
		return false;

	// 소켓 s를 멀티 캐스트 그룹에 가입한다.
	ip_mreq mreq;
	mreq.imr_multiaddr.s_addr = inet_addr(g_multiIP[groupid]); 
	mreq.imr_interface.s_addr = INADDR_ANY;
	int retv = setsockopt(g_recvSocket, IPPROTO_IP, IP_ADD_MEMBERSHIP, (char*)&mreq, sizeof(mreq));
	if (retv == SOCKET_ERROR)
		return false;
	
	g_hThread = CreateThread(0, 0, RecvThread, (LPVOID)g_recvSocket, 0, 0);

	return true;
}

DWORD WINAPI RecvThread(void* p)
{
	SOCKET s = (SOCKET)p;

	while (true)
	{
		SOCKADDR_IN c_addr;
		int sz = sizeof(c_addr);

		char buf[BUF_SIZE] = { 0 };

		recvfrom(s, buf, BUF_SIZE, 0, (SOCKADDR*)&c_addr, &sz);

		RecvData((TCHAR*)buf);
	}

	closesocket(s);
	return 0;
}

void wbnet_ExitSocket()
{	
	TerminateThread(g_hThread, 0);
	CloseHandle(g_hThread);

	closesocket(g_recvSocket); 
}

void wbnet_SendData(int port, int groupid, TCHAR* packet)
{
	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);
	addr.sin_addr.s_addr = inet_addr(g_multiIP[groupid]);

	sendto(g_sendSocket, (char*)packet, sizeof(TCHAR)*200 , 0, (SOCKADDR*)&addr, sizeof(addr));
}