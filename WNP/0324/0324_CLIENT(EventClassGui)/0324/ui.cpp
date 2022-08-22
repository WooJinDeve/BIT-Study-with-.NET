#include "std.h"

UI::UI(HWND handler)
{
	hDlg = handler;
}

UI::~UI()
{
}

void UI::GetControlHandle()
{
	hEditView = GetDlgItem(hDlg, IDC_EDIT1);
	hEditSend = GetDlgItem(hDlg, IDC_EDIT2);
}
void UI::ViewPrint(const TCHAR* msg)
{
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)msg);
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}

void UI::GetSendData(TCHAR* msg, int size)
{
	GetDlgItemText(hDlg, IDC_EDIT2, msg, size);
}

