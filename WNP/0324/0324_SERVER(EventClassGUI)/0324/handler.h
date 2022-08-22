#pragma once

#define SERVER_PORT 9000

class Handler
{
private:
	HWND hDlg;
	WbServer* pserver;
	UI* pui;

public:
	Handler(HWND Handler);
	~Handler();

public:
	BOOL OnInitDialog(WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(WPARAM wParam, LPARAM lParam);

	void LogFuntion(int flag);
	void RecvMessage(TCHAR* msg);

private:
	void OnExit();
	void OnServerStart();
	void OnServerStop();
};