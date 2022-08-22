#pragma once

class MainDlg
{
	HINSTANCE hInst;		//Main Instance
	HWND	  hdlg;			//자신의 윈도우 핸들(OnInitDialog에서 대입연산)

	//UI컨트롤들...(HANDLE을 얻는 위치 : OnIniDialog)
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

	//사용자 정의 함수
public:
	void OnExit();
	void OnLogin();
	void OnNewMember();
};

