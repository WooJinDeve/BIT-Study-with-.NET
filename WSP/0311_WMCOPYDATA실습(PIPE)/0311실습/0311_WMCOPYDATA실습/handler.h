//handler.h
#pragma once

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCopyData(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnSetTitle(HWND hDlg);
void OnSendMessage(HWND hDlg);
