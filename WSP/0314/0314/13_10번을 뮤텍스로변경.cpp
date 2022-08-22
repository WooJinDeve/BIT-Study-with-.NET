//13_10���� ���ؽ��κ���
// ���ؽ� : �����ڿ��� �ϳ��� �����常 ���� ���!

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

int x;
HANDLE g_hmutex;

DWORD WINAPI ThreadFun1(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);

	WaitForSingleObject(g_hmutex, INFINITE); //<-------------
	for (int i = 0; i < 100; i++)
	{

		x = 100;
		TextOut(hdc, x, 100, TEXT("������"), 3);

	}
	ReleaseMutex(g_hmutex);			//<------------------------

	ReleaseDC((HWND)param, hdc);
	return 0;
}

DWORD WINAPI ThreadFun2(LPVOID param)
{
	HDC hdc = GetDC((HWND)param);

	WaitForSingleObject(g_hmutex, INFINITE); //<-------------
	for (int i = 0; i < 100; i++)
	{
		x = 200;
		//Sleep(1);
		TextOut(hdc, x, 200, TEXT("�����"), 3);
	}
	ReleaseMutex(g_hmutex);			//<------------------------

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
		g_hmutex = CreateMutex(NULL,	// ���ȼӼ�
			FALSE,	// ������ ���ؽ� ���� ���� --> FALSE : SIGNAL
			NULL); // �ϳ��� ���μ��� �� ������ ����ȭ
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


		PostQuitMessage(0);
		return 0;
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
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
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
