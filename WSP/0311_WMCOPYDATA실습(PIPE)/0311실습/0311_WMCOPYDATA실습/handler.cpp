//handler.cpp

#include "std.h"

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL: EndDialog(hDlg, IDCANCEL);	return TRUE;
	case IDC_BUTTON1:  OnSetTitle(hDlg);		return TRUE;
	case IDC_BUTTON2:  OnSendMessage(hDlg);		return TRUE;
		
	}
	return TRUE;
}


BOOL OnCopyData(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	DATA temp;
	
	fun_GetCopyData(lParam, &temp);

	ui_PrintView(hDlg, temp.name, temp.msg);
	return TRUE;
}


void OnSetTitle(HWND hDlg)
{
	ui_SetTile(hDlg);
}

void OnSendMessage(HWND hDlg)
{
	TCHAR myname[20], username[20], msg[100];

	ui_GetMessageInfo(hDlg, myname, username, msg);

	if(fun_SendMessage(myname, username, msg))
		ui_PrintView(hDlg, myname, msg);

}