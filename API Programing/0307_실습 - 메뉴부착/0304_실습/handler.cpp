#include"std.h"

LRESULT OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	con_Init();

	return 0;
}

LRESULT OnDestroy(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	PostQuitMessage(0);
	return 0;
}

LRESULT OnPaint(HWND hwnd, WPARAM wParam, LPARAM lParam) {
	PAINTSTRUCT ps;
	HDC hdc = BeginPaint(hwnd, &ps);
	
	//Ãâ·Â
	con_InfoPrint(hdc);
	con_ShapePrint(hdc);

	EndPaint(hwnd, &ps);
	return 0;
}

LRESULT OnKeyDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	con_CurUpdate(hwnd ,wParam);
	return 0;
}


LRESULT OnLButtonDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	POINTS pt = MAKEPOINTS(lParam);

	if (pt.x < 200 && pt.y < 100)
		return 0;

	con_CurPointUpdate(hwnd, pt);
	if (wParam & MK_SHIFT)
		con_shapedelete(hwnd);
	else
		con_ShapeInsert(hwnd);
	return 0;
}

LRESULT OnCommand(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case ID_40001: SendMessage(hwnd, WM_CLOSE, 0, 0); break;
	case ID_40002: con_SelectMenuColor(hwnd, ID_40002); break;
	case ID_40003: con_SelectMenuColor(hwnd, ID_40003); break;
	case ID_40004: con_SelectMenuColor(hwnd, ID_40004); break;
	}
	return 0;
}