#include"std.h"

extern vector<SHAPE> g_shapes;
extern SHAPE g_curshape;

//설정 정보 초기화
void con_Init()
{
	g_curshape.color = RGB(255, 0, 0);
	g_curshape.pt.x = 0;
	g_curshape.pt.y = 0;
	g_curshape.type = 1;
	g_curshape.width = 1;
}

//좌상단에 설정 정보 출력
void con_InfoPrint(HDC hdc)
{
	TCHAR buf[50];
	wsprintf(buf, TEXT("타입 : %s"), shape_TypeToString(g_curshape.type));
	TextOut(hdc, 10, 10, buf, _tcslen(buf));

	wsprintf(buf, TEXT("펜 : %d"), g_curshape.width);
	TextOut(hdc, 10, 30, buf, _tcslen(buf));

	wsprintf(buf, TEXT("브러쉬 : RGB(%d,%d,%d)"), GetRValue(g_curshape.color), GetGValue(g_curshape.color), GetBValue(g_curshape.color));
	TextOut(hdc, 10, 50, buf, _tcslen(buf));

	wsprintf(buf, TEXT("좌표 : (%d %d)"), g_curshape.pt.x, g_curshape.pt.y);
	TextOut(hdc, 10, 70, buf, _tcslen(buf));
}

//설정 정보 변경(타입, 두께, 색상)
void con_CurUpdate(HWND hwnd, WPARAM wParam) {
	switch (wParam)
	{
	//타입 변경
	case 'Q':	g_curshape.type = 1; break;
	case 'W':	g_curshape.type = 2; break;
	case 'E':	g_curshape.type = 3; break;
	//두께
	case '1':	g_curshape.width = 1; break;
	case '3':	g_curshape.width = 3; break;
	case '5':	g_curshape.width = 5; break;
	//색상(RGB)
	case 'R':	g_curshape.color = RGB(255, 0, 0); break;
	case 'G':	g_curshape.color = RGB(0, 255, 0); break;
	case 'B':	g_curshape.color = RGB(0, 0, 255); break;
	}
	InfoUpdate(hwnd, FALSE);
}
//설정 정보 변경(좌표)
void con_CurPointUpdate(HWND hwnd, POINTS pt)
{
	g_curshape.pt = pt;
	InfoUpdate(hwnd, TRUE);
}
//설정 정보 갱신(무효화 영역 발생)
void InfoUpdate(HWND hwnd, BOOL b)
{
	RECT rc = { 0,0,200,100 };
	InvalidateRect(hwnd, &rc, b);
}
//타이틀바 문자열 변경(저장된 도형의 개수)
void TitleUpdate(HWND hwnd)
{
	TCHAR buf[100];
	wsprintf(buf, _T("저장된 도형의 개수(%d)"), g_shapes.size());
	SetWindowText(hwnd, buf);
}

//도형 저장
void con_ShapeInsert(HWND hwnd) {
	g_shapes.push_back(g_curshape);

	TitleUpdate(hwnd);

	InvalidateRect(hwnd, 0, FALSE);
}

//도형 삭제
void con_shapedelete(HWND hwnd)
{
	if (g_shapes.size() <= 0)
		return;

	g_shapes.pop_back();
	TitleUpdate(hwnd);
	InvalidateRect(hwnd, 0, FALSE);
}

//도형 출력
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


