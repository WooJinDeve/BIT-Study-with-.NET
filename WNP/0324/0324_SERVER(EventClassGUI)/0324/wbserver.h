#pragma once

//1 : Ŭ���̾�Ʈ���� 2. Ŭ���̾�Ʈ�������� 
//typedef void (:* LOGFUN)(int); �����Լ�

class Handler; //��������(�Լ��� �����, �������� extern

struct SocketInfo
{
	SOCKET sock;
	WSAEVENT hEvent;
};

struct WbServer
{
private:
	vector<SocketInfo> socketinfo;
	HANDLE hthread;
	Handler* phan;

public:
	WbServer(Handler* phandler);
	~WbServer();
public:
	bool CreateListenSocket(int port); // ���� �ʱ�ȭ(AddSoket)
	HANDLE Run(); // ������ ����
	void CloseListenSocket();
private:
	bool InitLibrary(); //���� ���̺귯�� �ʱ�ȭ
	void ExitLibrary(); //���� ���̺귯�� ����ó��
	bool AddSocket(SOCKET sock, int networkflag); // ���� �߰�
	bool DeleteSocket(SOCKET sock); //���� ����

private:
	static unsigned int WINAPI WorkerTrhread(void* pParam);
	bool GetNetworkEvent(int* idx, WSANETWORKEVENTS* NetworkEvents);
	bool Accept(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
	bool Read(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
	bool Close(SOCKET sock, WSANETWORKEVENTS NetworkEvents);
};

