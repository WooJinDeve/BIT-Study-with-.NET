//02_�ذ��ڵ�

#include <windows.h>
#include <stdio.h>

int n = 0;

DWORD ExceptionFilter(DWORD code)
{
	if (code == EXCEPTION_INT_DIVIDE_BY_ZERO) // c0000094
	{
		return 1; // ó���Ҽ� ���� ���� �̹Ƿ� �ڵ鷯�� �����Ѵ�.
	}
}

void main()
{
	int s;
	__try
	{
		s = 10 / n;
		printf("��� : %d\n", s);
	}
	__except (ExceptionFilter(GetExceptionCode()))
	{
		printf("���� �߻� : %x\n", GetExceptionCode());
		ExitProcess(0); // ���ܰ� �߻��Ѱ�� ��κ� ���μ����� ���� �Ѵ�.
	}
	printf("���α׷� ��� ����\n");
}
