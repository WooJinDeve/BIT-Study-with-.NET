
//���� ���� ��û --> (���� -> ����)

#include<Windows.h>
#include<stdio.h>

HANDLE hpipe;

DWORD WINAPI ReadThread(LPVOID temp)
{
	HANDLE hp = (HANDLE)temp;

	BOOL b;
	char input[256] = "";
	DWORD dwRead; //���� �о�� ũ��
	while (true)
	{
		b = ReadFile(hpipe, input, strlen(input) + 1, &dwRead, 0);
		if (b == false || dwRead == 0)
			break;

		printf("���� ������ : %s\n", input);
	}
	return 0;
}

BOOL ServerConnect()
{
	hpipe = CreateFile(TEXT("\\\\.\\pipe\\MultiPipe"), // UNC
		GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING,	0, 0); //OPEN_EXISTING:�����Ҷ� ��û

	if (hpipe == INVALID_HANDLE_VALUE)
	{
		printf("Pipe ������   �����Ҽ�   �����ϴ�\n");
		return false;
	}

	printf("���Ӽ���\n");
	CloseHandle(CreateThread(0, 0, ReadThread, (LPVOID)hpipe, 0, 0));
	return true;
}

int main(void)
{
	if (ServerConnect() == false)
		return 0;

	//����
	BOOL b;
	char input[256] = "hello, World~";
	DWORD dwWrite; //���� �о�� ũ��
	while (true)
	{
		///*gets_s(input, sizeof(input));
		//getchar();*/
		//if (strlen(input) == 0)
		//	break;
		printf("�Է� ���� ; %s\n", input);
		b = WriteFile(hpipe, input, strlen(input)+1, &dwWrite, 0);
		if (b == FALSE || dwWrite == 0) // ����, ���� byteũ�Ⱑ 0 
			break;
	}

	return 0;
}