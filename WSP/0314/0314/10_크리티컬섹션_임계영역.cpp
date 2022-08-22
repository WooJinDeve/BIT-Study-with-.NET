//10_크리티컬섹션 - 임계영역

//09_강제적코드작성으로해결(문제가 있다)
// 스케줄링이 필요없는 경우에도 스케줄링 상태에 있다.

//가능한 임계영역을 최소화 하는 것이 바람직하다.

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

int x;
CRITICAL_SECTION cs;   //<=================================

DWORD WINAPI ThreadFun1(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);

	//EnterCriticalSection(&cs);
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&cs);
		x = 100;
		TextOut(hdc, x, 100, TEXT("강아지"), 3);
		LeaveCriticalSection(&cs);
	}
	//LeaveCriticalSection(&cs);

	ReleaseDC((HWND)param, hdc);
	return 0;
}

DWORD WINAPI ThreadFun2(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);

	//EnterCriticalSection(&cs);
	for (int i = 0; i < 100; i++)
	{
		EnterCriticalSection(&cs);
		x = 200;
		//Sleep(1);
		TextOut(hdc, x, 200, TEXT("고양이"), 3);
		LeaveCriticalSection(&cs);
	}
	//LeaveCriticalSection(&cs);

	ReleaseDC((HWND)param, hdc);
	return 0;
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HWND hPrg;
	static HANDLE hThread;

	switch (msg)
	{
	case WM_CREATE:
	{
		InitializeCriticalSection(&cs);

		return 0;
	}

	case WM_LBUTTONDOWN:
	{
		DWORD ThreadID;
		HANDLE hThread = CreateThread(NULL, 0, ThreadFun1, hwnd, 0, &ThreadID);
		CloseHandle(hThread);
		hThread = CreateThread(NULL, 0, ThreadFun2, hwnd, 0, &ThreadID);
		CloseHandle(hThread);
	}
	return 0;


	case WM_DESTROY:
		DeleteCriticalSection(&cs);

		PostQuitMessage(0);
		return 0;
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
