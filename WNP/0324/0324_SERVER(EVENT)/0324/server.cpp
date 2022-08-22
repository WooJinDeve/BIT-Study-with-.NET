#include "std.h"

#define SERVER_PORT 9000

int main(void)
{
	if (wbnet_InitLibrary() == false)
		return 0;

	if (wbnet_CreateListenSocket(SERVER_PORT) == false)
		return 0;
	
	HANDLE hthread = wbnet_Run();

	WaitForSingleObject(hthread, INFINITE);


	return 0;
}