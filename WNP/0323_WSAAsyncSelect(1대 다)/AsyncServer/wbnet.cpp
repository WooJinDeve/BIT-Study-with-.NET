//wbnet.cpp

#include "std.h"

SOCKET g_ListenSocket;
vector<SOCKET> g_ClientSockets;

BOOL wbnet_LibraryInit()
{
	WSADATA wsaData;

	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		return FALSE;
	return TRUE;
}

void wbnet_LibraryExit()
{
	WSACleanup();
}

BOOL wbnet_ServerStart(HWND hDlg, int port)
{
	g_ListenSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (g_ListenSocket == INVALID_SOCKET) 
		return FALSE;

	SOCKADDR_IN serv_addr;
	serv_addr.sin_family = AF_INET;
	serv_addr.sin_port = htons(port);
	serv_addr.sin_addr.s_addr = htonl(INADDR_ANY);
	if (bind(g_ListenSocket, (LPSOCKADDR)&serv_addr, sizeof(serv_addr)) ==	SOCKET_ERROR)
		return FALSE;

	if (listen(g_ListenSocket, SOMAXCONN) == SOCKET_ERROR)
		return FALSE;

	if (WSAAsyncSelect(g_ListenSocket, hDlg, WM_ASYNC, FD_ACCEPT) == SOCKET_ERROR)
		return FALSE;

	return TRUE;
}

BOOL wbnet_ServerStop(HWND hDlg)
{
	closesocket(g_ListenSocket);
	return TRUE;
}

BOOL wbnet_Accept(HWND hDlg, SOCKET sock, int errcode)
{
	if (g_ClientSockets.size() >= (FD_SETSIZE - 1))
		return FALSE;

	SOCKADDR_IN cli_addr;
	int cli_len = sizeof(SOCKADDR_IN);
	SOCKET s = accept(sock, (SOCKADDR*)&cli_addr, &cli_len);
	if (s == INVALID_SOCKET)
		return FALSE;

	g_ClientSockets.push_back(s);

	WSAAsyncSelect(s, hDlg, WM_ASYNC, FD_READ | FD_CLOSE);
	return TRUE;
}

BOOL wbnet_Read(HWND hDlg, SOCKET sock, TCHAR* msg, int errcode)
{
	int recv_len = recv(sock, (char*)msg, SOCK_BUFFER, 0);
	if (recv_len == SOCKET_ERROR || recv_len == 0)
		return FALSE;

	return TRUE;	
}

void wbnet_WriteAll(HWND hDlg, SOCKET sock, TCHAR* msg)
{
	for (int i = 0; i <(int)g_ClientSockets.size(); i++)
	{
		SOCKET sock = g_ClientSockets[i];
		send(sock, (const char*)msg, SOCK_BUFFER, 0);
	}
}

BOOL wbnet_Close(HWND hDlg, SOCKET sock)
{
	for (int i = 0; i < (int)g_ClientSockets.size(); i++)
	{
		SOCKET s = g_ClientSockets[i];
		if (sock == s)
		{
			g_ClientSockets.erase(g_ClientSockets.begin() + i);
			closesocket(sock);
			return TRUE;
		}
	}	
	return FALSE;
}