//Server
/*
* [Client�� ������ ��ٸ�, Client�� ��û ó��(��û�� ���� ����)]
* 
*/

#include <Windows.h>
#include <stdio.h>

//����� �߻�( echo : �޾Ƹ�)
DWORD WINAPI WorkThread(LPVOID temp)
{
	HANDLE hPipe = (HANDLE)temp;

	char input[256] = "";
	memset(input, 0,sizeof(input));

	DWORD dwRead=0, dwWrite=0;	//���� �о�� ũ��
	
	while (true)
	{
		printf("���� thread ����\n");
		//����
		BOOL b = ReadFile(hPipe, input, sizeof(input), &dwRead, 0);
		if (b == FALSE)   //����, ���� byteũ�Ⱑ 0
			break;

		printf("���ŵ����� : %s, %dbyte\n", input, dwRead);

		//����
		b = WriteFile(hPipe, input, dwRead, &dwWrite, 0);
		if (b == FALSE || dwRead == 0)  //����, ���� byteũ�Ⱑ 0
			break;
	}
	printf("���Ž���\n");
	return 0;
}

DWORD WINAPI MainThread(LPVOID temp)
{
	HANDLE hPipe;

	while (true)
	{
		// 1. named pipe �����
		hPipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\MutiPipe"), // pipe�̸�
			PIPE_ACCESS_DUPLEX, // ����� ����(read, write)
			PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,//PIPE_TYPE_BYTE,
			5, // ����    �̸���   ��������   �����   �ִ�   �ִ�   ����. 
			1024, 1024, // �����   ����    ũ��(OS�˾Ƽ� ó����)
			1000, // WaitNamedPipe�Լ���   ����� ��   �ִ�    �ð� 
			0); // KO ����
		if (hPipe == INVALID_HANDLE_VALUE)
		{
			printf("Pipe��   �������   �����ϴ�.");
			return 0;
		}

		//2. Ŭ���̾�Ʈ�� ���� ���
		printf("Ŭ���̾�Ʈ ������ ��ٸ�......\n");
		if (ConnectNamedPipe(hPipe, 0) == FALSE)
		{
			CloseHandle(hPipe);
		}
		else
		{
			printf("�۾� ������ ����\n");
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