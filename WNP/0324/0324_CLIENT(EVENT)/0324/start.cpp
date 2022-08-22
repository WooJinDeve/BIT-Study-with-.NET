#include "std.h"

#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1"

int main(void)
{
	if (wbnet_InitLibrary() == false)
		return 0;

	if (wbnet_CreateSocket(SERVER_IP, SERVER_PORT) == false)
		return 0;
	

	char buf[1024];
	while (true)
	{
		gets_s(buf, sizeof(buf));
		if (strlen(buf) == 0)
			break;

		wbnet_SenData(buf, strlen(buf) + 1);
	}

	printf("프로그램 종료 \n");

	wbnet_ExitLibrary();
	return 0;
}