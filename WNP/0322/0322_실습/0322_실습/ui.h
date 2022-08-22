#pragma once

//초기화
void ui_GetHandle(HWND hDlg);
void ui_InitCombobox(HWND hDlg);

//로그인
void ui_GetLoginData(HWND hDlg, TCHAR* name, int* group);
void ui_SetTitle(HWND hDlg, int group, bool b);

//로그아웃
void ui_GetLogoutData(HWND hDlg, int* group);