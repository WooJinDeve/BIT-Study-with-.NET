#include "std.h";

const TCHAR* shape_TypeToString(int type)
{
	if (type == 1)
		return _T("�簢��");
	else if (type == 2)
		return _T("Ÿ��");
	else if (type == 3)
		return _T("�ﰢ��");
	else return _T("-");
}
void shape_Triangle(HDC hdc, POINTS pt)
{
	//POINT points[] = { {100,100}, {125, 150}, {75,150 }};
	POINT points[] = { { pt.x, pt.y}, { pt.x + 25, pt.y + 50 },	{ pt.x - 25 , pt.y + 50} };

	Polygon(hdc, points, 3);
}
