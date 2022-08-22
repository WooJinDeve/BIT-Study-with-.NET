//member.h

#pragma once

#ifdef DLL_SOURCE
#define DLLFUNC __declspec(dllexport) 
#else
#define DLLFUNC __declspec(dllimport)
#endif


#include <tchar.h>

struct Member
{
	TCHAR name[20];
	int   age;
};

extern "C" DLLFUNC Member CreateMember(TCHAR* name, int age);

extern "C" DLLFUNC void AddAge(Member* pmem);