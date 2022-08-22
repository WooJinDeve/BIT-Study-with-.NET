//04_종료핸들러

#include <stdio.h>
#include <windows.h>

DWORD foo() 
{
	int ret = 1;
	__try 
	{
		return ret;
	}
	__finally      //무조건 호출된다.....
	{
		ret = 10;
		printf("try 블록을 벗어나기 전에 finally 구문은 반드시 실행됩니다\n");
	}
	return 0;
}

void main()
{
	printf("결과 : %d\n", foo()); // ?
}
