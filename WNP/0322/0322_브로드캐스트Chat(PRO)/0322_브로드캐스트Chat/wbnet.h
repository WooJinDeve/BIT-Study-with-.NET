//wbfilenet.h
#pragma once

typedef void (*RecvFunc)(TCHAR*);

//���̺귯�� �ʱ�ȭ
bool wbnet_LibraryInit();

//���̺귯�� ����
void wbnet_LibraryExit();

//���� ���� ����
bool wbnet_CreateSocket(int port, int groupid, RecvFunc fun);

//���� ������
DWORD WINAPI RecvThread(void* p);

//���� ������ ����
void wbnet_ExitSocket();

//������ ����
void wbnet_SendData(int port, int groupid, TCHAR* packet);
