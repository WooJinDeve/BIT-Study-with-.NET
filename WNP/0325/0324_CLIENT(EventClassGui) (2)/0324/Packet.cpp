//packet.cpp

#include "std.h"

ShortMessagePack Packet::ShortMessage(TCHAR* nickname, TCHAR* msg)
{
	ShortMessagePack pack;
	ZeroMemory(&pack, sizeof(ShortMessagePack));

	pack.flag = PACK_SHORTMESSAGE;
	_tcscpy_s(pack.nickname, _countof(pack.nickname), nickname);
	_tcscpy_s(pack.msg, _countof(pack.msg), msg);

	return pack;
}

NEWMEMBER Packet::NewMember(TCHAR* id, TCHAR* pw, TCHAR* nickname)
{
	NEWMEMBER pack;

	pack.flag = PACK_NEWMEMBER;
	_tcscpy_s(pack.id, _countof(pack.id), id);
	_tcscpy_s(pack.pw, _countof(pack.pw), pw);
	_tcscpy_s(pack.nickname, _countof(pack.nickname), nickname);

	return pack;
}
LOGIN Packet::Login(TCHAR* id, TCHAR* pw)
{
	LOGIN pack;

	pack.flag = PACK_LOGIN;
	_tcscpy_s(pack.id, _countof(pack.id), id);
	_tcscpy_s(pack.pw, _countof(pack.pw), pw);

	return pack;
}