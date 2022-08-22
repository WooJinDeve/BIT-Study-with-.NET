//handler.cpp

#include "std.h"

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	wbnet_LibraryInit();
	ui_GetControlHandle(hDlg);

	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		OnExit(hDlg);			return TRUE;
	case IDC_BTN_START:	OnServerStart(hDlg);	return TRUE;
	case IDC_BTN_EXIT:	OnServerStop(hDlg);		return TRUE;
	}
	return TRUE;
}

BOOL OnAsync(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	// �����߻�    ����    Ȯ�� 
	/*if (WSAGETSELECTERROR(lParam))
		RemoveSocketInfo(GetSocketInfo(wParam));*/

	// �޽��� ó��
	switch (WSAGETSELECTEVENT(lParam))
	{
	case FD_ACCEPT:  OnAccept(hDlg, wParam, lParam); break;
	case FD_READ:	 OnRead(hDlg, wParam, lParam);	 break;
	case FD_CLOSE:	 OnClose(hDlg, wParam, lParam);  break;
	}
	return TRUE;
}

void OnExit(HWND hDlg)
{
	wbnet_LibraryExit();

	EndDialog(hDlg, IDCANCEL);
}

void OnServerStart(HWND hDlg)
{
	if (wbnet_ServerStart(hDlg, SERVER_PORT) == TRUE)
	{
		ui_ViewPrint(hDlg, TEXT("���� ����"));
	}
	else
	{
		ui_ViewPrint(hDlg, TEXT("���� ���� ����"));
	}
}

void OnServerStop(HWND hDlg)
{
	wbnet_ServerStop(hDlg);
	ui_ViewPrint(hDlg, TEXT("���� ����"));
}

void OnAccept(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	if (wbnet_Accept(hDlg, wParam, WSAGETSELECTERROR(lParam)) == TRUE)
	{
		ui_ViewPrint(hDlg, TEXT("Ŭ���̾�Ʈ ���� ����"));
	}
	else
	{
		ui_ViewPrint(hDlg, TEXT("Ŭ���̾�Ʈ ���� ����"));
	}
}

void OnRead(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	TCHAR msg[SOCK_BUFFER];

	if (wbnet_Read(hDlg, wParam, msg, WSAGETSELECTERROR(lParam)) == TRUE)
	{
		ui_ViewPrint(hDlg, msg);

		wbnet_WriteAll(hDlg, wParam, msg);
	}
}

void OnClose(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	if (wbnet_Close(hDlg, wParam) == TRUE)
	{
		ui_ViewPrint(hDlg, TEXT("Ŭ���̾�Ʈ ���� ����"));		
	}
}

