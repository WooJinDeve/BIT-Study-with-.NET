//fun.cpp

#include "std.h"

BOOL fun_SendMessage(TCHAR* myname, TCHAR* username, TCHAR* msg)
{
	HWND hwnd = FindWindow(0, username);
	if (hwnd == 0)
	{
		MessageBox(NULL, _T("없다"), _T("알림"), MB_OK);
		return false;
	}
	DATA data;
	_tcscpy_s(data.name, _countof(data.name), myname);
	_tcscpy_s(data.msg, _countof(data.msg), msg);
	GetLocalTime(&data.st);

	COPYDATASTRUCT cds;
	cds.cbData = sizeof(DATA);  // 전달한   data 크기
	cds.dwData = 1;
	cds.lpData = &data;                    // 전달할   Pointer
	SendMessage(hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
	return true;
}


void fun_GetCopyData(LPARAM lParam, DATA* ptemp)
{
	DATA* pdata;
	COPYDATASTRUCT* ps = (COPYDATASTRUCT*)lParam;
	pdata = (DATA*)ps->lpData;

	*ptemp = *pdata;
}