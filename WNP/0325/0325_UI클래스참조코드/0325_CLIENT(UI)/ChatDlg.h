#pragma once

class ChatDlg
{
	HWND	  hdlg;			//�ڽ��� ������ �ڵ�
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

	//����� ���� �Լ�
public:
	void OnExit();
};

