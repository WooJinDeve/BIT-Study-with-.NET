#pragma once

//�ʱ�ȭ
void ui_GetHandle(HWND hDlg);
void ui_InitCombobox(HWND hDlg);

//�α���
void ui_GetLoginData(HWND hDlg, TCHAR* name, int* group);
void ui_SetTitle(HWND hDlg, int group, bool b);

//�α׾ƿ�
void ui_GetLogoutData(HWND hDlg, int* group);