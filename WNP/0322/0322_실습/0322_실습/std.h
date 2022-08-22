//std.h
#pragma once

#define _WINSOCK_DEPRECATED_NO_WARNINGS

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <WinSock2.h>
#include <ws2tcpip.h>	
#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include <stdio.h>

#pragma comment(lib, "ws2_32.lib")
#include <process.h>		//_beginthread()
#include <conio.h>

#include"handler.h"
#include"ui.h"
#include"wbnet.h"

