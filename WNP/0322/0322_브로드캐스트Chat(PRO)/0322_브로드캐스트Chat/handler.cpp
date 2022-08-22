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
		MessageBox(NULL, TEXT("���� ���̺귯�� �ʱ�ȭ ����"), TEXT("�˸�"), MB_OK);
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
	case IDC_BTN_SENDDATA:	OnSendData(hDlg);	return TRUE;
	}
	return TRUE;
}

void OnExit(HWND hDlg)
{
	wbnet_LibraryExit();

	EndDialog(hDlg, IDCANCEL);
}

void OnLogin(HWND hDlg)
{
	TCHAR name[20];
	int groupid;  //�޺��ڽ� �ε����� ���޵�

	ui_GetLoginData(hDlg, name, &groupid);
	_tcscpy_s(g_name, _countof(name), name);

	if (wbnet_CreateSocket(SERVER_PORT, groupid + 1, OnRecvData) == false)
	{
		MessageBox(NULL, TEXT("�α��� ����"), TEXT("�˸�"), MB_OK);
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

void OnSendData(HWND hDlg)
{
	TCHAR name[20], msg[100];
	int groupid;  //�޺��ڽ� �ε����� ���޵�

	ui_GetSendData(hDlg, name, &groupid, msg);
	
	TCHAR packet[200];
	ui_GetSendMessage(hDlg, name, msg, packet);

	wbnet_SendData(SERVER_PORT, groupid+1, packet);
}

void OnRecvData(TCHAR* msg)
{
	ui_PrintData(msg);
}

