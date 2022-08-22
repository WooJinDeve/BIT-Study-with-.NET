#include"std.h"

HWND g_hListView, g_hComboBox;

void ui_GetControlHandle(HWND hDlg)
{
	g_hListView = GetDlgItem(hDlg,IDC_LIST1);
	g_hComboBox = GetDlgItem(hDlg, IDC_COMBO1);
}

void ui_ComboboxInitData(HWND hDlg)
{
	SendMessage(g_hComboBox, CB_ADDSTRING, 0, (LPARAM)_T("����"));
	SendMessage(g_hComboBox, CB_ADDSTRING, 0, (LPARAM)_T("����"));
}

void ui_ListViewCreateHeader(HWND hDlg)
{
	LVCOLUMN COL;
	// ����� �߰��Ѵ�.
	COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	COL.fmt = LVCFMT_LEFT;
	//-------------------------------------------
	COL.cx = 125;
	COL.pszText = (LPTSTR)_T("ȸ����ȣ");				// ù ��° ���
	COL.iSubItem = 0;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)_T("�̸�");			// �� ��° ���
	COL.iSubItem = 1;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

	COL.cx = 130;
	COL.pszText = (LPTSTR)_T("��ȭ��ȣ");				// �� ��° ���
	COL.iSubItem = 2;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)_T("����");				// �� ��° ���
	COL.iSubItem = 3;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 3, (LPARAM)&COL);

	ListView_SetExtendedListViewStyle(g_hListView, LVS_EX_FULLROWSELECT |
	LVS_EX_GRIDLINES  | LVS_EX_HEADERDRAGDROP);

}

void ui_ListViewPrintAll(HWND hDlg, vector<MEMBER> members)
{
	//����Ʈ�� ��µ� ������ clear
	ListView_DeleteAllItems(g_hListView);

	for (int i = 0; i < (int)members.size(); i++)
	{
		MEMBER mem = members[i];

		//-----------------------------------------------------
		// �ؽ�Ʈ�� �̹����� ���� �����۵��� ����Ѵ�.
		LVITEM LI;
		LI.mask = LVIF_TEXT;

		LI.iItem = i;    //����Ʈ�信 ��µǴ� ���� ��ġ
		LI.iSubItem = 0;

		TCHAR temp[20];
		wsprintf(temp, TEXT("%d"), mem.id);
		LI.pszText = temp;			// ù ��° ������
		SendMessage(g_hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 1;
		LI.pszText = mem.name;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText = mem.phone;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 3;
		wsprintf(temp, TEXT("%s"),
			(mem.gender == TRUE ? TEXT("����") : TEXT("����")));
		LI.pszText = temp;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);


	}
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
				SetDlgItemText(hDlg, IDC_EDIT5, temp);

				ListView_GetItemText(g_hListView, nlv->iItem, 2, temp, _countof(temp));
				SetDlgItemText(hDlg, IDC_EDIT4, temp);

				ListView_GetItemText(g_hListView, nlv->iItem, 3, temp, _countof(temp));

				if (_tcscmp(temp, TEXT("����")) == 0)
					SendMessage(g_hComboBox, CB_SETCURSEL, 0, 0);
				else
					SendMessage(g_hComboBox, CB_SETCURSEL, 1, 0);
				//-----------------------------------------------------------
			}
		}
	}
}

void ui_DummyDataInput(HWND hDlg, vector<MEMBER>* pmembers)
{
	MEMBER mem1 = { 1,_T("ȫ�浿"), _T("010-1111-1111"),TRUE };
	pmembers->push_back(mem1);

	MEMBER mem2 = { 2,_T("�̱��"), _T("010-2222-333"),FALSE };
	pmembers->push_back(mem2);

	MEMBER mem3 = { 3,_T("�ֱ浿"), _T("010-3333-3333"),TRUE };
	pmembers->push_back(mem3);

	ui_ListViewPrintAll(hDlg, *pmembers);;
}

//---------------------------------------------------------------
void ui_SelectPrint(HWND hDlg, MEMBER mem)
{
	SetDlgItemInt(hDlg, IDC_EDIT1, mem.id, 0);
	SetDlgItemText(hDlg,IDC_EDIT5,mem.name);
	SetDlgItemText(hDlg, IDC_EDIT4, mem.phone);
	if (mem.gender == TRUE)
		SendMessage(g_hComboBox, CB_SETCURSEL, 0, 0);
	else
		SendMessage(g_hComboBox, CB_SETCURSEL, 1, 0);

}