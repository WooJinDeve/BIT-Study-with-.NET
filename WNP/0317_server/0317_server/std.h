#pragma once

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include<stdio.h>
#include<Winsock2.h>
#pragma comment(lib,"ws2_32.lib")
#include <process.h> // _beginthread(); // c언어 사용
#include <vector>
using namespace std;

#include "packet.h"
#include "wbnet.h"