//04_�����ڵ鷯

#include <stdio.h>
#include <windows.h>

DWORD foo() 
{
	int ret = 1;
	__try 
	{
		return ret;
	}
	__finally      //������ ȣ��ȴ�.....
	{
		ret = 10;
		printf("try ����� ����� ���� finally ������ �ݵ�� ����˴ϴ�\n");
	}
	return 0;
}

void main()
{
	printf("��� : %d\n", foo()); // ?
}
