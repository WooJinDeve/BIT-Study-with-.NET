//std.h

#pragma once

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <windows.h>
#include <stdio.h>
#include <process.h>
#include <tchar.h>
#include <vector>
using namespace std;
#include "resource.h"

#include "packet.h"
#include "wbserver.h"
#include "MainDlg.h"
#include "container.h"

extern MainDlg* pMainDlg;



