//handler.cpp

#include "std.h"

#define SERVER_PORT 8000
#define SERVER_IP1 _T("234.5.5.1")
#define SERVER_IP2 _T("234.5.5.2")
#define SERVER_IP3 _T("234.5.5.3")
#define SERVER_IP4 _T("234.5.5.4")
#define SERVER_IP5 _T("234.5.5.5")

TCHAR g_name[20];
int g_group;

HWND g_hwnd;
extern HWND g_hcombo;
BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	g_hwnd = hDlg;

	ui_GetHandle(hDlg);
	ui_InitCombobox(hDlg);
	OnConnect(hDlg);
	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		EndDialog(hDlg, IDCANCEL); return TRUE;
	case IDC_BUTTON1:	OnLogin(hDlg);		return TRUE;
	case IDC_BUTTON2:	OnLogout(hDlg);		return TRUE;
	}
	return TRUE;
}

void OnConnect(HWND hDlg)
{
	//---------------- 초기화 ------------------------------
	if (wbnet_LibraryInit() == -1)
	{
		MessageBox(hDlg, _T("초기화 에러"), _T("알림"), MB_OK);
		return;
	}
}

void OnLogin(HWND hDlg)
{
	//1
	ui_GetLoginData(hDlg, g_name, &g_group);
	switch (g_group)
	{
	case 1: wbnet_CreateSocket(SERVER_PORT, SERVER_IP1); break;
	case 2: wbnet_CreateSocket(SERVER_PORT, SERVER_IP2); break;
	case 3: wbnet_CreateSocket(SERVER_PORT, SERVER_IP2); break;
	case 4: wbnet_CreateSocket(SERVER_PORT, SERVER_IP4); break;
	case 5: wbnet_CreateSocket(SERVER_PORT, SERVER_IP5); break;
	}

	//6
	ui_SetTitle(hDlg, g_group,true);
}

void OnLogout(HWND hDlg)
{
	ui_GetLogoutData(hDlg, &g_group);
	OnExit(hDlg);
	ui_SetTitle(hDlg, g_group, false);
}

void OnExit(HWND hDlg)
{
	wbnet_DeleteSocket();
	wbnet_LibraryExit();
}