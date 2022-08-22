#include "std.h"

HWND hListbox;
HWND hEdit;
HWND hBtn_Send, hBtn_Exit;

void ui_GetHandle(HWND hDlg)
{
	hListbox = GetDlgItem(hDlg, IDC_CLEINT);
	hEdit = GetDlgItem(hDlg, IDC_EDIT);
	hBtn_Send = GetDlgItem(hDlg, IDC_SEND);
	hBtn_Send = GetDlgItem(hDlg, IDC_EXIT);
}