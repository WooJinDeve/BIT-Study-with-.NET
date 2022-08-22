#include "std.h"

BOOL CALLBACK InsertDlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static MEMBER* pmember = NULL;
	static HWND hcombo;
	switch (msg)
	{
		//���� ȣ�� ����.
	case WM_INITDIALOG:
	{
		pmember = (MEMBER*)lParam;

		//�ߺ��ڽ� �ʱ�ȭ(����, ����)
		hcombo = GetDlgItem(hDlg, IDC_COMBO1);

		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)_T("����"));
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)_T("����"));
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
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}
