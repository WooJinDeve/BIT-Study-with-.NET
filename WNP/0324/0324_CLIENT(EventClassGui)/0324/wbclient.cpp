#include "std.h"

WbClient::WbClient()
{
	InitLibrary();
}

WbClient::~WbClient()
{
	ExitLibrary();
}

//라이브러리 초기화 코드
bool WbClient::InitLibrary()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	return true;
}

//소켓 라이브러리 종료처리
void WbClient::ExitLibrary()
{
	closesocket(clientSocket);
	WSACleanup();
}

// 소켓 생성 & 접속 & 수신thread 실행
bool WbClient::CreateSocket(const char* ip, int port)
{
	clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);  // SOCK_DGRAM --> IPPROTO_UDP
	if (clientSocket == INVALID_SOCKET)
	{
		printf("소켓 생성 에러\n");
		return false;
	}

	SOCKADDR_IN addr;
	ZeroMemory(&addr, sizeof(addr));

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);
	addr.sin_addr.s_addr = inet_addr(ip); //문자열 -> 숫자 -> NetworkByteOrder로 변경

	int retval = connect(clientSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		printf("서버 연결 실패\n");
		return false;
	}

	printf("서버 연결 성공\n");

	//_beginthreadex(0, 0, ReciveThread, (void*)clientSocket, 0, 0);
	_beginthreadex(0, 0, ReciveThread, (void*)this, 0, 0);
	return true;
}

// 수신 thread (static)
unsigned int __stdcall WbClient::ReciveThread(void* param)
{
	//SOCKET sock = (SOCKET)param;
	WbClient* pc = (WbClient*)param;
	SOCKET sock = pc->clientSocket;

	char buf[BUFSIZE];
	int size;

	//수신
	while (true)
	{
		ZeroMemory(buf, sizeof(buf));
		//size = wbrecive(sock, buf);
		size = recv(sock, buf, sizeof(buf), 0);
		if (size == SOCKET_ERROR || size == 0)
		{
			printf("소켓 수신 오류 || 상대방 소켓 종료\n");
			break;
		}
		else
		{
			printf("(%dbyte수신) %s\n", size, buf);
		}
	}

	closesocket(sock);

	return 0;
}

//데이터 전송
int WbClient::SenData(char* buf, int length)
{
	int size = send(clientSocket, buf, length, 0);
	return size;
}

