#pragma once

#define BUFSIZE 1024

class MainDlg;

class WbClient
{
private:
	//�ɹ��ʵ�(�ɹ�����)
	SOCKET clientSocket;
	MainDlg* pDlg;

	//������&�Ҹ���
	//������ : �ɹ��ʵ� �ʱ�ȭ
	//�Ҹ��� : ��ü�� �Ҹ�� �� �ʿ��� �۾�
public:
	WbClient(MainDlg *p);
	~WbClient();

	//get & set �޼���

	// ��� �޼���
public:
	bool CreateSocket(const char* ip, int port); // ���� ���� & ���� & ����thread ����
	int SenData(char* buf, int length);			 // ������ ����
	void ExitLibrary(); //���� ���̺귯�� ����ó��
private:
	static unsigned int __stdcall ReciveThread(void* param); // ���� ������ 
private:
	bool InitLibrary(); //���� ���̺귯�� �ʱ�ȭ
};


