#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//자식의 ID로 사용할 정보를 정의
#define IDC_BUTTON1		1
#define IDC_BUTTON2		2

HWND g_button1, g_button2;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static BOOL isbrush = FALSE;

	switch (msg)
	{

	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HBRUSH br;

		if (isbrush == TRUE)
		{
			br = CreateSolidBrush(RGB(255, 0, 0));
		}
		else
		{
			br = CreateSolidBrush(RGB(255, 255, 255));
		}

		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);

		if (SendMessage(g_button1, BM_GETCHECK, 0, 0) == BST_CHECKED)
		{
			Rectangle(hdc, 200, 10, 310, 110);
		}
		else
		{
			Ellipse(hdc, 200, 10, 310, 110);
		}

		DeleteObject(SelectObject(hdc, oldbr));
		EndPaint(hwnd, &ps);
		return 0;
	}
	case WM_CREATE:
	{
		g_button1 =CreateWindow(_T("button"), _T("사각형 출력"), 
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTOCHECKBOX
			, 20, 20, 120, 25, hwnd, (HMENU)IDC_BUTTON1, GetModuleHandle(0), 0);

		g_button2 = CreateWindow(_T("button"), _T("브러쉬 사용"),
			WS_CHILD | WS_BORDER | WS_VISIBLE | BS_AUTOCHECKBOX
			, 20, 50, 120, 25, hwnd, (HMENU)IDC_BUTTON2, GetModuleHandle(0), 0);

	
		return 0;
	}
	// 통지 메시지 수신(자식 메시지)
	case WM_COMMAND:
	{
		if (LOWORD(wParam) == IDC_BUTTON1) //누가 통지했는가?
		{
			if (HIWORD(wParam) == BN_CLICKED) // 굳이 조건문을 넣어야겠다면 사용하는 코드
			{
				InvalidateRect(hwnd, 0, TRUE);
			}
		}
		else if (LOWORD(wParam) == IDC_BUTTON2) // 클릭으로 가정하고 구현함
		{
			isbrush = !isbrush;

			InvalidateRect(hwnd, 0, FALSE);
		}
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