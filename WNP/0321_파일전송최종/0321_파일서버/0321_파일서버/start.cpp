//start.cpp

#include "std.h"

#define SERVER_PORT	5000

//[������]���� ������ ���� ���� ���� ����-���-
//������ ����� ���� ���
int main()
{
	HANDLE h = CreateThread(0, 0, wbfilenet_FileThread, (void*)SERVER_PORT, 0, 0);

	WaitForSingleObject(h, INFINITE);
	CloseHandle(h);

	return 0;
}