//"MainDlg.h"

#include "std.h"

#define SOCK_BUFFER 100
#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1"

//extern MainDlg* pMainDlg;  //std.h에 추가 
MainDlg* pMainDlg;	//생성자에서 대입

//수신 메시지 처리 시작점!
void MainDlg::RecvMessage(TCHAR* msg)
{
	int* flag = (int*)msg;
	if (*flag == PACK_LOGIN_S)
	{
		//수신버튼 활성화
		EnableWindow(hBtnLogin, FALSE);
		EnableWindow(hBtnSend, TRUE);
	}
	else if (*flag == PACK_LOGIN_F)
	{
		MessageBox(hdlg, _T("송신오류"), _T("알림"), MB_OK);
	}
	else if (*flag == SHORTMESSAGE_S)
	{
		//수신 처리 요청
		ShortMessagePack* p = (ShortMessagePack*)msg;		

		//UI출력 요청
		TCHAR arr[100];
		wsprintf(arr, TEXT("[%s] %s"), p->nickname, p->msg);
		ui_ViewPrint(arr);
	}
	else if (*flag == SHORTMESSAGE_F)
	{

	}
	else if (*flag == PACK_NEWMEMBER_S)
	{
		MessageBox(NULL, _T("회원 가입 성공"), _T("알림"), MB_OK);
		EnableWindow(hBtnLogin, FALSE);
	}
	else if (*flag == PACK_NEWMEMBER_F)
	{
		MessageBox(hdlg, _T("회원 가입 실패"), _T("알림"), MB_OK);
	}
}

MainDlg::MainDlg(HINSTANCE _hInst)
{
	hInst = _hInst;
	hdlg = 0;
	pMainDlg = this;
	pwb = new WbClient(this);
}

MainDlg::~MainDlg()
{
	delete pwb;
}

void MainDlg::Create()
{
	UINT ret = DialogBox(hInst,// instance GetModuleHandle(0)
		MAKEINTRESOURCE(IDD_CLIENT), // 다이얼로그 선택
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
	ui_GetControlHandle();

	return TRUE;
}

BOOL MainDlg::OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		OnExit();		return TRUE;
	case IDC_BUTTON1:	OnConnect();	return TRUE;
	case IDC_BUTTON2:	OnDisConnect();	return TRUE;
	case IDC_BUTTON3:	OnSend();		return TRUE;
	case IDC_BUTTON4:	OnLogin();		return TRUE;
	case IDC_BUTTON5:	OnNewMember();	return TRUE;
	}
	return TRUE;
}

void MainDlg::OnExit()
{
	EndDialog(hdlg, IDCANCEL);
}

void MainDlg::OnConnect()
{
	if (pwb->CreateSocket(SERVER_IP, SERVER_PORT) == TRUE)
	{
		ui_ViewPrint(TEXT("서버 연결"));
	}
	else
	{
		ui_ViewPrint(TEXT("서버 연결 오류"));
	}
}

void MainDlg::OnDisConnect()
{
	ui_ViewPrint(TEXT("서버 연결 해제"));
	pwb->ExitLibrary();
}

void MainDlg::OnSend()
{
	TCHAR msg[SOCK_BUFFER];
	TCHAR name[20] = { 0 };
	ui_GetSendData(msg, _countof(msg));

	ShortMessagePack pack =  Packet::ShortMessage(name, msg);

	pwb->SenData((char*)&pack, sizeof(pack));
}

void MainDlg::OnLogin()
{
	TCHAR ID[20];
	TCHAR PW[20];

	ui_GetLoginData(ID, PW, sizeof(ID));

	LOGIN pack = Packet::Login(ID,PW);

	pwb->SenData((char*)&pack, sizeof(pack));
}

void MainDlg::OnNewMember()
{
	ShowWindow(hdlg, SW_HIDE);
	AddMemberDlg pDlg;
	TCHAR ID[20];
	TCHAR PW[20];
	TCHAR NICK[20];
	if (pDlg.Create(hdlg) == TRUE)
	{
		_tcscpy_s(ID, _countof(ID), pDlg.getId());
		_tcscpy_s(PW, _countof(PW), pDlg.getPw());
		_tcscpy_s(NICK, _countof(NICK), pDlg.getNickName());
	}
	NEWMEMBER pack = Packet::NewMember(ID, PW, NICK);

	pwb->SenData((char*)&pack, sizeof(pack));
	ShowWindow(hdlg, SW_SHOW);
}


void MainDlg::ui_GetControlHandle()
{
	hEditView = GetDlgItem(hdlg, IDC_EDIT1);
	hEditSend = GetDlgItem(hdlg, IDC_EDIT3);
	hEditID = GetDlgItem(hdlg, IDC_EDIT5);
	hEditPW = GetDlgItem(hdlg, IDC_EDIT4);
	hBtnLogin = GetDlgItem(hdlg, IDC_BUTTON4);
	hBtnSend = GetDlgItem(hdlg, IDC_BUTTON3);
	tBtnMember = GetDlgItem(hdlg, IDC_BUTTON5);
	EnableWindow(hBtnSend, TRUE);
}

void MainDlg::ui_ViewPrint(const TCHAR* msg)
{
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)msg);
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}

void MainDlg::ui_GetSendData(TCHAR* msg, int size)
{
	GetDlgItemText(hdlg, IDC_EDIT3, msg, size);
}


void MainDlg::ui_GetLoginData(TCHAR* ID, TCHAR* PW,int size)
{
	GetDlgItemText(hdlg, IDC_EDIT5, ID, size);
	GetDlgItemText(hdlg, IDC_EDIT4, PW, size);
}


void MainDlg::ui_GetIDData(TCHAR* ID,int size)
{
	GetDlgItemText(hdlg, IDC_EDIT5, ID, size);
}