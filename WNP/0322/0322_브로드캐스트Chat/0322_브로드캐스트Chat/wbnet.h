//wbfilenet.h
#pragma once

//���̺귯�� �ʱ�ȭ
bool wbnet_LibraryInit();

//���̺귯�� ����
void wbnet_LibraryExit();

//���� ���� ����
bool wbnet_CreateSocket(int port, int groupid);

//���� ������
DWORD WINAPI RecvThread(void* p);

//���� ������ ����
void wbnet_ExitSocket();

void wbnet_SendData(const char*, int gruopid, int port);