//ui.cpp

#include "std.h"

HWND g_hEditView, g_hEditSend;
HWND g_hDlg;

WNDPROC  OldEditProc;

void ui_GetControlHandle(HWND hDlg)
{
	g_hDlg		= hDlg;
	g_hEditView = GetDlgItem(hDlg, IDC_EDIT_VIEW);
	g_hEditSend = GetDlgItem(hDlg, IDC_EDIT_SEND);
}

void ui_ViewPrint(HWND hDlg, TCHAR* msg)
{
	SendMessage(g_hEditView, EM_REPLACESEL, 0, (LPARAM)msg);
	SendMessage(g_hEditView, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}

void ui_GetSendData(HWND hDlg, TCHAR* msg, int size)
{
	GetDlgItemText(hDlg, IDC_EDIT_SEND, msg, size);
}

void ui_EditProcChange(HWND hDlg)
{	
	OldEditProc = (WNDPROC)SetWindowLong(g_hEditSend, GWL_WNDPROC, (LONG)EditSubProc);
}

static LRESULT CALLBACK EditSubProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam)
{	
	switch (iMessage) 
	{
	case WM_KEYUP:
	{
		if (wParam == VK_RETURN)
		{			
			SendMessage(g_hDlg, WM_COMMAND, IDC_BTN_SEND, 0);
			SetWindowText(hWnd, TEXT(""));
			SetFocus(hWnd);
		}
		break;
	}	
	}
	return CallWindowProc(OldEditProc, hWnd, iMessage, wParam, lParam);
}
