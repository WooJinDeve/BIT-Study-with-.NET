/*
* ������ ��� ������ ����
* ���� calc.h���ϰ� calc.cpp ������ �߰�
* -----------------------------------------------
* [������] pch.h ���ϰ��� ���� �߻�! (pre complie header : �̸� �����ϵ� h�� ����ϰڴ�)
* 
*  error : [������Ʈ] >> [�Ӽ�] >> [�����Ӽ� >> C/C++ >> �̸� �����ϵ� ���] >> �̸������ϵ� ��带 ������� �ʰڴ�]
* ---------------------------------------------------
*/

#define DLL_SOURCE

#include "calc.h"

float cal_add(int n1, int n2)
{
	return (float)n1 + n2;
}

float cal_sub(int n1, int n2)
{
	return (float)n1 - n2;
}

float cal_mul(int n1, int n2)
{
	return (float)n1 * n2;
}

float cal_div(int n1, int n2)
{
	return (float)n1 / n2;
}