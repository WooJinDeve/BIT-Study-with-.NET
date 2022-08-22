/*
* DLL 명시적 사용
* 
* [대부분 DLL 사용은 암시적이다]
* 
* -명시적 사용 : 일시적으로 DLL이 필요한 경우
* 필요할 때 DLL을 메모리에 로딩
* 필요 없을 때 DLL을 메모리에서 제거
*/
//----------------------------------------------
//DLL 파일을 복사해서 현재 소스파일 위치에 붙혀넣기
// 문법적으로 함수를 호출하기 위해서는 함수의 선언부가 반드시 있어야한다
// [대안]
//	1) LoadLibrary 함수를 이용해서 원하는 dLL을 메모리에 로딩
//		- 정상적으로 로딩이 되면 DLL의 Handle을 반환해 준다
//	2) DLL의 handle이 있으면 함수명을 이용해서 DLL에 있는 함수의 주소를 획득 
//		GetProcAddress("찾고자 하는 함수의 이름");
//		- 함수포인터..
//  3) 함수 호출 가능
//  4) FreeLibrary 함수를 호출해서 해당 DLL을 메모리에서 제거
//----------------------------------------------
#include <stdio.h>
#include <Windows.h>
typedef float(*DLLFUN)(int, int);

int main(void)
{
	HMODULE hDll = LoadLibrary(TEXT("0315_CalDll.dll"));
	if (hDll == NULL)
	{
		printf("DLL 로드 실패\n");
		return -1;
	}
	printf("DLL 로드성공 \n");

	//DLL 안에 있는 함수의 주소 획득!
	float (*fAdd)(int, int);
	fAdd = (float(*)(int,int))GetProcAddress(hDll, "cal_add");

	//호출
	printf("%d + %d = %5.1f\n", 10, 20, fAdd(10, 20));

	//typedef 사용
	DLLFUN fsub = (DLLFUN)GetProcAddress(hDll, "cal_sub");
	printf("%d - %d = %5.1f\n", 10, 20, fsub(10, 20));


	FreeLibrary(hDll);
	return 0;
}