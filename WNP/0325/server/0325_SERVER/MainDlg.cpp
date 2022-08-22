//"MainDlg.h"

#include "std.h"
extern vector<NEWMEMBER> g_members;
#define SERVER_PORT 9000

//extern MainDlg* pMainDlg;  //std.h에 추가 
MainDlg* pMainDlg;	//생성자에서 대입

void MainDlg::LogFunction(int flag)
{
	if (flag == 1)
		ui_ViewLogPrint(TEXT("클라이언트 연결"));
	else if (flag == 2)
		ui_ViewLogPrint(TEXT("클라이언트 연결 해제"));
}

//수신 메시지 처리 시작점!
void MainDlg::RecvMessage(TCHAR* msg)
{
	int* flag = (int*)msg;
	if (*flag == PACK_SHORTMESSAGE)
	{
		//수신 처리 요청
		ShortMessagePack* p = (ShortMessagePack*)msg;
		if (pcon->ShortMessage((ShortMessagePack*)msg) == true)
		{
			//UI출력 요청
			TCHAR arr[100];
			wsprintf(arr, TEXT("[%s] %s"), p->nickname, p->msg);
			ui_MessagePrint(arr);

			//리턴 패킷
			Packet::ShortMessageAck(p, true);
		}
		else
			Packet::ShortMessageAck(p, false);
	}	
	else if (*flag == PACK_NEWMEMBER)
	{
		NEWMEMBER* p = (NEWMEMBER*)msg;
		if (pcon->NewMemberMessage((NEWMEMBER*)msg) == true)
		{
			g_members.push_back(*p);

			TCHAR arr[100];
			wsprintf(arr, TEXT("[nickname : %s] NewMember"), p->nickname);
			ui_MessagePrint(arr);

			Packet::NewMemberMessageAck(p, true);
		}
		else
			Packet::NewMemberMessageAck(p,false);
	}
	else if (*flag == PACK_LOGIN)
	{
		LOGIN* p = (LOGIN*)msg;
		if (pcon->LoginMessage((LOGIN*)msg) == true)
		{
			TCHAR arr[100];
			wsprintf(arr, TEXT("[id : %s] join"), p->id);
			ui_MessagePrint(arr);
			Packet::LoginMessageAck(p, true);
		}
		else
			Packet::LoginMessageAck(p, false);
	}
}

MainDlg::MainDlg(HINSTANCE _hInst)
{
	hInst = _hInst;
	hdlg = 0;
	pMainDlg = this;
	pserver = new WbServer(this);
	pcon = new Container(this);
}

MainDlg::~MainDlg()
{
	delete pserver;
	delete pcon;
}

void MainDlg::Create()
{
	UINT ret = DialogBox(hInst,// instance GetModuleHandle(0)
		MAKEINTRESOURCE(IDD_SERVER), // 다이얼로그 선택
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
	case IDCANCEL:		OnExit();			return TRUE;
	case IDC_BUTTON1:	OnServerStart();	return TRUE;
	case IDC_BUTTON2:	OnServerStop();		return TRUE;
	}
	return TRUE;
}

void MainDlg::OnExit()
{
	EndDialog(hdlg, IDCANCEL);
}

void MainDlg::OnServerStart()
{
	if (pserver->CreateListenSocket(SERVER_PORT) == TRUE)
	{
		pserver->Run();
		ui_ViewLogPrint(TEXT("서버 실행"));
	}
	else
	{
		ui_ViewLogPrint(TEXT("서버 실행 오류"));
	}
}

void MainDlg::OnServerStop()
{
	pserver->CloseListenSocket();
	ui_ViewLogPrint(TEXT("서버 종료"));
}

void MainDlg::ui_GetControlHandle()
{
	hEditView = GetDlgItem(hdlg, IDC_EDIT1);
	hListView = GetDlgItem(hdlg, IDC_LIST1);
}

void MainDlg::ui_ViewLogPrint(const TCHAR* msg)
{
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)msg);
	SendMessage(hEditView, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}

void MainDlg::ui_MessagePrint(TCHAR* msg)
{
	SendMessage(hListView, LB_ADDSTRING, 0, (LPARAM)msg);
}