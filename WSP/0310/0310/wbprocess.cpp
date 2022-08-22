#include "wbprocess.h"
#include "resource.h"

//프로세스 생성(메모장)
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
		//MessageBox(NULL, TEXT("프로세스가 동작"), TEXT("알림"), MB_OK);

		SetDlgItemInt(hDlg, IDC_EDIT2, pi.dwProcessId,0);
	}

}

//프로세스종료
/*
* [윈도우] SendMessage(윈도우 핸들, WM_CLOSE, 0, 0);
* [대화상자기반] EndDialog(윈도우핸들,IDCANCLE);
* --------------------------------------------------
* [프로세스 핸들 활용]
* ExitProcess(0); //자신의 프로세스를 종료하는 함수
* TerminateProcess(프로세스핸들, 종료코드);
*/
void wb_ExitProcess(HWND hDlg)
{
	int pid = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid); // 자신의 핸들테이블에 하나 추가

	//CloseHandle(h); //핸들테이블에서 해당 핸들이 제거된다 (KO COUNT 1감소)

	TerminateProcess(h, 0);
	WaitForSingleObject(h, INFINITE); // SIGNAL 아니면 무한대기

	MessageBox(NULL, TEXT("죽었다"), TEXT("알림"), MB_OK);
}

//종료 코드 값 얻기
void wb_GetExitCodeProcess(HWND hDlg)
{
	int pid = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
	HANDLE h = OpenProcess(PROCESS_ALL_ACCESS, 0, pid); // 자신의 핸들테이블에 하나 추가

	DWORD code;
	GetExitCodeProcess(h, &code);

	if (STILL_ACTIVE == code)
		SetDlgItemText(hDlg, IDC_EDIT3, TEXT("살아있다."));
	else

		SetDlgItemInt(hDlg, IDC_EDIT3, code, 0);
}

//윈도우핸들 -> 프로세스핸들
void wb_HwndToProcessHandle(HWND hDlg)
{
	HWND hwnd = FindWindow(0, TEXT("제목 없음 - Windows 메모장"));
	if (hwnd == NULL)
	{
		MessageBox(NULL, TEXT("메모장 먼저 실행"), TEXT("알림"), MB_OK);
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