//control.h

#pragma once

int con_Init();
void con_Run();
void con_Exit();

//내부 함수
bool isIdUniqCheck(int id);
void PrintAllAccount();
int  AccidToIdx(int id);

//데이터 수신 함수(wbnet 호출) CALLBACK
//CALLBACK : 미리 함수를 등록 --> 특정 사건이 벌어졌을 때 호출됨.
void con_RecvData(char* msg);

//데이터 수신에 따른 처리 함수
bool OnNewAccount(NEWACCOUNT* msg);
bool OnSelectAccount(SELEALLACCOUNT* msg);
bool OnInputMoney(IOMONEY* msg);
bool OnOutputMoney(IOMONEY* msg);
bool OnSelectAccount(NEWACCOUNT* msg);