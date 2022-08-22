#include "std.h"

SOCKET sClient;
extern HWND hListbox;

BOOL wbnet_LibraryInit(HWND hDlg)
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)  //버전 초기화(2.2버전)
	{
		return FALSE;
	}

	return TRUE;
}

BOOL wbnet_CreateSocket(HWND hDlg, const char* ip, int port)
{
	SOCKADDR_IN m_addr;
	sClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sClient == INVALID_SOCKET)
	{
		return FALSE;
	}
	
	m_addr.sin_family = AF_INET;
	m_addr.sin_port = htons(port);
	m_addr.sin_addr.s_addr = inet_addr(ip);
	if (connect(sClient, (LPSOCKADDR)&m_addr, sizeof(m_addr)) == SOCKET_ERROR) {
			return FALSE;
	}
	if (WSAAsyncSelect(sClient, hDlg, WM_ASYNC, FD_READ|FD_CLOSE) == SOCKET_ERROR)
	{
		return FALSE;
	}

	return TRUE;
}

void wb_SendData(HWND hDlg, const char* msg)
{
	send(sClient, msg, strlen(msg) + 1, 0);
	SendMessage(hListbox, LB_INSERTSTRING, 0, (LPARAM)msg);
}

void wb_Exit(HWND hDlg)
{
	WSACleanup();
}

BOOL wb_Connet(HWND hDlg)
{
	if (WSAAsyncSelect(sClient, hDlg, WM_ASYNC, FD_READ | FD_WRITE | FD_CLOSE) == SOCKET_ERROR)
	{
		return FALSE;
	}
	return TRUE;
}

void wbnet_Close(HWND hDlg)
{
	closesocket(sClient);
	sClient = INVALID_SOCKET;
}

void wb_recv(HWND hDlg, const char* msg)
{
	int recv_len = recv(sClient, (char*)msg, sizeof(msg), 0);
	if (recv_len == 0)
		return;
	SendMessage(hListbox, LB_INSERTSTRING, 0, (LPARAM)msg);
}

void wb_write(HWND hDlg, const char* msg)
{
	SendMessage(hListbox, LB_INSERTSTRING, 0, (LPARAM)msg);
	wsprintf((LPTSTR)msg, TEXT("connect"));
	send(sClient,msg, sizeof(msg), 0);
}

