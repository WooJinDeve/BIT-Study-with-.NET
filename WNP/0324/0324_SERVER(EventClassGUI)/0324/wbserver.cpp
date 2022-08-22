#include "std.h"


#define BUFSIZE 1024

WbServer::WbServer(Handler* phandler)
{
	phan = phandler;  // 쌍방참조 완료
	InitLibrary();
}

WbServer::~WbServer()
{
	ExitLibrary();
}

//라이브러리 초기화 코드
bool WbServer::InitLibrary()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	return true;
}

//소켓 라이브러리 종료처리
void WbServer::ExitLibrary()
{
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		closesocket(socketinfo[i].sock);
	}
	WSACleanup();
}

// 소켓 생성 + 주소 연결 + 망연결
bool WbServer::CreateListenSocket(int port)
{
	//socket()
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listen_sock == INVALID_SOCKET)
		return false;
	
	// 소켓 정보 추가
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
	//listen + 모든 소켓 close
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		closesocket(socketinfo[i].sock);
	}
	//vector 초기화
	socketinfo.clear();
	//thread 종료
	TerminateThread(hthread, 0);
	CloseHandle(hthread);
}

// 소켓 + 이벤트 정보 추가
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

	//저장
	SocketInfo info = { sock, hEvent };
	socketinfo.push_back(info);
	return true;
}

// 소켓 + 이벤트 객체 삭제
bool WbServer::DeleteSocket(SOCKET sock)
{
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		if (socketinfo[i].sock == sock)
		{
			//소켓 해제 + 이벤트 해제 + 객체 삭제
			closesocket(socketinfo[i].sock);
			CloseHandle(socketinfo[i].hEvent);
			socketinfo.erase(socketinfo.begin() + i);
			return true;
		}
	}
	return false;
}

// 쓰레드 생성 
HANDLE WbServer::Run()
{
	printf("서버 동작 \n");
	hthread = (HANDLE)_beginthreadex(0, 0, WorkerTrhread, this, 0, 0);

	return hthread;
}

//무한 루프 + ACCEPT
unsigned int WINAPI WbServer::WorkerTrhread(void* pParam)
{
	WbServer* p = (WbServer*)pParam;

	int index;
	WSANETWORKEVENTS NetworkEvents;

	while (true)
	{
		if (p->GetNetworkEvent(&index, &NetworkEvents) == false)
			continue;
		
		//네트워크 이벤트 코드(FD_ACCEPT, FD_READ, FD_CLOSE)
		if (NetworkEvents.lNetworkEvents & FD_ACCEPT)
			p->Accept(p->socketinfo[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_READ)
			p->Read(p->socketinfo[index].sock, NetworkEvents);
		else if (NetworkEvents.lNetworkEvents & FD_CLOSE)
			p->Close(p->socketinfo[index].sock, NetworkEvents);
	}

	//printf("네트워크 이벤트 종료\n");
	//---------------------------------------------
	return 0;
}

// 임시변수 Index + eventarr 생성 후 정보 리턴
bool WbServer::GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents)
{
	int index;
	WSAEVENT eventarr[WSA_MAXIMUM_WAIT_EVENTS];
	for (int i = 0; i < (int)socketinfo.size(); i++)
	{
		eventarr[i] = socketinfo[i].hEvent;
	}


	// 이벤트 객체 관찰 + 이벤트 객체에 배열 넣어야함.
	index = WSAWaitForMultipleEvents(socketinfo.size(), eventarr, FALSE, WSA_INFINITE, FALSE);
	if (index == WSA_WAIT_FAILED) {
		printf("WSAWaitForMultipleEvents()\n");
		return false;
	}

	index -= WSA_WAIT_EVENT_0;

	// 구체적인 네트워크 이벤트 알아내기
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

	printf("[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트 번호=%d\n",inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

	if (socketinfo.size() >= WSA_MAXIMUM_WAIT_EVENTS)
	{
		printf("[오류] 더 이상 접속을 받아들일 수 없습니다!\n");
		closesocket(client_sock);
		return false;
	}

	if (AddSocket(client_sock, FD_READ | FD_CLOSE) == false)
		return false;

	printf("클라이언트 접속 성공\n");
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

	//1:1 송신
	//retval = send(sock, buf , retval, 0);


	//1:다 송신
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