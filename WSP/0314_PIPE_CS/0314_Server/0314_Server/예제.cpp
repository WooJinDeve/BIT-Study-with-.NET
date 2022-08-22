
#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

#define BUF_SIZE 1024
HANDLE hPipe;

int CommToClient(HANDLE);

int _tmain(int argc, TCHAR* argv[])
{
	TCHAR pipeName[100];
	_tcscpy_s(pipeName, _countof(pipeName), _T("\\\\.\\pipe\\simple_pipe"));
	
	_tprintf(_T("\\\\.\\pipe\\simple_pipe\n"));
	hPipe = CreateNamedPipe(
		_T("\\\\.\\pipe\\simple_pipe"),        // 파이프 이름
		PIPE_ACCESS_DUPLEX,     // 읽기, 쓰기 모드 지정
		PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
		PIPE_UNLIMITED_INSTANCES, // 최대 인스턴스 개수
		0,     // 출력 버퍼 사이즈
		0,     // 입력 버퍼 사이즈
		20000,        // 클라이언트 타임-아웃
		NULL        // 디폴트 보안 속성
	);
	if (hPipe == INVALID_HANDLE_VALUE)
	{
		_tprintf(_T("CreatePipeFailed"));
		return -1;
	}

	while (1)
	{
		if (ConnectNamedPipe(hPipe, NULL) == false)
		{
			CloseHandle(hPipe);
			return 1;
		}
		else
		{
			if (CommToClient(hPipe) == -1)
				break;
		}			
	}

	DisconnectNamedPipe(hPipe);
	CloseHandle(hPipe);
	_tprintf(_T("Program exit"));
	return 0;
}

int CommToClient(HANDLE hPipe)
{
	TCHAR recvMessage[100];
	TCHAR sendMessage[100];
	DWORD recvSize;
	DWORD sendSize;

	while (true)
	{
		_tprintf(_T("Input Send Message :"));
		_tcscanf_s(_T("%s"), sendMessage, _countof(sendMessage));
		_tprintf(_T("\n"));
		BOOL b = WriteFile(
			hPipe, // 파이프 핸들
			sendMessage, // 전송할 데이터 버퍼
			(_tcslen(sendMessage)+1)* sizeof(TCHAR), // 전송할 데이터 크기
			&sendSize, // 전송된 데이터 크기
			NULL
		);
		if (b == FALSE)
		{
			_tprintf(_T("WriteFile error\n"));
			return -1;
		}
		FlushFileBuffers(hPipe);

		b = ReadFile(
			hPipe, // 파이프 핸들
			recvMessage, // 전송할 데이터 버퍼
			_countof(recvMessage), // 전송할 데이터 크기
			&recvSize, // 전송된 데이터 크기
			NULL
		);
		if (b == FALSE)
		{
			_tprintf(_T("WriteFile error\n"));
			return -1;
		}



		_tprintf(_T("Recv Message : %s\n"), recvMessage);
		
	}

	FlushFileBuffers(hPipe);
	DisconnectNamedPipe(hPipe);
	CloseHandle(hPipe);
	return 1;
}
