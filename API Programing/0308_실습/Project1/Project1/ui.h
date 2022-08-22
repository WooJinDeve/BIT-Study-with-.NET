#pragma once

void ui_GetControlHandle(HWND hDlg);
void ui_ComboboxInitData(HWND hDlg);
//listview
void ui_ListViewCreateHeader(HWND hDlg);
void ui_ListViewPrintAll(HWND hDlg, vector<MEMBER> members);
void ui_DummyDataInput(HWND hDlg, vector<MEMBER>* members);
void ui_SelectListView(HWND hDlg, WPARAM wParam, LPARAM lParam);
//-------------------------------------------
void ui_SelectPrint(HWND hDlg, MEMBER mem);