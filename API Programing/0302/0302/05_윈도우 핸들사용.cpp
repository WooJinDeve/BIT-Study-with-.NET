/*
*  User��ü (������ �ڵ�)
*   - �ý��ۿ� ������ �ڵ��̴�.
*/
#include<Windows.h>
#include<tchar.h>
#include<stdio.h>
int main()
{
	HWND hwnd = FindWindow(0, _TEXT("���� ���� - Windows �޸���"));
	if (hwnd == 0)
	{
		printf("���� ����\n");
		return 0;
	}
	printf("�޸��� - ������ �ڵ� : %d\n", (int)hwnd);

	TCHAR c_name[60];
	GetClassName(hwnd, c_name, sizeof(c_name));
	printf("�޸��� - Ŭ������ : %s\n", c_name);

	GetWindowText(hwnd, c_name, sizeof(c_name));
	printf("�޸��� - ������� : %s\n", c_name);

	RECT rc;
	GetWindowRect(hwnd, &rc);
	printf("�޸��� - ��ġ(ũ��) (%d,%d)~(%d,%d)\n", rc.left, rc.top, rc.right, rc.bottom);
	printf("�޸��� - ũ��(%d * %d)\n", rc.right - rc.left, rc.top - rc.bottom);
	system("pause");

	MoveWindow(hwnd, 0, 0, 500, 500, FALSE);
	printf("������ �����\n");
	system("pause");
	ShowWindow(hwnd, SW_HIDE);

	printf("������ �����ֱ�\n");
	system("pause");
	ShowWindow(hwnd, SW_SHOW);
	
	printf("������ ���̱�\n");
	system("pause");
	SendMessage(hwnd, WM_CLOSE, 0, 0);
	return 0;
}