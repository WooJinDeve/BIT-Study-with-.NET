//Client

//���� ���� ��û --> (���� ->����)

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

		printf("���� ������ : %s\n", input);
	}

	return 0;
}

bool ServerConnect()
{
	//���� ��û
	hPipe = CreateFile(TEXT("\\\\.\\pipe\\MutiPipe"), // UNC
		GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);//OPEN_EXISTING:�����Ҷ� ��û

	if (hPipe == INVALID_HANDLE_VALUE)
	{
		printf("Pipe ������ �����Ҽ� �����ϴ�\n");
		return false;
	}

	DWORD pipeMode = PIPE_READMODE_MESSAGE | PIPE_WAIT; // �޼��� ������� ��� ����
	BOOL isSuccess = SetNamedPipeHandleState(
		hPipe, // ������ �ڵ�
		&pipeMode, // ������ ��� ����,
		NULL,
		NULL);
	if (!isSuccess)
	{
		return 0;
	}

	printf("���� ����\n");
	CloseHandle(CreateThread(0, 0, ReadThread, (LPVOID)hPipe, 0, 0));
	return true;
}

int main()
{
	if (ServerConnect() == false)
		return 0;

	//����
	BOOL b;
	char input[256] = "hello,World~";
	DWORD dwWrite=0;

	while (true)
	{
		gets_s(input, sizeof(input));
		if (strlen(input) == 0)
			break;
		printf("�Է� ���� : %s\n", input);

		b = WriteFile(hPipe, input, strlen(input) + 1, &dwWrite, 0);
		if (b == FALSE || dwWrite == 0)  //����, ���� byteũ�Ⱑ 0
			break;

		FlushFileBuffers(hPipe);

		printf("����  ������ ũ��  : %dbyte\n", dwWrite);
	}
	printf("���α׷� ����\n");
	return 0;
}