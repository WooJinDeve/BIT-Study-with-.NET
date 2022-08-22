//wbfilenet1.cpp

#include "std.h"

vector<SOCKET> g_clientsockets; //����(����), ����(����), ���(����)
#define FILE_BUFFERSIZE	4096

DWORD WINAPI wbfilenet_FileThread(void* p)
{
	int port = (int)p;

	WSADATA wsadata;

	if (WSAStartup(MAKEWORD(2, 2), &wsadata) != 0)
	{
		printf("Can't Initialize Socket !\n");
		return 1;
	}
	//--------------------------------------------
	SOCKET s = socket(AF_INET, SOCK_STREAM, 0);

	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);
	addr.sin_addr.s_addr = INADDR_ANY;

	if (bind(s, (SOCKADDR*)&addr, sizeof(addr)) == -1)
	{
		printf("Can't Bind\n");
		closesocket(s);
		return 1;
	}

	if (listen(s, 10) == -1)
	{
		printf("Can't Listen\n");
		closesocket(s);
		return 1;
	}

	printf("���� ���� ���� ����....................\n");

	while (1)
	{
		SOCKADDR_IN c_addr;
		int size = sizeof(c_addr);

		SOCKET c_s = accept(s, (SOCKADDR*)&c_addr, &size);

		printf("Client ���� : %s\n", inet_ntoa(c_addr.sin_addr));

		g_clientsockets.push_back(c_s);

		CloseHandle(CreateThread(0, 0, FileServer, (void*)c_s, 0, 0));
	}
	//--------------------------------------------
	closesocket(s);
	WSACleanup();

	return 0;
}

DWORD WINAPI FileServer(void* p)
{
	SOCKET s = (SOCKET)p;

	// 1. ���� ������ �����Ѵ�.
	FILE_INFO file_info;
	recv(s, (char*)&file_info, sizeof(file_info), 0);

	//1.1 ���� ������ ��� Ŭ���̾�Ʈ���� ����------------------------------
	for (int i = 0; i < (int)g_clientsockets.size(); i++)
	{
		SOCKET cs = g_clientsockets[i];
		if( cs != s)
			send(cs, (char*)&file_info, sizeof(file_info), 0);
	}
	//--------------------------------------------------------------------

	//2. ���ŵ� ������ �̿��Ͽ� ������ ����
	HANDLE hFile = CreateFile(file_info.FileName, GENERIC_WRITE, FILE_SHARE_READ,
		0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	//3. ���� ����
	int total = file_info.size;
	int current = 0;
	char buf[FILE_BUFFERSIZE];

	while (total > current)
	{
		int nRecv = recv(s, buf, FILE_BUFFERSIZE, 0);
		if (nRecv <= 0) break;

		DWORD len;
		WriteFile(hFile, buf, nRecv, &len, 0);

		current += nRecv;

		printf("���� ���� : (%d/%d)byte\n", current, total);
		printf("���� ���� : %.1f\n", ((float)current / total) * 100.0f);

		//1.1 ���� ������ ��� Ŭ���̾�Ʈ���� ����------------------------------
		for (int i = 0; i < (int)g_clientsockets.size(); i++)
		{
			SOCKET cs = g_clientsockets[i];
			if (cs != s)
				send(cs, buf, FILE_BUFFERSIZE, 0);
		}
		//--------------------------------------------------------------------

	}

	//4. ���� ���� ��� ó��
	if (current != total)
		printf("���� ���� ����\n");
	else
		printf("���� ���� ����\n");

	//4. ���� ó��
	for (int i = 0; i < (int)g_clientsockets.size(); i++)
	{
		SOCKET cs = g_clientsockets[i];
		if (cs == s)
		{
			g_clientsockets.erase(g_clientsockets.begin() + i);
			break;
		}
	}

	closesocket(s);
	CloseHandle(hFile);

	return 0;
}
