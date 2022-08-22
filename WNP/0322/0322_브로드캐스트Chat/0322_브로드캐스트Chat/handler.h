//handler.h

#pragma once

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnSendData(HWND hDlg);
void OnExit(HWND hDlg);
void OnLogin(HWND hDlg);
void OnLogOut(HWND hDlg);
void OnRecvData(TCHAR* buf);