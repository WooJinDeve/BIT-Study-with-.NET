#pragma once

struct SocketInfo
{
	SOCKET sock;
	WSAEVENT hEvent;
};

struct WbServer
{
private:
	vector<SocketInfo> socketinfo;

public:
	WbServer();
	~WbServer();
public:
	bool CreateListenSocket(int port); // 소켓 초기화(AddSoket)
	HANDLE Run(); // 스레드 생성
private:
	bool InitLibrary(); //소켓 라이브러리 초기화
	void ExitLibrary(); //소켓 라이브러리 종료처리
	bool AddSocket(SOCKET sock, int networkflag); // 소켓 추가
	bool DeleteSocket(SOCKET sock); //소켓 삭제

private:
	static unsigned int WINAPI WorkerTrhread(void* pParam);
	bool GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents);
	bool Accept(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
	bool Read(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
	bool Close(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
};

