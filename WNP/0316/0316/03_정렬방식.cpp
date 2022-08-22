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

	//------------------------------------------------
	short s = 0x1234;
	long l = 0x12345678;

	short ns = htons(s);
	long nl = htonl(l);

	//host -> network 
	printf("0x%4x -> 0x%04x\n", s, ns);
	printf("0x%8x -> 0x%08x\n", l, nl);

	//network -> host
	printf("0x%4x -> 0x%04x\n", ns, ntohs(ns));
	printf("0x%8x -> 0x%08x\n", nl, ntohl(nl));
	//------------------------------------------------


	WSACleanup(); //STARTUP 설정 해제 

	return 0;
}