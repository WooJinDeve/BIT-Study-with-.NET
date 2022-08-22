//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static COLORREF color = RGB(0, 0, 0);
	switch (msg)
	{
	// 마우스 오른쪽 버튼 클릭시 contextmenu를 실행
	case WM_CONTEXTMENU:
	{
		// 기존 메뉴를 활용해서 처리 
		//HMENU hmenu = GetMenu(hwnd);
		//HMENU hsubmenu = GetSubMenu(hmenu, 1);

		//새로만든 메뉴를 활용해서 처리
		HMENU hmenu = LoadMenu(GetModuleHandle(0), MAKEINTRESOURCE(IDR_MENU2));
		HMENU hsubmenu = GetSubMenu(hmenu, 0);
		POINT pt = { LOWORD(lParam), HIWORD(lParam) }; // 스크린좌표

		TrackPopupMenu(hsubmenu, TPM_LEFTBUTTON, pt.x, pt.y, 0, hwnd, 0);
		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH hbr = CreateSolidBrush(color);
		HBRUSH oldb = (HBRUSH)SelectObject(hdc, hbr);

		Rectangle(hdc, 10, 10, 110, 110);

		DeleteObject(SelectObject(hdc, oldb));

		EndPaint(hwnd, &ps);
	}

	// 메뉴 UI처리 ( 메뉴바의 메뉴를 클릭했을 때 popup을 하기 전 발생
	case WM_INITMENUPOPUP:
	{
		HMENU hmenu = GetMenu(hwnd);
		EnableMenuItem(hmenu, ID_40002, color == RGB(255,0,0)? TRUE:FALSE);
		EnableMenuItem(hmenu, ID_40003, color == RGB(0, 255, 0) ? TRUE : FALSE);
		EnableMenuItem(hmenu, ID_40004, color == RGB(0, 0, 255) ? TRUE : FALSE);
		EnableMenuItem(hmenu, ID_40005, color == RGB(0, 0, 0) ? TRUE : FALSE);
		return 0;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam)) // 메뉴 ID
		{
		case ID_40001: SendMessage(hwnd, WM_CLOSE, 0, 0); break;
		case ID_40002: color = RGB(255, 0, 0);  break;
		case ID_40003: color = RGB(0, 255, 0);  break;
		case ID_40004: color = RGB(0, 0, 255);  break;
		case ID_40005:  color = RGB(0, 0, 0);  break;
		}

		RECT rc = { 0,0,110,110 };
		InvalidateRect(hwnd, &rc, FALSE);

		return 0;
	}
	//case WM_LBUTTONDOWN:
	//{
	//	static HMENU hmenu = 0;
	//	if (hmenu == 0)
	//	{
	//		hmenu = GetMenu(hwnd); // 윈도우에 부착된 메뉴의 핸들 획득
	//		SetMenu(hwnd, 0);      // 현재 윈도우에 메뉴 부착(메뉴가 제거됨)
	//	}
	//	else
	//	{
	//		SetMenu(hwnd, hmenu);
	//		hmenu = 0;
	//	}
	//}
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
	wc.lpszMenuName = 0; //MAKEINTRESOURCE(IDR_MENU1);		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	//자신의 리소스에 있는 리소스 핸들 얻기(LoadIcon, LoadCursor, LoadMenu)
	HMENU hmenu = LoadMenu(hInst, MAKEINTRESOURCE(IDR_MENU1));

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, hmenu, hInst, 0);

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