#include "std.h"

UI::UI(HWND h)
{
	hDlg = h;
}
UI::~UI()
{

}

void UI::GetControlHandle()
{
	hEditView = GetDlgItem(hDlg, IDC_EDIT1);
	hListView = GetDlgItem(hDlg, IDC_LIST1);
}

void UI::ViewLogPrint(const TCHAR* msg)
{
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)msg);
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}

void UI::MessagePrint(TCHAR* msg)
{
	SendMessage(hListView, LB_ADDSTRING, 0, (LPARAM)msg);
}
