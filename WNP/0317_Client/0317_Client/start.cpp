#include "std.h"

#define SERVER_PORT 9000
#define SERVER_IP "127.0.0.1" //192.168.0.6

//net���κ��� ���� ack ������
void fun_RecvData(char* msg)
{
	int* p = (int*)msg;
	if (*p == PACK_NEWMEMBER_S)
	{
		printf("ȸ�������� ����\n");
	}
	else if (*p == PACK_NEWMEMBER_F)
	{
		printf("ȸ������ ����(�ߺ����̵�)\n");
	}
	else if (*p == PACK_LOGIN_S)
	{
		printf("�α��� ����\n");
	}
	else if (*p == PACK_LOGIN_F)
	{
		printf("�α��� ����\n");
	}
	else if (*p == PACK_DELETEMEMBER_S)
	{
		printf("������ ���� ����\n");
	}
	else if (*p == PACK_DELETEMEMBER_F)
	{
		printf("������ ���� ����\n");
	}
	else if (*p == PACK_LOGOUT_S)
	{
		printf("�α׾ƿ� ����\n");
	}
	else if (*p == PACK_LOGOUT_F)
	{
		printf("�α׾ƿ� ����\n");
	}

}

void fun_NewMenber()
{
	printf("[ȸ������ ���� �Է�] \n\n");
	char name[20], id[20], pw[20];

	printf("���̵� : ");	gets_s(id, sizeof(id));
	printf("�н����� : ");	gets_s(pw, sizeof(pw));
	printf("�̸� : ");		gets_s(name, sizeof(name));

	//������ ������ ��Ŷ�� ����
	NEWMEMBER packet = pack_NewMember(id, pw, name);

	//������ ����
	if (wbnet_SenData((const char*)&packet, sizeof(packet)) == -1)
	{
		printf("�۽� ����\n");
	}
	else
	{
		printf("���� ����\n");
	}
}

void fun_Login()
{
	printf("[�α��� ���� �Է�] \n\n");
	char id[20], pw[20];

	printf("���̵� : ");	gets_s(id, sizeof(id));
	printf("�н����� : ");	gets_s(pw, sizeof(pw));

	//������ ������ ��Ŷ�� ����
	LOGIN packet = pack_Login(id, pw);

	//������ ����
	if (wbnet_SenData((const char*)&packet, sizeof(packet)) == -1)
	{
		printf("�۽� ����n");
	}
	else
	{
		printf("���� ����\n");
	}
}

void fun_Delete()
{
	printf("[ID ���� �Է�] \n\n");
	char id[20];

	printf("���̵� : ");	gets_s(id, sizeof(id));

	//������ ������ ��Ŷ�� ����
	LOGIN packet = pack_Delete(id);

	//������ ����
	if (wbnet_SenData((const char*)&packet, sizeof(packet)) == -1)
	{
		printf("�۽� ����\n");
	}
	else
	{
		printf("���� ����\n");
	}
}

void fun_Logout()
{
	printf("[�α׾ƿ� �� ID �Է�] \n\n");
	char id[20];
	printf("���̵� : ");	gets_s(id, sizeof(id));

	LOGIN packet = pack_Logout(id);

	//������ ����
	if (wbnet_SenData((const char*)&packet, sizeof(packet)) == -1)
	{
		printf("���� ����\n");
	}
	else
	{
		printf("���� ����\n");
	}
}

void Run()
{
	
	char buf[512];

	while (true)
	{
		//�޴�
		printf("------------------------------------------------------------------\n");
		printf("[0] ���α׷� ����[1] ȸ�� ���� [2] �α��� [3] ���� [4] �α׾ƿ�\n");
		printf("------------------------------------------------------------------\n");
		//����
		switch (_getch()) // #include<conio.h>
		{
		case '0':	return;
		case '1':	fun_NewMenber();  break;
		case '2': fun_Login(); break;
		case '3': fun_Delete(); break;
		case '4': fun_Logout(); break;
		}
		//�б�
		Sleep(1000);
	}
}

int main(void)
{
	//-----------------�ʱ�ȭ----------------------
	if (wbnet_LibraryInit() == 0)
	{
		printf("���̺귯�� �ʱ�ȭ ����\n");
		return 0;
	}

	if (wbnet_CreateSocket() == 0)
	{
		printf("���� ���� ����\n");
		return 0;
	}
	//-----------------����---------------------
	if (Wbnet_Run(SERVER_PORT, SERVER_IP) == 0)
	{
		printf("���� ���� ����");
		return 0;
	}

	Run();

	//------------------����------------------------
	wbnet_DeleteSocket();

	wbnet_Libraryexit();

	return 0;
}