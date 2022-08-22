//WSAASYNCSELECT��(����)
//Ŭ���̾�Ʈ �������� �ִ� 64�� ���� ����!
// FD_SETSIZE : ������ �� �ִ� �ִ� ũ��

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

// ���� ���������� ���� ����ü
struct SOCKETINFO 
{
	SOCKET sock;
	char buf[BUFSIZE]; 
	int recvbytes; 
	int sendbytes; 
	BOOL recvdelayed;		//??
};

HWND hWnd;							//�������� �ڵ�
SOCKET listenSock;					//��� ����
vector<SOCKETINFO*> g_Clients;		//��� ���ϵ�


// ���� �Լ� ���� ��� 
void DisplayMessage() 
{
	TCHAR* pMsg; 
	FormatMessage(
	FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM, NULL,
	WSAGetLastError(),
	MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR)&pMsg, 0, NULL);
	MessageBox(NULL, pMsg, TEXT("�˸�"), MB_OK); 
	LocalFree(pMsg);
}

//------------------------------------  ����� ���� �Լ�--------------------
//��� ���� ����(��Ʈ��ũ�̺�Ʈ-������޽��� ����)
bool CreateListenSocket(HWND hwnd) 
{
	// ���� �ʱ�ȭ
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return false;
	
	int retval;
	// ��� ���� ����
	listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); 
	if (listenSock == INVALID_SOCKET)
	{
		DisplayMessage(); 
		return false;
	}

	//��� ���ϰ�   �����   ����   �޽���    WM_SOCKET��    �����Ѵ�. 
	//�ͺ��ŷ    ��������    �����ϴ�    �ϵ�    ����    �Ѵ�.
	retval = WSAAsyncSelect(listenSock, hwnd, WM_SOCKET, FD_ACCEPT | FD_CLOSE);
	if (retval == SOCKET_ERROR) 
	{
		DisplayMessage(); 
		return false;
	}

	// ���    ������    ����    �ּ�, ��Ʈ    ����
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
	// ���    ������   ����    ���    ť    ����    ��    Ŭ���̾�Ʈ    ����    ���
	retval = listen(listenSock, SOMAXCONN); 
	if (retval == SOCKET_ERROR)
	{
		DisplayMessage(); return false;
	}
	return true;
}

//���� --> ���� �ε��� ��ȯ
int GetSocketInfo(SOCKET sock) 
{
	for (int i = 0; i < (int)g_Clients.size(); ++i) 
	{
		if (g_Clients[i]->sock == sock)
			return i;
	}
	return -1; 
}

// ���� ������ �����Ѵ�.
void RemoveSocketInfo(int nIndex) 
{
	SOCKETINFO* ptr = g_Clients[nIndex];

	SOCKADDR_IN clientaddr;
	int addrlen = sizeof(clientaddr);
	getpeername(ptr->sock, (SOCKADDR*)&clientaddr, &addrlen); 
	TCHAR msg[100];
	wsprintf(msg, TEXT("[TCP ����] Ŭ���̾�Ʈ    ����: IP �ּ�=%s, ��Ʈ    ��ȣ=%d"),
		inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));
	MessageBox(NULL, msg, TEXT("�˸�"), MB_OK);
	
	closesocket(ptr->sock); 
	delete ptr;
	g_Clients.erase(g_Clients.begin() + nIndex);
}

//���� ������ �߰��Ѵ�.
BOOL AddSocketInfo(SOCKET sock) 
{
	// FD_SETSIZE - ����   ���   ���� 
	if(g_Clients.size() >= (FD_SETSIZE-1))
	{
		printf("[����] ����    ������    �߰���    ��    �����ϴ�!\n"); 
		return FALSE;
	}

	SOCKETINFO* ptr = new SOCKETINFO; 
	if (ptr == NULL) 
	{
		printf("[����] �޸𸮰�    �����մϴ�!\n"); 
		return FALSE;
	}

	ptr->sock = sock;
	ptr->recvbytes = 0;
	ptr->sendbytes = 0;
	g_Clients.push_back(ptr);

	return TRUE;
}


//��Ʈ��ũ �̺�Ʈ ó��
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
	
	printf("[TCP ����] Ŭ���̾�Ʈ    ����: IP �ּ�=%s, ��Ʈ    ��ȣ=%d\n",
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

	//�ڽ��� �Է¹��ۿ� ������ �޾� �� �����Ͱ� ����
	//��Ʈ��ũ �Է� ���ۿ� �����ϴ� Ŭ���̾�Ʈ�� ���� ������ �������� �ʰڴ�.
	if (ptr->recvbytes > 0) 
	{
		ptr->recvdelayed = TRUE; 
		return;
	}

	// ������ �ޱ�
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
	
	// �����͸� ȹ���ϸ� �� byteũ�⸸ŭ recvbytes ���� ���� ó��
	ptr->recvbytes = retval;

	// ����  ������ ���
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
	
	//���ŵ����ͺ��� ���� �����Ͱ� �� ����..
	if (ptr->recvbytes <= ptr->sendbytes)
		return;

	// ������ ������(���� �����Ͱ� �� ���� ���)
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

	// ����    �����͸�    ���    ���´���   üũ
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
	// ����    �߻�    ����    Ȯ�� 
	if(WSAGETSELECTERROR(lParam)) 
		RemoveSocketInfo(GetSocketInfo(wParam)); 

	// �޽��� ó��
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
			MessageBox(NULL, TEXT("��� ���� ����  ����!"), TEXT("�˸�"),MB_OK); 
			SendMessage(hwnd, WM_CLOSE, 0, 0);
		}
		return 0;
	}
	
	case WM_DESTROY:
	{
		// ����    ����
		WSACleanup();
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("WSAAsyncSelect��-����"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, 500, 500,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}