#include<stdio.h>
#include<Winsock2.h>;
#pragma comment(lib,"ws2_32.lib")

int main(void)
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2,2),&wsa)!=0) //���� �ʱ�ȭ (2.2����)
	{
		printf("������ ���� �ʱ�ȭ ����\n");
		return -1;
	}
	printf("������ ���� �ʱ�ȭ ����\n");

	WSACleanup(); //STARTUP ���� ���� 

	return 0;
}