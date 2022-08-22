//07_C라이브러리 사용시 _beginthread 사용하자.
#ifndef _MT
#define _MT 

#include <iostream>
#include <process.h> // _beginthreadex() 를 사용하기 위해..
#include <windows.h>
using namespace std;

unsigned int __stdcall foo(void* p) // 결국 DWORD WINAPI foo() 이다 ~!!
{
	cout << "foo" << endl;
	Sleep(1000);
	cout << "foo finish" << endl;
	return 0;
}

int  main()
{
	unsigned long h = _beginthreadex(0, 0, foo, 0, 0, 0);

	// h가 결국 핸들이다.
	WaitForSingleObject((HANDLE)h, INFINITE);
	CloseHandle((HANDLE)h);

	return 0;
}