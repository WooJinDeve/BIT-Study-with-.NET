//14_��������
// �޼��� Box �� OK�� ������ ����.. 4�� �̻� ������ ������..

#include <windows.h>
#include <stdio.h>

int main()
{
	HANDLE hSemaphore = CreateSemaphore(0, // ����
		3, // count �ʱⰪ
		3, // �ִ� count
		TEXT("s")); // �̸�


	printf("������� ����մϴ�.\n");
	WaitForSingleObject(hSemaphore, INFINITE); // ����Ҷ����� �ڵ����� count 1 ����

	printf("���� ��� ȹ��\n");

	char dummy = getchar();

	LONG old;
	ReleaseSemaphore(hSemaphore, 1, &old); // count �� 1 ����
	CloseHandle(hSemaphore);
}
