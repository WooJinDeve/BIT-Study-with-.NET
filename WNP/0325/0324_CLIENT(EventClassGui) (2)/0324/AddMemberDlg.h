#pragma once


class AddMemberDlg
{

private:
	HWND	  hdlg;			//자신의 윈도우 핸들
	TCHAR id[20];
	TCHAR pw[20];
	TCHAR nickname[20];
public:
	AddMemberDlg();
	~AddMemberDlg();

public:
	TCHAR* getId() { return id;  }
	TCHAR* getPw() { return pw;  }
	TCHAR* getNickName() { return nickname; }


public:
	bool Create(HWND hparentDlg);

private:
	static BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam);

	//handler
public:
	BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

	//사용자 정의 함수
public:
	void OnExit();
	void OnNewMember();

private:
	void GetNewMemberData();
};

