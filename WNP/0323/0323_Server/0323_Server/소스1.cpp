//server
//다중 접속 가능
//모든 접속된 클라이언트로부터 데이터 수신이 가능
//+ 모든 클라이언트에게 메시지를 전송
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define WIN32_LEAN_AND_MEAN
#include <winsock2.h>
#include <windows.h>
#pragma comment(lib, "ws2_32.lib")
#include <vector>
using namespace std;

vector<SOCKET> g_clients;

struct LINE
{
	POINTS ptFrom;
	POINTS ptTo;
};

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static SOCKET listen_socket, link_socket;
	static POINTS ptFrom;

	switch (msg)
	{
	case WM_LBUTTONDOWN: ptFrom = MAKEPOINTS(lParam); return 0;
	case WM_MOUSEMOVE:
		if (wParam & MK_LBUTTON)
		{
			POINTS pt = MAKEPOINTS(lParam);
			HDC hdc = GetDC(hwnd);

			MoveToEx(hdc, ptFrom.x, ptFrom.y, 0);
			LineTo(hdc, pt.x, pt.y);
			ReleaseDC(hwnd, hdc);

			// client로 보낸다.
			LINE line;
			line.ptFrom = ptFrom;
			line.ptTo = pt;

			//send(link_socket, (char*)&line, sizeof(line), 0);
			for (int i = 0; i < (int)g_clients.size(); i++)
			{
				send(g_clients[i], (char*)&line, sizeof(line), 0);
			}
			ptFrom = pt;
		}
		return 0;
	case WM_USER + 100:
	{
		WORD event = WSAGETSELECTEVENT(lParam);
		WORD error = WSAGETSELECTERROR(lParam);

		switch (event)
		{
		case FD_CLOSE:
			SetWindowText(hwnd, TEXT("접속이 끊어 졌습니다."));
			
			for (int i = 0; i < (int)g_clients.size(); i++)
			{
				if (wParam == g_clients[i])
				{
					g_clients.erase(g_clients.begin() + i);
					break;
				}
			}
			
			closesocket(wParam);  //<-----------------

			return 0;

		case FD_READ:
		{
			// data 도착 - 읽어 낸다.
			LINE line;
			//recv(link_socket, (char*)&line, sizeof(line), 0);
			recv(wParam, (char*)&line, sizeof(line), 0);
			HDC hdc = GetDC(hwnd);
			MoveToEx(hdc, line.ptFrom.x, line.ptFrom.y, 0);
			LineTo(hdc, line.ptTo.x, line.ptTo.y);
			ReleaseDC(hwnd, hdc);
		}
		return 0;

		case FD_ACCEPT:
		{
			SOCKADDR_IN addr;
			int	size = sizeof(addr);

			link_socket = accept(listen_socket, (SOCKADDR*)&addr, &size);

			g_clients.push_back(link_socket);  //<---------
			// 대기 소켓을 닫는다.(1:1 통신)
			//closesocket(listen_socket);

			SetWindowText(hwnd, TEXT("접속 되었습니다."));

			// 연결된 소켓을 비동기 소켓으로 변경한다.
			WSAAsyncSelect(link_socket, hwnd, WM_USER + 100, FD_READ | FD_CLOSE);
		}
		break;
		}
		break;
	}
	return 0;
	case WM_CREATE:
	{
		listen_socket = socket(AF_INET, SOCK_STREAM, 0);

		// socket 을 비동기 소켓으로 변경한다.
		WSAAsyncSelect(listen_socket, hwnd, WM_USER + 100, FD_ACCEPT);

		SOCKADDR_IN addr;
		addr.sin_family = AF_INET;
		addr.sin_port = htons(6000);
		addr.sin_addr.s_addr = INADDR_ANY;

		bind(listen_socket, (SOCKADDR*)&addr, sizeof(addr));

		listen(listen_socket, 5);
	}
	return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
	LPSTR	  lpCmdLine, int nShowCmd)
{
	WSADATA wsadata;
	if (WSAStartup(MAKEWORD(2, 2), &wsadata) != 0)
	{
		MessageBox(0, TEXT("Error"), TEXT(""), MB_OK);
		return 0;
	}

	ATOM atom;
	WNDCLASS wc;
	HWND hwnd;
	MSG msg;

	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInstance;
	wc.lpfnWndProc = WndProc;
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;
	wc.style = 0;

	atom = RegisterClass(&wc);

	if (atom == 0)
	{
		MessageBox(0, TEXT("Fail To RegisterClass"), TEXT("Error"), MB_OK);
		return 0;
	}

	hwnd = CreateWindowEx(0, TEXT("first"), TEXT("Hello"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, 0, 0,
		hInstance, 0);
	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);

	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	WSACleanup();
	return 0;
}


