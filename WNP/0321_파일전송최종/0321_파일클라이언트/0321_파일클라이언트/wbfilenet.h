//wbfilenet.h
#pragma once

//���� ����
bool wbfilenet_FileClient(const TCHAR* ip, int port);

//���� ���� ������
//���� ������ �����ϸ� ����(wbfilenet_FileClient() ���� ȣ��)
DWORD WINAPI FileRecvThread(void* p);



//���� ���� ��û
void wbfilenet_FileSend(HANDLE file, FILE_INFO *fi);

//���� ���� ������(wbfilenet_FileSend() ���� ȣ��
DWORD WINAPI FileSendThread(void* p);