#include "wbprocess.h"
#include "resource.h"

//���μ��� ����(�޸���)
void wb_CreateProcess(HWND hDlg)
{
	TCHAR name[20];

	GetDlgItemText(hDlg, IDC_EDIT1, name, _countof(name));

	STARTUPINFO si = { sizeof(si) };
	PROCESS_INFORMATION pi;

	BOOL b = CreateProcess(0, name, 0, 0, 0, 0, 0, 0, &si, &pi);
	if (b)
	{
		WaitForInputIdle(pi.hProcess, INFINITE);
		//MessageBox(NULL, TEXT("���μ����� ����"), TEXT("�˸�"), MB_OK);

		SetDlgItemInt(hDlg, IDC_EDIT2, pi.dwProcessId,0);
	}

}

//���μ�������
/*
* [������] SendMessage(������ �ڵ�, WM_CLOSE, 0, 0);
* [��ȭ���ڱ��] EndDialog(�������ڵ�,IDCANCLE);
* --------------------------------------------------
* [���μ��� �ڵ� Ȱ��]
* ExitProcess(0); //�ڽ��� ���μ����� �����ϴ� �Լ�
* TerminateProcess(���μ����ڵ�, �����ڵ�);
*/
void wb_ExitProcess(HWND hDlg)
{
	int pid = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid); // �ڽ��� �ڵ����̺� �ϳ� �߰�

	//CloseHandle(h); //�ڵ����̺��� �ش� �ڵ��� ���ŵȴ� (KO COUNT 1����)

	TerminateProcess(h, 0);
	WaitForSingleObject(h, INFINITE); // SIGNAL �ƴϸ� ���Ѵ��

	MessageBox(NULL, TEXT("�׾���"), TEXT("�˸�"), MB_OK);
}

//���� �ڵ� �� ���
void wb_GetExitCodeProcess(HWND hDlg)
{
	int pid = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid); // �ڽ��� �ڵ����̺� �ϳ� �߰�

	DWORD code;
	GetExitCodeProcess(h, &code);

	if (STILL_ACTIVE == code)
		SetDlgItemText(hDlg, IDC_EDIT3, TEXT("����ִ�."));
	else

		SetDlgItemInt(hDlg, IDC_EDIT3, code, 0);
}

//�������ڵ� -> ���μ����ڵ�
void wb_HwndToProcessHandle(HWND hDlg)
{
	HWND hwnd = FindWindow(0, TEXT("���� ���� - Windows �޸���"));
	if (hwnd == NULL)
	{
		MessageBox(NULL, TEXT("�޸��� ���� ����"), TEXT("�˸�"), MB_OK);
		return;
	}

	//Hwnd -> PID
	DWORD pid;
	DWORD tid = GetWindowThreadProcessId(hwnd, &pid);

	//pid -> Hwnd
	HANDLE hprocess = OpenProcess(PROCESS_ALL_ACCESS, 0, pid);

	TerminateProcess(hprocess, 0);
	CloseHandle(hprocess);
}