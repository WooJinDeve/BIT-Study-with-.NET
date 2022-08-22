//wbfilenet.h
#pragma once

//라이브러리 초기화
bool wbnet_LibraryInit();

//라이브러리 해제
void wbnet_LibraryExit();

//수신 소켓 생성
bool wbnet_CreateSocket(int port, int groupid);

//수신 스레드
DWORD WINAPI RecvThread(void* p);

//수신 스레드 종료
void wbnet_ExitSocket();

void wbnet_SendData(const char*, int gruopid, int port);