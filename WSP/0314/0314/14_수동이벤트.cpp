//14_수동이벤트

#include <iostream>
#include <windows.h>
using namespace std;

//여러번 실행 한 후 테스트 

//수동이벤트 생성 
//WaitForSingleObject가 통과되더라도 Signal상태가 유지
void fun1()
{
	HANDLE hEvent = CreateEvent(NULL, // 보안속성
		TRUE, // 수동리셋(TRUE)
		FALSE, // 초기 상태( NON SIGNAL )
		TEXT("e")); // 공유할 이벤트 이름

	cout << "Event를 기다린다." << endl;
	WaitForSingleObject(hEvent, INFINITE);
	cout << "Event 획득 " << endl;


	cout << "Event를 기다린다." << endl;
	WaitForSingleObject(hEvent, INFINITE);
	cout << "Event 획득" << endl;


	CloseHandle(hEvent);
}

int main()
{
	fun1();

	return 0;
}