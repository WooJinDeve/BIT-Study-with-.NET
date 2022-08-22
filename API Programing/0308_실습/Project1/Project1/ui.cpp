#include"std.h"

HWND g_hListView, g_hComboBox;

void ui_GetControlHandle(HWND hDlg)
{
	g_hListView = GetDlgItem(hDlg,IDC_LIST1);
	g_hComboBox = GetDlgItem(hDlg, IDC_COMBO1);
}

void ui_ComboboxInitData(HWND hDlg)
{
	SendMessage(g_hComboBox, CB_ADDSTRING, 0, (LPARAM)_T("남성"));
	SendMessage(g_hComboBox, CB_ADDSTRING, 0, (LPARAM)_T("여성"));
}

void ui_ListViewCreateHeader(HWND hDlg)
{
	LVCOLUMN COL;
	// 헤더를 추가한다.
	COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	COL.fmt = LVCFMT_LEFT;
	//-------------------------------------------
	COL.cx = 125;
	COL.pszText = (LPTSTR)_T("회원번호");				// 첫 번째 헤더
	COL.iSubItem = 0;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)_T("이름");			// 두 번째 헤더
	COL.iSubItem = 1;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

	COL.cx = 130;
	COL.pszText = (LPTSTR)_T("전화번호");				// 세 번째 헤더
	COL.iSubItem = 2;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);

	COL.cx = 100;
	COL.pszText = (LPTSTR)_T("성별");				// 세 번째 헤더
	COL.iSubItem = 3;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 3, (LPARAM)&COL);

	ListView_SetExtendedListViewStyle(g_hListView, LVS_EX_FULLROWSELECT |
	LVS_EX_GRIDLINES  | LVS_EX_HEADERDRAGDROP);

}

void ui_ListViewPrintAll(HWND hDlg, vector<MEMBER> members)
{
	//리스트뷰 출력된 정보를 clear
	ListView_DeleteAllItems(g_hListView);

	for (int i = 0; i < (int)members.size(); i++)
	{
		MEMBER mem = members[i];

		//-----------------------------------------------------
		// 텍스트와 이미지를 가진 아이템들을 등록한다.
		LVITEM LI;
		LI.mask = LVIF_TEXT;

		LI.iItem = i;    //리스트뷰에 출력되는 행의 위치
		LI.iSubItem = 0;

		TCHAR temp[20];
		wsprintf(temp, TEXT("%d"), mem.id);
		LI.pszText = temp;			// 첫 번째 아이템
		SendMessage(g_hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 1;
		LI.pszText = mem.name;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText = mem.phone;
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 3;
		wsprintf(temp, TEXT("%s"),
			(mem.gender == TRUE ? TEXT("남성") : TEXT("여성")));
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
			// 선택된 항목을 에디트에 보여준다.
		case LVN_ITEMCHANGED:
			if (nlv->uChanged == LVIF_STATE && nlv->uNewState == (LVIS_SELECTED | LVIS_FOCUSED))
			{
				LVITEM LI;
				LI.iItem = nlv->iItem;
				LI.iSubItem = 0;
				ListView_GetItem(g_hListView, &LI);

				//UI작업------------------------------------------------------
				TCHAR temp[20];
				ListView_GetItemText(g_hListView, nlv->iItem, 0, temp, _countof(temp));
				SetDlgItemText(hDlg, IDC_EDIT1, temp);

				ListView_GetItemText(g_hListView, nlv->iItem, 1, temp, _countof(temp));
				SetDlgItemText(hDlg, IDC_EDIT5, temp);

				ListView_GetItemText(g_hListView, nlv->iItem, 2, temp, _countof(temp));
				SetDlgItemText(hDlg, IDC_EDIT4, temp);

				ListView_GetItemText(g_hListView, nlv->iItem, 3, temp, _countof(temp));

				if (_tcscmp(temp, TEXT("남성")) == 0)
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
	MEMBER mem1 = { 1,_T("홍길동"), _T("010-1111-1111"),TRUE };
	pmembers->push_back(mem1);

	MEMBER mem2 = { 2,_T("이길순"), _T("010-2222-333"),FALSE };
	pmembers->push_back(mem2);

	MEMBER mem3 = { 3,_T("최길동"), _T("010-3333-3333"),TRUE };
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