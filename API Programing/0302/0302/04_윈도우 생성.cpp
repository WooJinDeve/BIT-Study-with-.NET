#pragma comment(linker, "/subsystem:windows")
#include<Windows.h>
#include<tchar.h>
int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR cmd, int show)
{
	//1.클래스 정의
	WNDCLASS wc;
	wc.cbClsExtra = 0; // 임시맴버
	wc.cbWndExtra = 0;

	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //배경
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;           // 자신의 인스턴스(이 윈도우 클래스를 누가 만들었느냐?
	wc.lpfnWndProc = DefWindowProc; // 미리 만들어서 제공되는 프로시저

	wc.lpszClassName = _TEXT("First"); //KEY중복되면 안됨
	wc.lpszMenuName = 0; //메뉴의 리소스 아이디
	wc.style = 0;

	//2. 윈도우 클래스를 레지스트리에 등록
	RegisterClass(&wc);

	//3.윈도우 생성(첫번째 객체 : UI객체(메모리, 데이터) ->  핸들
	HWND hwnd = CreateWindowEx(0, _TEXT("First"), _TEXT("윈도우생성"),WS_OVERLAPPEDWINDOW,
		10,10,500,500,0,0,hInst,0);
	
	//4. 화면출력
	ShowWindow(hwnd, show);

	MessageBox(0, _TEXT("Hello, World!"), _TEXT("알림"), MB_OKCANCEL | MB_ICONQUESTION);

	return 0;
}