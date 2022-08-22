//14_세마포어
// 메세지 Box 의 OK를 누르지 말고.. 4번 이상 실행해 보세요..

#include <windows.h>
#include <stdio.h>

int main()
{
	HANDLE hSemaphore = CreateSemaphore(0, // 보안
		3, // count 초기값
		3, // 최대 count
		TEXT("s")); // 이름


	printf("세마포어를 대기합니다.\n");
	WaitForSingleObject(hSemaphore, INFINITE); // 통과할때마다 자동으로 count 1 감소

	printf("세마 포어를 획득\n");

	char dummy = getchar();

	LONG old;
	ReleaseSemaphore(hSemaphore, 1, &old); // count 가 1 증가
	CloseHandle(hSemaphore);
}
