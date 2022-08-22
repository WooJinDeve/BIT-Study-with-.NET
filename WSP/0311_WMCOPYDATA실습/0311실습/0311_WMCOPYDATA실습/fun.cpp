//fun.cpp

#include "std.h"

BOOL fun_SendMessage(TCHAR* myname, TCHAR* username, TCHAR* msg)
{
	HWND hwnd = FindWindow(0, username);
	if (hwnd == 0)
	{
		MessageBox(NULL, _T("����"), _T("�˸�"), MB_OK);
		return false;
	}
	DATA data;
	_tcscpy_s(data.name, _countof(data.name), myname);
	_tcscpy_s(data.msg, _countof(data.msg), msg);
	GetLocalTime(&data.st);

	COPYDATASTRUCT cds;
	cds.cbData = sizeof(DATA);  // ������   data ũ��
	cds.dwData = 1;
	cds.lpData = &data;                    // ������   Pointer
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