//std.h
#pragma once

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#define WIN32_LEAN_AND_MEAN
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>
#include <ws2tcpip.h>
#include <windows.h>
#include <tchar.h>
#include < stdlib.h > //_countof
#include <commctrl.h>
#pragma comment( lib, "ws2_32.lib")

#include "resource.h"
#include "MainDlg.h"
#include "ChatDlg.h"
#include "ui.h"
#include "wbnet.h"
#include "AddMemberDlg.h"

extern MainDlg* pMainDlg;
extern ChatDlg* pChatDlg;
extern AddMemberDlg* pAddMemberDlg;