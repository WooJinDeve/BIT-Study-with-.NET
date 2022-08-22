
//서버 접속 요청 --> (전송 -> 수신)

#include<Windows.h>
#include<stdio.h>

HANDLE hpipe;

DWORD WINAPI ReadThread(LPVOID temp)
{
	HANDLE hp = (HANDLE)temp;

	BOOL b;
	char input[256] = "";
	DWORD dwRead; //실제 읽어온 크기
	while (true)
	{
		b = ReadFile(hpipe, input, strlen(input) + 1, &dwRead, 0);
		if (b == false || dwRead == 0)
			break;

		printf("받은 데이터 : %s\n", input);
	}
	return 0;
}

BOOL ServerConnect()
{
	hpipe = CreateFile(TEXT("\\\\.\\pipe\\MultiPipe"), // UNC
		GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING,	0, 0); //OPEN_EXISTING:존재할때 요청

	if (hpipe == INVALID_HANDLE_VALUE)
	{
		printf("Pipe 서버에   연결할수   없습니다\n");
		return false;
	}

	printf("접속성공\n");
	CloseHandle(CreateThread(0, 0, ReadThread, (LPVOID)hpipe, 0, 0));
	return true;
}

int main(void)
{
	if (ServerConnect() == false)
		return 0;

	//전송
	BOOL b;
	char input[256] = "hello, World~";
	DWORD dwWrite; //실제 읽어온 크기
	while (true)
	{
		///*gets_s(input, sizeof(input));
		//getchar();*/
		//if (strlen(input) == 0)
		//	break;
		printf("입력 정보 ; %s\n", input);
		b = WriteFile(hpipe, input, strlen(input)+1, &dwWrite, 0);
		if (b == FALSE || dwWrite == 0) // 실패, 읽은 byte크기가 0 
			break;
	}

	return 0;
}