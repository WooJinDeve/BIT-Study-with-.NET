//container.h

#pragma once

class MainDlg;

class Container
{
//	vector<>
	MainDlg* pDlg;

public:
	Container(MainDlg* p);
	~Container();

public:
	bool ShortMessage(ShortMessagePack* msg);
	bool NewMemberMessage(NEWMEMBER* msg);
	bool LoginMessage(LOGIN* msg);

private:
	bool isIdUniqCheck(const char* id);
};

