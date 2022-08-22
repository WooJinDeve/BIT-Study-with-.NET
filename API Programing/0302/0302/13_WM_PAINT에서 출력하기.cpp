//12_WM_PAINT
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	static POINTS g_pt; // 정적지역변수(전역정적공간)
	//case WM_LBUTTONUP:
	//{
	//	POINTS pt = MAKEPOINTS(lParam);

	//	HDC hdc = GetDC(hwnd);
	//	Rectangle(hdc, pt.x, pt.y, pt.x+50, pt.y+50);
	//	ReleaseDC(hwnd, hdc);
	//	return 0;
	//}

	case WM_LBUTTONUP:
	{
		g_pt = MAKEPOINTS(lParam);
		InvalidateRect(hwnd, 0, false); // 무효화 발생코드
		//0 or NULL : hwnd의 클라이언트 전체 영역을 무효화 
		//false : 무효화된 영역을 지우지 않겠다.
		//true : 무효화된 영역을 원래의 배경색으로 칠하겠다.
		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);	//무효화처리 더 들어감

		Rectangle(hdc, g_pt.x - 25, g_pt.y - 25, g_pt.x + 25, g_pt.y + 25);

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