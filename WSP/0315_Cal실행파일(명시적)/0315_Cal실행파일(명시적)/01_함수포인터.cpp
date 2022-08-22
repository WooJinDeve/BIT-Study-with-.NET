#include <stdio.h>

typedef unsigned int uint;
typedef void(*FUNTYPE)(int); //FUNTYPE 타입명을 새로 define

void foo(int n)
{
	printf("foo : %d\n", n);
}
int main(void)
{
	foo(10);
	void (*FUN1)(int) = foo;
	FUN1(20);

	FUNTYPE fun1 = foo;
	fun1(30);
	return 0;
}