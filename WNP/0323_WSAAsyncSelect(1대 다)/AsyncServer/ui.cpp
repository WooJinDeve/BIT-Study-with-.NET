//ui.cpp

#include "std.h"

HWND g_hEditView;

void ui_GetControlHandle(HWND hDlg)
{
	g_hEditView = GetDlgItem(hDlg, IDC_EDIT_VIEW);
}

void ui_ViewPrint(HWND hDlg, TCHAR* msg)
{
	SendMessage(g_hEditView, EM_REPLACESEL, 0, (LPARAM)msg);
	SendMessage(g_hEditView, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}
