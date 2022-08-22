//11_크리티컬섹션(콘솔)

#include <windows.h>
#include <iostream>
using namespace std;

void WorkFunc() { for (int i = 0; i < 10000000; i++); }// 시간 지연 함수

// 공유 자원(전역변수, 콘솔창)
int g_x = 0;

CRITICAL_SECTION g_cs; // 전역


DWORD WINAPI Func(PVOID p)
{

	for (int i = 0; i < 20; i++)
	{
		//EnterCriticalSection(&g_cs);
		g_x = 200;
		WorkFunc();
		g_x++;
		cout << " Func : " << g_x << endl;
		//LeaveCriticalSection(&g_cs);
		Sleep(1);
	}


	return 0;
}

int main()
{
	// 임계영역 변수 초기화.
	InitializeCriticalSection(&g_cs);

	DWORD tid;
	HANDLE hThread = CreateThread(NULL, 0, Func, 0, 0, &tid);

	// Sleep(1);

	for (int i = 0; i < 20; i++)
	{
		//EnterCriticalSection(&g_cs);
		g_x = 200;
		WorkFunc();
		g_x--;
		cout << " ...................Main : " << g_x << endl;
		//LeaveCriticalSection(&g_cs);
		Sleep(1);
	}


	WaitForSingleObject(hThread, INFINITE);
	CloseHandle(hThread);

	DeleteCriticalSection(&g_cs); // 파괴

	return 0;
}
