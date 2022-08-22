//Client

//서버 접속 요청 --> (전송 ->수신)

#include <Windows.h>
#include <stdio.h>

HANDLE hPipe;

DWORD WINAPI ReadThread(LPVOID temp)
{
	HANDLE hp = (HANDLE)temp;

	BOOL b;
	char input[256] = "";
	memset(input, 0, sizeof(input));
	DWORD dwRead=0;

	while (true)
	{
		b = ReadFile(hp, input, sizeof(input), &dwRead, 0);
		//if (b == false || dwRead == 0)
		//	break;

		printf("받은 데이터 : %s\n", input);
	}

	return 0;
}

bool ServerConnect()
{
	//접속 요청
	hPipe = CreateFile(TEXT("\\\\.\\pipe\\MutiPipe"), // UNC
		GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);//OPEN_EXISTING:존재할때 요청

	if (hPipe == INVALID_HANDLE_VALUE)
	{
		printf("Pipe 서버에 연결할수 없습니다\n");
		return false;
	}

	DWORD pipeMode = PIPE_READMODE_MESSAGE | PIPE_WAIT; // 메세지 기반으로 모드 변경
	BOOL isSuccess = SetNamedPipeHandleState(
		hPipe, // 파이프 핸들
		&pipeMode, // 변경할 모드 정보,
		NULL,
		NULL);
	if (!isSuccess)
	{
		return 0;
	}

	printf("접속 성공\n");
	CloseHandle(CreateThread(0, 0, ReadThread, (LPVOID)hPipe, 0, 0));
	return true;
}

int main()
{
	if (ServerConnect() == false)
		return 0;

	//전송
	BOOL b;
	char input[256] = "hello,World~";
	DWORD dwWrite=0;

	while (true)
	{
		gets_s(input, sizeof(input));
		if (strlen(input) == 0)
			break;
		printf("입력 정보 : %s\n", input);

		b = WriteFile(hPipe, input, strlen(input) + 1, &dwWrite, 0);
		if (b == FALSE || dwWrite == 0)  //실패, 보낸 byte크기가 0
			break;

		FlushFileBuffers(hPipe);

		printf("보낸  데이터 크기  : %dbyte\n", dwWrite);
	}
	printf("프로그램 종료\n");
	return 0;
}