//packet.h

#pragma once

#define PACK_SHORTMESSAGE	1
#define PACK_NEWMEMBER      2
#define PACK_LOGIN          3


#define SHORTMESSAGE_S			10
#define SHORTMESSAGE_F			11
#define PACK_NEWMEMBER_S        12   
#define PACK_NEWMEMBER_F        13   
#define PACK_LOGIN_S            14   
#define PACK_LOGIN_F            15   


struct ShortMessagePack
{
	int flag;
	TCHAR nickname[20];
	TCHAR msg[100];
};
struct NEWMEMBER
{
	int flag;
	TCHAR id[20];
	TCHAR pw[30];
	TCHAR nickname[20];
};

struct LOGIN
{
	int flag;
	TCHAR id[10];
	TCHAR pw[10];
};

class Packet
{
public:
	//Client --> Server
	static void ShortMessageAck(ShortMessagePack*pack, bool b);
	static void NewMemberMessageAck(NEWMEMBER* pack, bool b);
	static void LoginMessageAck(LOGIN* pack, bool b);
		
	//Server --> Client	
};

