//06_thread�� ��������(TLS)

//consle : ����Ÿ��...
#include <Windows.h>
#include <stdio.h>
#include <tchar.h>

void goo(TCHAR* name)
{
	// TLS ������ ������ �����Ѵ�.
	__declspec(thread) static int c = 0;

	//�������������� ������ �����Ѵ�.
	//static int c = 0;
	++c;
	_tprintf(TEXT("%s : %d\n"), name, c); // �Լ��� ��� ȣ��Ǿ����� �˰� �ʹ�.
}

unsigned long __stdcall foo(void* p)
{
	TCHAR* name = (TCHAR*)p;
	goo(name);
	goo(name);
	goo(name);
	return 0;
}

int main()
{
	HANDLE h1 = CreateThread(0, 0, foo, (void*)TEXT("A"), 0, 0);
	HANDLE h2 = CreateThread(0, 0, foo, (void*)TEXT("\tB"), 0, 0);

	HANDLE h[2] = { h1, h2 };
	//TRUE : 2���� �ڵ��� ��� signal�ɶ����� ����ϰڴ�.
	//FALSE : ��ٸ��� �ڵ� �� �ϳ��� signal�ǵ� �����Ѵ�.
	//������ : ����Ǹ� signal�ȴ�.
	WaitForMultipleObjects(2, h, TRUE, INFINITE);
	CloseHandle(h1);
	CloseHandle(h2);

	return 0;
}
