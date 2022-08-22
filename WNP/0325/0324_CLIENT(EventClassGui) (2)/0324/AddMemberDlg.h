#pragma once


class AddMemberDlg
{

private:
	HWND	  hdlg;			//�ڽ��� ������ �ڵ�
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

	//����� ���� �Լ�
public:
	void OnExit();
	void OnNewMember();

private:
	void GetNewMemberData();
};

