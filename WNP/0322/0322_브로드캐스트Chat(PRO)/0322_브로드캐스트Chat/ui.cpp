//ui.cpp

#include "std.h"

HWND g_hCombo, g_hEditView;

void ui_GetHandle(HWND hDlg)
{
	g_hCombo = GetDlgItem(hDlg, IDC_COMBO1);
	g_hEditView = GetDlgItem(hDlg, IDC_EDIT_VIEW);
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

void ui_GetSendData(HWND hDlg, TCHAR* name, int* pgroupid, TCHAR* msg)
{
	GetDlgItemText(hDlg, IDC_EDIT_NAME, name, sizeof(TCHAR) * 10);
	*pgroupid = SendMessage(g_hCombo, CB_GETCURSEL, 0, 0);
	GetDlgItemText(hDlg, IDC_EDIT_SEND, msg, sizeof(TCHAR) * 100);
}

void ui_GetSendMessage(HWND hDlg, TCHAR* name, TCHAR* msg, TCHAR* packet)
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	wsprintf(packet, TEXT("[%s] %s(%02d:%02d:%02d)"), 
		name, msg, st.wHour, st.wMinute, st.wSecond);
}

void ui_PrintData(TCHAR* msg)
{
	SendMessage(g_hEditView, EM_REPLACESEL, 0, (LPARAM)msg);
	SendMessage(g_hEditView, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}
