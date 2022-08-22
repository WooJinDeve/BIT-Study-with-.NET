#include "std.h"

void pack_NewAccountAck(char* msg, bool b)
{
	NEWACCOUNT* p = (NEWACCOUNT*)msg;
	if (b == true)
		p->flag = PACK_NEWACCOUNT_S;
	else
		p->flag = PACK_NEWACCOUNT_F;
}

void pack_SeleteAllAcountAck(char* msg, bool b)
{
	SELEALLACCOUNT* p = (SELEALLACCOUNT*)msg;
	p->flag = PACK_SELEALLACCOUNT_S;
}

void pack_InputMoneyAck(char* msg, bool b)
{
	IOMONEY* p = (IOMONEY*)msg;
	if (b == true)
		p->flag = PACK_INPUTMONEY_S;
	else
		p->flag = PACK_INPUTMONEY_F;
}

void pack_OutputMoneyAck(char* msg, bool b)
{
	IOMONEY* p = (IOMONEY*)msg;
	if (b == true)
		p->flag = PACK_OUTPUTMONEY_S;
	else
		p->flag = PACK_OUTPUTMONEY_S;
}

void pack_SeleteAccountAck(char* msg, bool b)
{
	NEWACCOUNT* p = (NEWACCOUNT*)msg;
	if (b == true)
		p->flag = PACK_SELECTACCOUNT_S;
	else
		p->flag = PACK_SELECTACCOUNT_S;
}