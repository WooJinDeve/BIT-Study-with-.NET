//wbnet.cpp

#include "std.h"

SOCKET g_socket;

BOOL wbnet_LibraryInit()
{
	WSADATA wsaData;

	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		return FALSE;
	return TRUE;
}

void wbnet_LibraryExit()
{
	closesocket(g_socket);

	WSACleanup();
}

BOOL wbnet_Connect(HWND hDlg, int port, char* ip)
{
	g_socket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (g_socket == INVALID_SOCKET)
		return FALSE;
		
	SOCKADDR_IN m_addr;
	m_addr.sin_family = AF_INET;
	m_addr.sin_port = htons(port);
	m_addr.sin_addr.s_addr = inet_addr(ip);
	if (connect(g_socket, (LPSOCKADDR)&m_addr, sizeof(m_addr)) == SOCKET_ERROR) 
		return FALSE;	

	if (WSAAsyncSelect(g_socket, hDlg, WM_ASYNC, FD_READ | FD_CLOSE) == SOCKET_ERROR)
		return FALSE;

	return TRUE;
}

void wbnet_DisConnect(HWND hDlg)
{
	closesocket(g_socket);
}

void wbnet_SendData(HWND hDlg, TCHAR* msg, int size)
{
	send(g_socket, (const char*)msg, size, 0);
}

BOOL wbnet_Read(HWND hDlg, SOCKET sock, TCHAR* msg, int errcode)
{
	int recv_len = recv(sock, (char*)msg, SOCK_BUFFER, 0);
	if (recv_len == SOCKET_ERROR || recv_len == 0)
		return FALSE;

	return TRUE;
}

BOOL wbnet_Close(HWND hDlg, SOCKET sock)
{
	closesocket(g_socket);
	return TRUE;
}