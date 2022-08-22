//control.cpp

#include "std.h"

#define SERVER_PORT 9000

vector< Account > g_accounts;

int con_Init()
{
	if (wbnet_LibraryInit(con_RecvData) == 0)
	{
		printf("라이브러리 초기화 오류\n");
		return 0;
	}

	if (wbnet_CreateListenSocket(SERVER_PORT) == -1)
		return 0;

	return 1;
}

void con_Run()
{

	wbnet_ServerRun();
}

void con_Exit()
{
	wbnet_DeleteListenSocket();

	wbnet_LibraryExit();
}

void PrintAllAccount()
{
	system("cls");

	for (int i = 0; i < (int)g_accounts.size(); i++)
	{
		Account acc = g_accounts[i];

		printf("[%d] %s\t %d원\t %04d-%02d-%02d %02d:%02d:%02d\n",
			acc.id, acc.name, acc.balance,
			acc.stime.wYear, acc.stime.wMonth, acc.stime.wDay,
			acc.stime.wHour, acc.stime.wMinute, acc.stime.wSecond);
	}
}

//중복이 있으면 true반환
bool isIdUniqCheck(int id)
{
	for (int i = 0; i < (int)g_accounts.size(); i++)
	{
		Account acc = g_accounts[i];
		if (acc.id == id)
			return true;
	}
	return false;
}

//계좌번호로 인덱스 반환
int  AccidToIdx(int id)
{
	for (int i = 0; i < (int)g_accounts.size(); i++)
	{
		Account acc = g_accounts[i];
		if (acc.id == id)
			return i;
	}
	return -1;
}

void con_RecvData(char* msg)
{
	int* p = (int*)msg;
	if (*p == PACK_NEWACCOUNT)
	{
		bool b = OnNewAccount((NEWACCOUNT*)msg);		
		pack_NewAccount((NEWACCOUNT*)msg, b);
	}
	else if (*p == PACK_SELEALLACCOUNT)
	{
		bool b = OnSelectAccount((SELEALLACCOUNT*)msg);
		pack_SelectAccount((SELEALLACCOUNT*)msg, b);
	}
	else if (*p == PACK_INPUTMONEY)
	{
		bool b = OnInputMoney((IOMONEY*)msg);
		pack_InputMoney((IOMONEY*)msg, b);
	}
	else if (*p == PACK_OUTPUTMONEY)
	{
		bool b = OnOutputMoney((IOMONEY*)msg);
		pack_OutputMoney((IOMONEY*)msg, b);
	}
	else if (*p == PACK_SELECTACCOUNT)
	{
		bool b = OnSelectAccount((NEWACCOUNT*)msg);
		pack_SelectAccount((NEWACCOUNT*)msg, b);
	}

	PrintAllAccount();
}

bool OnNewAccount(NEWACCOUNT* msg)
{
	if (isIdUniqCheck(msg->id) == true)
	{
		return false;
	}
	else
	{
		Account acc;
		acc.id = msg->id;
		strcpy_s(acc.name, sizeof(acc.name), msg->name);
		acc.balance = msg->balance;
		GetLocalTime(&acc.stime);

		g_accounts.push_back(acc);
		return true;
	}
}

bool OnSelectAccount(SELEALLACCOUNT* msg)
{
	for (int i = 0, idx = 0; i < (int)g_accounts.size(); i++)
	{
		Account acc = g_accounts[i];
		if ( strcmp(acc.name , msg->name) ==0)
		{
			msg->accid[idx++] = acc.id;
		}
	}
	return 1;
}

//(실패 1 : 없는 계좌번호,  실패 2 : 입금액이 0보다 작거나 같은경우)
bool OnInputMoney(IOMONEY* msg)
{
	int idx = AccidToIdx(msg->id);
	if (idx == -1)
	{
		msg->errcode = 1;
		return false;
	}
	
	if (msg->money <= 0)
	{
		msg->errcode = 2;
		return false;;
	}

	g_accounts[idx].balance += msg->money;
	msg->money = g_accounts[idx].balance;
	return true;
}

//(실패 1 : 없는 계좌번호,  실패 2 : 출금액이 0보다 작거나 같은경우 실패 3 : 잔액이 부족한 경우
bool OnOutputMoney(IOMONEY* msg)
{
	int idx = AccidToIdx(msg->id);
	if (idx == -1)
	{
		msg->errcode = 1;
		return false;
	}

	if (msg->money <= 0)
	{
		msg->errcode = 2;
		return false;;
	}

	if (msg->money > g_accounts[idx].balance)
	{
		msg->errcode = 3;
		return false;
	}

	g_accounts[idx].balance -= msg->money;
	msg->money = g_accounts[idx].balance;
	return true;
}

bool OnSelectAccount(NEWACCOUNT* msg)
{
	int idx = AccidToIdx(msg->id);
	if(idx == -1)
		return false;

	Account acc = g_accounts[idx];	
	strcpy_s(msg->name, sizeof(msg->name), acc.name);
	msg->balance = acc.balance;
	msg->sdate = acc.stime;
	return true;
}

