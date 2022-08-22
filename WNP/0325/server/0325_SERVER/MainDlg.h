//maindlg.h

class WbServer; //��������(������ Ÿ���� ���·λ��� ���� ���������� �����ϴ�.)
class Container;

class MainDlg
{
	//����
private:
	WbServer* pserver;
	Container* pcon;

private:
	HINSTANCE hInst;		//Main Instance
	HWND	  hdlg;			//�ڽ��� ������ �ڵ�(OnInitDialog���� ���Կ���)

	//�ڽ� ��Ʈ�� �ڵ�(HANDLE�� ��� ��ġ : OnIniDialog)
private:
	HWND hEditView;
	HWND hListView;

public:
	MainDlg(HINSTANCE _hInst);
	~MainDlg();

public:
	void Create(); //���̾�α� ����!

public:
	static BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam);

	//handler
public:
	BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam);
	BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam);

	//����� ���� �Լ�(handler)
public:
	void OnExit();	
	void OnServerStart();
	void OnServerStop();

public:
	void LogFunction(int flag);
	void RecvMessage(TCHAR* msg);

	//UI���� �Լ�
public:
	void ui_GetControlHandle();
	void ui_ViewLogPrint(const TCHAR* msg);
	void ui_MessagePrint(TCHAR* msg);
};




	