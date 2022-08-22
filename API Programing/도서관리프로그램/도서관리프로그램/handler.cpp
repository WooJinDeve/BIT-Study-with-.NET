#include "std.h"

extern vector<BOOK> g_books;
extern HWND g_child;
int g_selectid;

//-----------------Insert-------------------------------------------
BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_GetControlHandle(hDlg);
	ui_ListViewCreateHeader(hDlg);
	ui_DummyDataInput(hDlg, &g_books);
	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
	case IDC_BUTTON1: OnInsert(hDlg); return TRUE;
	case IDC_BUTTON2: OnSelect(hDlg); return TRUE;
	case IDC_BUTTON3: OnUpdate(hDlg); return TRUE;
	case IDC_BUTTON4: OnDelete(hDlg); return TRUE;
	case IDC_BUTTON5: ui_ListViewPrintAll(hDlg, g_books); return TRUE;
	}
	return TRUE;
}

void OnInsert(HWND hDlg)
{
	BOOK temp;

	UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG2), hDlg, InsertDlgProc, (LPARAM)&temp);
	if (ret == IDOK)
	{

		g_books.push_back(temp);
		TCHAR buf[20];
		wsprintf(buf, _T("등록된 책 : %d개"), g_books.size());
		SetDlgItemText(hDlg, IDC_STATIC2, buf);

		//회원리스트 출력
		ui_ListViewPrintAll(hDlg, g_books);
	}
}

//----------------------------------검색
BOOL OnApply(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	for (int i = 0; i < (int)g_books.size(); i++)
	{
		BOOK book = g_books[i];
		if (book.id == g_selectid)
		{
			ui_SelectPrint(hDlg, book);
			return TRUE;
		}
	}
	MessageBox(hDlg, _T("없는 번호입니다"), _T("알림"), MB_OK);
	return TRUE;
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

//----------------------------------클릭선택
BOOL OnNotify(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_SelectListView(hDlg, wParam, lParam);
	return TRUE;
}
//---------------------------------수정
void OnUpdate(HWND hDlg)
{
	ui_update(hDlg);
}

//---------------------------------삭제
void OnDelete(HWND hDlg)
{
	ui_delete(hDlg, &g_books);
}
