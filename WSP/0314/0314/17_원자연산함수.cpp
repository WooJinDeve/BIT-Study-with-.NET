//���� �����Լ� 
//����ȭ�� ���¿��� ���� ������ ���ҽ�ų �� �ִ� �Լ��� ����
#include <stdio.h>
#include <windows.h>

// �����ڿ�
long value = 0;

DWORD WINAPI ThreadFunc(void* p)
{
	int i = 0;
	for (i = 0; i < 10000000; ++i)
		//++value;
		//InterlockedIncrement(&value);  //1����
		InterlockedDecrement(&value);	 //1����
	return 0;
}

int main()
{
	int i = 0;

	HANDLE hThread[5];
	for (i = 0; i < 5; ++i)
		hThread[i] = CreateThread(0, 0, ThreadFunc, 0, 0, 0);

	//TRUE : 5���� thread�� �� signal(����)�ɶ����� ��� 
	WaitForMultipleObjects(5, hThread, TRUE, INFINITE);
	for (i = 0; i < 5; ++i)
		CloseHandle(hThread[i]);

	//��� ���
	printf("value = %d\n", value);

	return 0;
}
