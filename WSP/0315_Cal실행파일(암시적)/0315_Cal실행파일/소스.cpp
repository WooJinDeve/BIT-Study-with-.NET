#include <stdio.h>

//DLL�� �Ͻ������� ���
//���������� ����ɶ� �ڵ����� DLL�� �޸𸮿� �ε�
//���������� ����� �� �ڵ����� DLL�� �޸𸮿��� ����

//DLL ������ �����ؼ� ���� �ҽ����� ��ġ�� �����ֱ�

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
