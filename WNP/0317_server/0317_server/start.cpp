#include "std.h"

#define SERVER_PORT 9000

vector<NEWMEMBER> g_members; // flag(로그인 여부), id, pw, name;

bool isIdUniqCheck(const char* id)
{
	for (int i = 0; i < (int)g_members.size(); i++)
	{
		NEWMEMBER mem = g_members[i];
		if (strcmp(mem.id, id) == 0)
			return true;
	}
	return false;
}

void fun_RecvData(char* msg)
{
	system("cls");
	int* p = (int*)msg;
	if (*p == PACK_NEWMEMBER)
	{
		NEWMEMBER* p = (NEWMEMBER*)msg;
		if (isIdUniqCheck(p->id) == true)
		{
			//클라이언트에게 결과 전송하기 위한 데이터 설정
			pack_NewMemberAck(msg, false);
		}
		else
		{
			NEWMEMBER mem;
			mem.flag = 0;
			strcpy_s(mem.id, sizeof(mem.id), p->id);
			strcpy_s(mem.pw, sizeof(mem.pw), p->pw);
			strcpy_s(mem.name, sizeof(mem.name), p->name);
			g_members.push_back(mem);

			//클라이언트에게 결과 전송하기 위한 데이터 설정
			pack_NewMemberAck(msg, true);
		}
		
	}
	else if (*p == PACK_LOGIN)
	{
		LOGIN* p = (LOGIN*)msg;
		for (int i = 0; i < (int)g_members.size(); i++)
		{
			NEWMEMBER mem = g_members[i];
			if ( strcmp(mem.id,p->id) == 0 && strcmp(mem.pw, p->pw) == 0)
			{
				g_members[1].flag = 1;

				//클라이언트에게 결과 전송하기 위한 데이터 설정
				pack_LoginAck(msg, true);
				break;
			}
			else
			{
				pack_LoginAck(msg, true);
			}
		}
		
	}
	else if (*p == PACK_DELETEMEMBER)
	{
		LOGIN* p = (LOGIN*)msg;
		for (int i = 0; i < (int)g_members.size(); i++)
		{
			NEWMEMBER mem = g_members[i];
			if (strcmp(mem.id, p->id) == 0)
			{
				g_members.erase(g_members.begin() + i);
				pack_DeleteAck(msg,true);

				break;
			}
			else
			{
				pack_DeleteAck(msg, false);
			}
		}
	}

	else if (*p == PACK_LOGOUT)
	{
		LOGIN* p = (LOGIN*)msg;

		for (int i = 0; i < (int)g_members.size(); i++)
		{
			NEWMEMBER mem = g_members[i];
			if (strcmp(mem.id, p->id) == 0)
			{
				g_members[i].flag = 0;

				pack_LogoutAck(msg, true);
				break;
			}
			else
			{
				pack_LogoutAck(msg, false);
			}
		}
	}

	// 전체 출력
	for (int i = 0; i < (int)g_members.size(); i++)
	{
		NEWMEMBER mem = g_members[i];

		printf("[%d] %s\t %s\t %s\n", mem.flag, mem.id, mem.pw, mem.name);
	}


}

int main(void)
{
	//-----------------초기화----------------------
	if (wbnet_LibraryInit() == 0)
	{
		printf("라이브러리 초기화 오류\n");
		return 0;
	}

	if (wbnet_CreateListenSocket(SERVER_PORT) == -1)
		return 0;
	
	//------------------실행-------------------------
	wbnet_ServerRun();

	//------------------종료------------------------
	wbnet_DeleteListenSocket();
	wbnet_Libraryexit();


	
	return 0;
}