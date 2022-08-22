#include "std.h"

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_GetControlHandle(hDlg);
	ui_ListViewCreateHeader(hDlg);
	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL:		EndDialog(hDlg, IDCANCEL);	return TRUE;
	case IDC_BUTTON1:	OnExecuteProcess(hDlg);		return TRUE;
	case IDC_BUTTON2:	OnEnumProcess(hDlg);		return TRUE;
	case IDC_BUTTON3:	OnCheckState(hDlg);		return TRUE;
	case IDC_BUTTON4:	OnExitProcess(hDlg);		return TRUE;
	}
	return TRUE;
}

BOOL OnNotify(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ui_SelectPidView(hDlg, wParam, lParam);
	return TRUE;
}

void OnExitProcess(HWND hDlg)
{

	//���μ���ID�� �̿��Ͽ� ���μ����� �ڵ��� ���� �� �ִ�.
	int pid = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid);    //�ڽ��� �ڵ����̺� �ϳ��� �߰� 

	//CloseHandle(h); //�ڵ����̺��� �ش� �ڵ��� ���ŵȴ�.(KO COUNT 1����)

	TerminateProcess(h, 10);

	WaitForSingleObject(h, INFINITE); //SIGNAL �ƴϸ� ���Ѵ��
	OnEnumProcess(hDlg);


}
void OnCheckState(HWND hDlg)
{
	//���μ���ID�� �̿��Ͽ� ���μ����� �ڵ��� ���� �� �ִ�.
	int pid = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid);    //�ڽ��� �ڵ����̺� �ϳ��� �߰� 

	DWORD code;
	GetExitCodeProcess(h, &code);
	if (STILL_ACTIVE == code)
		SetDlgItemText(hDlg, IDC_EDIT3, TEXT("������"));
	else
		SetDlgItemText(hDlg, IDC_EDIT3, TEXT("����"));
}
void OnEnumProcess(HWND hDlg)
{
	ui_EnumProcess(hDlg);
	ui_PrintProcess(hDlg);
}

void OnExecuteProcess(HWND hDlg)
{
	TCHAR name[20];
	GetDlgItemText(hDlg, IDC_EDIT2, name, _countof(name));

	STARTUPINFO si = { sizeof(si) };
	PROCESS_INFORMATION pi;

	BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi);
	if (b)	//���μ��� ������ �����ߴ�..
	{
		WaitForInputIdle(pi.hProcess, INFINITE);
		//MessageBox(NULL, TEXT("���μ����� ����"), TEXT("�˸�"), MB_OK);
		OnEnumProcess(hDlg);
	}

	
}


