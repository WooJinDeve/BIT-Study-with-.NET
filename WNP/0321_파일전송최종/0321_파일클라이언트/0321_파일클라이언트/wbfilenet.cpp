//wbfilenet.cpp

#include "std.h"

//스레드함수에 전달할 정보가 많을 경우!
struct SendFileData
{
	SOCKET s;
	HANDLE hFile;
	FILE_INFO info;
};

#define FILE_SIZE 4096

SOCKET g_sock;
extern HWND g_hDlg;
  
//접속 -> 파일보내는 thread 호출- 종료 
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

	//수신 처리
	CloseHandle(CreateThread(0, 0, FileRecvThread, (void*)g_sock, 0, 0));

	return true;
}

//파일 수신
DWORD WINAPI FileRecvThread(void* p)
{
	SOCKET s = (SOCKET)p;

	// 1. 파일 정보를 수신한다.
	FILE_INFO file_info;
	recv(s, (char*)&file_info, sizeof(file_info), 0);

	PostMessage(g_hDlg, WM_USER + 100, 0, (LPARAM)file_info.size);

	//2. 수신된 정보를 이용하여 파일을 생성
	HANDLE hFile = CreateFile(file_info.FileName, GENERIC_WRITE, FILE_SHARE_READ,
		0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	//3. 파일 수신
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

		//프로그래스바 이동....
		PostMessage(g_hDlg, WM_USER+200, (WPARAM)total, (LPARAM)current);
	}

	//4. 파일 수신 결과 처리
	if (current != total)
		PostMessage(g_hDlg, WM_USER + 300, 0, (LPARAM)1);
	else
		PostMessage(g_hDlg, WM_USER + 300, 0, (LPARAM)0);

	closesocket(s);
	CloseHandle(hFile);
	return 0;
}

//파일 전송
void wbfilenet_FileSend(HANDLE file, FILE_INFO* fi)
{
	//소켓, 파일 핸들, 파일정보
	SendFileData data;
	data.hFile = file;
	data.info = *fi;
	data.s = g_sock;

	CloseHandle(CreateThread(0, 0, FileSendThread, (void*)&data, 0, 0));
}

//파일이 서버로 전송
DWORD WINAPI FileSendThread(void* p)
{
	SendFileData *pdata = (SendFileData*)p;
	HANDLE hFile	= pdata->hFile;
	SOCKET s		= pdata->s;
	FILE_INFO info	= pdata->info;
	//--------------------------------------------

	send(pdata->s, (char*)&info, sizeof(FILE_INFO), 0); //<-----------------
	//-------------------------------------------------
	// 화일 전송
	int total = info.size; // 전송할 전체 크기
	int current = 0;   // 전송한 크기
	int nRead = 0;
	char buf[FILE_SIZE];    // 4k 버퍼.

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
		MessageBox(NULL, TEXT("전송성공"), TEXT("알림"), MB_OK);
	else					
		MessageBox(NULL, TEXT("전송에러"), TEXT("알림"), MB_OK);

	closesocket(s);
	CloseHandle(hFile);

	return 0;
}