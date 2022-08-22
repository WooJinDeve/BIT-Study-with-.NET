//std.h

#pragma once

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#include <windows.h>
#include <tchar.h>
#include "resource.h"
#include <vector>
#include <process.h>
using namespace std;

#include "wbserver.h"
#include "ui.h"
#include "handler.h"
