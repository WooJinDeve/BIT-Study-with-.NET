//ui.h
#pragma once


//---- OnInitDialog --------------------------------
void ui_GetHandle(HWND hDlg);
void ui_InitComboBox(HWND hDlg);

//----OnLogin------------------------------------------
void ui_GetLoginData(HWND hDlg, TCHAR*name, int *pgroupid);

//----OnLogOut------------------------------------------
void ui_GetLogOutData(HWND hDlg, int* pgroupid);

//-----OnLogin & OnLogOut ------------------------------
void ui_SetTitle(HWND hDlg, int groupid, bool b);

//-----OnSendData---------------------------------------
void ui_GetSendData(HWND hDlg, TCHAR* name, int* pgroupid, TCHAR* msg);
void ui_GetSendMessage(HWND hDlg, TCHAR* name, TCHAR* msg, TCHAR* packet);

//------OnRecvData---------------------------------------
void ui_PrintData(TCHAR* msg);
