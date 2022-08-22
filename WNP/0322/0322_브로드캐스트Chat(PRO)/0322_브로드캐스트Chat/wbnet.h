//wbfilenet.h
#pragma once

typedef void (*RecvFunc)(TCHAR*);

//라이브러리 초기화
bool wbnet_LibraryInit();

//라이브러리 해제
void wbnet_LibraryExit();

//수신 소켓 생성
bool wbnet_CreateSocket(int port, int groupid, RecvFunc fun);

//수신 스레드
DWORD WINAPI RecvThread(void* p);

//수신 스레드 종료
void wbnet_ExitSocket();

//데이터 전송
void wbnet_SendData(int port, int groupid, TCHAR* packet);
