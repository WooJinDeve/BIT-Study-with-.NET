//handler.h
#pragma once

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnExit(HWND hDlg);
void OnConnect(HWND hDlg);
void OnDisConnect(HWND hDlg);
void OnSend(HWND hDlg);

BOOL OnAsync(HWND hDlg, WPARAM wParam, LPARAM lParam);

void OnRead(HWND hDlg, WPARAM wParam, LPARAM lParam);
void OnClose(HWND hDlg, WPARAM wParam, LPARAM lParam);