//start.cpp

#include "std.h"

//WNDPROC OldProc;
//TCHAR msg[1024];
//SOCKET sClient;
//SOCKADDR_IN m_addr;
//IN_ADDR m_in;
//HWND hParent;


BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_INITDIALOG:	OnInitDialog(hDlg, wParam, lParam);	return TRUE;
	case WM_COMMAND:	OnCommand(hDlg, wParam, lParam);	return TRUE;
	case WM_ASYNC:		OnAsync(hDlg, wParam, lParam);		return TRUE;
	}
	return FALSE;
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	UINT ret = DialogBox(hInst,// instance
		MAKEINTRESOURCE(IDD_CLIENT), // 다이얼로그 선택
		0, // 부모 윈도우
		DlgProc); // Proc..

	return 0;
}


/*
		break;
	case WM_ASYNC:
		hListBox = GetDlgItem(hWnd, IDC_CLIENT);
		switch((WORD)(lParam))
		{

		case FD_CLOSE:
			closesocket(sClient);
			sClient = INVALID_SOCKET;
			wsprintf(msgInfo, TEXT("서버와 연결이 끊어졌습니다."));
			SendMessage(hListBox, LB_INSERTSTRING, 0 , (LPARAM)msgInfo);
			WSACleanup();
			break;

		case FD_READ:
			recv_len = recv(sClient,(char*)msg,sizeof(msg),0);
			if( recv_len == 0)
				return FALSE;
			SendMessage(hListBox,LB_INSERTSTRING,0,(LPARAM)msg);

			break;
		
*/