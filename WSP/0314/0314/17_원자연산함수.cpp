//원자 연산함수 
//동기화된 상태에서 값을 증가나 감소시킬 수 있는 함수가 제공
#include <stdio.h>
#include <windows.h>

// 공유자원
long value = 0;

DWORD WINAPI ThreadFunc(void* p)
{
	int i = 0;
	for (i = 0; i < 10000000; ++i)
		//++value;
		//InterlockedIncrement(&value);  //1증가
		InterlockedDecrement(&value);	 //1감소
	return 0;
}

int main()
{
	int i = 0;

	HANDLE hThread[5];
	for (i = 0; i < 5; ++i)
		hThread[i] = CreateThread(0, 0, ThreadFunc, 0, 0, 0);

	//TRUE : 5개의 thread가 다 signal(종료)될때까지 대기 
	WaitForMultipleObjects(5, hThread, TRUE, INFINITE);
	for (i = 0; i < 5; ++i)
		CloseHandle(hThread[i]);

	//결과 출력
	printf("value = %d\n", value);

	return 0;
}
