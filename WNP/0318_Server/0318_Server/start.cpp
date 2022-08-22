#include "std.h"

#define SERVER_PORT 9000

vector<NEWACCOUNT> g_accounts;

bool isAccoutUniqCheck(int id)
{
	for (int i = 0; i < (int)g_accounts.size(); i++)
	{
		NEWACCOUNT mem = g_accounts[i];
		if (mem.id == id)
			return true;
	}
	return false;
}

bool isIdUniqCheckName(const char* name) {
	for (int i = 0; i < (int)g_accounts.size(); i++) {
		NEWACCOUNT mem = g_accounts[i];
		if (strcmp(mem.name, name) == 0)
			return true;
	}
	return false;
}


void fun_RecvData(char* msg)
{
	system("cls");
	int* p = (int*)msg;
	if (*p == PACK_NEWACCOUNT)
	{
		NEWACCOUNT* p = (NEWACCOUNT*)msg;
		if (isAccoutUniqCheck(p->id) == true)
		{
			//클라이언트에게 결과 전송하기 위한 데이터 설정
			pack_NewAccountAck(msg, false);
		}
		else
		{
			NEWACCOUNT mem;
			mem.flag = 0;
			mem.id = p->id;
			strcpy_s(mem.name, sizeof(mem.name), p->name);
			mem.balance = p->balance;
			mem.sdate = p->sdate;
			g_accounts.push_back(mem);

			//클라이언트에게 결과 전송하기 위한 데이터 설정
			pack_NewAccountAck(msg, true);
		}

	}
	else if (*p == PACK_SELEALLACCOUNT)
	{
		SELEALLACCOUNT* p = (SELEALLACCOUNT*)msg;

		int count = 0;

		for (int i = 0; (int)g_accounts.size(); i++) 
		{
			NEWACCOUNT mem = g_accounts[i];
			if (strcmp(mem.name, p->name) == 0)
			{
				p->accid[count] = mem.id;
				count++;
				pack_SeleteAllAcountAck(msg, true);
			}
		}
		if (isIdUniqCheckName(p->name) == false)
		{
			pack_SeleteAllAcountAck(msg, false);
		}
	}

	else if (*p == PACK_INPUTMONEY)
	{
		IOMONEY* p = (IOMONEY*)msg;
	
		for (int i = 0; i < (int)g_accounts.size(); i++)
		{
			NEWACCOUNT mem = g_accounts[i];
			if (p->id == mem.id)
			{
				if (p->money <= 0)
				{
					p->errcode = 2;
					pack_InputMoneyAck(msg, false);
					break;
				}
				else
				{
					g_accounts[i].balance += p->money;
					pack_InputMoneyAck(msg, true);
					break;
				}
			}
			else 
			{
				p->errcode = 1;
				pack_InputMoneyAck(msg, false);
			}
		}
	}

	else if (*p == PACK_OUTPUTMONEY)
	{
		IOMONEY* p = (IOMONEY*)msg;

		for (int i = 0; i < (int)g_accounts.size(); i++)
		{
			NEWACCOUNT mem = g_accounts[i];
			if (p->id == mem.id)
			{
				if (p->money <= 0)
				{
					p->errcode = 2;
					pack_OutputMoneyAck(msg, false);
					break;
				}
				if (mem.balance - p->money < 0)
				{
					p->errcode = 3;
					pack_OutputMoneyAck(msg, false);
					 break;
				}

				else
				{
					g_accounts[i].balance -= p->money;
					pack_OutputMoneyAck(msg, true);
					break;
				}
			}
			else 
			{
				p->errcode = 1;
				pack_OutputMoneyAck(msg, false);
			}
		}
	}

	else if (*p == PACK_SELECTACCOUNT)
	{
		NEWACCOUNT* p = (NEWACCOUNT*)msg;

		for (int i = 0; i < (int)g_accounts.size(); i++)
		{
			NEWACCOUNT acc = g_accounts[i];
			if (acc.id == p->id)
			{
				strcpy_s(p->name, sizeof(p->name), acc.name);
				p->balance = acc.balance;
				p->sdate = acc.sdate;

				pack_SeleteAccountAck(msg, true);
				break;
			}
			else
			{
				pack_SeleteAccountAck(msg, false);
			}
		}

	}
	// 전체 출력
	for (int i = 0; i < (int)g_accounts.size(); i++)
	{
		NEWACCOUNT mem = g_accounts[i];

		printf("[%d] %d\t %s\t %d %d:%d:%d\n", mem.flag, mem.id, mem.name, mem.balance, mem.sdate.wYear, mem.sdate.wMonth, mem.sdate.wDay);
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