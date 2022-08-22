#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include<stdio.h>
#include<Winsock2.h>
#pragma comment(lib,"ws2_32.lib")

#define SERVER_PORT 9000

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
	SOCKET listenSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // SOCK_DGRAM --> IPPROTO_UDP
	if (listenSocket == INVALID_SOCKET)
	{
		printf("���� ���� ����\n");
		return -1;
	}
	//2. �ּ� �Ҵ�
	SOCKADDR_IN addr = { 0 };
	memset(&addr, 0, sizeof(addr)); // C���̺귯�� �Լ�
	ZeroMemory(&addr, sizeof(addr)); // <Winsock2.h>���̺귯�� ��� ���� 0���� �ʱ�ȭ 

	addr.sin_family = AF_INET;
	addr.sin_port = htons(SERVER_PORT); // ��Ʈ��ȣ �Է½� �����Լ��� ����Ǳ⶧���� �̸� ��ȯ������ ��ħ 
	addr.sin_addr.s_addr = htonl(INADDR_ANY); // cmd >> ipconfig >> 192.168.56.1

	int retval = bind(listenSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		printf("�ּ� ���� ����");
		return -1;
	}
	//3. ������
	retval = listen(listenSocket, SOMAXCONN); // int backlog : ���Ӵ��ť�� ũ��
	if (retval == SOCKET_ERROR)
	{
		printf("�� ���� ����");
		return -1;
	}
	//--------------------------------------------
	//-------------------����---------------------

	while (true)
	{
		//4. ���Ӵ��
		SOCKADDR_IN clientaddr;
		int clientaddrlen = sizeof(clientaddr);
		SOCKET clientSocket = accept(listenSocket, (SOCKADDR*)&clientaddr, &clientaddrlen);
		if (clientSocket == INVALID_SOCKET)
		{
			printf("Ŭ���̾�Ʈ ���� ����\n");
			continue;
		}
		printf("Ŭ���̾�Ʈ ���� : %s:%d\n", inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));
		
		while (true) // ���ÿ� �ϳ� �̻��� Ŭ���̾�Ʈ�� ��ȭ �Ұ���
		{
			//--������ ����--
			char buf[512];
			int size; // ���� ���� 

			size = recv(clientSocket, buf, sizeof(buf), 0);
			if (size == SOCKET_ERROR)
			{
				printf("���� ����\n"); break;
			}
			else if (size == 0)
			{
				printf("������ ������ closesoket()��\n"); break;
			}
			else
			{
				printf("[%dbyte] %s\n", size, buf);
			}
		}
		closesocket(clientSocket);
	}
	//--------------------------------------------
	//----------------����ó��--------------------
	closesocket(listenSocket);
	//--------------------------------------------
	WSACleanup(); //STARTUP ���� ���� 

	return 0;
}