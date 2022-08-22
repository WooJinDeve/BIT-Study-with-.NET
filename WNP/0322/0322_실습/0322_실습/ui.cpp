#include "std.h"

HWND hBtnLogin, hBtnLogout;
HWND g_hcombo;
HWND hMainDlg;

void ui_GetHandle(HWND hDlg)
{
	hMainDlg = hDlg;

	//버튼 핸들러
	hBtnLogin = GetDlgItem(hDlg, IDC_BUTTON1);
	hBtnLogout = GetDlgItem(hDlg, IDC_BUTTON2);
	//콤보박스
	g_hcombo = GetDlgItem(hDlg, IDC_COMBO1);

}

void ui_InitCombobox(HWND hDlg)
{
	SendMessage(g_hcombo, CB_ADDSTRING, 0,(LPARAM)_T("1조"));
	SendMessage(g_hcombo, CB_ADDSTRING, 0, (LPARAM)_T("2조"));
	SendMessage(g_hcombo, CB_ADDSTRING, 0, (LPARAM)_T("3조"));
	SendMessage(g_hcombo, CB_ADDSTRING, 0, (LPARAM)_T("4조"));
	SendMessage(g_hcombo, CB_ADDSTRING, 0, (LPARAM)_T("5조"));
}

void ui_GetLoginData(HWND hDlg, TCHAR* name, int* group)
{
	GetDlgItemText(hDlg, IDC_EDIT1, name, sizeof(TCHAR) * 20);
	*group = SendMessage(g_hcombo, CB_GETCURSEL, 0, 0) + 1;
}
void ui_SetTitle(HWND hDlg, int group, bool b)
{
	TCHAR buf[20];
	if (b == true)
		wsprintf(buf, TEXT("%d조 가입"), group);
	else
		wsprintf(buf, TEXT("%d조 탈퇴"), group);
	SetWindowText(hDlg, buf);
}

void ui_GetLogoutData(HWND hDlg, int* group)
{
	*group = SendMessage(g_hcombo, CB_GETCURSEL, 0, 0) + 1;
}