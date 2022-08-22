#pragma once
//핸들을 얻기위한 함수
void ui_GetControlHandle(HWND hDlg);

//리스트뷰 
void ui_ListViewCreateHeader(HWND hDlg); 
void ui_ListViewPrintAll(HWND hDlg, vector<BOOK> books); //저장된 책의 정보 출력

void ui_SelectPrint(HWND hDlg, BOOK book); //고유번호를 이용해 정보 검색 후 Listview로 출력

void ui_DummyDataInput(HWND hDlg, vector<BOOK>* books); //책의 정보를 임시로 입력 (삭제해도 상관없음)..

void ui_SelectListView(HWND hDlg, WPARAM wParam, LPARAM lParam);// WM_NOTIFY

void ui_update(HWND hDlg);

void ui_delete(HWND hDlg, vector<BOOK>* books);