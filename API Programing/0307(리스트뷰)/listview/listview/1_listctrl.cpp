//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#pragma comment(lib,"Comctl32.lib")

#include <Windows.h>
#include <tchar.h>
#include <CommCtrl.h>
#include "resource.h"


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	LVCOLUMN COL;
	LVITEM LI;
	switch (msg)
	{
	case WM_CREATE:
	{
		InitCommonControls();

		HWND hList = CreateWindow(WC_LISTVIEW, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER |
			LVS_REPORT, 10, 10, 500, 200, hwnd, NULL, GetModuleHandle(0), NULL);

		HIMAGELIST hImgSm = ImageList_LoadBitmap(GetModuleHandle(0), MAKEINTRESOURCE(IDB_BITMAP1),
			16, 1, RGB(255, 255, 255));
		HIMAGELIST hlmgLa = ImageList_LoadBitmap(GetModuleHandle(0), MAKEINTRESOURCE(IDB_BITMAP2),
			32, 1, RGB(255, 255, 255));

		SendMessage(hList, LVM_SETIMAGELIST, (WPARAM)LVSIL_SMALL, (LPARAM)hImgSm);
		SendMessage(hList, LVM_SETIMAGELIST, (WPARAM)LVSIL_NORMAL, (LPARAM)hlmgLa);

		COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
		COL.fmt = LVCFMT_LEFT;
		COL.cx = 150;
		COL.pszText = (LPWSTR)TEXT("이름");			// 첫 번째 헤더
		COL.iSubItem = 0;
		SendMessage(hList, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

		COL.pszText = (LPWSTR)TEXT("전화번호");				// 두 번째 헤더
		COL.iSubItem = 1;
		SendMessage(hList, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

		COL.cx = 300;
		COL.pszText = (LPWSTR)TEXT("주소");					// 세 번째 헤더
		COL.iSubItem = 2;
		SendMessage(hList, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);

		// 텍스트와 이미지를 가진 아이템들을 등록한다.
		LI.mask = LVIF_TEXT | LVIF_IMAGE;

		LI.iImage = 0;
		LI.iSubItem = 0;
		LI.iItem = 0;
		LI.pszText = (LPWSTR)TEXT("박미영"); ;			// 첫 번째 아이템
		SendMessage(hList, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iImage = -1;
		LI.iSubItem = 1;
		LI.pszText =  (LPWSTR)TEXT("123-4567");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText = (LPWSTR)TEXT("서울시 논현동");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iImage = 0;
		LI.iItem = 1;
		LI.iSubItem = 0;
		LI.pszText = (LPWSTR)TEXT("권진숙");		// 두 번째 아이템
		SendMessage(hList, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iImage = -1;
		LI.iSubItem = 1;
		LI.pszText =  (LPWSTR)TEXT("543-9876");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText =  (LPWSTR)TEXT("부산시 대신동");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iImage = 1;
		LI.iItem = 2;
		LI.iSubItem = 0;
		LI.pszText = (LPWSTR)TEXT("허수진");		// 세 번째 아이템
		SendMessage(hList, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iImage = -1;
		LI.iSubItem = 1;
		LI.pszText =  (LPWSTR)TEXT("101-0920");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText =  (LPWSTR)TEXT("부산시 장전동");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);
		return 0;
	}
	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//윈도우 클래스 정의
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = TEXT("ListCtrl");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("ListCtrl"), TEXT("ListCtrl"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}