
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
		_T("\\\\.\\pipe\\simple_pipe"),        // ������ �̸�
		PIPE_ACCESS_DUPLEX,     // �б�, ���� ��� ����
		PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
		PIPE_UNLIMITED_INSTANCES, // �ִ� �ν��Ͻ� ����
		0,     // ��� ���� ������
		0,     // �Է� ���� ������
		20000,        // Ŭ���̾�Ʈ Ÿ��-�ƿ�
		NULL        // ����Ʈ ���� �Ӽ�
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
			hPipe, // ������ �ڵ�
			sendMessage, // ������ ������ ����
			(_tcslen(sendMessage)+1)* sizeof(TCHAR), // ������ ������ ũ��
			&sendSize, // ���۵� ������ ũ��
			NULL
		);
		if (b == FALSE)
		{
			_tprintf(_T("WriteFile error\n"));
			return -1;
		}
		FlushFileBuffers(hPipe);

		b = ReadFile(
			hPipe, // ������ �ڵ�
			recvMessage, // ������ ������ ����
			_countof(recvMessage), // ������ ������ ũ��
			&recvSize, // ���۵� ������ ũ��
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
