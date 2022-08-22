#include "std.h"

#define SERVER_PORT 9000


BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static Handler p(hDlg);

	switch (msg)
	{
	case WM_INITDIALOG:	p.OnInitDialog(wParam, lParam);	return TRUE;
	case WM_COMMAND:	p.OnCommand(wParam, lParam);	return TRUE;
	//case WM_ASYNC:		OnAsync(hDlg, wParam, lParam);		return TRUE;*/
	}
	return FALSE;
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	UINT ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_SERVER), // ���̾�α� ����
		0, // �θ� ������
		DlgProc); // Proc..

	return 0;
}



//int main(void)
//{
//	WbServer server;
//
//	if (server.CreateListenSocket(SERVER_PORT) == false)
//		return 0;
//	
//	HANDLE hthread = server.Run();
//
//	WaitForSingleObject(hthread, INFINITE);
//
//	printf("���α׷� ����\n");
//	return 0;
//}