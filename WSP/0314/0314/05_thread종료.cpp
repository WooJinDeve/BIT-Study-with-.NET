//05_스레드종료

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <CommCtrl.h>	//표준컨트롤
#include <tchar.h>

DWORD WINAPI foo(LPVOID temp)
{
	//프로그래스바 핸들
	HWND hPrg = (HWND)temp;
	for (int i = 0; i < 1000; ++i)
	{
		SendMessage(hPrg, PBM_SETPOS, i, 0); // 프로그래스 전진

		for (int k = 0; k < 5000000; ++k); // 0 6개 - some work.!!
	}
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
		hPrg = CreateWindow(PROGRESS_CLASS, TEXT(""),
			WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
			10, 10, 500, 30, hwnd, (HMENU)1, 0, 0);

		//범위:0 ~1000 초기위치:0으로 초기화.
		SendMessage(hPrg, PBM_SETRANGE32, 0, 1000);
		SendMessage(hPrg, PBM_SETPOS, 0, 0);

		break;
	}

	case WM_LBUTTONDOWN:
	{
		DWORD tid;
		hThread = CreateThread(0, 0, foo, (void*)hPrg, 0, &tid);
	}
	return 0;
	case WM_RBUTTONDOWN:
	{
		DWORD code;
		GetExitCodeThread(hThread, &code);  //종료코드값 획득
		if (code == STILL_ACTIVE)   //살아있다...
		{
			TerminateThread(hThread, 100);	//죽인다.
			//CloseHandle(hThread);			//핸들 감소
		}
		else
		{
			//CloseHandle(hThread);			//죽었는데 핸들을 갖고 있을 필요가 없다.
		}
		CloseHandle(hThread);
		return 0;
	}

	case WM_DESTROY:
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