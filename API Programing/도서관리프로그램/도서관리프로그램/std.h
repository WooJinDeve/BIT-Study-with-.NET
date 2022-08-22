// std.h은 모든 헤더파일의 정보를 가지고 있음.
#pragma once

#pragma comment (linker, "/subsystem:windows")
#pragma comment(lib,"Comctl32.lib")
#include <Windows.h>
#include <tchar.h>
#include <vector>
#include <CommCtrl.h>
using namespace std;

//사용자 정의 통지 메세지
#define WM_APPLY WM_USER+100

//----------헤더파일----------------
#include "booklist.h"
#include "handler.h"
#include "ui.h"
#include "resource.h"
#include "insertDlg.h"
#include "selectDlg.h"
//#include "deleteDlg.h"