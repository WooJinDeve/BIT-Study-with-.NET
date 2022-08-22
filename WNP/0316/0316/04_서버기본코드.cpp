#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include<stdio.h>
#include<Winsock2.h>
#pragma comment(lib,"ws2_32.lib")

#define SERVER_PORT 9000

int main(void)
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) //버전 초기화 (2.2버전)
	{
		printf("윈도우 소켓 초기화 실패\n");
		return -1;
	}
	printf("윈도우 소켓 초기화 성공\n");

	//------------------초기화--------------------
	//1.소켓생성
	// SOCK_STREAM : byte기준, 데이터별 경계가 없다.
	// DGRAM : 데이터 기준, 경계가 명확하다.
	SOCKET listenSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // SOCK_DGRAM --> IPPROTO_UDP
	if (listenSocket == INVALID_SOCKET)
	{
		printf("소켓 생성 에러\n");
		return -1;
	}
	//2. 주소 할당
	SOCKADDR_IN addr = { 0 };
	memset(&addr, 0, sizeof(addr)); // C라이브러리 함수
	ZeroMemory(&addr, sizeof(addr)); // <Winsock2.h>라이브러리 모든 값을 0으로 초기화 

	addr.sin_family = AF_INET;
	addr.sin_port = htons(SERVER_PORT); // 포트번호 입력시 소켓함수에 저장되기때문에 미리 변환과정을 거침 
	addr.sin_addr.s_addr = htonl(INADDR_ANY); // cmd >> ipconfig >> 192.168.56.1

	int retval = bind(listenSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		printf("주소 연결 실패");
		return -1;
	}
	//3. 망연결
	retval = listen(listenSocket, SOMAXCONN); // int backlog : 접속대기큐의 크기
	if (retval == SOCKET_ERROR)
	{
		printf("망 연결 실패");
		return -1;
	}
	//--------------------------------------------
	//-------------------실행---------------------

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
		printf("클라이언트 접속 : %s:%d\n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));
		
		while (true) // 동시에 하나 이상의 클라이언트와 대화 불가능
		{
			//--데이터 수신--
			char buf[512];
			int size; // 수신 길이 

			size = recv(clientSocket, buf, sizeof(buf), 0);
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
				printf("[%dbyte] %s\n", size, buf);
			}
		}
		closesocket(clientSocket);
	}
	//--------------------------------------------
	//----------------종료처리--------------------
	closesocket(listenSocket);
	//--------------------------------------------
	WSACleanup(); //STARTUP 설정 해제 

	return 0;
}