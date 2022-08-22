#pragma once

class WbClient; //��������(������ Ÿ���� ���·λ��� ���� ���������� �����ϴ�.)

class MainDlg
{
	//����
private:
	WbClient* pwb;

private:
	HINSTANCE hInst;		//Main Instance
	HWND	  hdlg;			//�ڽ��� ������ �ڵ�(OnInitDialog���� ���Կ���)

	//�ڽ� ��Ʈ�� �ڵ�(HANDLE�� ��� ��ġ : OnIniDialog)
private:
	HWND hEditView;
	HWND hEditSend;
	HWND hEditID;
	HWND hEditPW;
	HWND hBtnLogin;
	HWND hBtnSend;
	HWND tBtnMember;

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

	//����� ���� �Լ�(handler)
public:
	void OnExit();
	void OnConnect();
	void OnDisConnect();
	void OnSend();
	void OnLogin();
	void OnNewMember();

public:
	void RecvMessage(TCHAR* msg);

	//UI���� �Լ�
public:
	void ui_GetControlHandle();
	void ui_ViewPrint(const TCHAR* msg);
	void ui_GetSendData(TCHAR* msg, int size);
	void ui_GetLoginData(TCHAR* ID, TCHAR* PW, int size);
	void ui_GetIDData(TCHAR* ID, int size);
};

