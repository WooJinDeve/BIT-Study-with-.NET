using System;
using _0329_과제.이벤트처리;

namespace _0329_과제
{
    class Control
    {
        #region 싱글톤 패턴
        public static Control Singleton { get; private set; }

        static Control()
        {
            Singleton = new Control();
        }

        private Control() 
        {
        }

        #endregion 

        //private List<Account> accounts = new List<Account>();
        private AccountCollection accounts 
            = new AccountCollection();

        #region 이벤트 정의
        public event AddAccountEvent AddAccount = null;
        public event AddIOEvent InputAccount = null;
        public event AddIOEvent OutputAccount = null;

        #endregion

        #region 파일 기능 제거
        public void FileSave()
        {
            //WbFile.WriteFile(accounts);
        }

        public void FileLoad()
        {
            //WbFile.ReadFile(accounts);
        }
        #endregion 

        public void InitData()
        {
            accounts.InitData();
        }


        //전체출력
        public void SelectAll()
        {
            Console.WriteLine("저장개수 : " + accounts.Count);
            foreach (Account acc in accounts)
            {
                Console.WriteLine(acc);
            }
            Console.WriteLine();
        }

        //입력
        public void Insert()
        {
            int id;                   
            int balance;          
            string name;

            Console.Write("계좌 번호 : ");
            id = int.Parse(Console.ReadLine());
            Console.Write("이름 : ");
            name = Console.ReadLine();
            Console.Write("잔액 : ");
            balance = int.Parse(Console.ReadLine());

            Account acc = new Account(id, name, balance);
            accounts.Add(acc);
            Console.WriteLine("계좌가 개설되었습니다.");

            if(AddAccount != null)
            {
                AddAccount(this, new AddAccountEventArgs(id, name, balance,
                    acc.Da));
            }
        }

        //Account(계좌번호)로 인덱스값 찾기
        public int AccountNumberToIdx(int accid)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Id == accid)
                    return i;
            }
            return -1;
        }

        public void Select()
        {
            Console.Write("계좌번호 입력: ");
            int temp = int.Parse(Console.ReadLine());

            int idx = AccountNumberToIdx(temp);

            if (idx == -1)
                return;

            accounts[idx].ShowAllData();

            Console.WriteLine("---------------------------------");
            IOControl.Singleton.PrintAccount(temp);
        }

        //입금
        public void Update1()
        {
            Console.Write("계좌번호 입력: ");
            int temp = int.Parse(Console.ReadLine());

            Console.Write("입금 액수 입력: ");
            int money = int.Parse(Console.ReadLine());

            int idx = AccountNumberToIdx(temp);
            if (idx == -1)
            {
                Console.WriteLine("없다");
            }
            else
            {
                accounts[idx].AddMoney(money);

                if(InputAccount != null)
                {
                    InputAccount(this,
                        new AddIOEventArgs(temp, money, 0,
                       accounts[idx].Balance, DateTime.Now));                        
                }
            }
        }

        //출금
        public void Update2()
        {
            Console.Write("계좌번호 입력: ");
            int temp = int.Parse(Console.ReadLine());

            Console.Write("출금 액수 입력: ");
            int money = int.Parse(Console.ReadLine());

            int idx = AccountNumberToIdx(temp);
            if (idx == -1)
            {
                Console.WriteLine("없다");
            }
            else
            {
                accounts[idx].MinMoney(money);

                if (OutputAccount != null)
                {
                    OutputAccount(this,
                        new AddIOEventArgs(temp, 0, money,
                       accounts[idx].Balance, DateTime.Now));
                }
            }
        }

        //계좌 삭제
        public void Delete()
        {
            Console.Write("삭제할 계좌번호 입력: ");
            int temp = int.Parse(Console.ReadLine());

            int idx = AccountNumberToIdx(temp);
            if(idx == -1)
                return ;

            accounts.RemoveAt(idx);
            Console.WriteLine("삭제 완료\n");
        }
    }
}
