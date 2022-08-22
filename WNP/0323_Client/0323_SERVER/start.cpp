//start.cpp


#include "std.h"

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_INITDIALOG:     OnInitDialog(hDlg, wParam, lParam); return TRUE;
    case WM_COMMAND:        OnCommand(hDlg, wParam, lParam); return TRUE;
    case WM_ASYNC:		    OnAsync(hDlg, wParam, lParam);		return TRUE;
    }
    return FALSE;
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
    UINT ret = DialogBox(hInst,// instance
        MAKEINTRESOURCE(IDD_DIALOG1), // ���̾�α� ����
        0, // �θ� ������
        DlgProc); // Proc..


    return 0;
}