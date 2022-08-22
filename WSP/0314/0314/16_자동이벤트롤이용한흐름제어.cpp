//16_자동이벤트를 이용한 흐름제어
#include <iostream>
#include <windows.h>
using namespace std;

HANDLE hEvent1, hEvent2;

BOOL bContinue = TRUE;
int g_x, sum;

DWORD WINAPI ServerThread(LPVOID p)
{
	while (bContinue)
	{
		WaitForSingleObject(hEvent1, INFINITE);
		sum = 0;
		for (int i = 0; i < g_x; i++)
			sum += i;
		SetEvent(hEvent2);
	}
	cout << "Server종료" << endl;
	return 0;
}

int main()
{
	//자동이벤트객체(초기화 : NON_SIGNAL) 2개 생성
	hEvent1 = CreateEvent(0, 0, 0, TEXT("e1"));
	hEvent2 = CreateEvent(0, 0, 0, TEXT("e2"));

	HANDLE hThread = CreateThread(NULL, NULL, ServerThread, 0, 0, 0);

	while (1)
	{
		cin >> g_x;
		if (g_x == -1) break;
		SetEvent(hEvent1); // Signal 발생...
		// ... 다른 일 수행
		WaitForSingleObject(hEvent2, INFINITE);
		cout << "결과 : " << sum << endl;
	}
	// 먼저 ServerThread 종료
	bContinue = FALSE;
	SetEvent(hEvent1);


	WaitForSingleObject(hThread, INFINITE);
	CloseHandle(hThread);

	return 0;
}