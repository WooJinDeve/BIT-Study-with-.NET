#pragma once

class AddMemberDlg
{
	HWND	  hdlg;			//자신의 윈도우 핸들
public:
	AddMemberDlg();
	~AddMemberDlg();

public:
	void Create(HWND hparentDlg);

private:
	static BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam);

	//handler
public:
	BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

	//사용자 정의 함수
public:
	void OnExit();

	//UI처리 함수들..

};

