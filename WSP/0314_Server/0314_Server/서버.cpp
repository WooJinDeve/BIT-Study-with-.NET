//Server
/*
* [Client의 접속을 기다림, Client의 요청 처리(요청에 대한 응답)]
* 
*/

#include <stdio.h>
#include <Windows.h>

//입출력
DWORD WINAPI WorkThread(LPVOID temp)
{
	HANDLE hpipe = (HANDLE)temp;

	char input[256] = "";
	DWORD dwRead, dwWrite; //실제 읽어온 크기

	while (true)
	{
		//읽음
		BOOL b = ReadFile(hpipe, input, sizeof(input), &dwRead, 0);
		if (b == FALSE || dwRead == 0) // 실패, 읽은 byte크기가 0 
			break;

		//전송
		b = WriteFile(hpipe, input, dwRead, &dwWrite, 0);
		if (b == FALSE || dwWrite == 0) // 실패, 읽은 byte크기가 0 
			break;
	}

	return 0;
}

DWORD WINAPI MainThread(LPVOID temp)
{
	HANDLE hPipe;
	while (true)
	{
		//1. named pipe 만들기
		hPipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\MultiPipe"), // pipe이름
			PIPE_ACCESS_DUPLEX, // 양방향 전용(read, write)
			PIPE_TYPE_BYTE,
			5, // 동일    이름의   파이프를   만들수   있는   최대   갯수. 
			4096, 4096, // 입출력   버퍼    크기(0S가 알아서 처리함)
			1000, // WaitNamedPipe함수로   대기할수   있는    시간 
			0); // KO 보안

		if (hPipe == INVALID_HANDLE_VALUE)
		{
			printf("Pipe를   만들수가   없습니다.\n");
			return 0;
		}
		//2. 클라이언트의 접속대기
		printf("클라이언트 접속을 기다림.... \n");
		BOOL b = ConnectNamedPipe(hPipe, 0);
		if (b == FALSE && GetLastError() == ERROR_PIPE_CONNECTED)
			b = TRUE;
		//성공
		if (b == TRUE)
		{
			HANDLE hThread;
			hThread = CreateThread(0, 0, WorkThread, (LPVOID)hPipe, 0, 0);
			CloseHandle(hThread);
		}
		//실패
		else
		{
			CloseHandle(hPipe);
		}
	}
	return 0;
}

int main(void)
{
	HANDLE h = CreateThread(0, 0, MainThread, 0, 0, 0);

	WaitForSingleObject(h, INFINITE);
	CloseHandle(h);

	return 0;
}