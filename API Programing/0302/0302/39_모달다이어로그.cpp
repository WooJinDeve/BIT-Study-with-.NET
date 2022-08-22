#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

struct DATA
{
	TCHAR name[20];
	int age;
};

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static DATA* pdata = NULL;
	switch (msg)
	{
		//최초 호출 시점.
		case WM_INITDIALOG:
		{
			pdata = (DATA*)lParam;

			SetDlgItemText(hDlg, IDC_EDIT1, pdata->name);
			SetDlgItemInt(hDlg, IDC_EDIT2, pdata->age,0);
			return TRUE;
		}
		case WM_COMMAND:
		{
			switch (LOWORD(wParam))
			{
				//종료시점.
				//EndDialog : 다이얼로그를 종료하는 함수
				//hDlg : 종료대상 , IDCANCEL : 종료시 반환값
				case IDOK:
				{
					GetDlgItemText(hDlg, IDC_EDIT1, pdata->name, _countof(pdata->name));
					pdata->age = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
					EndDialog(hDlg, IDOK);
					return TRUE;
				}
				case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
			}
		}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}
DATA g_data = { _T("홍길동"), 10 };

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		case WM_PAINT:
		{
			PAINTSTRUCT ps;
			HDC hdc = BeginPaint(hwnd, &ps);

			TextOut(hdc, 10, 10, g_data.name, _tcslen(g_data.name));
			TCHAR temp[20];
			wsprintf(temp, TEXT("%d"), g_data.age);
			TextOut(hdc, 10, 30, temp, _tcslen(temp));

			EndPaint(hwnd, &ps);
			return 0;
		}
		case WM_LBUTTONDOWN:
		{
			DATA temp;
			_tcscpy_s(temp.name, _countof(temp.name), g_data.name);
			temp.age = g_data.age;

			UINT ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG1), hwnd,DlgProc, (LPARAM)&temp);
			if (ret == IDOK)
			{
				_tcscpy_s(g_data.name, _countof(g_data.name), temp.name);
				g_data.age = temp.age;
				InvalidateRect(hwnd, 0, TRUE);
			}
			return 0;
		}
		case WM_CREATE:
		{
			return 0;
		}
		case WM_DESTROY:
		{
			PostQuitMessage(0);
			return 0;
		}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//윈도우 클래스 정의
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}