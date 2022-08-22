#pragma once

class MainDlg
{
	HINSTANCE hInst;		//Main Instance
	HWND	  hdlg;			//�ڽ��� ������ �ڵ�(OnInitDialog���� ���Կ���)

	//UI��Ʈ�ѵ�...(HANDLE�� ��� ��ġ : OnIniDialog)
public:
	MainDlg(HINSTANCE _hInst);
	~MainDlg();

public:
	void Create(); //���̾�α� ����!

public:
	static BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam);

	//handler
public:
	BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

	//����� ���� �Լ�
public:
	void OnExit();
	void OnLogin();
	void OnNewMember();
};

