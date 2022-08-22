//packet.cpp

#include "std.h"

void Packet::ShortMessageAck(ShortMessagePack* pack, bool b)
{
	if (b == true)
	{
		pack->flag = SHORTMESSAGE_S;
	}
	else
	{
		pack->flag = SHORTMESSAGE_F;
	}
}


void Packet::NewMemberMessageAck(NEWMEMBER* pack, bool b)
{
	if (b == true)
	{
		pack->flag = PACK_NEWMEMBER_S;
	}
	else
	{
		pack->flag = PACK_NEWMEMBER_F;
	}
}

void Packet::LoginMessageAck(LOGIN* pack, bool b)
{
	if (b == true)
	{
		pack->flag = PACK_LOGIN_S;
	}
	else
	{
		pack->flag = PACK_LOGIN_F;
	}
}