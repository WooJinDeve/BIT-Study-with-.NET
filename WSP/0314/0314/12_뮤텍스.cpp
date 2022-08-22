//12_뮤텍스

#include <windows.h>
#include <iostream>
using namespace std;

//반복실행..(프로세스와 프로세스간 동기화)
//첫번째 실행 : CreatMutex("mutex") : mutex 라는 key의 뮤텍스 생성
//두번째 실행 : CreateMutex("mutex") : 만약, 동일한 key로 mutex를 생성하는 코드가 있다면
//                                    자동으로 OpenMutex로 변경처리된다.

int main()
{
	// 뮤텍스 생성
	HANDLE hMutex = CreateMutex(NULL,	// 보안속성
		FALSE,	// 생성시 뮤텍스 소유 여부 --> FALSE : SIGNAL
		TEXT("mutex")); // 이름(KEY)

	cout << "뮤택스를 기다리고 있다." << endl;

	//Wait함수가 통과되면 : 해당 스레드가 뮤텍스를 소유 --> 자동으로 Signal 을 nonsignal로 바꾼다.
	DWORD d = WaitForSingleObject(hMutex, INFINITE); // 대기---------------------------
	if (d == WAIT_TIMEOUT)
		printf("%s", "WAIT_TIMEOUT --> ");
	else if (d == WAIT_OBJECT_0)
		printf("%s", "WAIT_OBJECT_0 --> ");
	else if (d == WAIT_ABANDONED_0)
		printf("%s", "WAIT_ABANDONED_0 --> ");
	cout << "뮤택스 획득" << endl;

	//작업.....

	MessageBox(NULL, TEXT("뮤택스를 놓는다."), TEXT(""), MB_OK);

	ReleaseMutex(hMutex);							//----------------------------------

	return 0;
}
