//container.cpp

#include "std.h"

vector<NEWMEMBER> g_members;

Container::Container(MainDlg* p)
{
	pDlg = p;
}

Container::~Container()
{

}

bool Container::isIdUniqCheck(const char* id)
{
	for (int i = 0; i < (int)g_members.size(); i++)
	{
		NEWMEMBER mem = g_members[i];
		if (strcmp((char*)mem.id, id) == 0)
			return true;
	}
	return false;
}

bool Container::ShortMessage(ShortMessagePack* msg)
{
	return true;
}

bool Container::NewMemberMessage(NEWMEMBER* msg)
{
	if (isIdUniqCheck((char*)msg->id) == true)
		return false;
	return true;
}

bool Container::LoginMessage(LOGIN* msg)
{
	for (int i = 0; i < (int)g_members.size(); i++)
	{
		NEWMEMBER mem = g_members[i];
		if (strcmp((char*)mem.id,(char*)msg->id) == 0 && strcmp((char*)mem.pw, (char*)msg->pw) == 0)
		{
			return true;
		}
	}
	return true;
}