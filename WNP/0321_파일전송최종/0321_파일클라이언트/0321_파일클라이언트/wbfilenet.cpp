//wbfilenet.cpp

#include "std.h"

//�������Լ��� ������ ������ ���� ���!
struct SendFileData
{
	SOCKET s;
	HANDLE hFile;
	FILE_INFO info;
};

#define FILE_SIZE 4096

SOCKET g_sock;
extern HWND g_hDlg;
  
//���� -> ���Ϻ����� thread ȣ��- ���� 
bool wbfilenet_FileClient(const TCHAR* ip, int port)
{
	WSADATA w;
	WSAStartup(MAKEWORD(2, 2), &w);

	g_sock = socket(AF_INET, SOCK_STREAM, 0);

	SOCKADDR_IN addr;
	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);
	addr.sin_addr.s_addr = inet_addr(ip);

	if (connect(g_sock, (SOCKADDR*)&addr, sizeof(addr)) == -1)
		return false;

	//���� ó��
	CloseHandle(CreateThread(0, 0, FileRecvThread, (void*)g_sock, 0, 0));

	return true;
}

//���� ����
DWORD WINAPI FileRecvThread(void* p)
{
	SOCKET s = (SOCKET)p;

	// 1. ���� ������ �����Ѵ�.
	FILE_INFO file_info;
	recv(s, (char*)&file_info, sizeof(file_info), 0);

	PostMessage(g_hDlg, WM_USER + 100, 0, (LPARAM)file_info.size);

	//2. ���ŵ� ������ �̿��Ͽ� ������ ����
	HANDLE hFile = CreateFile(file_info.FileName, GENERIC_WRITE, FILE_SHARE_READ,
		0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	//3. ���� ����
	int total = file_info.size;
	int current = 0;
	char buf[FILE_SIZE];

	while (total > current)
	{
		int nRecv = recv(s, buf, FILE_SIZE, 0);
		if (nRecv <= 0) break;

		DWORD len;
		WriteFile(hFile, buf, nRecv, &len, 0);

		current += nRecv;

		//���α׷����� �̵�....
		PostMessage(g_hDlg, WM_USER+200, (WPARAM)total, (LPARAM)current);
	}

	//4. ���� ���� ��� ó��
	if (current != total)
		PostMessage(g_hDlg, WM_USER + 300, 0, (LPARAM)1);
	else
		PostMessage(g_hDlg, WM_USER + 300, 0, (LPARAM)0);

	closesocket(s);
	CloseHandle(hFile);
	return 0;
}

//���� ����
void wbfilenet_FileSend(HANDLE file, FILE_INFO* fi)
{
	//����, ���� �ڵ�, ��������
	SendFileData data;
	data.hFile = file;
	data.info = *fi;
	data.s = g_sock;

	CloseHandle(CreateThread(0, 0, FileSendThread, (void*)&data, 0, 0));
}

//������ ������ ����
DWORD WINAPI FileSendThread(void* p)
{
	SendFileData *pdata = (SendFileData*)p;
	HANDLE hFile	= pdata->hFile;
	SOCKET s		= pdata->s;
	FILE_INFO info	= pdata->info;
	//--------------------------------------------

	send(pdata->s, (char*)&info, sizeof(FILE_INFO), 0); //<-----------------
	//-------------------------------------------------
	// ȭ�� ����
	int total = info.size; // ������ ��ü ũ��
	int current = 0;   // ������ ũ��
	int nRead = 0;
	char buf[FILE_SIZE];    // 4k ����.

	while (total > current)
	{
		DWORD len;
		nRead = ReadFile(hFile, buf, FILE_SIZE, &len, 0);

		if (len <= 0) break;

		int nSend = send(s, buf, len, 0);
		if (nSend <= 0) break;

		current += nSend;
	}
	if (total != current)	
		MessageBox(NULL, TEXT("���ۼ���"), TEXT("�˸�"), MB_OK);
	else					
		MessageBox(NULL, TEXT("���ۿ���"), TEXT("�˸�"), MB_OK);

	closesocket(s);
	CloseHandle(hFile);

	return 0;
}