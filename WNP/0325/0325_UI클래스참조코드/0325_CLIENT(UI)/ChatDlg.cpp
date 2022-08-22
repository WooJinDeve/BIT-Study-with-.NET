//chatdlg.cpp

#include "std.h"

ChatDlg* pChatDlg;

ChatDlg::ChatDlg()
{
	hdlg	= 0;
	pChatDlg = this;
}

ChatDlg::~ChatDlg()
{

}

void ChatDlg::Create(HWND hParentDlg)
{	
	UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_CHAT),
		hParentDlg, pChatDlg->DlgProc, (LPARAM)0);
	if (ret == IDOK)
	{

	}
}

BOOL CALLBACK ChatDlg::DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:		return pChatDlg->OnInitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:		return pChatDlg->OnCommand(hDlg, wParam, lParam);
	}
	return FALSE;
}

BOOL ChatDlg::OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	hdlg = hDlg;
	
	return TRUE;
}

BOOL ChatDlg::OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		OnExit();		return TRUE;
	}
	return TRUE;
}

void ChatDlg::OnExit()
{
	EndDialog(hdlg, IDCANCEL);
}
