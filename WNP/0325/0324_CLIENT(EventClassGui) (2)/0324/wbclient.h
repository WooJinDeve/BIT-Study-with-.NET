#pragma once

#define BUFSIZE 1024

class MainDlg;

class WbClient
{
private:
	//맴버필드(맴버변수)
	SOCKET clientSocket;
	MainDlg* pDlg;

	//생성자&소멸자
	//생성자 : 맴버필드 초기화
	//소멸자 : 객체가 소멸될 때 필요한 작업
public:
	WbClient(MainDlg *p);
	~WbClient();

	//get & set 메서드

	// 기능 메서드
public:
	bool CreateSocket(const char* ip, int port); // 소켓 생성 & 접속 & 수신thread 실행
	int SenData(char* buf, int length);			 // 데이터 전송
	void ExitLibrary(); //소켓 라이브러리 종료처리
private:
	static unsigned int __stdcall ReciveThread(void* param); // 수신 스레드 
private:
	bool InitLibrary(); //소켓 라이브러리 초기화
};


