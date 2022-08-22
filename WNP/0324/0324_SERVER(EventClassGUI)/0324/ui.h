//ui.h
#pragma once

class UI 
{
private:
	HWND hDlg;
	HWND hEditView;
	HWND hListView;

public:
	UI(HWND h);
	~UI();

public:
	void GetControlHandle();
	void ViewLogPrint(const TCHAR* msg);
	void MessagePrint(TCHAR* msg);
};
