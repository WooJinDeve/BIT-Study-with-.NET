//control.h

#pragma once

int con_Init();
void con_Run();
void con_Exit();

//내부 함수
bool isIdUniqCheck(int id);
void PrintAllAccount();
int  AccidToIdx(int id);

//데이터 수신 함수
void con_RecvData(char* msg);

//데이터 수신에 따른 처리 함수
bool OnNewAccount(NEWACCOUNT* msg);
bool OnSelectAccount(SELEALLACCOUNT* msg);
bool OnInputMoney(IOMONEY* msg);
bool OnOutputMoney(IOMONEY* msg);
bool OnSelectAccount(NEWACCOUNT* msg);