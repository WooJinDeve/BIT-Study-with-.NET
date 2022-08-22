#pragma once

class ChatDlg
{
	HWND	  hdlg;			//자신의 윈도우 핸들
public:
	ChatDlg();
	~ChatDlg();

public:
	void Create(HWND hParentDlg);

private:
	static BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam);

	//handler
public:
	BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

	//사용자 정의 함수
public:
	void OnExit();
};

