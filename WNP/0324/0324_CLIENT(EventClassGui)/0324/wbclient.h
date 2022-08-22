#pragma once

#define BUFSIZE 1024

/*
* 스레드함수는 맴버변수가 될 수 없다. 
*  - 정적맴버함수로 변경처리한다 (static)
* 
* [static 맴버 함수]
* - 클래스 맴버(반대의 개념은 인스턴스 맴버)
* 
* - 클래스 맴버는 외부에서 클래스 이름을 사용해서 접근 -> 객체가 필요없다.
* - 클래스 맴버는 클래스 맴버만 사용 가능
* - 인스턴스 맴버는 외부에서 접근시 객체를 통해서 접근 
*/

class WbClient
{
private:
	//맴버필드(맴버변수)
	SOCKET clientSocket;

	//생성자&소멸자
	//생성자 : 맴버필드 초기화
	//소멸자 : 객체가 소멸될 때 필요한 작업
public:
	WbClient();
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


