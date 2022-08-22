//ui.cpp

#include "std.h"

HWND g_hCombo;
HWND hEdit;

void ui_GetHandle(HWND hDlg)
{
	g_hCombo = GetDlgItem(hDlg, IDC_COMBO1);
	hEdit = GetDlgItem(hDlg, IDC_EDIT1);
}

void ui_InitComboBox(HWND hDlg)
{
	SendMessage(g_hCombo, CB_ADDSTRING, 0, (LPARAM)TEXT("1조"));
	SendMessage(g_hCombo, CB_ADDSTRING, 0, (LPARAM)TEXT("2조"));
	SendMessage(g_hCombo, CB_ADDSTRING, 0, (LPARAM)TEXT("3조"));
	SendMessage(g_hCombo, CB_ADDSTRING, 0, (LPARAM)TEXT("4조"));
	SendMessage(g_hCombo, CB_ADDSTRING, 0, (LPARAM)TEXT("5조"));
}

void ui_GetLoginData(HWND hDlg, TCHAR* name, int* pgroupid)
{
	GetDlgItemText(hDlg, IDC_EDIT_NAME, name, sizeof(TCHAR) * 10);
	*pgroupid = SendMessage(g_hCombo, CB_GETCURSEL, 0, 0);
}

void ui_GetLogOutData(HWND hDlg, int* pgroupid)
{
	*pgroupid = SendMessage(g_hCombo, CB_GETCURSEL, 0, 0);
}

void ui_GetSendData(HWND hDlg, TCHAR* name, int* pgroupid, TCHAR* msg)
{
	GetDlgItemText(hDlg, IDC_EDIT_NAME, name, sizeof(TCHAR) * 10);
	*pgroupid = SendMessage(g_hCombo, CB_GETCURSEL, 0, 0);
	GetDlgItemText(hDlg, IDC_EDIT_SEND, msg, sizeof(TCHAR) * 50);
}
void ui_GetSendMessage(TCHAR* name, TCHAR* msg, TCHAR* packet)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	wsprintf(packet, _T("[%s] %s (%d:%d:%d)"), name, msg, st.wHour, st.wMinute, st.wSecond);
}


void ui_SetTitle(HWND hDlg, int groupid, bool b)
{
	TCHAR buf[20];
	if (b)
	{
		wsprintf(buf, TEXT("%d조 가입"), groupid);
	}
	else
	{
		wsprintf(buf, TEXT("%d조 탈퇴"), groupid);
	}
	SetWindowText(hDlg, buf);
}

void ui_PrintData(TCHAR* buf)
{
	SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)buf);
	SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}