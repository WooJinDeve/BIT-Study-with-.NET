#include "std.h"

BOOL CALLBACK InsertDlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static BOOK* pbook = NULL;
	switch (msg)
	{
		//���� ȣ�� ����.
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
			//EDIT 1~5 �� �Է½� (������ȣ, å����, ���ǻ�, ����, ����) ������ pbook ����ü�� ����
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
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}
