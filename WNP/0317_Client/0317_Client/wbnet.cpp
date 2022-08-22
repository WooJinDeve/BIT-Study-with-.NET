#include "std.h"

SOCKET clientSocket;

void fun_RecvData(char* msg);

int wbnet_LibraryInit()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) //버전 초기화 (2.2버전)
	{
		return 0;
	}
	return 1;
}

int wbnet_CreateSocket()
{
	clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // SOCK_DGRAM --> IPPROTO_UDP
	if (clientSocket == INVALID_SOCKET)
		return 0;

	return 1;
}

void wbnet_DeleteSocket()
{
	closesocket(clientSocket);
}

void wbnet_Libraryexit()
{
	WSACleanup(); //STARTUP 설정 해제 
}

int Wbnet_Run(int port, const char* ip)
{
	SOCKADDR_IN addr;
	ZeroMemory(&addr, sizeof(addr)); // <Winsock2.h>라이브러리 모든 값을 0으로 초기화 

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port); // 포트번호 입력시 소켓함수에 저장되기때문에 미리 변환과정을 거침 
	addr.sin_addr.s_addr = inet_addr(ip); // 문자열 > 숫자 ->networkbyteorder로 변경

	//망 연결
	int retval = connect(clientSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		return 0;
	}
	_beginthreadex(0, 0, ReciveThread, (void*)clientSocket, 0, 0);

	return 1;
}

unsigned int __stdcall ReciveThread(void* param)
{
	SOCKET sock = (SOCKET)param;
	char buf[512];
	int size;

	while (true)
	{
		ZeroMemory(buf, sizeof(buf));
		size = wbrecive(sock, buf);
		if (size == SOCKET_ERROR || size == 0)
		{
			printf("연결 파이프가 끊어짐 or 서버 통신 소켓 종료");
			break;
		}
		else
			fun_RecvData(buf);
			//printf("(%dbyte수신) %s\n", size, buf);
	}
	closesocket(sock);
	return 0;
}

int wbnet_SenData(const char* msg, int size)
{
	int rev = wbsend(clientSocket, (char*)msg, size);
	//size = send(clientSocket, buf, strlen(buf) + 1, 0);
	if (rev == SOCKET_ERROR)
	{
		return -1;
	}
	return rev;
}

int recvn(SOCKET s, char* buf, int len, int flags)
{
	int received;
	char* ptr = buf;
	int left = len;   //8  5   1  0

	while (left > 0)
	{
		received = recv(s, ptr, left, flags);

		if (received == SOCKET_ERROR)
			return SOCKET_ERROR;

		else if (received == 0)
			break;

		left -= received;
		ptr += received;
	}
	return (len - left);
}

int wbsend(SOCKET s, char* buf, int len)
{
	//헤더
	send(s, (char*)&len, sizeof(int), 0);
	return send(s, buf, len, 0);
}

int wbrecive(SOCKET s, char* buf)
{
	int size;
	recvn(s, (char*)&size, sizeof(int), 0);

	return recvn(s, buf, size, 0);
}