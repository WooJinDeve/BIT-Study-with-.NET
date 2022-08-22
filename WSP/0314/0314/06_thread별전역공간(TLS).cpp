//06_thread별 전역공간(TLS)

//consle : 범용타입...
#include <Windows.h>
#include <stdio.h>
#include <tchar.h>

void goo(TCHAR* name)
{
	// TLS 공간에 변수를 생성한다.
	__declspec(thread) static int c = 0;

	//전역정적공간에 변수를 생성한다.
	//static int c = 0;
	++c;
	_tprintf(TEXT("%s : %d\n"), name, c); // 함수가 몇번 호출되었는지 알고 싶다.
}

unsigned long __stdcall foo(void* p)
{
	TCHAR* name = (TCHAR*)p;
	goo(name);
	goo(name);
	goo(name);
	return 0;
}

int main()
{
	HANDLE h1 = CreateThread(0, 0, foo, (void*)TEXT("A"), 0, 0);
	HANDLE h2 = CreateThread(0, 0, foo, (void*)TEXT("\tB"), 0, 0);

	HANDLE h[2] = { h1, h2 };
	//TRUE : 2개의 핸들이 모두 signal될때까지 대기하겠다.
	//FALSE : 기다리던 핸들 중 하나만 signal되도 리턴한다.
	//스레드 : 종료되면 signal된다.
	WaitForMultipleObjects(2, h, TRUE, INFINITE);
	CloseHandle(h1);
	CloseHandle(h2);

	return 0;
}
