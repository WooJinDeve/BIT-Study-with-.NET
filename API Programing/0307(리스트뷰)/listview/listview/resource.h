//{{NO_DEPENDENCIES}}
// Microsoft Visual C++에서 생성한 포함 파일입니다.
// listview.rc에서 사용되고 있습니다.
//
#include <Windows.h>
#include <tchar.h>
#include <CommCtrl.h>
#include <vector>

#define IDB_BITMAP1                     101
#define IDB_BITMAP2                     102
#define IDR_MENU1                       103
#define IDM_ICON                        40001
#define IDM_SMALLICON                   40002
#define IDM_LIST                        40003
#define IDM_REPORT                       40004

// Next default values for new objects
// 
#ifdef APSTUDIO_INVOKED
#ifndef APSTUDIO_READONLY_SYMBOLS
#define _APS_NEXT_RESOURCE_VALUE        104
#define _APS_NEXT_COMMAND_VALUE         40005
#define _APS_NEXT_CONTROL_VALUE         1001
#define _APS_NEXT_SYMED_VALUE           101
#endif
#endif


struct PEOPLE {
	TCHAR name[20];
	TCHAR tel[20];
	TCHAR addr[50];
	BOOL male;
};

void SetListViewStyle(HWND hList, DWORD dwView)
{
	DWORD dwStyle;

	dwStyle = GetWindowLong(hList, GWL_STYLE);
	if ((dwStyle & LVS_TYPEMASK) != dwView) {
		SetWindowLong(hList, GWL_STYLE, (dwStyle & ~LVS_TYPEMASK) | dwView);
	}
}