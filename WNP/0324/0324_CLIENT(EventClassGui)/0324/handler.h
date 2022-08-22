
class Handler
{
private:
	HWND hDlg;
	WbClient* pwb;
	UI* pui;

public:
	Handler(HWND Handle);
	~Handler();

public:
	BOOL OnInitDialog(WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(WPARAM wParam, LPARAM lParam);

private:
	void OnConnect();
	void OnDisConnect();
	void OnSend();
};

