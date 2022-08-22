#include "std.h"

BOOL CALLBACK InsertDlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static BOOK* pbook = NULL;
	switch (msg)
	{
		//최초 호출 시점.
	case WM_INITDIALOG:
	{
		pbook = (BOOK*)lParam;

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//EDIT 1~5 에 입력시 (고유번호, 책제목, 출판사, 가격, 제고) 순으로 pbook 구조체에 저장
			pbook->id = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			GetDlgItemText(hDlg, IDC_EDIT2, pbook->bk_title, _countof(pbook->bk_title));
			GetDlgItemText(hDlg, IDC_EDIT3, pbook->Publisher, _countof(pbook->Publisher));
			pbook->price = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
			pbook->bk_num = GetDlgItemInt(hDlg, IDC_EDIT5, 0, 0);

			EndDialog(hDlg, IDOK);
			return TRUE;
		}
		case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
		}
	}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}
