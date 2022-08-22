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

#include "resource.h"
#include "member.h"
#include "ui.h"
#include "insertdlg.h"
#include "handler.h"
#include "selectdlg.h"