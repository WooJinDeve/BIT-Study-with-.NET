//handler.h
#pragma once

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);
BOOL OnAsync(HWND hDlg, WPARAM wParam, LPARAM lParam);

//프로그램 종료
void OnExit(HWND hDlg);

//서버시작버튼
void OnServerStart(HWND hDlg);

//서버종료버튼
void OnServerStop(HWND hDlg);

//----------------------- 네트워크 이벤트 처리 ------------------------
//클라이언트 접속 처리
void OnAccept(HWND hDlg, WPARAM wParam, LPARAM lParam);

//데이터 수신 처리
void OnRead(HWND hDlg, WPARAM wParam, LPARAM lParam);

//소켓 종료
void OnClose(HWND hDlg, WPARAM wParam, LPARAM lParam);
