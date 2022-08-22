//wbnet.h
#pragma once

BOOL wbnet_LibraryInit();
void wbnet_LibraryExit();

BOOL wbnet_ServerStart(HWND hDlg, int port);

BOOL wbnet_ServerStop(HWND hDlg);

BOOL wbnet_Accept(HWND hDlg, SOCKET sock, int errcode);

BOOL wbnet_Read(HWND hDlg, SOCKET sock, TCHAR* msg, int errcode);

void wbnet_WriteAll(HWND hDlg, SOCKET sock, TCHAR* msg);

BOOL wbnet_Close(HWND hDlg, SOCKET sock);
