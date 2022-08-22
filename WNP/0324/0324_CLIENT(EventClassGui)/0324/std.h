#pragma once

#pragma comment (linker, "/subsystem:windows")

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <WinSock2.h>
#pragma comment(lib,"ws2_32.lib")
#include <windows.h>
#include <stdio.h>
#include <vector>
#include <tchar.h>
using namespace std;
#include<process.h>

#include "ui.h"
#include "resource.h"
#include "wbclient.h"
#include "handler.h"