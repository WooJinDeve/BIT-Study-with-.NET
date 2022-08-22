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

	//프로세스ID를 이용하여 프로세스의 핸들을 얻을 수 있다.
	int pid = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid);    //자신의 핸들테이블에 하나가 추가 

	//CloseHandle(h); //핸들테이블에서 해당 핸들이 제거된다.(KO COUNT 1감소)

	TerminateProcess(h, 10);

	WaitForSingleObject(h, INFINITE); //SIGNAL 아니면 무한대기
	OnEnumProcess(hDlg);


}
void OnCheckState(HWND hDlg)
{
	//프로세스ID를 이용하여 프로세스의 핸들을 얻을 수 있다.
	int pid = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid);    //자신의 핸들테이블에 하나가 추가 

	DWORD code;
	GetExitCodeProcess(h, &code);
	if (STILL_ACTIVE == code)
		SetDlgItemText(hDlg, IDC_EDIT3, TEXT("실행중"));
	else
		SetDlgItemText(hDlg, IDC_EDIT3, TEXT("종료"));
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
	if (b)	//프로세스 실행이 성공했다..
	{
		WaitForInputIdle(pi.hProcess, INFINITE);
		//MessageBox(NULL, TEXT("프로세스가 동작"), TEXT("알림"), MB_OK);
		OnEnumProcess(hDlg);
	}

	
}


