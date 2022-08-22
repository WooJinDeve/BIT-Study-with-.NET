#include "std.h"

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	MainDlg dlg(hInst);
	dlg.Create();

	return 0;
}