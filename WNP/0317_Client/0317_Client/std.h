#pragma once

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include<stdio.h>
#include<Winsock2.h>
#include <process.h> // _beginthread(); // c��� ���
#pragma comment(lib,"ws2_32.lib")
#include<conio.h>

#include "packet.h"
#include "wbnet.h"