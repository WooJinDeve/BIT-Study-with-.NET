#include <stdio.h>
#include"calc.h"

int main(void)
{
	int num1 = 10;
	int num2 = 20;

	printf("%d + %d = %5.1f\n", num1, num2, cal_add(num1, num2));
	printf("%d - %d = %5.1f\n", num1, num2, cal_sub(num1, num2));
	printf("%d * %d = %5.1f\n", num1, num2, cal_mul(num1, num2));
	printf("%d / %d = %5.1f\n", num1, num2, cal_div(num1, num2));

	return 0;
}