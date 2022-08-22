#include "std.h"

BOOL CALLBACK InsertDlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static MEMBER* pmember = NULL;
	static HWND hcombo;
	switch (msg)
	{
		//최초 호출 시점.
	case WM_INITDIALOG:
	{
		pmember = (MEMBER*)lParam;

		//콥보박스 초기화(남자, 여자)
		hcombo = GetDlgItem(hDlg, IDC_COMBO1);

		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)_T("남성"));
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)_T("여성"));
		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			pmember->id = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			GetDlgItemText(hDlg, IDC_EDIT2, pmember->name, _countof(pmember->name));
			GetDlgItemText(hDlg, IDC_EDIT3, pmember->phone, _countof(pmember->phone));
			int idx = SendMessage(hcombo, CB_GETCURSEL, 0, 0);
			if (idx == 0)
				pmember->gender = TRUE;
			else if (idx == 1)
				pmember->gender = FALSE;

			EndDialog(hDlg, IDOK);
			return TRUE;
		}
		case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
		}
	}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}
