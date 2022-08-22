//start.cpp

#include "std.h"

#define SERVER_PORT	5000

//[스레드]파일 수신을 위한 서버 소켓 생성-대기-
//스레드 종료시 까지 대기
int main()
{
	HANDLE h = CreateThread(0, 0, wbfilenet_FileThread, (void*)SERVER_PORT, 0, 0);

	WaitForSingleObject(h, INFINITE);
	CloseHandle(h);

	return 0;
}