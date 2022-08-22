//계좌개설 요청, 계좌정보 요청
struct NEWACCOUNT
{
    int flag;
    int id;             //계좌번호(정수 4자리수)//클라이언트사용
    char name[20];      //고객이름//클라이언트사용
    int  balance;       //입금액//클라이언트사용
    SYSTEMTIME sdate;   //개설일시//클라이언트사용
};

//내 계좌번호 리스트 요청
struct SELEALLACCOUNT
{
    int flag;
    char name[10];  //클라이언트사용
    int accid[10];  //클라이언트사용(다 0으로 초기화해서)
                    //서버 사용(전달된 이름으로 계좌전체를 검색해서 해당 계좌번호를 저장)
};

//입금 출금 요청 
struct IOMONEY
{
    int flag;
    int id;     //클라이언트
    int money;  //클라이언트, 서버(변경된 잔액정보 저장)
    int errcode;//서버
};