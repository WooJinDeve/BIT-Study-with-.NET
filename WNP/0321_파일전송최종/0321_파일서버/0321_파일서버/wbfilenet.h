//wbfilenet.h
#pragma once

/*
* 1�� �� ���� 
* 1�� 1 ���� ����
*   - (����)
*/

struct FILE_INFO
{
	char FileName[260];
	int  size;
};

//��� ������ 
DWORD WINAPI wbfilenet_FileThread(void* p);

//���� �ۼ��� ������
//[Ŭ���̾�Ʈ ���ӽ� ] ��� �����忡�� ȣ��
DWORD WINAPI FileServer(void* p);