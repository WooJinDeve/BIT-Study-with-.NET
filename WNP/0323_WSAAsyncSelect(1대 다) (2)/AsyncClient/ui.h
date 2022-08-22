//ui.h

#pragma once

void ui_GetControlHandle(HWND hDlg);

void ui_ViewPrint(HWND hDlg, TCHAR* msg);

void ui_GetSendData(HWND hDlg, TCHAR* msg, int size);

void ui_EditProcChange(HWND hDlg);
LRESULT CALLBACK EditSubProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam);
