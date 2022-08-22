#include<stdio.h>
#include<Winsock2.h>;
#pragma comment(lib,"ws2_32.lib")

int main(void)
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) //���� �ʱ�ȭ (2.2����)
	{
		printf("������ ���� �ʱ�ȭ ����\n");
		return -1;
	}
	printf("������ ���� �ʱ�ȭ ����\n");

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


	WSACleanup(); //STARTUP ���� ���� 

	return 0;
}