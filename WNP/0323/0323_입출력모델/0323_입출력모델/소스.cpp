//WSAASYNCSELECT모델(서버)
//클라이언트 동시접속 최대 64개 까지 가능!
// FD_SETSIZE : 저장할 수 있는 최대 크기

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <windows.h>
#include <tchar.h>
#include <stdio.h> 
#include <vector>
using namespace std;

#define SERVER_PORT 9000

#define BUFSIZE 1024
#define WM_SOCKET (WM_USER+1) 

// 소켓 정보저장을 위한 구조체
struct SOCKETINFO 
{
	SOCKET sock;
	char buf[BUFSIZE]; 
	int recvbytes; 
	int sendbytes; 
	BOOL recvdelayed;		//??
};

HWND hWnd;							//윈도우의 핸들
SOCKET listenSock;					//대기 소켓
vector<SOCKETINFO*> g_Clients;		//통신 소켓들


// 소켓 함수 오류 출력 
void DisplayMessage() 
{
	TCHAR* pMsg; 
	FormatMessage(
	FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM, NULL,
	WSAGetLastError(),
	MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR)&pMsg, 0, NULL);
	MessageBox(NULL, pMsg, TEXT("알림"), MB_OK); 
	LocalFree(pMsg);
}

//------------------------------------  사용자 정의 함수--------------------
//대기 소켓 생성(네트워크이벤트-윈도우메시지 연결)
bool CreateListenSocket(HWND hwnd) 
{
	// 윈속 초기화
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	
	int retval;
	// 대기 소켓 생성
	listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); 
	if (listenSock == INVALID_SOCKET)
	{
		DisplayMessage(); 
		return false;
	}

	//대기 소켓과   사용자   정의   메시지    WM_SOCKET을    연결한다. 
	//넌블로킹    소켓으로    변경하는    일도    같이    한다.
	retval = WSAAsyncSelect(listenSock, hwnd, WM_SOCKET, FD_ACCEPT | FD_CLOSE);
	if (retval == SOCKET_ERROR) 
	{
		DisplayMessage(); 
		return false;
	}

	// 대기    소켓의    로컬    주소, 포트    설정
	SOCKADDR_IN serveraddr;
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(SERVER_PORT);
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);
	retval = bind(listenSock, (SOCKADDR*)&serveraddr, sizeof(serveraddr)); 
	if (retval == SOCKET_ERROR)
	{
		DisplayMessage(); 
		return false;
	}
	// 대기    소켓의   접속    대기    큐    생성    및    클라이언트    접속    대기
	retval = listen(listenSock, SOMAXCONN); 
	if (retval == SOCKET_ERROR)
	{
		DisplayMessage(); return false;
	}
	return true;
}

//소켓 --> 저장 인덱스 반환
int GetSocketInfo(SOCKET sock) 
{
	for (int i = 0; i < (int)g_Clients.size(); ++i) 
	{
		if (g_Clients[i]->sock == sock)
			return i;
	}
	return -1; 
}

// 소켓 정보를 삭제한다.
void RemoveSocketInfo(int nIndex) 
{
	SOCKETINFO* ptr = g_Clients[nIndex];

	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(ptr->sock, (SOCKADDR*)&clientaddr, &addrlen); 
	TCHAR msg[100];
	wsprintf(msg, TEXT("[TCP 서버] 클라이언트    종료: IP 주소=%s, 포트    번호=%d"),
		inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));
	MessageBox(NULL, msg, TEXT("알림"), MB_OK);
	
	closesocket(ptr->sock); 
	delete ptr;
	g_Clients.erase(g_Clients.begin() + nIndex);
}

//소켓 정보를 추가한다.
BOOL AddSocketInfo(SOCKET sock) 
{
	// FD_SETSIZE - 연결   대기   소켓 
	if(g_Clients.size() >= (FD_SETSIZE-1))
	{
		printf("[오류] 소켓    정보를    추가할    수    없습니다!\n"); 
		return FALSE;
	}

	SOCKETINFO* ptr = new SOCKETINFO; 
	if (ptr == NULL) 
	{
		printf("[오류] 메모리가    부족합니다!\n"); 
		return FALSE;
	}

	ptr->sock = sock;
	ptr->recvbytes = 0;
	ptr->sendbytes = 0;
	g_Clients.push_back(ptr);

	return TRUE;
}


//네트워크 이벤트 처리
//FD_ACCEPT --> accept()
void OnAccept(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	SOCKET client_sock = accept(wParam, (SOCKADDR*)&clientaddr, &addrlen);
	if (client_sock == INVALID_SOCKET) 
	{
		if (WSAGetLastError() != WSAEWOULDBLOCK)
		{
			DisplayMessage();
			return;
		}
	}
	
	printf("[TCP 서버] 클라이언트    접속: IP 주소=%s, 포트    번호=%d\n",
		inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));
	
	AddSocketInfo(client_sock);

	int retval = WSAAsyncSelect(client_sock, hWnd, WM_SOCKET, 
		FD_READ | FD_WRITE | FD_CLOSE);
	if (retval == SOCKET_ERROR) 
	{
		DisplayMessage();
		RemoveSocketInfo(GetSocketInfo(client_sock));
	}
}

//FD_READ  --> read()
void OnRead(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	SOCKETINFO* ptr = g_Clients[GetSocketInfo(wParam)];

	//자신의 입력버퍼에 이전에 받아 둔 데이터가 존재
	//네트워크 입력 버퍼에 존재하는 클라이언트가 보낸 정보를 수신하지 않겠다.
	if (ptr->recvbytes > 0) 
	{
		ptr->recvdelayed = TRUE; 
		return;
	}

	// 데이터 받기
	int retval = recv(ptr->sock, ptr->buf, BUFSIZE, 0); 
	if (retval == SOCKET_ERROR) 
	{
		if (WSAGetLastError() != WSAEWOULDBLOCK) 
		{
			DisplayMessage();
			RemoveSocketInfo(GetSocketInfo(wParam));
		} 
		return;
	}
	
	// 데이터를 획득하면 그 byte크기만큼 recvbytes 값을 변경 처리
	ptr->recvbytes = retval;

	// 받은  데이터 출력
	ptr->buf[retval] = '\0'; 
	
	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(wParam, (SOCKADDR*)&clientaddr, &addrlen); 
	printf("[TCP/%s:%d] %s\n", inet_ntoa(clientaddr.sin_addr),
		ntohs(clientaddr.sin_port), ptr->buf);
}

//FD_WRITE --> write()
void OnWrite(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	SOCKETINFO* ptr = g_Clients[GetSocketInfo(wParam)];
	
	//수신데이터보다 보낸 데이터가 더 많다..
	if (ptr->recvbytes <= ptr->sendbytes)
		return;

	// 데이터 보내기(받은 데이터가 더 많을 경우)
	int retval = send(ptr->sock, ptr->buf + ptr->sendbytes,
									ptr->recvbytes - ptr->sendbytes, 0);
	if (retval == SOCKET_ERROR) 
	{
		if (WSAGetLastError() != WSAEWOULDBLOCK) 
		{
			DisplayMessage(); RemoveSocketInfo(wParam);
		} 
		return;
	}
	
	ptr->sendbytes += retval;

	// 받은    데이터를    모두    보냈는지   체크
	if (ptr->recvbytes == ptr->sendbytes) 
	{
		ptr->recvbytes = ptr->sendbytes = 0;
		if (ptr->recvdelayed) 
		{
			ptr->recvdelayed = FALSE; 
			PostMessage(hWnd, WM_SOCKET, wParam, FD_READ);
		}
	} 
	return;
}

void OnSocket(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	// 오류    발생    여부    확인 
	if(WSAGETSELECTERROR(lParam)) 
		RemoveSocketInfo(GetSocketInfo(wParam)); 

	// 메시지 처리
	switch (WSAGETSELECTEVENT(lParam))
	{
	case FD_ACCEPT:  OnAccept(hwnd, wParam, lParam); break;
	case FD_READ:	OnRead(hwnd, wParam, lParam); 
	case FD_WRITE:	OnWrite(hwnd, wParam, lParam); break;
	case FD_CLOSE:	RemoveSocketInfo(GetSocketInfo(wParam)); break;
	}
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_SOCKET:
	{
		OnSocket(hwnd, wParam, lParam);
		return 0;
	}
	case WM_CREATE:
	{
		if (!CreateListenSocket(hwnd))
		{
			MessageBox(NULL, TEXT("대기 소켓 생성  실패!"), TEXT("알림"),MB_OK); 
			SendMessage(hwnd, WM_CLOSE, 0, 0);
		}
		return 0;
	}
	
	case WM_DESTROY:
	{
		// 윈속    종료
		WSACleanup();
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//윈도우 클래스 정의
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("WSAAsyncSelect모델-서버"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 500, 500,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}