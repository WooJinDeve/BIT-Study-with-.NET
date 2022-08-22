//ui.cpp

#include "std.h"

void ui_SetTile(HWND hDlg)
{
	TCHAR name[20];
	GetDlgItemText(hDlg, IDC_EDIT1, name, _countof(name));

	SetWindowText(hDlg, name);
}

void ui_GetMessageInfo(HWND hDlg, TCHAR* myname, TCHAR* username, TCHAR* msg)
{
	GetDlgItemText(hDlg, IDC_EDIT1, myname, sizeof(TCHAR)*20);
	GetDlgItemText(hDlg, IDC_EDIT3, username, sizeof(TCHAR) * 20);
	GetDlgItemText(hDlg, IDC_EDIT4, msg, sizeof(TCHAR) * 100);
}

//[È«±æµ¿] ¾È³çÇÏ¼¼¿ä (14:22:12)
void ui_PrintView(HWND hDlg, TCHAR* myname, TCHAR* msg) 
{
	SYSTEMTIME st;
	GetLocalTime(&st);

	TCHAR buf[100];
	wsprintf(buf, TEXT("[%s] %s (%d:%d:%d)"), myname, msg, st.wHour, st.wMinute, st.wSecond);

	HWND hEdit = GetDlgItem(hDlg, IDC_EDIT2);
	HWND hEdit4 = GetDlgItem(hDlg, IDC_EDIT4);

	SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)buf);
	SendMessage(hEdit, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));

	SetDlgItemText(hDlg, IDC_EDIT4, TEXT(""));
	SetFocus(hEdit4);
}