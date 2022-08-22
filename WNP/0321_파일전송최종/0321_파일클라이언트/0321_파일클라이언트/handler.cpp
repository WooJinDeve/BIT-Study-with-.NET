//handler.cpp

#include "std.h"

#define SERVER_IP	TEXT("127.0.0.1")  //10.101.11.150
#define SERVER_PORT 5000

//������ ������ ����
FILE_INFO g_fi;
//������ ������ �ڵ�
HANDLE g_file;

HWND g_hDlg, g_hPrg;

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	g_hDlg = hDlg;
	g_hPrg  = GetDlgItem(hDlg, IDC_PROGRESS1);

	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL: EndDialog(hDlg, IDCANCEL);	return TRUE;	
	case IDC_BUTTON1:	OnGetFileName(hDlg);	return TRUE;
	case IDC_BUTTON2:	OnFileSend(hDlg);		return TRUE;
	case IDC_BUTTON3:	OnConnect(hDlg);		return TRUE;
	}
	return TRUE;
}

BOOL OnFileRecvStart(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	SetWindowText(hDlg, TEXT("���� ���� ����"));

	SendMessage(g_hPrg, PBM_SETRANGE32, 0, lParam);
	return TRUE;
}

BOOL OnFileRecving(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	SetWindowText(hDlg, TEXT("���� ���� ��"));

	SendMessage(g_hPrg, PBM_SETPOS, lParam, 0);

	TCHAR buf[20];
	wsprintf(buf, TEXT("%.1fbyte"), ((float)wParam / lParam) * 100.0f);
	SetDlgItemText(hDlg, IDC_STATIC1, buf);
	return TRUE;
}

BOOL OnFileRecvExit(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	if (lParam == 1)
		SetWindowText(hDlg, TEXT("���� ���� ����"));
	else
		SetWindowText(hDlg, TEXT("���� ���� ����"));
	return TRUE;
}


void OnConnect(HWND hDlg)
{
	if (wbfilenet_FileClient(SERVER_IP, SERVER_PORT) == true)
		SetWindowText(hDlg, TEXT("���� ���� ���� ����"));
	else
		SetWindowText(hDlg, TEXT("���� ���� ���� ����"));
}

void OnGetFileName(HWND hDlg)
{
	//1. ���� �̸� ȹ��
	TCHAR filename[20];
	GetDlgItemText(hDlg, IDC_EDIT1, filename, sizeof(filename));

	//2. ������ ����Open
	g_file = CreateFile(filename, GENERIC_READ, FILE_SHARE_READ,
		0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);
	if (g_file == INVALID_HANDLE_VALUE)
	{
		MessageBox(NULL, TEXT("���� ���� ����"), TEXT("�˸�"), MB_OK);		
		return;
	}

	//3. ũ�⸦ ���ϰ� ������ ���� ȹ��
	DWORD size1;
	DWORD size2 = GetFileSize(g_file, &size1);

	//4. ���������� ���� ���� ����
	_tcscpy_s(g_fi.FileName, sizeof(g_fi.FileName),  filename);
	g_fi.size = size2;

	//5. UI ���
	TCHAR buf[20];
	wsprintf(buf, TEXT("%dbyte"), size2);
	SetDlgItemText(hDlg, IDC_EDIT2, buf);
}

void OnFileSend(HWND hDlg)
{
	wbfilenet_FileSend(g_file, &g_fi);
}
