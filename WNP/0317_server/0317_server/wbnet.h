#pragma once

void fun_RecvData(char* msg);

//���̺귯�� �ʱ�ȭ 
int wbnet_LibraryInit();

// ���ϻ��� + �ּ��Ҵ� + �� ����
int wbnet_CreateListenSocket(int port);

//���� ����
void wbnet_DeleteListenSocket();

// ���̺귯�� ����;
void wbnet_Libraryexit();

//[Ŭ���̾�Ʈ�� ����ó�� + ������ ����] : ���� �ݺ� -> while(true)
void wbnet_ServerRun();

//��� ������ �Լ�
unsigned int __stdcall WorkThread(void* param);


//������Լ� 
int recvn(SOCKET s, char* buf, int len, int flags);
int wbsend(SOCKET s, char* buf, int len);
int wbrecive(SOCKET s, char* buf);