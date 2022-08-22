#include "std.h"

HWND g_hListView;
extern vector<WBPROCESS> g_processes;

void ui_SelectPidView(HWND hDlg, WPARAM wParam, LPARAM lParam)
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
				ListView_GetItemText(g_hListView, nlv->iItem, 1, temp, _countof(temp));
				SetDlgItemText(hDlg, IDC_EDIT4, temp);

				//-----------------------------------------------------------
			}
		}
	}
}

void ui_PrintProcess(HWND hDlg)
{
	ListView_DeleteAllItems(g_hListView);
	for (int i = 0; i < (int)g_processes.size(); i++)
	{
		WBPROCESS p = g_processes[i];

		LVITEM LI;
		LI.mask = LVIF_TEXT;

		LI.iItem = i;    //리스트뷰에 출력되는 행의 위치

		LI.iSubItem = 0;
		LI.pszText = p.name;
		SendMessage(g_hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 1;
		TCHAR temp[20];
		wsprintf(temp, TEXT("%d"), p.pid);
		LI.pszText = temp;			// 첫 번째 아이템
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);
	}
}

void ui_EnumProcess(HWND hDlg)
{
	// process list 가져오기(id값)
	DWORD aProcess[1024], cbNeeded, cProcesses;
	unsigned int i;

	// 배열    수, 리턴되는   바이트   수 // 배열에   id값들이   들어간다. 
	if (!EnumProcesses(aProcess, sizeof(aProcess), &cbNeeded))
		return;

	// 얼마나   많은   프로세스들이   리턴되었나   계산 
	cProcesses = cbNeeded / sizeof(DWORD); // process 이름   출력

	TCHAR temp[30];
	wsprintf(temp, TEXT("프로세스 개수 : %d 개"), cProcesses);
	SetDlgItemText(hDlg, IDC_STATIC2, temp);
	for (i = 0; i < cProcesses; i++)
		ui_PrintProcessNameAndID(aProcess[i]);
}

void ui_PrintProcessNameAndID(DWORD processID)
{
	TCHAR szProcessName[MAX_PATH] = TEXT("unknown");

	// 프로세스의   핸들   얻기	
	HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION |
		PROCESS_VM_READ, FALSE, processID); // process 이름   가져오기
	if (NULL != hProcess)
	{
		HMODULE hMod;
		DWORD    cbNeeded;
		if (EnumProcessModules(hProcess, &hMod, sizeof(hMod), &cbNeeded))
		{
			GetModuleBaseName(hProcess, hMod, szProcessName, sizeof(szProcessName));
		}
		else
			return;
	}
	else
		return;

	//print
	WBPROCESS pinfo;
	_tcscpy_s(pinfo.name, _countof(pinfo.name), szProcessName);
	pinfo.pid = processID;

	g_processes.push_back(pinfo);

	//_tprintf(TEXT("%s ( PROCESS ID : %u )\n"), szProcessName, processID); 
	CloseHandle(hProcess);
}

void ui_GetControlHandle(HWND hDlg)
{
	g_hListView = GetDlgItem(hDlg, IDC_LIST2);
}

void ui_ListViewCreateHeader(HWND hDlg)
{
	LVCOLUMN COL;

	COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	COL.fmt = LVCFMT_LEFT;
	COL.cx = 200;
	COL.pszText = (LPTSTR)TEXT("파일명");				// 첫 번째 헤더
	COL.iSubItem = 0;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.cx = 150;
	COL.pszText = (LPTSTR)TEXT("프로세스 ID");			// 두 번째 헤더
	COL.iSubItem = 1;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);


	ListView_SetExtendedListViewStyle(g_hListView, LVS_EX_FULLROWSELECT |
		LVS_EX_GRIDLINES | LVS_EX_HEADERDRAGDROP);
}