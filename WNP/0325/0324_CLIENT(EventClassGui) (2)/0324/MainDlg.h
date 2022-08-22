#pragma once

class WbClient; //전방참조(포인터 타입의 형태로사용될 때만 전방참조가 가능하다.)

class MainDlg
{
	//소유
private:
	WbClient* pwb;

private:
	HINSTANCE hInst;		//Main Instance
	HWND	  hdlg;			//자신의 윈도우 핸들(OnInitDialog에서 대입연산)

	//자식 컨트롤 핸들(HANDLE을 얻는 위치 : OnIniDialog)
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
	void Create(); //다이얼로그 생성!

public:
	static BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam);

	//handler
public:
	BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

	//사용자 정의 함수(handler)
public:
	void OnExit();
	void OnConnect();
	void OnDisConnect();
	void OnSend();
	void OnLogin();
	void OnNewMember();

public:
	void RecvMessage(TCHAR* msg);

	//UI관련 함수
public:
	void ui_GetControlHandle();
	void ui_ViewPrint(const TCHAR* msg);
	void ui_GetSendData(TCHAR* msg, int size);
	void ui_GetLoginData(TCHAR* ID, TCHAR* PW, int size);
	void ui_GetIDData(TCHAR* ID, int size);
};

