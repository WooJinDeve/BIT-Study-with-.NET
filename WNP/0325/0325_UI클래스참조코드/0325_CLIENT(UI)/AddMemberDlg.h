#pragma once

class AddMemberDlg
{
	HWND	  hdlg;			//�ڽ��� ������ �ڵ�
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

	//����� ���� �Լ�
public:
	void OnExit();

	//UIó�� �Լ���..

};

