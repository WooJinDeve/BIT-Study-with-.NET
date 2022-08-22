#pragma once


BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnAsync(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnSendData(HWND hDlg);
void OnExit(HWND hDlg);

void OnConnet(HWND hDlg, WPARAM wParam, LPARAM lParam);
void OnRead(HWND hDlg, WPARAM wParam, LPARAM lParam);
void OnWrite(HWND hDlg, WPARAM wParam, LPARAM lParam);
void OnClose(HWND hDlg, WPARAM wParam, LPARAM lParam);
