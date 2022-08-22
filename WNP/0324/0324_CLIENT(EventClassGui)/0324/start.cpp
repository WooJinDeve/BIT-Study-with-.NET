#include "std.h"

#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1"

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static Handler p(hDlg);
	switch (msg)
	{
	case WM_INITDIALOG:	p.OnInitDialog(wParam, lParam);	return TRUE;
	case WM_COMMAND:	p.OnCommand(wParam, lParam);	return TRUE;
	//case WM_ASYNC:		OnAsync(hDlg, wParam, lParam);		return TRUE;
	}
	return FALSE;
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	UINT ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_CLIENT), // 다이얼로그 선택
		0, // 부모 윈도우
		DlgProc); // Proc..

	return 0;
}

//int main(void)
//{
//	WbClient client; // 생성자 호출
//
//	if (client.CreateSocket(SERVER_IP, SERVER_PORT) == false)
//		return 0;
//	
//	TCHAR buf[1024];
//	while (true)
//	{
//		_getts_s(buf, _countof(buf));
//		if (_tcslen(buf) == 0)
//			break;
//
//		client.SenData((char*)buf, (_tcslen(buf)+1)*2);
//	}
//
//	printf("프로그램 종료 \n");
//	return 0;
//}