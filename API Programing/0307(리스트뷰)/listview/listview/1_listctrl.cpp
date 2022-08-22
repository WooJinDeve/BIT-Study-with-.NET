//skeleton �ڵ�
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
		COL.pszText = (LPWSTR)TEXT("�̸�");			// ù ��° ���
		COL.iSubItem = 0;
		SendMessage(hList, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

		COL.pszText = (LPWSTR)TEXT("��ȭ��ȣ");				// �� ��° ���
		COL.iSubItem = 1;
		SendMessage(hList, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

		COL.cx = 300;
		COL.pszText = (LPWSTR)TEXT("�ּ�");					// �� ��° ���
		COL.iSubItem = 2;
		SendMessage(hList, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);

		// �ؽ�Ʈ�� �̹����� ���� �����۵��� ����Ѵ�.
		LI.mask = LVIF_TEXT | LVIF_IMAGE;

		LI.iImage = 0;
		LI.iSubItem = 0;
		LI.iItem = 0;
		LI.pszText = (LPWSTR)TEXT("�ڹ̿�"); ;			// ù ��° ������
		SendMessage(hList, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iImage = -1;
		LI.iSubItem = 1;
		LI.pszText =  (LPWSTR)TEXT("123-4567");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText = (LPWSTR)TEXT("����� ������");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iImage = 0;
		LI.iItem = 1;
		LI.iSubItem = 0;
		LI.pszText = (LPWSTR)TEXT("������");		// �� ��° ������
		SendMessage(hList, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iImage = -1;
		LI.iSubItem = 1;
		LI.pszText =  (LPWSTR)TEXT("543-9876");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText =  (LPWSTR)TEXT("�λ�� ��ŵ�");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iImage = 1;
		LI.iItem = 2;
		LI.iSubItem = 0;
		LI.pszText = (LPWSTR)TEXT("�����");		// �� ��° ������
		SendMessage(hList, LVM_INSERTITEM, 0, (LPARAM)&LI);

		LI.iImage = -1;
		LI.iSubItem = 1;
		LI.pszText =  (LPWSTR)TEXT("101-0920");
		SendMessage(hList, LVM_SETITEM, 0, (LPARAM)&LI);

		LI.iSubItem = 2;
		LI.pszText =  (LPWSTR)TEXT("�λ�� ������");
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
	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("ListCtrl");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("ListCtrl"), TEXT("ListCtrl"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}