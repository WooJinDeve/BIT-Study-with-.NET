#include "std.h"

SOCKET listenSocket;
vector<SOCKET> g_sockets;

int wbnet_LibraryInit()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) //버전 초기화 (2.2버전)
	{
		return 0;
	}
	return 1;
}

int wbnet_CreateListenSocket(int port)
{
	listenSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); 
	if (listenSocket == INVALID_SOCKET)
	{
		return -1;
	}
	//2. 주소 할당
	SOCKADDR_IN addr = { 0 };
	memset(&addr, 0, sizeof(addr));
	ZeroMemory(&addr, sizeof(addr));

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port); 
	addr.sin_addr.s_addr = htonl(INADDR_ANY); // cmd >> ipconfig >> 192.168.56.1

	int retval = bind(listenSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		return -1;
	}
	//3. 망연결
	retval = listen(listenSocket, SOMAXCONN);
	if (retval == SOCKET_ERROR)
	{
		return -1;
	}
	return 1;
}

void wbnet_DeleteListenSocket()
{
	closesocket(listenSocket);
}

void wbnet_Libraryexit()
{
	WSACleanup(); //STARTUP 설정 해제 
}

void wbnet_ServerRun()
{
	while (true)
	{
		//4. 접속대기
		SOCKADDR_IN clientaddr;
		int clientaddrlen = sizeof(clientaddr);
		SOCKET clientSocket = accept(listenSocket, (SOCKADDR*)&clientaddr, &clientaddrlen);
		if (clientSocket == INVALID_SOCKET)
		{
			printf("클라이언트 연결 실패\n");
			continue;
		}

		g_sockets.push_back(clientSocket);

		printf("클라이언트 접속 : %s:%d\n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

		//Thread 생성
		_beginthreadex(0, 0, WorkThread, (void*)clientSocket, 0, 0); //process.h
	}
}

unsigned int __stdcall WorkThread(void* param)
{
	SOCKET clientSocket = (SOCKET)param;
	while (true) // 동시에 하나 이상의 클라이언트와 대화 불가능
	{
		//--데이터 수신--
		char buf[512];
		int size; // 수신 길이 

		size = wbrecive(clientSocket, buf);
		if (size == SOCKET_ERROR)
		{
			printf("수신 오류\n"); break;
		}
		else if (size == 0)
		{
			printf("상대방이 소켓을 closesoket()함\n"); break;
		}
		else
		{
			fun_RecvData(buf);

			wbsend(clientSocket, buf, size);
			////클라이언트에게 송신하겠다.
			//for (int i = 0; i < (int)g_sockets.size(); i++) // 전체 송신 순회
			//{
			//	SOCKET sock = g_sockets[i];
			//	wbsend(sock, buf, strlen(buf) + 1);
			//}
		}
	}
	for (int i = 0; i < (int)g_sockets.size(); i++) //삭제
	{
		SOCKET sock = g_sockets[i];
		if (sock == clientSocket)
		{
			g_sockets.erase(g_sockets.begin() + i);
		}
	}

	//clientSocket을 통해서 상대방 주소 획득!
	SOCKADDR_IN clientaddr;
	int length = sizeof(clientaddr);
	getpeername(clientSocket, (sockaddr*)&clientaddr, &length);
	printf("클라이언트 해제 : %s:%d\n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

	closesocket(clientSocket);
	return 0;
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