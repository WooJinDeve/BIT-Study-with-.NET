//AddMemberDlg.cpp

#include "std.h"

AddMemberDlg* pAddMemberDlg;

AddMemberDlg::AddMemberDlg()
{
	hdlg = 0;
	pAddMemberDlg = this;
}

AddMemberDlg::~AddMemberDlg()
{

}

bool AddMemberDlg::Create(HWND hparentDlg)
{
	UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_NEWMEMBER),
		hparentDlg, pAddMemberDlg->DlgProc, (LPARAM)0);
	if (ret == IDOK)
	{
		return true;
	}
	return false;
}

BOOL CALLBACK AddMemberDlg::DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:		return pAddMemberDlg->OnInitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:		return pAddMemberDlg->OnCommand(hDlg, wParam, lParam);
	}
	return FALSE;
}

BOOL AddMemberDlg::OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	hdlg = hDlg;
	return TRUE;
}

BOOL AddMemberDlg::OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		OnExit();		return TRUE;
	case IDC_BUTTON1:	OnNewMember();	return TRUE;
	}
	return TRUE;
}

void AddMemberDlg::OnExit()
{
	EndDialog(hdlg, IDCANCEL);
}

void AddMemberDlg::OnNewMember()
{
	GetNewMemberData();
	EndDialog(hdlg, IDOK);
}

void AddMemberDlg::GetNewMemberData()
{
	GetDlgItemText(hdlg, IDC_ID, id, sizeof(id));
	GetDlgItemText(hdlg, IDC_PW, pw, sizeof(pw));
	GetDlgItemText(hdlg, IDC_NAME, nickname, sizeof(nickname));
}