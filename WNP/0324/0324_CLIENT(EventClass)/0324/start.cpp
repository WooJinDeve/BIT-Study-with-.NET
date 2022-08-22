#include "std.h"

#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1"

int main(void)
{
	WbClient client; // 생성자 호출

	if (client.CreateSocket(SERVER_IP, SERVER_PORT) == false)
		return 0;
	
	TCHAR buf[1024];
	while (true)
	{
		_getts_s(buf, _countof(buf));
		if (_tcslen(buf) == 0)
			break;

		client.SenData((char*)buf, (_tcslen(buf)+1)*2);
	}

	printf("프로그램 종료 \n");
	return 0;
}