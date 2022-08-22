//wbfilenet.h
#pragma once

//서버 접속
bool wbfilenet_FileClient(const TCHAR* ip, int port);

//파일 수신 스레드
//서버 접속이 성공하면 생성(wbfilenet_FileClient() 에서 호출)
DWORD WINAPI FileRecvThread(void* p);



//파일 전송 요청
void wbfilenet_FileSend(HANDLE file, FILE_INFO *fi);

//파일 전송 스레드(wbfilenet_FileSend() 에서 호출
DWORD WINAPI FileSendThread(void* p);