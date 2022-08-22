#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include<stdio.h>
#include<Winsock2.h>
#pragma comment(lib,"ws2_32.lib")

#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1" //192.168.0.6
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
	SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // SOCK_DGRAM --> IPPROTO_UDP
	if (clientSocket == INVALID_SOCKET)
	{
		printf("소켓 생성 에러\n");
		return -1;
	}

	//-------------------실행---------------------
	//2. 연결 할당(연결할 서버의 주소)
	SOCKADDR_IN addr;
	ZeroMemory(&addr, sizeof(addr)); // <Winsock2.h>라이브러리 모든 값을 0으로 초기화 

	addr.sin_family = AF_INET;
	addr.sin_port = htons(SERVER_PORT); // 포트번호 입력시 소켓함수에 저장되기때문에 미리 변환과정을 거침 
	addr.sin_addr.s_addr = inet_addr(SERVER_IP); // 문자열 > 숫자 ->networkbyteorder로 변경

	//망 연결
	int retval = connect(clientSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		printf("서버 연결 실패\n");
		return -1;

	}

	printf("서버 연결 성공\n");

	char buf[512];
	int size;
	while (true)
	{
		printf(">> ");
		gets_s(buf, sizeof(buf));
		if (strlen(buf) == 0)         //엔터를 입력한 경우('0')
			break;

		size = send(clientSocket, buf, strlen(buf) + 1, 0);
		if (size == SOCKET_ERROR)
		{
			printf("송신 오류\n"); break;
		}
		printf("(%dbyte)전송\n", size);

		//수신처리


	}
	//--------------------------------------------
	//----------------종료처리--------------------
	closesocket(clientSocket);

	//--------------------------------------------
	WSACleanup(); //STARTUP 설정 해제 

	return 0;
}