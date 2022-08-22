#include "std.h"

void Handler::LogFuntion(int flag)
{
	if (flag == 1)
		pui->ViewLogPrint(TEXT("클라이언트 연결"));

	else if (flag == 2)
		pui->ViewLogPrint(TEXT("클라이언트 연결 해제"));
}
//수신한 메시지 처리 시작점!
void Handler::RecvMessage(TCHAR* msg)
{
	pui->MessagePrint(msg);
}

Handler::Handler(HWND handle)
{
	hDlg = handle;
	pserver = new WbServer(this); // 쌍방 참조
	pui = new UI(handle);
}

Handler::~Handler()
{
	delete pserver;
	delete pui;
}

BOOL Handler::OnInitDialog(WPARAM wParam, LPARAM lParam)
{
	pui->GetControlHandle();

	return TRUE;
}

BOOL Handler::OnCommand(WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		OnExit();			return TRUE;
	case IDC_BUTTON1:	OnServerStart();	return TRUE;
	case IDC_BUTTON2:	OnServerStop();		return TRUE;
	}
	return TRUE;
}

void Handler::OnExit()
{
	EndDialog(hDlg, IDCANCEL);
}

void Handler::OnServerStart()
{
	if (pserver->CreateListenSocket(SERVER_PORT) == true)
	{
		pserver->Run();
		pui->ViewLogPrint(TEXT("서버 실행"));
	}
	else
	{
		pui->ViewLogPrint(TEXT("서버 실행 오류"));
	}
}

void Handler::OnServerStop()
{
	pserver->CloseListenSocket();
	pui->ViewLogPrint(TEXT("서버 종료"));
}
