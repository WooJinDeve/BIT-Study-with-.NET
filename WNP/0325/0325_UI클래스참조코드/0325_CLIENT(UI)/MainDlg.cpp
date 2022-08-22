//"MainDlg.h"

#include "std.h"

MainDlg* pMainDlg;	//생성자에서 대입

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
		MAKEINTRESOURCE(IDD_LOGIN), // 다이얼로그 선택
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
		MessageBox(NULL, TEXT("소켓 라이브러리 초기화 오류"), TEXT("알림"), MB_OK);
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
	//로그인 성공했다는 가정
	//자신의 컨트롤에서 ID, PW 획득
	//서버로 전송...-> 응답이 온다
	//응답이 오는 위치에서 아래 코드를 실행.....
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