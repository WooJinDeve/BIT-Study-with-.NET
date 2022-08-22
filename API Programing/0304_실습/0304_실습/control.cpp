#include"std.h"

extern vector<SHAPE> g_shapes;
extern SHAPE g_curshape;

//���� ���� �ʱ�ȭ
void con_Init()
{
	g_curshape.color = RGB(255, 0, 0);
	g_curshape.pt.x = 0;
	g_curshape.pt.y = 0;
	g_curshape.type = 1;
	g_curshape.width = 1;
}

//�»�ܿ� ���� ���� ���
void con_InfoPrint(HDC hdc)
{
	TCHAR buf[50];
	wsprintf(buf, TEXT("Ÿ�� : %s"), shape_TypeToString(g_curshape.type));
	TextOut(hdc, 10, 10, buf, _tcslen(buf));

	wsprintf(buf, TEXT("�� : %d"), g_curshape.width);
	TextOut(hdc, 10, 30, buf, _tcslen(buf));

	wsprintf(buf, TEXT("�귯�� : RGB(%d,%d,%d)"), GetRValue(g_curshape.color), GetGValue(g_curshape.color), GetBValue(g_curshape.color));
	TextOut(hdc, 10, 50, buf, _tcslen(buf));

	wsprintf(buf, TEXT("��ǥ : (%d %d)"), g_curshape.pt.x, g_curshape.pt.y);
	TextOut(hdc, 10, 70, buf, _tcslen(buf));
}

//���� ���� ����(Ÿ��, �β�, ����)
void con_CurUpdate(HWND hwnd, WPARAM wParam) {
	switch (wParam)
	{
	//Ÿ�� ����
	case 'Q':	g_curshape.type = 1; break;
	case 'W':	g_curshape.type = 2; break;
	case 'E':	g_curshape.type = 3; break;
	//�β�
	case '1':	g_curshape.width = 1; break;
	case '3':	g_curshape.width = 3; break;
	case '5':	g_curshape.width = 5; break;
	//����(RGB)
	case 'R':	g_curshape.color = RGB(255, 0, 0); break;
	case 'G':	g_curshape.color = RGB(0, 255, 0); break;
	case 'B':	g_curshape.color = RGB(0, 0, 255); break;
	}
	InfoUpdate(hwnd, FALSE);
}
//���� ���� ����(��ǥ)
void con_CurPointUpdate(HWND hwnd, POINTS pt)
{
	g_curshape.pt = pt;
	InfoUpdate(hwnd, TRUE);
}
//���� ���� ����(��ȿȭ ���� �߻�)
void InfoUpdate(HWND hwnd, BOOL b)
{
	RECT rc = { 0,0,200,100 };
	InvalidateRect(hwnd, &rc, b);
}
//Ÿ��Ʋ�� ���ڿ� ����(����� ������ ����)
void TitleUpdate(HWND hwnd)
{
	TCHAR buf[100];
	wsprintf(buf, _T("����� ������ ����(%d)"), g_shapes.size());
	SetWindowText(hwnd, buf);
}

//���� ����
void con_ShapeInsert(HWND hwnd) {
	g_shapes.push_back(g_curshape);

	TitleUpdate(hwnd);

	InvalidateRect(hwnd, 0, FALSE);
}

//���� ����
void con_shapedelete(HWND hwnd)
{
	if (g_shapes.size() <= 0)
		return;

	g_shapes.pop_back();
	TitleUpdate(hwnd);
	InvalidateRect(hwnd, 0, FALSE);
}

//���� ���
void con_ShapePrint(HDC hdc) {

	for (int i = 0; i < (int)g_shapes.size(); i++)
	{
		SHAPE sh = g_shapes[i];

		HPEN pen = CreatePen(PS_SOLID, sh.width, RGB(0, 0, 0));
		HBRUSH br = CreateSolidBrush(sh.color);

		HPEN oldpen = (HPEN)SelectObject(hdc, pen);
		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);

		switch (sh.type)
		{
		case 1: Rectangle(hdc, sh.pt.x, sh.pt.y, sh.pt.x + 50, sh.pt.y + 50);  break;
		case 2: Ellipse(hdc, sh.pt.x, sh.pt.y, sh.pt.x + 50, sh.pt.y + 50);  break;
		case 3: shape_Triangle(hdc, sh.pt);  break;
		}

		DeleteObject(SelectObject(hdc, oldpen));
		DeleteObject(SelectObject(hdc, oldbr));
	}
}


