//"MainDlg.h"

#include "std.h"

MainDlg* pMainDlg;	//�����ڿ��� ����

MainDlg::MainDlg(HINSTANCE _hInst)
{
	hInst	= _hInst;
	hdlg    = 0;
	pMainDlg = this;
}

MainDlg::~MainDlg()
{

}

void MainDlg::Create()
{
	UINT ret = DialogBox(hInst,// instance GetModuleHandle(0)
		MAKEINTRESOURCE(IDD_LOGIN), // ���̾�α� ����
		0, DlgProc);
}

BOOL CALLBACK MainDlg::DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:		return pMainDlg->OnInitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:		return pMainDlg->OnCommand(hDlg, wParam, lParam);
	}
	return FALSE;
}

BOOL MainDlg::OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	hdlg = hDlg;
	/*ui_GetHandle(hDlg);
	ui_InitComboBox(hDlg);

	if (wbnet_LibraryInit() == false)
	{
		MessageBox(NULL, TEXT("���� ���̺귯�� �ʱ�ȭ ����"), TEXT("�˸�"), MB_OK);
		EndDialog(hDlg, IDCANCEL);
	}*/

	return TRUE;
}

BOOL MainDlg::OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		OnExit();		return TRUE;
	case IDC_LOGIN:		OnLogin();		return TRUE;
	case IDC_ADDMEMBER:	OnNewMember();	return TRUE;
	}
	return TRUE;
}

void MainDlg::OnExit()
{
//	wbnet_LibraryExit();

	EndDialog(hdlg, IDCANCEL);
}

void MainDlg::OnLogin()
{
	//�α��� �����ߴٴ� ����
	//�ڽ��� ��Ʈ�ѿ��� ID, PW ȹ��
	//������ ����...-> ������ �´�
	//������ ���� ��ġ���� �Ʒ� �ڵ带 ����.....
	ShowWindow(hdlg, SW_HIDE);
	ChatDlg pDlg;
	pDlg.Create(hdlg);
	ShowWindow(hdlg, SW_SHOW);
}

void MainDlg::OnNewMember()
{
	ShowWindow(hdlg, SW_HIDE);
	AddMemberDlg pDlg;
	pDlg.Create(hdlg);
	ShowWindow(hdlg, SW_SHOW);
}