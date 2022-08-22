#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include<stdio.h>
#include<Winsock2.h>
#pragma comment(lib,"ws2_32.lib")

#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1" //192.168.0.6
int main(void)
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) //���� �ʱ�ȭ (2.2����)
	{
		printf("������ ���� �ʱ�ȭ ����\n");
		return -1;
	}
	printf("������ ���� �ʱ�ȭ ����\n");

	//------------------�ʱ�ȭ--------------------
	//1.���ϻ���
	// SOCK_STREAM : byte����, �����ͺ� ��谡 ����.
	// DGRAM : ������ ����, ��谡 ��Ȯ�ϴ�.
	SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // SOCK_DGRAM --> IPPROTO_UDP
	if (clientSocket == INVALID_SOCKET)
	{
		printf("���� ���� ����\n");
		return -1;
	}

	//-------------------����---------------------
	//2. ���� �Ҵ�(������ ������ �ּ�)
	SOCKADDR_IN addr;
	ZeroMemory(&addr, sizeof(addr)); // <Winsock2.h>���̺귯�� ��� ���� 0���� �ʱ�ȭ 

	addr.sin_family = AF_INET;
	addr.sin_port = htons(SERVER_PORT); // ��Ʈ��ȣ �Է½� �����Լ��� ����Ǳ⶧���� �̸� ��ȯ������ ��ħ 
	addr.sin_addr.s_addr = inet_addr(SERVER_IP); // ���ڿ� > ���� ->networkbyteorder�� ����

	//�� ����
	int retval = connect(clientSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		printf("���� ���� ����\n");
		return -1;

	}

	printf("���� ���� ����\n");

	char buf[512];
	int size;
	while (true)
	{
		printf(">> ");
		gets_s(buf, sizeof(buf));
		if (strlen(buf) == 0)         //���͸� �Է��� ���('0')
			break;

		size = send(clientSocket, buf, strlen(buf) + 1, 0);
		if (size == SOCKET_ERROR)
		{
			printf("�۽� ����\n"); break;
		}
		printf("(%dbyte)����\n", size);

		//����ó��


	}
	//--------------------------------------------
	//----------------����ó��--------------------
	closesocket(clientSocket);

	//--------------------------------------------
	WSACleanup(); //STARTUP ���� ���� 

	return 0;
}