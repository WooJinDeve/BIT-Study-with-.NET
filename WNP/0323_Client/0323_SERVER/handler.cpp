#include "std.h"

//초기화
BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_GetHandle(hDlg);
	if (wbnet_LibraryInit(hDlg) == false)
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
	case IDCANCEL:		EndDialog(hDlg, IDCANCEL); return TRUE;
	case IDC_SEND:		OnSendData(hDlg); return TRUE;
	case IDC_EXIT:		OnExit(hDlg);	return TRUE;
	}
	return TRUE;
}

BOOL OnAsync(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (WSAGETSELECTEVENT(lParam))
	{
	case FD_CONNECT:  OnConnet(hDlg, wParam, lParam); break;
	case FD_READ:	 OnRead(hDlg, wParam, lParam);	 break;
	case FD_WRITE:	 OnWrite(hDlg, wParam, lParam);	 break;
	case FD_CLOSE:	 OnClose(hDlg, wParam, lParam);  break;
	}
	return TRUE;
}
TCHAR msgInfo[60];

void OnSendData(HWND hDlg)
{
	if (wbnet_CreateSocket(hDlg, (const char*)SERVER_ADDR, PORT_NUMBER) == FALSE)
	{
		EndDialog(hDlg, IDCANCEL);
	}
	GetDlgItemText(hDlg, IDC_EDIT, msgInfo, 60);

	wb_SendData(hDlg, (const char*)msgInfo);

}

void OnExit(HWND hDlg)
{
	wb_Exit(hDlg);
	EndDialog(hDlg, IDCANCEL);
}

void OnConnet(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	if (wb_Connet(hDlg) == FALSE)
	{
		return;
	}
}

void OnClose(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	wbnet_Close(hDlg);
}

void OnRead(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	wb_recv(hDlg, (const char*)msgInfo);

}
void OnWrite(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	wb_write(hDlg,(const char*)msgInfo);
}
	