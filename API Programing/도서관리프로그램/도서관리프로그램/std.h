// std.h�� ��� ��������� ������ ������ ����.
#pragma once

#pragma comment (linker, "/subsystem:windows")
#pragma comment(lib,"Comctl32.lib")
#include <Windows.h>
#include <tchar.h>
#include <vector>
#include <CommCtrl.h>
using namespace std;

//����� ���� ���� �޼���
#define WM_APPLY WM_USER+100

//----------�������----------------
#include "booklist.h"
#include "handler.h"
#include "ui.h"
#include "resource.h"
#include "insertDlg.h"
#include "selectDlg.h"
//#include "deleteDlg.h"