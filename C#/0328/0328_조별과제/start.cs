using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0328_조별과제
{ 
    class start
    {        
        static void Main(string[] args)
        {
            AccManager acc = new AccManager();

            while(true)
            {
                Console.Clear();

                choice choice = 0;
                acc.PrintMenu();
                Console.Write("선택 : ");
                choice = (choice)int.Parse(Console.ReadLine());
               
                switch (choice)
                {
                    case choice.MAKE:     acc.MakeAccount(); break;
                    case choice.DEPOSIT:  acc.Deposit();     break;
                    case choice.WITHDRAW: acc.Withdraw();    break;
                    case choice.INQUIRE:  acc.Inquire();     break;
                    case choice.EXIT:                       return;

                    default:
                        Console.WriteLine("잘못된 메뉴 입력");
                        break;
                }

                WbLib.Pause();
            }
        }
    }
}
