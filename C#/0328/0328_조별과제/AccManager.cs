using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{    
    class AccManager
    {
        private Container ctr;

        public AccManager(int max = 50)
        {
            ctr = new Container(max);
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. 계좌 개설, 2. 입금, 3.출금, 4. 잔액 조회");
        }

        public void MakeAccount()
        {
            Console.WriteLine("1. 일반 계좌");
            Console.WriteLine("2. 신용 계좌");
            Console.WriteLine("3. 기부 계좌");

            int sel     = WbLib.getNumber("개설할 계좌의 종류");
            int id      = WbLib.getNumber("ID 입력"); 
            string name = WbLib.getString("이   름");
            int balance = WbLib.getNumber("입금금액");

            if (sel == 1)
            {
                ctr.insert(new Account(id, balance, name));
            }
            else if (sel == 2)
            {
                ctr.insert(new FaithAccount(id, name, balance));
            }
            else if (sel == 3)
            {
                ctr.insert(new ContriAccount(id, name, balance));
            }
            else
                Console.WriteLine("선택 오류");
        }

        public void Deposit()
        {
            Console.Write("계좌 ID : ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("입 금 액: ");
            int money = int.Parse(Console.ReadLine());

            int num = ctr.GetElemSum(); // 계좌 총 개수

            for (int i = 0; i < num; i++)
            {
                //Account acc = ctr.GetItem(i);
                Account acc = ctr[i];
                if(acc.Id == id)
                {
                    acc.AddMoney(money);
                    Console.WriteLine("입금 완료");
                    return;
                }
            }
            Console.WriteLine("유효하지 않은 ID입니다");
        }

        public void Withdraw()
        {
            Console.Write("계좌 ID : ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("출 금 액: ");
            int money = int.Parse(Console.ReadLine());

            int num = ctr.GetElemSum(); // 계좌 총 개수
            for(int i = 0; i < num;i++)
            {
                Account acc = ctr.GetItem(i);
                if (acc.Id == id)
                {
                    if(acc.Balance < money)
                    {
                        Console.WriteLine("잔액 부족");
                        return;
                    }
                    acc.MinMoney(money);
                    Console.WriteLine("출금 완료");
                    return;
                }
                Console.WriteLine("유효하지 않은 ID입니다");
            }
        }

        public void Inquire()
        {
            int num = ctr.GetElemSum();

            for(int i = 0;i< num;i++)
            {
                //Account acc = ctr.GetItem(i);
                Account acc = ctr[i];
                acc.ShowAllData();
            }
        }
    }
}
