//01_에러코드

#include <iostream>
using namespace std;
#include <stdlib.h>

int n = 0;

int main(void)
{
	int s = 10 / n;

	printf("결과 : %d\n", s);
	return 0;
}