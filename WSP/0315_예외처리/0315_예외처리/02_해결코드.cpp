//02_해결코드

#include <windows.h>
#include <stdio.h>

int n = 0;

DWORD ExceptionFilter(DWORD code)
{
	if (code == EXCEPTION_INT_DIVIDE_BY_ZERO) // c0000094
	{
		return 1; // 처리할수 없는 예외 이므로 핸들러를 수행한다.
	}
}

void main()
{
	int s;
	__try
	{
		s = 10 / n;
		printf("결과 : %d\n", s);
	}
	__except (ExceptionFilter(GetExceptionCode()))
	{
		printf("예외 발생 : %x\n", GetExceptionCode());
		ExitProcess(0); // 예외가 발생한경우 대부분 프로세스를 종료 한다.
	}
	printf("프로그램 계속 실행\n");
}
