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

		LI.iItem = i;    //����Ʈ�信 ��µǴ� ���� ��ġ

		LI.iSubItem = 0;
		LI.pszText = p.name;
		SendMessage(g_hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 1;
		TCHAR temp[20];
		wsprintf(temp, TEXT("%d"), p.pid);
		LI.pszText = temp;			// ù ��° ������
		SendMessage(g_hListView, LVM_SETITEM, 0, (LPARAM)&LI);
	}
}

void ui_EnumProcess(HWND hDlg)
{
	// process list ��������(id��)
	DWORD aProcess[1024], cbNeeded, cProcesses;
	unsigned int i;

	// �迭    ��, ���ϵǴ�   ����Ʈ   �� // �迭��   id������   ����. 
	if (!EnumProcesses(aProcess, sizeof(aProcess), &cbNeeded))
		return;

	// �󸶳�   ����   ���μ�������   ���ϵǾ���   ��� 
	cProcesses = cbNeeded / sizeof(DWORD); // process �̸�   ���

	TCHAR temp[30];
	wsprintf(temp, TEXT("���μ��� ���� : %d ��"), cProcesses);
	SetDlgItemText(hDlg, IDC_STATIC2, temp);
	for (i = 0; i < cProcesses; i++)
		ui_PrintProcessNameAndID(aProcess[i]);
}

void ui_PrintProcessNameAndID(DWORD processID)
{
	TCHAR szProcessName[MAX_PATH] = TEXT("unknown");

	// ���μ�����   �ڵ�   ���	
	HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION |
		PROCESS_VM_READ, FALSE, processID); // process �̸�   ��������
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
	COL.pszText = (LPTSTR)TEXT("���ϸ�");				// ù ��° ���
	COL.iSubItem = 0;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.cx = 150;
	COL.pszText = (LPTSTR)TEXT("���μ��� ID");			// �� ��° ���
	COL.iSubItem = 1;
	SendMessage(g_hListView, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);


	ListView_SetExtendedListViewStyle(g_hListView, LVS_EX_FULLROWSELECT |
		LVS_EX_GRIDLINES | LVS_EX_HEADERDRAGDROP);
}