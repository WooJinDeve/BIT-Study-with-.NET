#include "std.h"
extern vector<MEMBER> g_members;
extern HWND g_child;
int g_selectid;

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_GetControlHandle(hDlg);
    ui_ComboboxInitData(hDlg);
	ui_ListViewCreateHeader(hDlg);
	ui_DummyDataInput(hDlg, &g_members);
	return TRUE;
}
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
	case IDC_BUTTON1: OnInsert(hDlg); return TRUE;
	case IDC_BUTTON2: OnSelect(hDlg); return TRUE;
	}
	return TRUE;
}

BOOL OnApply(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	for (int i = 0; i < (int)g_members.size(); i++)
	{
		MEMBER mem = g_members[i];
		if (mem.id == g_selectid)
		{
			ui_SelectPrint(hDlg,mem);
			return TRUE;
		}
	}
	MessageBox(hDlg, _T("���� ��ȣ�Դϴ�"), _T("�˸�"), MB_OK);
	return TRUE;
}

BOOL OnNotify(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_SelectListView(hDlg, wParam, lParam);
	return TRUE;
}

void OnInsert(HWND hDlg)
{
	MEMBER temp;

	UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG2), hDlg, InsertDlgProc, (LPARAM)&temp);
	if (ret == IDOK)
	{
		
		g_members.push_back(temp);
		TCHAR buf[20];
		wsprintf(buf,_T("ȸ���� : %d��"), g_members.size());
		SetDlgItemText(hDlg, IDC_STATIC1, buf);

		//ȸ������Ʈ ���
		ui_ListViewPrintAll(hDlg, g_members);
	}
}

void OnSelect(HWND hDld)
{
	
	if (g_child == NULL)
	{
		g_child = CreateDialogParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG3), hDld, SelectDlgProc, (LPARAM)&g_selectid);
		ShowWindow(g_child, SW_SHOW);
	}
	else
	{
		SetFocus(g_child);
	}
}