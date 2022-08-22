//wbnet.h

#pragma once

#define BUFSIZE 1024

class MainDlg;	//전방참조(함수의 선언부, 전역변수extern)

struct SocketInfo
{
	SOCKET sock;
	WSAEVENT hEvent;
};

class WbServer
{
private:
	vector< SocketInfo> socketinfos;
	HANDLE hthread;
	MainDlg* pMainDlg;

public:
	WbServer(MainDlg*phandle);
	~WbServer();

public:
	bool CreateListenSocket(int port);
	HANDLE Run();
	void CloseListenSocket();

private:
	bool InitLibrary();
	void ExitLibrary();

private:
	bool AddSocket(SOCKET sock, int networkflag);	
	bool DeleteSocket(SOCKET sock);

private:
	static unsigned int WINAPI WorkerThread(void* pParam);
	bool GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents);
	bool Accept(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
	bool Read(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
	bool Close(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
};














