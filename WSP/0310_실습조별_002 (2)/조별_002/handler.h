#pragma once

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnNotify(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnEnumProcess(HWND hDlg);
void OnExecuteProcess(HWND hDlg);
void OnCheckState(HWND hDlg);
void OnExitProcess(HWND hDlg);