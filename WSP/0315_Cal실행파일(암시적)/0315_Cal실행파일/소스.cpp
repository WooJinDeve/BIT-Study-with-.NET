#include <stdio.h>

//DLL을 암시적으로 사용
//실행파일이 실행될때 자동으로 DLL이 메모리에 로등
//실행파일이 종료될 때 자동으로 DLL이 메모리에서 제거

//DLL 파일을 복사해서 현재 소스파일 위치에 붙혀넣기

#include "calc.h"
//#pragma comment(lib,"0315_CalDLL.lib")

int main(void)
{
	int num1 = 10;
	int num2 = 20;

	printf("%d + %d = %5.1f \n", num1, num2, cal_add(num1, num2));
	printf("%d - %d = %5.1f \n", num1, num2, cal_sub(num1, num2));
	printf("%d * %d = %5.1f \n", num1, num2, cal_mul(num1, num2));
	printf("%d / %d = %5.1f \n", num1, num2, cal_div(num1, num2));


	return 0;
}
