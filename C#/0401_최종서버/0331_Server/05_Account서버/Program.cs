using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _0331_Server._05_Account서버
{
    internal class Program
    {
        private AccountControl con = AccountControl.Instance;

        public bool Init()
        {
            return true;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                switch (MenuPrint())
                {
                    case ConsoleKey.F1: con.Start(); break;
                    case ConsoleKey.F2: con.Stop(); break;
                    case ConsoleKey.Escape: return;
                    default: Console.WriteLine("잘못된 메뉴 입력"); break;
                }
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
            Console.WriteLine(" [F1] 서버 시작");
            Console.WriteLine(" [F2] 서버 종료");
            Console.WriteLine("******************************************************");
            ConsoleKey key = Console.ReadKey().Key;
            Console.Write("\b");
            return key;
        }

        public static void Main()
        {
            Program program = new Program();
            if (program.Init() == true)
                program.Run();
            program.Exit();
        }
    }
}
