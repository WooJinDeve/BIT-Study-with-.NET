#pragma once

class UI
{
private:
	HWND hDlg;
	HWND hEditView;
	HWND hEditSend;

public:
	UI(HWND handler);
	~UI();
public:
	void GetControlHandle();
	void ViewPrint(const TCHAR* msg);
	void GetSendData(TCHAR* msg, int size);

};


