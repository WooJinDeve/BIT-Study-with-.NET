/*
* DLL ����� ���
* 
* [��κ� DLL ����� �Ͻ����̴�]
* 
* -����� ��� : �Ͻ������� DLL�� �ʿ��� ���
* �ʿ��� �� DLL�� �޸𸮿� �ε�
* �ʿ� ���� �� DLL�� �޸𸮿��� ����
*/
//----------------------------------------------
//DLL ������ �����ؼ� ���� �ҽ����� ��ġ�� �����ֱ�
// ���������� �Լ��� ȣ���ϱ� ���ؼ��� �Լ��� ����ΰ� �ݵ�� �־���Ѵ�
// [���]
//	1) LoadLibrary �Լ��� �̿��ؼ� ���ϴ� dLL�� �޸𸮿� �ε�
//		- ���������� �ε��� �Ǹ� DLL�� Handle�� ��ȯ�� �ش�
//	2) DLL�� handle�� ������ �Լ����� �̿��ؼ� DLL�� �ִ� �Լ��� �ּҸ� ȹ�� 
//		GetProcAddress("ã���� �ϴ� �Լ��� �̸�");
//		- �Լ�������..
//  3) �Լ� ȣ�� ����
//  4) FreeLibrary �Լ��� ȣ���ؼ� �ش� DLL�� �޸𸮿��� ����
//----------------------------------------------
#include <stdio.h>
#include <Windows.h>
typedef float(*DLLFUN)(int, int);

int main(void)
{
	HMODULE hDll = LoadLibrary(TEXT("0315_CalDll.dll"));
	if (hDll == NULL)
	{
		printf("DLL �ε� ����\n");
		return -1;
	}
	printf("DLL �ε强�� \n");

	//DLL �ȿ� �ִ� �Լ��� �ּ� ȹ��!
	float (*fAdd)(int, int);
	fAdd = (float(*)(int,int))GetProcAddress(hDll, "cal_add");

	//ȣ��
	printf("%d + %d = %5.1f\n", 10, 20, fAdd(10, 20));

	//typedef ���
	DLLFUN fsub = (DLLFUN)GetProcAddress(hDll, "cal_sub");
	printf("%d - %d = %5.1f\n", 10, 20, fsub(10, 20));


	FreeLibrary(hDll);
	return 0;
}