//���°��� ��û, �������� ��û
struct NEWACCOUNT
{
    int flag;
    int id;             //���¹�ȣ(���� 4�ڸ���)//Ŭ���̾�Ʈ���
    char name[20];      //���̸�//Ŭ���̾�Ʈ���
    int  balance;       //�Աݾ�//Ŭ���̾�Ʈ���
    SYSTEMTIME sdate;   //�����Ͻ�//Ŭ���̾�Ʈ���
};

//�� ���¹�ȣ ����Ʈ ��û
struct SELEALLACCOUNT
{
    int flag;
    char name[10];  //Ŭ���̾�Ʈ���
    int accid[10];  //Ŭ���̾�Ʈ���(�� 0���� �ʱ�ȭ�ؼ�)
                    //���� ���(���޵� �̸����� ������ü�� �˻��ؼ� �ش� ���¹�ȣ�� ����)
};

//�Ա� ��� ��û 
struct IOMONEY
{
    int flag;
    int id;     //Ŭ���̾�Ʈ
    int money;  //Ŭ���̾�Ʈ, ����(����� �ܾ����� ����)
    int errcode;//����
};