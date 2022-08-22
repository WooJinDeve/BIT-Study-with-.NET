#pragma once

#define BUFSIZE 1024

/*
* �������Լ��� �ɹ������� �� �� ����. 
*  - �����ɹ��Լ��� ����ó���Ѵ� (static)
* 
* [static �ɹ� �Լ�]
* - Ŭ���� �ɹ�(�ݴ��� ������ �ν��Ͻ� �ɹ�)
* 
* - Ŭ���� �ɹ��� �ܺο��� Ŭ���� �̸��� ����ؼ� ���� -> ��ü�� �ʿ����.
* - Ŭ���� �ɹ��� Ŭ���� �ɹ��� ��� ����
* - �ν��Ͻ� �ɹ��� �ܺο��� ���ٽ� ��ü�� ���ؼ� ���� 
*/

class WbClient
{
private:
	//�ɹ��ʵ�(�ɹ�����)
	SOCKET clientSocket;

	//������&�Ҹ���
	//������ : �ɹ��ʵ� �ʱ�ȭ
	//�Ҹ��� : ��ü�� �Ҹ�� �� �ʿ��� �۾�
public:
	WbClient();
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


