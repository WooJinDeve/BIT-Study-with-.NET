//handler.cpp

#include "std.h"

#define SERVER_PORT 8000

TCHAR g_name[20];

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_GetHandle(hDlg);
	ui_InitComboBox(hDlg);

	if (wbnet_LibraryInit() == false)
	{
		MessageBox(NULL, TEXT("소켓 라이브러리 초기화 오류"), TEXT("알림"), MB_OK);
		EndDialog(hDlg, IDCANCEL);
	}

	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:			OnExit(hDlg);		return TRUE;
	case IDC_BTN_LOGIN:		OnLogin(hDlg);		return TRUE;
	case IDC_BTN_LOGOUT:	OnLogOut(hDlg);		return TRUE;
	case IDC_BTN_Sned:		OnSendData(hDlg);	return TRUE;
	}
	return TRUE;
}

void OnSendData(HWND hDlg)
{
	TCHAR name[20];
	int groupid;  
	TCHAR msg[50];
	ui_GetSendData(hDlg, name, &groupid, msg);

	TCHAR packet[200];
	ui_GetSendMessage(name, msg, packet);

	wbnet_SendData((const char*)packet, groupid+1, SERVER_PORT);

}

void OnExit(HWND hDlg)
{
	wbnet_LibraryExit();

	EndDialog(hDlg, IDCANCEL);
}

void OnLogin(HWND hDlg)
{
	TCHAR name[20];
	int groupid;  //콤보박스 인덱스가 전달됨

	ui_GetLoginData(hDlg, name, &groupid);
	_tcscpy_s(g_name, _countof(name), name);

	if (wbnet_CreateSocket(SERVER_PORT, groupid + 1) == false)
	{
		MessageBox(NULL, TEXT("로그인 실패"), TEXT("알림"), MB_OK);
		return;
	}

	ui_SetTitle(hDlg, groupid +1, true);
}

void OnLogOut(HWND hDlg)
{
	int groupid;
	
	ui_GetLogOutData(hDlg, &groupid);
	wbnet_ExitSocket();
	ui_SetTitle(hDlg, groupid + 1, false);
}

void OnRecvData(TCHAR* buf)
{
	ui_PrintData(buf);
}