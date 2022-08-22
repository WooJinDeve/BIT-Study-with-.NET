//handler.cpp

#include "std.h"

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	wbnet_LibraryInit();
	ui_GetControlHandle(hDlg);
	ui_EditProcChange(hDlg);

	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:				OnExit(hDlg);			return TRUE;
	case IDC_BTN_CONNECT:		OnConnect(hDlg);		return TRUE;
	case IDC_BTN_DISCONNECT:	OnDisConnect(hDlg);		return TRUE;
	case IDC_BTN_SEND:			OnSend(hDlg);			return TRUE;
	}
	return TRUE;
}

void OnExit(HWND hDlg)
{
	wbnet_LibraryExit();

	EndDialog(hDlg, IDCANCEL);
}

void OnConnect(HWND hDlg)
{
	if (wbnet_Connect(hDlg, PORT_NUMBER, SERVER_ADDR) == TRUE)
	{
		ui_ViewPrint(hDlg, TEXT("서버 연결"));
	}
	else
	{
		ui_ViewPrint(hDlg, TEXT("서버 연결 오류"));
	}
}

void OnDisConnect(HWND hDlg)
{
	wbnet_DisConnect(hDlg);
	ui_ViewPrint(hDlg, TEXT("서버 연결 해제"));
}

void OnSend(HWND hDlg)
{
	TCHAR msg[SOCK_BUFFER];

	ui_GetSendData(hDlg, msg, _countof(msg));

	wbnet_SendData(hDlg, msg, (_tcslen(msg) + sizeof(TCHAR)) * sizeof(TCHAR));

	ui_ViewPrint(hDlg, msg);
}

BOOL OnAsync(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	// 오류발생    여부    확인 
		/*if (WSAGETSELECTERROR(lParam))
			RemoveSocketInfo(GetSocketInfo(wParam));*/

	// 메시지 처리
	switch (WSAGETSELECTEVENT(lParam))
	{
	case FD_READ:	 OnRead(hDlg, wParam, lParam);	 break;
	case FD_CLOSE:	 OnClose(hDlg, wParam, lParam);  break;
	}
	return TRUE;
}

void OnRead(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	TCHAR msg[SOCK_BUFFER];

	if (wbnet_Read(hDlg, wParam, msg, WSAGETSELECTERROR(lParam)) == TRUE)
	{
		ui_ViewPrint(hDlg, msg);
	}
}

void OnClose(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	if (wbnet_Close(hDlg, wParam) == TRUE)
	{
		ui_ViewPrint(hDlg, TEXT("클라이언트 연결 해제"));
	}
}