/*
* 생성된 모든 파일을 제거
* 직접 calc.h파일과 calc.cpp 파일을 추가
* -----------------------------------------------
* [컴파일] pch.h 파일관련 에러 발생! (pre complie header : 미리 컴파일된 h를 사용하겠다)
* 
*  error : [프로젝트] >> [속성] >> [구성속성 >> C/C++ >> 미리 컴파일된 헤더] >> 미리컴파일된 헤드를 사용하지 않겠다]
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