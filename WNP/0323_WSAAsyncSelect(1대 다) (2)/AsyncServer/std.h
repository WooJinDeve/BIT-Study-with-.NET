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
using namespace std;

#include "data.h"
#include "handler.h"
#include "wbnet.h"
#include "ui.h"