#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

POINTS g_pt;

//문자열 출력 (마우스의 위치
void PrintText(HDC hdc)
{
	TCHAR buf[50] = _TEXT(" ");
	wsprintf(buf, TEXT("마우스 좌표 : %04d %04d"), g_pt.x, g_pt.y);
	TextOut(hdc, 10, 10, buf, _tcslen(buf));
}

void PixcelPrint(HDC hdc) 
{
	SetPixel(hdc, g_pt.x, g_pt.y, RGB(0, 0, 255));
}

void LinePrint(HDC hdc)
{
	MoveToEx(hdc, 100, 100, 0);
	LineTo(hdc, 200, 200);
}

void RectAndEllipsePrint(HDC hdc)
{
	Rectangle(hdc, 20, 20, 120, 120);
	Ellipse(hdc, 20, 20, 120, 120);
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_KEYDOWN:
	{
		HDC hdc = GetDC(hwnd);

		if (wParam == 'L')
			LinePrint(hdc);
		else if (wParam == 'R' || wParam == 'r')
			RectAndEllipsePrint(hdc);
		ReleaseDC(hwnd, hdc);
	}
	case WM_MOUSEMOVE:
	{
		g_pt = MAKEPOINTS(lParam);
		InvalidateRect(hwnd, 0, false);
		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);	//무효화처리 더 들어감

		PrintText(hdc);
		PixcelPrint(hdc);

		EndPaint(hwnd, &ps);
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
	UpdateWindow(hwnd);				//<-----------------(0)

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);		//<--------------------(0)
		DispatchMessage(&msg);
	}
	return 0;
}