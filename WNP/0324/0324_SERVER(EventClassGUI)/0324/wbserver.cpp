#include "std.h"


#define BUFSIZE 1024

WbServer::WbServer(Handler* phandler)
{
	phan = phandler;  // �ֹ����� �Ϸ�
	InitLibrary();
}

WbServer::~WbServer()
{
	ExitLibrary();
}

//���̺귯�� �ʱ�ȭ �ڵ�
bool WbServer::InitLibrary()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	return true;
}

//���� ���̺귯�� ����ó��
void WbServer::ExitLibrary()
{
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		closesocket(socketinfo[i].sock);
	}
	WSACleanup();
}

// ���� ���� + �ּ� ���� + ������
bool WbServer::CreateListenSocket(int port)
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

void WbServer::CloseListenSocket()
{
	//listen + ��� ���� close
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		closesocket(socketinfo[i].sock);
	}
	//vector �ʱ�ȭ
	socketinfo.clear();
	//thread ����
	TerminateThread(hthread, 0);
	CloseHandle(hthread);
}

// ���� + �̺�Ʈ ���� �߰�
bool WbServer::AddSocket(SOCKET sock, int networkflag)
{
	if (socketinfo.size() >= WSA_MAXIMUM_WAIT_EVENTS) 
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
	socketinfo.push_back(info);
	return true;
}

// ���� + �̺�Ʈ ��ü ����
bool WbServer::DeleteSocket(SOCKET sock)
{
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		if (socketinfo[i].sock == sock)
		{
			//���� ���� + �̺�Ʈ ���� + ��ü ����
			closesocket(socketinfo[i].sock);
			CloseHandle(socketinfo[i].hEvent);
			socketinfo.erase(socketinfo.begin() + i);
			return true;
		}
	}
	return false;
}

// ������ ���� 
HANDLE WbServer::Run()
{
	printf("���� ���� \n");
	hthread = (HANDLE)_beginthreadex(0, 0, WorkerTrhread, this, 0, 0);

	return hthread;
}

//���� ���� + ACCEPT
unsigned int WINAPI WbServer::WorkerTrhread(void* pParam)
{
	WbServer* p = (WbServer*)pParam;

	int index;
	WSANETWORKEVENTS NetworkEvents;

	while (true)
	{
		if (p->GetNetworkEvent(&index, &NetworkEvents) == false)
			continue;
		
		//��Ʈ��ũ �̺�Ʈ �ڵ�(FD_ACCEPT, FD_READ, FD_CLOSE)
		if (NetworkEvents.lNetworkEvents & FD_ACCEPT)
			p->Accept(p->socketinfo[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_READ)
			p->Read(p->socketinfo[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_CLOSE)
			p->Close(p->socketinfo[index].sock, NetworkEvents);
	}

	//printf("��Ʈ��ũ �̺�Ʈ ����\n");
	//---------------------------------------------
	return 0;
}

// �ӽú��� Index + eventarr ���� �� ���� ����
bool WbServer::GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents)
{
	int index;
	WSAEVENT eventarr[WSA_MAXIMUM_WAIT_EVENTS];
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		eventarr[i] = socketinfo[i].hEvent;
	}


	// �̺�Ʈ ��ü ���� + �̺�Ʈ ��ü�� �迭 �־����.
	index = WSAWaitForMultipleEvents(socketinfo.size(), eventarr, FALSE, WSA_INFINITE, FALSE);
	if (index == WSA_WAIT_FAILED) {
		printf("WSAWaitForMultipleEvents()\n");
		return false;
	}

	index -= WSA_WAIT_EVENT_0;

	// ��ü���� ��Ʈ��ũ �̺�Ʈ �˾Ƴ���
	int retval = WSAEnumNetworkEvents(socketinfo[index].sock, socketinfo[index].hEvent, NetworkEvents);
	if (retval == SOCKET_ERROR) {
		printf("WSAEnumNetworkEvents()\n");
		return false;
	}
	*idx = index;
	return true;
}

bool WbServer::Accept(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
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

	phan->LogFuntion(1);

	printf("[TCP ����] Ŭ���̾�Ʈ ����: IP �ּ�=%s, ��Ʈ ��ȣ=%d\n",inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

	if (socketinfo.size() >= WSA_MAXIMUM_WAIT_EVENTS)
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

bool WbServer::Read(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
{
	if (NetworkEvents.iErrorCode[FD_READ_BIT] != 0)
	{
		printf("recv error : %d\n", NetworkEvents.iErrorCode[FD_READ_BIT]);
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

	phan->RecvMessage((TCHAR*)buf);

	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(sock, (SOCKADDR*)&clientaddr, &addrlen);
	//printf("[TCP/%s:%d] %s\n", inet_ntoa(clientaddr.sin_addr),ntohs(clientaddr.sin_port), buf);

	//1:1 �۽�
	//retval = send(sock, buf , retval, 0);


	//1:�� �۽�
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		SOCKET s = socketinfo[i].sock;

		if (sock != s)
			retval = send(s, buf, retval, 0);
	}

	return true;
}

bool WbServer::Close(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
{
	//if (NetworkEvents.iErrorCode[FD_CLOSE_BIT] != 0)
	//{
	//	printf("Close error : %d\n", NetworkEvents.iErrorCode[FD_CLOSE_BIT]);
	//	return false;
	//}

	phan->LogFuntion(2);

	DeleteSocket(sock);
	return true;
}