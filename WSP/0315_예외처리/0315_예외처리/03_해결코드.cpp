//03_해결코드

#include <windows.h>
#include <stdio.h>

int n = 0;

DWORD ExceptionFilter(DWORD code) 
{
	if (code == EXCEPTION_INT_DIVIDE_BY_ZERO) // c0000094
	{
		printf("0으로 나누는 예외 발생. 새로운 값을 넣어 주세요 >> ");
		scanf_s("%d", &n);
		return -1;	// 예외의 원인을 수정했으므로 예외난 곳을 다시 실행해 본다.
	}
	return 1;	 // 처리할수 없는 예외 이므로 핸들러를 수행한다.
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
