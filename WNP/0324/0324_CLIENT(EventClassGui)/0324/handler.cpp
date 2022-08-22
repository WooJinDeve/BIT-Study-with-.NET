#include "std.h"

#define SOCK_BUFFER 512
#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1"

Handler::Handler(HWND Handle)
{
	hDlg = Handle;
	pwb = new WbClient;
	pui = new UI(Handle);
}
Handler::~Handler()
{
	delete pwb;
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
	case IDCANCEL:			EndDialog(hDlg, IDCANCEL);		return TRUE;
	case IDC_BUTTON1:		OnConnect();			return TRUE;
	case IDC_BUTTON2:		OnDisConnect();		return TRUE;
	case IDC_BUTTON3:		OnSend();			return TRUE;
	}
	return TRUE;
}

void Handler::OnConnect()
{
	if (pwb->CreateSocket(SERVER_IP, SERVER_PORT) == TRUE)
	{
		pui->ViewPrint(TEXT("서버 연결"));
	}
	else
	{
		pui->ViewPrint(TEXT("서버 연결 오류"));
	}
}

void Handler::OnDisConnect()
{
	pui->ViewPrint(TEXT("서버 연결 해제"));
	pwb->ExitLibrary();
}

void Handler::OnSend()
{
	TCHAR msg[SOCK_BUFFER];

	pui->GetSendData(msg, _countof(msg));

	pwb->SenData((char*)msg, (_tcslen(msg) + sizeof(TCHAR)) * sizeof(TCHAR));

	pui->ViewPrint(msg);
}