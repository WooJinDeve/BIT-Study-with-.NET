// 클라이언트 코드

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

int ConnectServer(HANDLE hPipe)
{
	TCHAR recvMessage[100];
	TCHAR sendMessage[100];
	DWORD recvSize;
	DWORD sendSize;

	_tprintf(_T("connect server start\n"));
	while (true)
	{
		BOOL b = ReadFile(
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

		_tprintf(_T("Input Send Message :"));
		_tcscanf_s(_T("%s"), sendMessage, _countof(sendMessage));
		_tprintf(_T("\n"));

		b = WriteFile(
			hPipe, // 파이프 핸들
			sendMessage, // 전송할 데이터 버퍼
			(_tcslen(sendMessage) + 1) * sizeof(TCHAR), // 전송할 데이터 크기
			&sendSize, // 전송된 데이터 크기
			NULL
		);
		if (b == FALSE)
		{
			_tprintf(_T("WriteFile error\n"));
			return -1;
		}
		FlushFileBuffers(hPipe);
	}

	FlushFileBuffers(hPipe);
	DisconnectNamedPipe(hPipe);
	CloseHandle(hPipe);
	return 1;
}


int _tmain(int argc, TCHAR* argv[])
{
	HANDLE hPipe;
	//TCHAR readDataBuf[BUFSIZ + 1];
	TCHAR pipeName[100];
	_tcscpy_s(pipeName, _countof(pipeName), _T("\\\\.\\pipe\\simple_pipe"));


	hPipe = CreateFile(
		_T("\\\\.\\pipe\\simple_pipe"), // 파이프 이름
		GENERIC_READ | GENERIC_WRITE, // 읽기 쓰기 모드 동시 지정
		0,
		NULL,
		OPEN_EXISTING,
		0,
		NULL
	);
	if (hPipe == INVALID_HANDLE_VALUE)
		return -1;

	DWORD pipeMode = PIPE_READMODE_MESSAGE | PIPE_WAIT; // 메세지 기반으로 모드 변경
	if (SetNamedPipeHandleState(
		hPipe, // 파이프 핸들
		&pipeMode, // 변경할 모드 정보,
		NULL,
		NULL) == false)
	{
		_tprintf(_T("SetNamedPipeHandle State error!\n"));
		CloseHandle(hPipe);
		return -1;
	}
	else
	{
		if (ConnectServer(hPipe) == -1)
			CloseHandle(hPipe);
	}	
	
}
