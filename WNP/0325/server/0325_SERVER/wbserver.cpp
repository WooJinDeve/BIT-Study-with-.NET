//wbnet.cpp

#include "std.h"

WbServer::WbServer(MainDlg* phandle)
{
	pMainDlg = phandle;	// �ֹ����� �Ϸ�
	InitLibrary();
}

WbServer::~WbServer()
{
	ExitLibrary();
}

bool WbServer::InitLibrary()
{
	// ���� �ʱ�ȭ
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	return true;
}

void WbServer::ExitLibrary()
{
	for (int i = 0; i < (int)socketinfos.size(); i++)
	{
		closesocket(socketinfos[i].sock);
	}
	WSACleanup();
}

bool WbServer::CreateListenSocket(int port)
{
	// socket()
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listen_sock == INVALID_SOCKET)
		return false;

	// ���� ���� �߰�
	if (AddSocket(listen_sock, FD_ACCEPT) == false)
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
	for (int i = 0; i < (int)socketinfos.size(); i++)
	{
		closesocket(socketinfos[i].sock);
	}
	//vector�ʱ�ȭ
	socketinfos.clear();

	//thread����
	TerminateThread(hthread, 0);
	CloseHandle(hthread);
}

bool WbServer::AddSocket(SOCKET sock, int networkflag)
{
	if (socketinfos.size() >= WSA_MAXIMUM_WAIT_EVENTS) 
	{
		printf("[����] ���� ������ �߰��� �� �����ϴ�!\n");
		return false;
	}

	WSAEVENT hEvent = WSACreateEvent();
	if (hEvent == WSA_INVALID_EVENT) 
	{
		printf("[����] �̺�Ʈ ��ü ���� �Ұ�!\n");
		return false;
	}

	// WSAEventSelect()
	int retval = WSAEventSelect(sock, hEvent,networkflag);
	if (retval == SOCKET_ERROR)
		return false;

	//����
	SocketInfo info = { sock, hEvent };
	socketinfos.push_back(info);
	return true;
}

HANDLE WbServer::Run()
{
	printf("���� ����\n");
	//#include <process.h>
	hthread = (HANDLE)_beginthreadex(0, 0, WorkerThread, this, 0, 0);

	return hthread;
}

unsigned int WINAPI WbServer::WorkerThread(void* pParam)
{
	WbServer* p = (WbServer*)pParam;

	int index;
	WSANETWORKEVENTS NetworkEvents;

	while (true)
	{
		if (p->GetNetworkEvent(&index, &NetworkEvents) == false)
			continue;

		//��Ʈ��ũ �̺�Ʈ ó��(FD_ACCEPT, FD_READ, FD_CLOSE)
		if (NetworkEvents.lNetworkEvents & FD_ACCEPT)
			p->Accept(p->socketinfos[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_READ)
			p->Read(p->socketinfos[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_CLOSE)
			p->Close(p->socketinfos[index].sock, NetworkEvents);
	}
	return 0;
}

bool WbServer::GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents)
{
	WSAEVENT eventarr[WSA_MAXIMUM_WAIT_EVENTS];
	int index;

	int i;
	for (i = 0; i < (int)socketinfos.size(); i++)
	{
		eventarr[i] = socketinfos[i].hEvent;
	}

	// �̺�Ʈ ��ü ����
	printf("��Ʈ��ũ �̺�Ʈ ��� ��..............\n");
	index = WSAWaitForMultipleEvents(i, eventarr, FALSE, WSA_INFINITE, FALSE);
	if (index == WSA_WAIT_FAILED) {
		printf("WSAWaitForMultipleEvents()\n");
		return false;
	}
	index -= WSA_WAIT_EVENT_0;

	// ��ü���� ��Ʈ��ũ �̺�Ʈ �˾Ƴ���
	int retval = WSAEnumNetworkEvents(socketinfos[index].sock,
		socketinfos[index].hEvent, NetworkEvents);
	if (retval == SOCKET_ERROR) {
		printf("WSAEnumNetworkEvents()");
		return false;
	}

	*idx = index;
	return true;
}

bool WbServer::Accept(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
{
	if (NetworkEvents.iErrorCode[FD_ACCEPT_BIT] != 0) {
		printf("accept���� : %d", NetworkEvents.iErrorCode[FD_ACCEPT_BIT]);
		return false;
	}

	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	SOCKET client_sock = accept(sock,(SOCKADDR*)&clientaddr, &addrlen);
	if (client_sock == INVALID_SOCKET) {
		printf("[����] accept()\n");
		return false;
	}

	pMainDlg->LogFunction(1);

	printf("[����] %s:%d\n",inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

	if (socketinfos.size() >= WSA_MAXIMUM_WAIT_EVENTS) 
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
		printf("recv���� : %d", NetworkEvents.iErrorCode[FD_READ_BIT]);
		return false;		
	}
	
	int retval;
	char buf[BUFSIZE];

	retval = recv(sock, buf, BUFSIZE, 0);
	if (retval == SOCKET_ERROR) 
	{
		if (WSAGetLastError() != WSAEWOULDBLOCK) 
		{
			printf("recv()����\n");
			DeleteSocket(sock);
		}
		return false;
	}	

	pMainDlg->RecvMessage((TCHAR*)buf);


	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(sock, (SOCKADDR*)&clientaddr, &addrlen);
	printf("[TCP/%s:%d] %s\n", inet_ntoa(clientaddr.sin_addr),
		ntohs(clientaddr.sin_port), buf);	

	//1:1�۽�
	//retval = send(sock, buf, sizeof(TCHAR)*124, 0);
	
	//1:�� �۽�
	for (int i = 0; i < (int)socketinfos.size(); i++)
	{
		SOCKET s = socketinfos[i].sock;
		//�ڽ��� �����ϰ�....
		if( sock !=s)
			retval = send(s, buf, (sizeof(TCHAR)+1) * 124, 0);
	}
	return true;
}

bool WbServer::Close(SOCKET sock, WSANETWORKEVENTS NetworkEvents)
{
	pMainDlg->LogFunction(2);

	DeleteSocket(sock);
	return true;
}

bool WbServer::DeleteSocket(SOCKET sock)
{
	for (int i = 0; i < (int)socketinfos.size(); i++)
	{
		if (socketinfos[i].sock == sock)
		{
			closesocket(socketinfos[i].sock);
			CloseHandle(socketinfos[i].hEvent);
			socketinfos.erase(socketinfos.begin() + i);
			return true;
		}
	}
	return false;
}
