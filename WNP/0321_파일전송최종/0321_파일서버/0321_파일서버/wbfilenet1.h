//wbfilenet1.h

#pragma once


/*
* 1대 다 접속
* 1대 다 파일 전송
*   - (파일 수신)
*   - (파일 저장)
* -----------------------------------------------------------------
*   - (보낸 사람을 제외한 다른 사람들에게 파일을 전송)
*/

struct FILE_INFO
{
	char FileName[260];
	int  size;
};

//대기 스레드 
DWORD WINAPI wbfilenet_FileThread(void* p);

//파일 송수신 스레드
//[클라이언트 접속시 ] 대기 스레드에서 호출
DWORD WINAPI FileServer(void* p);