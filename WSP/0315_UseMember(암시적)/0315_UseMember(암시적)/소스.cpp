
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#pragma comment (lib, "0315_member.lib")	
#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include "member.h"

Member mem;

void GetMember(HWND hDlg)
{
	TCHAR name[20];
	int age;
	GetDlgItemText(hDlg, IDC_EDIT1, name, _countof(name));
	age = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);

	mem = CreateMember(name, age);

	SetDlgItemText(hDlg, IDC_EDIT3, mem.name);
	SetDlgItemInt(hDlg, IDC_EDIT4, mem.age, 0);
}
void AddAge(HWND hDlg)
{
	AddAge(&mem);
	SetDlgItemInt(hDlg, IDC_EDIT4, mem.age, 0);
}

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//최초 호출 시점.
		case WM_INITDIALOG:
		{
			return TRUE;
		}
		case WM_COMMAND:
		{
			switch (LOWORD(wParam))
			{
				case IDC_BUTTON1: GetMember(hDlg); break;
				case IDC_BUTTON2:  AddAge(hDlg); break;
				case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
			}
		}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	UINT ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_DIALOG1), // 다이얼로그 선택
		0, // 부모 윈도우
		DlgProc); // Proc..


	return 0;
}