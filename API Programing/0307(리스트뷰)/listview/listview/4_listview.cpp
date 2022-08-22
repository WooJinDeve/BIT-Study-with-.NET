//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#pragma comment(lib,"Comctl32.lib")

#include "resource.h"
using namespace std;

vector<PEOPLE> people = {
	{_T("김상형"),_T("123-4567"),_T("서울시 강남구 논현동"),TRUE},
	{_T("이송우"),_T("543-9876"),_T("서울시 구의동"),TRUE},
	{_T("박다희"),_T("111-3333"),_T("경기도 광명시"),FALSE},
	{_T("오궁섭"),_T("236-1818"),_T("서울시 강남구 반포동"),TRUE},
	{_T("조기순"),_T("358-2277"),_T("서울시 압구정동"),FALSE},
	{_T("오뱅훈"),_T("548-1109"),_T("서울시 신사동"),TRUE},
};

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HWND hList;
	LVCOLUMN COL;
	LVITEM LI;
	switch (msg)
	{

	case WM_CREATE:
	{
		InitCommonControls();

		hList = CreateWindow(WC_LISTVIEW, NULL, WS_CHILD | WS_VISIBLE | WS_BORDER |
			LVS_REPORT | LVS_SHOWSELALWAYS, 10, 10, 600, 200, hwnd, NULL, GetModuleHandle(0), NULL);
		HIMAGELIST hImgSm = ImageList_LoadBitmap(GetModuleHandle(0), MAKEINTRESOURCE(IDB_BITMAP1),
			16, 1, RGB(255, 255, 255));
		HIMAGELIST hlmgLa = ImageList_LoadBitmap(GetModuleHandle(0), MAKEINTRESOURCE(IDB_BITMAP2),
			32, 1, RGB(255, 255, 255));
		SendMessage(hList, LVM_SETIMAGELIST, (WPARAM)LVSIL_SMALL, (LPARAM)hImgSm);
		SendMessage(hList, LVM_SETIMAGELIST, (WPARAM)LVSIL_NORMAL, (LPARAM)hlmgLa);
		//--------------------------------------------------------------------------------------
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
		for (int i = 0; i < (int)people.size(); i++) {
			PEOPLE p1 = people[i];
			LI.mask = LVIF_TEXT | LVIF_IMAGE;
			LI.iItem = i;
			LI.iSubItem = 0;
			LI.pszText = p1.name;
			LI.iImage = (p1.male ? TRUE : FALSE);
			ListView_InsertItem(hList, &LI);

			ListView_SetItemText(hList, i, 1, p1.tel);
			ListView_SetItemText(hList, i, 2, p1.addr);
		}
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
	wc.lpszClassName = TEXT("MyListCtrl");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	//HMENU hmenu = LoadMenu(hInst, MAKEINTRESOURCE(IDR_MENU1));

	HWND hwnd = CreateWindowEx(0,
		TEXT("MyListCtrl"), TEXT("MyListCtrl"), WS_OVERLAPPEDWINDOW,
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