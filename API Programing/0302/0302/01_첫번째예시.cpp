/*#include<stdio.h>
// �Լ� : ȣ��Ծ�(�Լ��� ȣ��� �� ��� ������ ���ΰ�?)
// __cdecl ���(C/C++ �Լ�), __stdcall ���(Win32api���� �����Ǵ� �ý����Լ�)
// Visual Studio �⺻ ȣ��Ծ� : __cdecl���
// �ý����Լ� : �ý��ۿ� ���ؼ� ȣ��Ǵ� �Լ� 

int __cdecl main() //console �����Լ�
{
	printf("hello world\n");

	return 0;
}*/

#include<Windows.h> //win32api �⺻�Լ�
#include<tchar.h> //���ڿ��� ���������� ����ϱ� ���� h(��Ƽ����Ʈ or �����ڵ�)

// _tWinMain�� ���� ȯ�濡 ���� �����ڵ带 ����ϴ� wWinMain���� ġȯ�ǰų�
//                               ��Ƽ����Ʈ�� ����ϴ� WinMain���� ġȯ�ȴ�.
//#define WINAPI __stdcall // ȣ�� �Ծ�� ���õ� ����
//#define _tWinMain wWinMain // �����ڵ�(���ڿ� ǥ��)
//#define _wWinMain WinMain // ��Ƽ����Ʈ(���ڿ� ǥ��)

//type : Win32API���� ���Ǵ� Ÿ���� �빮��Ÿ��..
//       ����Ÿ���� �̸��� ������ ����.
//  ex : typedef unsigned int uint;   //�ܼ��ϰ� ����ϱ� ���� ����
//       typedef unsigned int size_t; //Ÿ�Կ� �ǹ̸� �ο� ����(Win32API ����)
//       typedef void* HINSTANCE;
//       typedef wchar* LPWSTR // �����ڵ�
//       typedef char* LPSTR   // ��Ƽ����Ʈ
//  t�� �� �Լ� �Ǵ� ������ Ÿ���� ���뼺�� ���Ѵ�.
// #define LPTSTR LPWSTR  : �����ڵ� (���ڿ� ǥ��)
// #define LPTSTR LPSTR   : ��Ƽ���̵� (���ڿ� ǥ��)
int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show) // gui �����Լ�
{
	return 0;
}
