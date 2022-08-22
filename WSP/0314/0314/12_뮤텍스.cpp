//12_���ؽ�

#include <windows.h>
#include <iostream>
using namespace std;

//�ݺ�����..(���μ����� ���μ����� ����ȭ)
//ù��° ���� : CreatMutex("mutex") : mutex ��� key�� ���ؽ� ����
//�ι�° ���� : CreateMutex("mutex") : ����, ������ key�� mutex�� �����ϴ� �ڵ尡 �ִٸ�
//                                    �ڵ����� OpenMutex�� ����ó���ȴ�.

int main()
{
	// ���ؽ� ����
	HANDLE hMutex = CreateMutex(NULL,	// ���ȼӼ�
		FALSE,	// ������ ���ؽ� ���� ���� --> FALSE : SIGNAL
		TEXT("mutex")); // �̸�(KEY)

	cout << "���ý��� ��ٸ��� �ִ�." << endl;

	//Wait�Լ��� ����Ǹ� : �ش� �����尡 ���ؽ��� ���� --> �ڵ����� Signal �� nonsignal�� �ٲ۴�.
	DWORD d = WaitForSingleObject(hMutex, INFINITE); // ���---------------------------
	if (d == WAIT_TIMEOUT)
		printf("%s", "WAIT_TIMEOUT --> ");
	else if (d == WAIT_OBJECT_0)
		printf("%s", "WAIT_OBJECT_0 --> ");
	else if (d == WAIT_ABANDONED_0)
		printf("%s", "WAIT_ABANDONED_0 --> ");
	cout << "���ý� ȹ��" << endl;

	//�۾�.....

	MessageBox(NULL, TEXT("���ý��� ���´�."), TEXT(""), MB_OK);

	ReleaseMutex(hMutex);							//----------------------------------

	return 0;
}
