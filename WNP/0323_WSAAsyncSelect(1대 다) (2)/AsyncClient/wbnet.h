//wbnet.h
#pragma once

BOOL wbnet_LibraryInit();
void wbnet_LibraryExit();

BOOL wbnet_Connect(HWND hDlg, int port, char* ip);
void wbnet_DisConnect(HWND hDlg);

void wbnet_SendData(HWND hDlg, TCHAR* msg, int size);

BOOL wbnet_Read(HWND hDlg, SOCKET sock, TCHAR* msg, int errcode);

BOOL wbnet_Close(HWND hDlg, SOCKET sock);
