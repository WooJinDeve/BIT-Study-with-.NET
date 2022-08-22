#include "std.h"

extern HWND g_child;

BOOL CALLBACK SelectDlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static int* pid = NULL;
	switch (msg)
	{
		//최초 호출 시점.
	case WM_INITDIALOG:
	{
		pid = (int*)lParam;

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
			//종료시점.
			//EndDialog : 다이얼로그를 종료하는 함수
			//hDlg : 종료대상 , IDCANCEL : 종료시 반환값
		case IDOK:
		{
			*pid = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			SendMessage(GetParent(hDlg), WM_APPLY, 0, 0);
			return TRUE;
		}
		case IDCANCEL:
		{
			g_child = NULL;
			EndDialog(hDlg, IDCANCEL);
			return TRUE;
			}
		}
	}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}