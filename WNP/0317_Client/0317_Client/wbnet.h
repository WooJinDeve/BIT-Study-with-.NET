#pragma once


//���̺귯�� �ʱ�ȭ
int wbnet_LibraryInit();

//���� ����
int wbnet_CreateSocket();

//���� ����
void wbnet_DeleteSocket();

// ���̺귯�� ����;
void wbnet_Libraryexit();

//���� ���� + recvthread ����
int Wbnet_Run(int port, const char* ip);

//������ ���� - wbnet_run���� ȣ��
unsigned int __stdcall ReciveThread(void* param);
//������ ���� �Լ�
int wbnet_SenData(const char* msg, int size);

int recvn(SOCKET s, char* buf, int len, int flags);
int wbsend(SOCKET s, char* buf, int len);
int wbrecive(SOCKET s, char* buf);
