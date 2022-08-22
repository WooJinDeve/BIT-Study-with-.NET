//packet.h
#pragma once

/*
* ���°��� ��û
* [Ŭ] ��Ŷ ���� ������ ����
* [��] ���� ��Ŷ vector�� ���� (���¹�ȣ �ߺ� üũ)
*
* ���¹�ȣ ����Ʈ ��û
* [Ŭ] ��Ŷ�� �ڽ��� �̸��� ��Ƽ�, ��ܿ� ���¹�ȣ ���� �迭�� 0���� �ʱ�ȭ  ����
* [��] �̸����� �˻��ؼ� ã�� ���¹�ȣ�� 0��° �ε����������� ��Ƽ� ����
*
* �Ա� ��û
* [Ŭ] ��Ŷ�� ���¹�ȣ�� �Աݾ��� ��Ƽ� ����
* [��] ���¹�ȣ�� �˻��ؼ� �Ա�ó�� (���� 1 : ���� ���¹�ȣ,  ���� 2 : �Աݾ��� 0���� �۰ų� �������)
*      �������� ��� : errcode �� 1 �̳� 2�� �־ ����'
*      ������ ����� �ܾ������� money�� ����
*
* ��� ��û
* [Ŭ] ��Ŷ�� ���¹�ȣ�� ��ݾ��� ��Ƽ� ����
* [��] ���¹�ȣ�� �˻��ؼ� ���ó�� (���� 1 : ���� ���¹�ȣ,  ���� 2 : ��ݾ��� 0���� �۰ų� �������)
*                                  ���� 3 : �ܾ��� ������ ���
*      �������� ��� : errcode �� 1 �̳� 2, 3�� �־ ����
*      ������ ����� �ܾ������� money�� ����
*
* ���� ���� ��û
* [Ŭ] ��Ŷ�� ���¹�ȣ�� ��Ƽ� ����
* [��] ���¹�ȣ�� �˻��ؼ� ���¸� ã�� �̸�, �ܾ�, �������� ��Ƽ� ����
*     (���� : ���� ���¹�ȣ)
*/

//Ŭ���̾�Ʈ --> ����
#define PACK_NEWACCOUNT           1   //���°��� ��û
#define PACK_SELEALLACCOUNT       2   //�� ���¹�ȣ ����Ʈ ��û
#define PACK_INPUTMONEY           3   //���� �Ա� ��û
#define PACK_OUTPUTMONEY          4   //���� ��� ��û
#define PACK_SELECTACCOUNT        5   //���� ���� ��û

//���� --> Ŭ���̾�Ʈ
#define PACK_NEWACCOUNT_S    10    //���°����� ���� ������Ŷ(����)
#define PACK_NEWACCOUNT_F    11    //���°����� ���� ������Ŷ(���¹�ȣ�� �ߺ��Ǿ��� ��)

#define PACK_SELEALLACCOUNT_S       12   //�� ���¹�ȣ ����Ʈ ��û�� ���� ���� ��Ŷ

#define PACK_INPUTMONEY_S        13   //�� ���¹�ȣ ����Ʈ ��û�� ���� ���� ��Ŷ
#define PACK_INPUTMONEY_F        14   //�� ���¹�ȣ ����Ʈ ��û�� ���� ���� ��Ŷ
#define PACK_OUTPUTMONEY_S       15   //�� ���¹�ȣ ����Ʈ ��û�� ���� ���� ��Ŷ
#define PACK_OUTPUTMONEY_F       16   //�� ���¹�ȣ ����Ʈ ��û�� ���� ���� ��Ŷ

#define PACK_SELECTACCOUNT_S       17   //���� ���� ��û ��û�� ���� ���� ��Ŷ
#define PACK_SELECTACCOUNT_F       18   //���� ���� ��û ��û�� ���� ���� ��Ŷ


//������ ����ü Ÿ���� flag�� �����ؼ� ����!
void pack_NewAccountAck(char* msg, bool b);
void pack_SeleteAllAcountAck(char* msg, bool b);
void pack_InputMoneyAck(char* msg, bool b);
void pack_OutputMoneyAck(char* msg, bool b);
void pack_SeleteAccountAck(char* msg, bool b);