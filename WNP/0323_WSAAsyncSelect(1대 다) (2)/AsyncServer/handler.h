//handler.h
#pragma once

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnAsync(HWND hDlg, WPARAM wParam, LPARAM lParam);

//���α׷� ����
void OnExit(HWND hDlg);

//�������۹�ư
void OnServerStart(HWND hDlg);

//���������ư
void OnServerStop(HWND hDlg);

//----------------------- ��Ʈ��ũ �̺�Ʈ ó�� ------------------------
//Ŭ���̾�Ʈ ���� ó��
void OnAccept(HWND hDlg, WPARAM wParam, LPARAM lParam);

//������ ���� ó��
void OnRead(HWND hDlg, WPARAM wParam, LPARAM lParam);

//���� ����
void OnClose(HWND hDlg, WPARAM wParam, LPARAM lParam);
