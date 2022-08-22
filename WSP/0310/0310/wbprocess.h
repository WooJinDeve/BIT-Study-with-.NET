#pragma once

#include<windows.h>
#include"wbprocess.h"

void wb_CreateProcess(HWND hDlg);
void wb_ExitProcess(HWND hDlg);
void wb_GetExitCodeProcess(HWND hDlg);
void wb_HwndToProcessHandle(HWND hDlg);