//packet.cpp

#include "std.h"

void pack_NewAccount(NEWACCOUNT* msg, bool b)
{
	if( b == true)
		msg->flag = PACK_NEWACCOUNT_S;
	else
		msg->flag = PACK_NEWACCOUNT_F;
}

void pack_SelectAccount(SELEALLACCOUNT* msg, bool b)
{
	if (b == true)
		msg->flag = PACK_SELEALLACCOUNT_S;
	else
		msg->flag = PACK_SELEALLACCOUNT_S;
}

void pack_InputMoney(IOMONEY* msg, bool b)
{
	if (b == true)
		msg->flag = PACK_INPUTMONEY_S;
	else
		msg->flag = PACK_INPUTMONEY_F;
}

void pack_OutputMoney(IOMONEY* msg, bool b)
{
	if (b == true)
		msg->flag = PACK_OUTPUTMONEY_S;
	else
		msg->flag = PACK_OUTPUTMONEY_F;
}

void pack_SelectAccount(NEWACCOUNT* msg, bool b)
{
	if (b == true)
		msg->flag = PACK_SELECTACCOUNT_S;
	else
		msg->flag = PACK_SELECTACCOUNT_F;
}