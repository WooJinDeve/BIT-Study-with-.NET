//skeleton �ڵ�
/*
* [������]
* 1. ���ҽ� ����
*   1) resource.h ���� ���� [�ڵ����� ID�� ���ǵ�]
*   2) ������Ʈ�� .rv ���� ���� [�ڵ����� ��ũ��Ʈ �ڵ� ������]
*   3) �������� �����߱� ������ ICON ���� ���� (icon1.ico)
*/

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

HICON g_hicon[4] = {
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON3)),
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON4)),
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON5)),
	LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON6))
};

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_TIMER:
	{
		// ������ ����
		if (wParam == 1) // timer ID
		{
			static int num = 0;

			SetClassLong(hwnd, GCL_HICON, (LONG)g_hicon[num]);
			num = (num + 1) % 4;

			// ���� �ð� 
			SYSTEMTIME st;
			GetLocalTime(&st);
			TCHAR buf[20];
			wsprintf(buf, TEXT("%d:%d:%d"), st.wHour, st.wMinute, st.wSecond);
			SetWindowText(hwnd, buf);
		}
	}
	case WM_CREATE:
	{
		SetTimer(hwnd, 1, 1000, NULL);
		SendMessage(hwnd, WM_TIMER, 1, 0);
		return 0;
	}
	case WM_DESTROY:
	{
		KillTimer(hwnd, 1);
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	//wc.hIcon = LoadIcon(hInst, IDI_APPLICATION);
	wc.hIcon = LoadIcon(hInst, MAKEINTRESOURCE(IDI_ICON2));
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}