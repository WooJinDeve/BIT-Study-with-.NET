//17_생산자소비자문제(패턴코드)
// 생산자 : 정보를 생성 --> 큐에 저장
// 소비자 : 큐에 저장된 정보를 얻어와 --> 작업

#include <windows.h>
#include <iostream>
#include <queue> // STL의 Q
#include <time.h>
using namespace std;

// 2개의 스레드가 동시에 사용하는 공유 자원
queue<int> Q;

// Q에 접근을 동기화 하기 위해 Mutex사용(CRITICAL_SECTION 이 더 좋긴 하지만.mutex예제를 위해)
HANDLE hMutex;

// Q의 갯수를 Count 하기 위해.(큐에 저장된 데이터 갯수)
HANDLE hSemaphore;

// 생산자(Q에 접근)
DWORD WINAPI Produce(void*)
{
	static int value = 0;
	while (1)
	{
		// Q에 생산을 한다.
		++value;

		// Q의 접근에 대한 독점권을 얻는다.
		WaitForSingleObject(hMutex, INFINITE);
		//---------------------------------------------
		Q.push(value);
		printf("Produce : %d\n", value);
		LONG old;
		ReleaseSemaphore(hSemaphore, 1, &old); // 세마포어의 값이 1증가
											   // Q에 데이터가 있으면 S, 없으면 NS
		//------------------------------------------------
		ReleaseMutex(hMutex);

		Sleep((rand() % 20) * 100); // 0.1s ~ 2s간 대기.
	}
	return 0;
}

// 소비자(Q에 접근)
DWORD WINAPI Consume(void* p)
{
	while (1)
	{
		WaitForSingleObject(hSemaphore, INFINITE); // Q가 비어 있다면 대기.
		WaitForSingleObject(hMutex, INFINITE);
		//----------------------------------------------
		int n = Q.front(); // Q의 제일 앞요소 얻기(제거하지 않는다.)
		Q.pop(); // 제거.
		printf(" Consume : %d\n", n);
		//----------------------------------------------
		ReleaseMutex(hMutex);
		Sleep((rand() % 20) * 100); // 0.1s ~ 2s간 대기
	}
	return 0;
}

int main()
{
	hMutex = CreateMutex(0, FALSE, TEXT("Q_ACCESS_GUARD"));
	hSemaphore = CreateSemaphore(0, 0, 1000, TEXT("Q_RESOURCE_COUNT")); //최대
	// 1000개의 , 초기 0

	srand(time(0));

	HANDLE h[2];
	h[0] = CreateThread(0, 0, Produce, 0, 0, 0);		//생산자
	h[1] = CreateThread(0, 0, Consume, 0, 0, 0);		//소비자

	WaitForMultipleObjects(2, h, TRUE, INFINITE);
	CloseHandle(h[0]);
	CloseHandle(h[1]);

	CloseHandle(hMutex);
	CloseHandle(hSemaphore);

	return 0;
}
