#pragma once
//�ڵ��� ������� �Լ�
void ui_GetControlHandle(HWND hDlg);

//����Ʈ�� 
void ui_ListViewCreateHeader(HWND hDlg); 
void ui_ListViewPrintAll(HWND hDlg, vector<BOOK> books); //����� å�� ���� ���

void ui_SelectPrint(HWND hDlg, BOOK book); //������ȣ�� �̿��� ���� �˻� �� Listview�� ���

void ui_DummyDataInput(HWND hDlg, vector<BOOK>* books); //å�� ������ �ӽ÷� �Է� (�����ص� �������)..

void ui_SelectListView(HWND hDlg, WPARAM wParam, LPARAM lParam);// WM_NOTIFY

void ui_update(HWND hDlg);

void ui_delete(HWND hDlg, vector<BOOK>* books);