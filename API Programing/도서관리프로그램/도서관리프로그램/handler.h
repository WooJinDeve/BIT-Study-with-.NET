#pragma once


//å���� �ʱ�ȭ �ܰ�
BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnInsert(HWND hDlg);

BOOL OnApply(HWND hDlg, WPARAM wParam, LPARAM lParam);
void OnSelect(HWND hDld);

BOOL OnNotify(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnUpdate(HWND hDlg);

void OnDelete(HWND hDlg);