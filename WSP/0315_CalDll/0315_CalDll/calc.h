#pragma once


#ifdef DLL_SOURCE
	#define DLLFUNC __declspec(dllexport)
#else
	#define DLLFUNC __declspec(dllimport)
#endif

extern "C" DLLFUNC float cal_add(int n1, int n2);
extern "C" DLLFUNC float cal_sub(int n1, int n2);
extern "C" DLLFUNC float cal_mul(int n1, int n2);
extern "C" DLLFUNC float cal_div(int n1, int n2);