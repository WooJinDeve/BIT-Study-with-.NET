#pragma once

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#define WM_APPLY WM_USER+100

#include <windows.h>
#include <tchar.h>
#include <commctrl.h>
#include <vector>
using namespace std;

#include "resource.h"
#include "handler.h"
#include "ui.h"
#include "psapi.h"
#include "wbprocess.h"