#pragma once

typedef struct tagSHAPE
{
	int			type;
	int			width;
	COLORREF	color;
	POINTS		pt;

}SHAPE, *PSHAPE;

const TCHAR* shape_TypeToString(int type);

void shape_Triangle(HDC hdc, POINTS pt);