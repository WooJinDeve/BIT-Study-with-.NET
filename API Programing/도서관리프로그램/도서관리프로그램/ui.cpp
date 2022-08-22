#include "std.h"
extern vector<BOOK> g_books;
HWND g_hListView;

void ui_GetControlHandle(HWND hDlg)
{
	g_hListView = GetDlgItem(hDlg, IDC_LIST1);
}

void ui_ListViewCreateHeader(HWND hDlg)
{
	LVCOLUMN COL;
	// ����� �߰��Ѵ�.
	COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	COL.fmt = LVCFMT_LEFT;
	//-------------------------------------------
	COL.cx = 125;
	COL.pszText = (LPTSTR)_T("������ȣ");				// ù ��° ���
	COL.iSubItem = 0;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)_T("å����");			// �� ��° ���
	COL.iSubItem = 1;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

	COL.cx = 130;
	COL.pszText = (LPTSTR)_T("���ǻ�");				// �� ��° ���
	COL.iSubItem = 2;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)_T("����");				// �� ��° ���
	COL.iSubItem = 3;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 3, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)_T("����");				// �� ��° ���
	COL.iSubItem = 4;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 4, (LPARAM)&COL);
	ListView_SetExtendedListViewStyle(g_hListView, LVS_EX_FULLROWSELECT |
		LVS_EX_GRIDLINES | LVS_EX_HEADERDRAGDROP);

}

void ui_ListViewPrintAll(HWND hDlg, vector<BOOK> books)
{
	//����Ʈ�� ��µ� ������ clear
	ListView_DeleteAllItems(g_hListView);

	for (int i = 0; i < (int)books.size(); i++)
	{
		BOOK book = books[i];

		//-----------------------------------------------------
		// �ؽ�Ʈ�� �̹����� ���� �����۵��� ����Ѵ�.
		LVITEM LI;
		LI.mask = LVIF_TEXT;

		TCHAR temp[20]; // ���ڸ� �Է¹ޱ����� ����
     	LI.iItem = i;    //����Ʈ�信 ��µǴ� ���� ��ġ

		LI.iSubItem = 0;
		wsprintf(temp, TEXT("%d"), book.id);
		LI.pszText = temp;			// ù ��° ������
		SendMessage(g_hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 1;
		LI.pszText = book.bk_title;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText = book.Publisher;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 3;
		wsprintf(temp, TEXT("%d"), book.price);
		LI.pszText = temp;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 4;
		wsprintf(temp, TEXT("%d"), book.bk_num);
		LI.pszText = temp;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);
	}
}

void ui_SelectPrint(HWND hDlg, BOOK book)
{
	ListView_DeleteAllItems(g_hListView);

	BOOK mem = book;

	//-----------------------------------------------------
	// �ؽ�Ʈ�� �̹����� ���� �����۵��� ����Ѵ�.
	LVITEM LI;
	LI.mask = LVIF_TEXT;

	TCHAR temp[20]; // ���ڸ� �Է¹ޱ����� ����
	LI.iItem = 0;    //����Ʈ�信 ��µǴ� ���� ��ġ
	LI.iSubItem = 0;
	wsprintf(temp, TEXT("%d"), book.id);
	LI.pszText = temp;			// ù ��° ������
	SendMessage(g_hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

	LI.iSubItem = 1;
	LI.pszText = book.bk_title;
	SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

	LI.iSubItem = 2;
	LI.pszText = book.Publisher;
	SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

	LI.iSubItem = 3;
	wsprintf(temp, TEXT("%d"), book.price);
	LI.pszText = temp;
	SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

	LI.iSubItem = 4;
	wsprintf(temp, TEXT("%d"), book.bk_num);
	LI.pszText = temp;
	SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

}

void ui_DummyDataInput(HWND hDlg, vector<BOOK>* books)
{
	BOOK book1 = { 1111,_T("���"), _T("��۴��б�"),10000,20 };
	books->push_back(book1);

	BOOK book2 = { 2222,_T("��Ʈ"), _T("IT�����к�"),20000,24 };
	books->push_back(book2);

	BOOK book3 = { 3333,_T("���"), _T("��ǻ����������"),12000,26 };
	books->push_back(book3);

	ui_ListViewPrintAll(hDlg, *books);
}

void ui_SelectListView(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	LPNMHDR hdr;
	LPNMLISTVIEW nlv;
	hdr = (LPNMHDR)lParam;
	nlv = (LPNMLISTVIEW)lParam;

	if (hdr->hwndFrom == g_hListView)
	{
		switch (hdr->code)
		{
			// ���õ� �׸��� ����Ʈ�� �����ش�.
			case LVN_ITEMCHANGED:
			{
				if (nlv->uChanged == LVIF_STATE && nlv->uNewState == (LVIS_SELECTED | LVIS_FOCUSED))
				{
					LVITEM LI;
					LI.iItem = nlv->iItem;
					LI.iSubItem = 0;
					ListView_GetItem(g_hListView, &LI);

					//UI�۾�------------------------------------------------------
					TCHAR temp[20];
					ListView_GetItemText(g_hListView, nlv->iItem, 0, temp, _countof(temp));
					SetDlgItemText(hDlg, IDC_EDIT1, temp);

					ListView_GetItemText(g_hListView, nlv->iItem, 1, temp, _countof(temp));
					SetDlgItemText(hDlg, IDC_EDIT2, temp);

					ListView_GetItemText(g_hListView, nlv->iItem, 2, temp, _countof(temp));
					SetDlgItemText(hDlg, IDC_EDIT3, temp);

					ListView_GetItemText(g_hListView, nlv->iItem, 3, temp, _countof(temp));
					SetDlgItemText(hDlg, IDC_EDIT6, temp);

					ListView_GetItemText(g_hListView, nlv->iItem, 4, temp, _countof(temp));
					SetDlgItemText(hDlg, IDC_EDIT7, temp);
					break;
					//-----------------------------------------------------------
				}
			}
		}
	}
}

void ui_update(HWND hDlg) 
{
	LVITEM LI;
	BOOK bk;
	TCHAR buf[20];
	int idx = ListView_GetNextItem(g_hListView, -1, LVNI_ALL | LVNI_SELECTED);
	if (idx == -1) {
		MessageBox(hDlg, _T("������ �׸��� ���� �����Ͻʽÿ�."), _T("�˸�"), MB_OK);
	}
	else {
		LI.iItem = idx;
		LI.iSubItem = 0;
		ListView_SetItem(g_hListView, &LI);

		GetDlgItemText(hDlg, IDC_EDIT1, buf, 255);
		ListView_SetItemText(g_hListView, idx, 0, buf);
		bk.id = _tstoi(buf);

		GetDlgItemText(hDlg, IDC_EDIT2, bk.bk_title, 255);
		ListView_SetItemText(g_hListView, idx, 1, bk.bk_title);
	
		GetDlgItemText(hDlg, IDC_EDIT3, bk.Publisher, 255);
		ListView_SetItemText(g_hListView, idx, 2, bk.Publisher);

		GetDlgItemText(hDlg, IDC_EDIT6, buf, 255);
		ListView_SetItemText(g_hListView, idx, 3, buf);
		bk.price = _tstoi(buf);

		GetDlgItemText(hDlg, IDC_EDIT7, buf, 255);
		ListView_SetItemText(g_hListView, idx, 4, buf);
		bk.bk_num = _tstoi(buf);
	}
	g_books[idx] = bk;
}

void ui_delete(HWND hDlg, vector<BOOK>* books)
{
	int idx = ListView_GetNextItem(g_hListView, -1, LVNI_ALL | LVNI_SELECTED);
	if (idx == -1) {
		MessageBox(hDlg, _T("������ �׸��� ���� �����Ͻʽÿ�."), _T("�˸�"), MB_OK);
	}
	else{
		ListView_DeleteItem(g_hListView, idx);
		books->erase(books->begin()+idx);
	}
}