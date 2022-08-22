#define _WINSOCK_DEPRECATED_NO_WARNINGS
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

	//---------------------------------------
	//������ ������� IP�� ǥ��
	char ipaddr[20] = { "127.0.0.1" };

	//���� �Լ� ���ο����� ������ ����Ѵ�.
	//���ڿ� --> ����
	int ipnumber = inet_addr(ipaddr);
	printf("%d\n", ipnumber);
	
	//���� --> ���ڿ�
	IN_ADDR in_addr;
	in_addr.s_addr = ipnumber;
	char ipaddr1[20];
	strcpy_s(ipaddr1, sizeof(ipaddr1), inet_ntoa(in_addr));
	printf("%s\n", ipaddr1);
	//--------------------------------------

	WSACleanup(); //STARTUP ���� ���� 

	
	return 0;
}