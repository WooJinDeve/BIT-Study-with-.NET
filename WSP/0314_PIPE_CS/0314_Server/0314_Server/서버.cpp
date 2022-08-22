//Server
/*
* [Client의 접속을 기다림, Client의 요청 처리(요청에 대한 응답)]
* 
*/

#include <Windows.h>
#include <stdio.h>

//입출력 발생( echo : 메아리)
DWORD WINAPI WorkThread(LPVOID temp)
{
	HANDLE hPipe = (HANDLE)temp;

	char input[256] = "";
	memset(input, 0,sizeof(input));

	DWORD dwRead=0, dwWrite=0;	//실제 읽어온 크기
	
	while (true)
	{
		printf("수신 thread 동작\n");
		//읽음
		BOOL b = ReadFile(hPipe, input, sizeof(input), &dwRead, 0);
		if (b == FALSE)   //실패, 읽은 byte크기가 0
			break;

		printf("수신데이터 : %s, %dbyte\n", input, dwRead);

		//전송
		b = WriteFile(hPipe, input, dwRead, &dwWrite, 0);
		if (b == FALSE || dwRead == 0)  //실패, 보낸 byte크기가 0
			break;
	}
	printf("수신실패\n");
	return 0;
}

DWORD WINAPI MainThread(LPVOID temp)
{
	HANDLE hPipe;

	while (true)
	{
		// 1. named pipe 만들기
		hPipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\MutiPipe"), // pipe이름
			PIPE_ACCESS_DUPLEX, // 양방향 전용(read, write)
			PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,//PIPE_TYPE_BYTE,
			5, // 동일    이름의   파이프를   만들수   있는   최대   갯수. 
			1024, 1024, // 입출력   버퍼    크기(OS알아서 처리함)
			1000, // WaitNamedPipe함수로   대기할 수   있는    시간 
			0); // KO 보안
		if (hPipe == INVALID_HANDLE_VALUE)
		{
			printf("Pipe를   만들수가   없습니다.");
			return 0;
		}

		//2. 클라이언트의 접속 대기
		printf("클라이언트 접속을 기다림......\n");
		if (ConnectNamedPipe(hPipe, 0) == FALSE)
		{
			CloseHandle(hPipe);
		}
		else
		{
			printf("작업 스레드 생성\n");
			HANDLE hThread;
			hThread = CreateThread(0, 0, WorkThread, (LPVOID)hPipe, 0, 0);
			CloseHandle(hThread);
		}
	}

	return 0;
}

int main()
{
	HANDLE h = CreateThread(0, 0, MainThread, 0, 0, 0);

	WaitForSingleObject(h, INFINITE);
	CloseHandle(h);

	return 0;
}