//01_대화상자기반.cpp
/*
* skeleton 코드가 변경됨.
* 1. 리소스로 대화상자를 생성
* 2. 1번에서 만든 대화상자의 메시지를 처리할 프로시저 구현(윈도우기반 프로시저와는 다르다..)
* 3. WinMain에서는 1번에서 만든 대화상자를 실행하는 함수 호출
*    - 해당함수는 대화상자가 종료되기 전까지 리턴을 안함
*/
#include "std.h"

vector<WBPROCESS> g_processes;

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:		return OnInitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:		return OnCommand(hDlg, wParam, lParam);
	case WM_NOTIFY:			return OnNotify(hDlg, wParam, lParam);

	}
	return FALSE;
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
    UINT ret = DialogBox(hInst,// instance
        MAKEINTRESOURCE(IDD_DIALOG1), // 다이얼로그 선택
        0, // 부모 윈도우
        DlgProc); // Proc..


    return 0;
}