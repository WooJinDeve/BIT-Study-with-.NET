//control.h

#pragma once

int con_Init();
void con_Run();
void con_Exit();

//���� �Լ�
bool isIdUniqCheck(int id);
void PrintAllAccount();
int  AccidToIdx(int id);

//������ ���� �Լ�
void con_RecvData(char* msg);

//������ ���ſ� ���� ó�� �Լ�
bool OnNewAccount(NEWACCOUNT* msg);
bool OnSelectAccount(SELEALLACCOUNT* msg);
bool OnInputMoney(IOMONEY* msg);
bool OnOutputMoney(IOMONEY* msg);
bool OnSelectAccount(NEWACCOUNT* msg);