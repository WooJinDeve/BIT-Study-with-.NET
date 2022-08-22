#pragma once

void con_Init();

void con_InfoPrint(HDC hdc);
void con_ShapePrint(HDC hdc);

void con_CurUpdate(HWND hwnd, WPARAM wParam);
void con_CurPointUpdate(HWND hwnd, POINTS pt);
void con_ShapeInsert(HWND hwnd);
void con_shapedelete(HWND hwnd);

void TitleUpdate(HWND hwnd);
void InfoUpdate(HWND hwnd, BOOL b);
