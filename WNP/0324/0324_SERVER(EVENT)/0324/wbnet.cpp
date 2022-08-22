#include "std.h"

vector<SocketInfo> g_socketinfo;
#define BUFSIZE 1024

//���̺귯�� �ʱ�ȭ �ڵ�
bool wbnet_InitLibrary()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	return true;
}

// ���� ���� + �ּ� ���� + ������
bool wbnet_CreateListenSocket(int port)
{
	//socket()
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listen_sock == INVALID_SOCKET)
		return false;
	
	// ���� ���� �߰�
	if (AddSocket(listen_sock,FD_ACCEPT) == false)
		return false;
	
	
	// bind()
	SOCKADDR_IN serveraddr;
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(port);
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);
	int retval = bind(listen_sock, (SOCKADDR*)&serveraddr, sizeof(serveraddr));
	if (retval == SOCKET_ERROR)
		return false;
	
	// listen()
	retval = listen(listen_sock, SOMAXCONN);
	if (retval == SOCKET_ERROR) 
		return false;

	return true;
}

// ���� + �̺�Ʈ ���� �߰�
bool AddSocket(SOCKET sock, int networkflag)
{
	if (g_socketinfo.size() >= WSA_MAXIMUM_WAIT_EVENTS) 
		return false;

	WSAEVENT hEvent = WSACreateEvent();
	if (hEvent == WSA_INVALID_EVENT) 
		return false;

	// WSAEventSelect()
	int retval = WSAEventSelect(sock, hEvent, networkflag);
	if (retval == SOCKET_ERROR)
		return false;

	//����
	SocketInfo info = { sock, hEvent };
	g_socketinfo.push_back(info);
	return true;
}

// ���� + �̺�Ʈ ��ü ����
bool DeleteSocket(SOCKET sock)
{
	for (int i = 0; i < (int)g_socketinfo.size(); i++)
	{
		if (g_socketinfo[i].sock == sock)
		{
			//���� ���� + �̺�Ʈ ���� + ��ü ����
			closesocket(g_socketinfo[i].sock);
			CloseHandle(g_socketinfo[i].hEvent);
			g_socketinfo.erase(g_socketinfo.begin() + i);
			return true;
		}
	}
	return false;
}

// ������ ���� 
HANDLE wbnet_Run()
{
	printf("���� ���� \n");
	return(HANDLE)_beginthreadex(0, 0, WorkerTrhread, 0, 0, 0);
}

//���� ���� + ACCEPT
unsigned int WINAPI WorkerTrhread(void* pParam)
{
	int index;
	WSANETWORKEVENTS NetworkEvents;

	while (true)
	{
		if (GetNetworkEvent(&index, &NetworkEvents) == false)
			continue;
		
		//��Ʈ��ũ �̺�Ʈ �ڵ�(FD_ACCEPT, FD_READ, FD_CLOSE)
		if (NetworkEvents.lNetworkEvents & FD_ACCEPT)
			Accept(g_socketinfo[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_READ)
			Read(g_socketinfo[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_CLOSE)
			Close(g_socketinfo[index].sock, NetworkEvents);
	}

	//printf("��Ʈ��ũ �̺�Ʈ ����\n");
	//---------------------------------------------
	return 0;
}

// �ӽú��� Index + eventarr ���� �� ���� ����
bool GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents)
{
	int index;
	WSAEVENT eventarr[WSA_MAXIMUM_WAIT_EVENTS];
	for (int i = 0; i < (int)g_socketinfo.size(); i++)
	{
		eventarr[i] = g_socketinfo[i].hEvent;
	}


	// �̺�Ʈ ��ü ���� + �̺�Ʈ ��ü�� �迭 �־����.
	index = WSAWaitForMultipleEvents(g_socketinfo.size(), eventarr, FALSE, WSA_INFINITE, FALSE);
	if (index == WSA_WAIT_FAILED) {
		printf("WSAWaitForMultipleEvents()\n");
		return false;
	}

	index -= WSA_WAIT_EVENT_0;

	// ��ü���� ��Ʈ��ũ �̺�Ʈ �˾Ƴ���
	int retval = WSAEnumNetworkEvents(g_socketinfo[index].sock, g_socketinfo[index].hEvent, NetworkEvents);
	if (retval == SOCKET_ERROR) {
		printf("WSAEnumNetworkEvents()\n");
		return false;
	}
	*idx = index;
	return true;
}

bool Accept(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
{
	if (NetworkEvents.iErrorCode[FD_ACCEPT_BIT] != 0)
	{
		printf("accept error : %d\n",NetworkEvents.iErrorCode[FD_ACCEPT_BIT]);
		return false;
	}

	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	SOCKET client_sock = accept(sock,(SOCKADDR*)&clientaddr, &addrlen);
	if (client_sock == INVALID_SOCKET)
	{
		printf("[error] accept()\n");
		return false;
	}
	printf("[TCP ����] Ŭ���̾�Ʈ ����: IP �ּ�=%s, ��Ʈ ��ȣ=%d\n",inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

	if (g_socketinfo.size() >= WSA_MAXIMUM_WAIT_EVENTS)
	{
		printf("[����] �� �̻� ������ �޾Ƶ��� �� �����ϴ�!\n");
		closesocket(client_sock);
		return false;
	}

	if (AddSocket(client_sock, FD_READ | FD_CLOSE) == false)
		return false;

	printf("Ŭ���̾�Ʈ ���� ����\n");
	return true;
}

bool Read(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
{
	if (NetworkEvents.lNetworkEvents & FD_READ	&& NetworkEvents.iErrorCode[FD_READ_BIT] != 0)
	{
		printf("recv error : %d\n", NetworkEvents.iErrorCode[FD_ACCEPT_BIT]);
		return false;
	}

	int retval;
	char buf[BUFSIZE];

	retval = recv(sock, buf, BUFSIZE, 0);
	if (retval == SOCKET_ERROR)
	{
		if (WSAGetLastError() != WSAEWOULDBLOCK)
		{
			printf("[error] recv()\n");
			if (DeleteSocket(sock) == false)
				return false;
		}
		return false;
	}

	buf[retval] = '\0';

	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(sock, (SOCKADDR*)&clientaddr, &addrlen);
	printf("[TCP/%s:%d] %s\n", inet_ntoa(clientaddr.sin_addr),ntohs(clientaddr.sin_port), buf);

	//1:1 �۽�
	retval = send(sock, buf , strlen(buf)+1, 0);


	//1:�� �۽�
	for (int i = 0; i < (int)g_socketinfo.size(); i++)
	{
		SOCKET s = g_socketinfo[i].sock;

		if (sock != s)
			retval = send(s, buf, strlen(buf) + 1, 0);
	}

	return true;
}

bool Close(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
{
	if (NetworkEvents.iErrorCode[FD_CLOSE_BIT] != 0)
	{
		printf("Close error : %d\n", NetworkEvents.iErrorCode[FD_ACCEPT_BIT]);
		return false;
	}
	if (DeleteSocket(sock) == false)
		return false;;

	return true;
}