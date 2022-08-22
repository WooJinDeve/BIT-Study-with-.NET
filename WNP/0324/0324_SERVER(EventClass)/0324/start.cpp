#include "std.h"

#define SERVER_PORT 9000

int main(void)
{
	WbServer server;

	if (server.CreateListenSocket(SERVER_PORT) == false)
		return 0;
	
	HANDLE hthread = server.Run();

	WaitForSingleObject(hthread, INFINITE);

	printf("프로그램 종료\n");
	return 0;
}