#include "std.h"

#define BUFSIZE 1024
SOCKET clientSocket;

//���̺귯�� �ʱ�ȭ �ڵ�
bool wbnet_InitLibrary()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	return true;
}

// ���� ���� & ���� & ����thread ����
bool wbnet_CreateSocket(const char* ip, int port)
{
	clientSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);  // SOCK_DGRAM --> IPPROTO_UDP
	if (clientSocket == INVALID_SOCKET)
	{
		printf("���� ���� ����\n");
		return false;
	}

	SOCKADDR_IN addr;
	ZeroMemory(&addr, sizeof(addr));

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);
	addr.sin_addr.s_addr = inet_addr(ip); //���ڿ� -> ���� -> NetworkByteOrder�� ����

	int retval = connect(clientSocket, (SOCKADDR*)&addr, sizeof(addr));
	if (retval == SOCKET_ERROR)
	{
		printf("���� ���� ����\n");
		return false;
	}

	printf("���� ���� ����\n");

	_beginthreadex(0, 0, ReciveThread, (void*)clientSocket, 0, 0);

	return true;
}

// ���� thread
unsigned int __stdcall ReciveThread(void* param)
{
	SOCKET sock = (SOCKET)param;
	char buf[BUFSIZE];
	int size;

	//����
	while (true)
	{
		ZeroMemory(buf, sizeof(buf));
		//size = wbrecive(sock, buf);
		size = recv(sock, buf, sizeof(buf), 0);
		if (size == SOCKET_ERROR || size == 0)
		{
			printf("���� ���� ���� || ���� ���� ����\n");
			break;
		}
		else
		{
			printf("(%dbyte����) %s\n", size, buf);
		}
	}

	closesocket(sock);

	return 0;
}

//������ ����
int wbnet_SenData(char* buf, int length)
{
	int size = send(clientSocket, buf, length, 0);
	return size;
}

//���� ���̺귯�� ����ó��
void wbnet_ExitLibrary()
{
	closesocket(clientSocket);
	WSACleanup();
}