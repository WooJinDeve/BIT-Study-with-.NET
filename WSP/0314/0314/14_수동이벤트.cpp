//14_�����̺�Ʈ

#include <iostream>
#include <windows.h>
using namespace std;

//������ ���� �� �� �׽�Ʈ 

//�����̺�Ʈ ���� 
//WaitForSingleObject�� ����Ǵ��� Signal���°� ����
void fun1()
{
	HANDLE hEvent = CreateEvent(NULL, // ���ȼӼ�
		TRUE, // ��������(TRUE)
		FALSE, // �ʱ� ����( NON SIGNAL )
		TEXT("e")); // ������ �̺�Ʈ �̸�

	cout << "Event�� ��ٸ���." << endl;
	WaitForSingleObject(hEvent, INFINITE);
	cout << "Event ȹ�� " << endl;


	cout << "Event�� ��ٸ���." << endl;
	WaitForSingleObject(hEvent, INFINITE);
	cout << "Event ȹ��" << endl;


	CloseHandle(hEvent);
}

int main()
{
	fun1();

	return 0;
}