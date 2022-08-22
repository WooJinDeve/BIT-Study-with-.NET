#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include<stdio.h>
#include<Winsock2.h>;
#pragma comment(lib,"ws2_32.lib")

int main(void)
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) //버전 초기화 (2.2버전)
	{
		printf("윈도우 소켓 초기화 실패\n");
		return -1;
	}
	printf("윈도우 소켓 초기화 성공\n");

	//---------------------------------------
	//문자형 방시응로 IP를 표현
	char ipaddr[20] = { "127.0.0.1" };

	//소켓 함수 내부에서는 정수로 사용한다.
	//문자열 --> 정수
	int ipnumber = inet_addr(ipaddr);
	printf("%d\n", ipnumber);
	
	//정수 --> 문자열
	IN_ADDR in_addr;
	in_addr.s_addr = ipnumber;
	char ipaddr1[20];
	strcpy_s(ipaddr1, sizeof(ipaddr1), inet_ntoa(in_addr));
	printf("%s\n", ipaddr1);
	//--------------------------------------

	WSACleanup(); //STARTUP 설정 해제 

	
	return 0;
}