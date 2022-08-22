using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _0328_조별과제
{ 
    class Program
    {
        private AccountControl con = AccountControl.Instance;
        
        public bool Init()
        {
            return con.Init();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                switch (MenuPrint())
                {
                    case ConsoleKey.F1: con.MakeAccount();      break;
                    case ConsoleKey.F2: con.SelectAccount();    break;
                    case ConsoleKey.F3: con.InputAccount();     break;
                    case ConsoleKey.F4: con.OutputAccount();    break;
                    case ConsoleKey.F5: con.TransAccount();     break;
                    case ConsoleKey.F6: con.DeleteAccount();    break;
                    case ConsoleKey.F7: con.PrintAll();         break;
                    case ConsoleKey.Escape: return;
                    default: Console.WriteLine("잘못된 메뉴 입력"); break;
                }
                Thread.Sleep(1000);
                WbLib.Pause();
            }
        }

        public void Exit()
        {

        }

        private ConsoleKey MenuPrint()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [ESC] 프로그램종료");
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [F1] 계좌 생성");
            Console.WriteLine(" [F2] 계좌 검색");
            Console.WriteLine(" [F3] 입금");
            Console.WriteLine(" [F4] 출금");
            Console.WriteLine(" [F5] 계좌이체");
            Console.WriteLine(" [F6] 삭제");
            Console.WriteLine(" [F7] 전체 계좌 정보 출력");
            Console.WriteLine("******************************************************");
            ConsoleKey key = Console.ReadKey().Key;
            Console.Write("\b");
            return key;
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            if( program.Init() == true)
                program.Run();
            program.Exit();
        }
    }
}
