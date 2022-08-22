#pragma once

BOOL wbnet_LibraryInit(HWND hDlg);

BOOL wbnet_CreateSocket(HWND hDlg, const char* ip, int port);

void wb_SendData(HWND hDlg, const char* msg);

void wb_Exit(HWND hDlg);

BOOL wb_Connet(HWND hDlg);

void wbnet_Close(HWND hDlg);

void wb_recv(HWND hDlg, const char* msg);

void wb_write(HWND hDlg, const char* msg);