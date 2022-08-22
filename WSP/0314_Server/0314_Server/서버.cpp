//Server
/*
* [Client�� ������ ��ٸ�, Client�� ��û ó��(��û�� ���� ����)]
* 
*/

#include <stdio.h>
#include <Windows.h>

//�����
DWORD WINAPI WorkThread(LPVOID temp)
{
	HANDLE hpipe = (HANDLE)temp;

	char input[256] = "";
	DWORD dwRead, dwWrite; //���� �о�� ũ��

	while (true)
	{
		//����
		BOOL b = ReadFile(hpipe, input, sizeof(input), &dwRead, 0);
		if (b == FALSE || dwRead == 0) // ����, ���� byteũ�Ⱑ 0 
			break;

		//����
		b = WriteFile(hpipe, input, dwRead, &dwWrite, 0);
		if (b == FALSE || dwWrite == 0) // ����, ���� byteũ�Ⱑ 0 
			break;
	}

	return 0;
}

DWORD WINAPI MainThread(LPVOID temp)
{
	HANDLE hPipe;
	while (true)
	{
		//1. named pipe �����
		hPipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\MultiPipe"), // pipe�̸�
			PIPE_ACCESS_DUPLEX, // ����� ����(read, write)
			PIPE_TYPE_BYTE,
			5, // ����    �̸���   ��������   �����   �ִ�   �ִ�   ����. 
			4096, 4096, // �����   ����    ũ��(0S�� �˾Ƽ� ó����)
			1000, // WaitNamedPipe�Լ���   ����Ҽ�   �ִ�    �ð� 
			0); // KO ����

		if (hPipe == INVALID_HANDLE_VALUE)
		{
			printf("Pipe��   �������   �����ϴ�.\n");
			return 0;
		}
		//2. Ŭ���̾�Ʈ�� ���Ӵ��
		printf("Ŭ���̾�Ʈ ������ ��ٸ�.... \n");
		BOOL b = ConnectNamedPipe(hPipe, 0);
		if (b == FALSE && GetLastError() == ERROR_PIPE_CONNECTED)
			b = TRUE;
		//����
		if (b == TRUE)
		{
			HANDLE hThread;
			hThread = CreateThread(0, 0, WorkThread, (LPVOID)hPipe, 0, 0);
			CloseHandle(hThread);
		}
		//����
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