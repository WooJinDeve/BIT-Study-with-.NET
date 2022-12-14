//packet.h
#pragma once

/*
* 계좌개설 요청
* [클] 패킷 생성 서버로 전송
* [서] 받은 패킷 vector에 저장 (계좌번호 중복 체크)
*
* 계좌번호 리스트 요청
* [클] 패킷에 자신의 이름을 담아서, 담겨올 계좌번호 저장 배열은 0으로 초기화  전송
* [서] 이름으로 검색해서 찾은 계좌번호를 0번째 인덱스에서부터 담아서 전송
*
* 입금 요청
* [클] 패킷에 계좌번호랑 입금액을 담아서 전송
* [서] 계좌번호를 검색해서 입금처리 (실패 1 : 없는 계좌번호,  실패 2 : 입금액이 0보다 작거나 같은경우)
*      실패했을 경우 : errcode 에 1 이나 2를 넣어서 전달'
*      성공시 변경된 잔액정보를 money에 저장
*
* 출금 요청
* [클] 패킷에 계좌번호랑 출금액을 담아서 전송
* [서] 계좌번호를 검색해서 출금처리 (실패 1 : 없는 계좌번호,  실패 2 : 출금액이 0보다 작거나 같은경우)
*                                  실패 3 : 잔액이 부족한 경우
*      실패했을 경우 : errcode 에 1 이나 2, 3를 넣어서 전달
*      성공시 변경된 잔액정보를 money에 저장
*
* 계좌 정보 요청
* [클] 패킷에 계좌번호만 담아서 전송
* [서] 계좌번호를 검색해서 계좌를 찾아 이름, 잔액, 개설일을 담아서 전송
*     (실패 : 없는 계좌번호)
*/

//클라이언트 --> 서버
#define PACK_NEWACCOUNT           1   //계좌개설 요청
#define PACK_SELEALLACCOUNT       2   //내 계좌번호 리스트 요청
#define PACK_INPUTMONEY           3   //계좌 입금 요청
#define PACK_OUTPUTMONEY          4   //계좌 출금 요청
#define PACK_SELECTACCOUNT        5   //계좌 정보 요청

//서버 --> 클라이언트
#define PACK_NEWACCOUNT_S    10    //계좌개설에 대한 응답패킷(성공)
#define PACK_NEWACCOUNT_F    11    //계좌개설에 대한 응답패킷(계좌번호가 중복되었을 때)

#define PACK_SELEALLACCOUNT_S       12   //내 계좌번호 리스트 요청에 대한 응답 패킷

#define PACK_INPUTMONEY_S        13   //내 계좌번호 리스트 요청에 대한 응답 패킷
#define PACK_INPUTMONEY_F        14   //내 계좌번호 리스트 요청에 대한 응답 패킷
#define PACK_OUTPUTMONEY_S       15   //내 계좌번호 리스트 요청에 대한 응답 패킷
#define PACK_OUTPUTMONEY_F       16   //내 계좌번호 리스트 요청에 대한 응답 패킷

#define PACK_SELECTACCOUNT_S       17   //계좌 정보 요청 요청에 대한 응답 패킷
#define PACK_SELECTACCOUNT_F       18   //계좌 정보 요청 요청에 대한 응답 패킷


//수신한 구조체 타입의 flag만 변경해서 전달!
void pack_NewAccountAck(char* msg, bool b);
void pack_SeleteAllAcountAck(char* msg, bool b);
void pack_InputMoneyAck(char* msg, bool b);
void pack_OutputMoneyAck(char* msg, bool b);
void pack_SeleteAccountAck(char* msg, bool b);