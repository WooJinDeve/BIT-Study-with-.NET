//handler.h

#pragma once

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

BOOL OnFileRecvStart(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnFileRecvExit(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnFileRecving(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnConnect(HWND hDlg);
void OnGetFileName(HWND hDlg);
void OnFileSend(HWND hDlg);
