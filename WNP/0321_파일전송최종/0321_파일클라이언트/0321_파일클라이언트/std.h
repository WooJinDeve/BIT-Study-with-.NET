//std.h
#pragma once

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#define WIN32_LEAN_AND_MEAN
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>
#include <windows.h>
#include <tchar.h>
#include <commctrl.h>
#pragma comment( lib, "ws2_32.lib")

#include "resource.h"
#include "File_info.h"
#include "handler.h"
#include "wbfilenet.h"