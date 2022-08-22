using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _0408_날씨
{ 
    class Program
    {
        private Control con = Control.Instance;
        
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
                    case ConsoleKey.F1: con.Parsing();      break;
                    case ConsoleKey.F2: con.PrintAll();    break;
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
            Console.WriteLine(" [F1] 날씨 정보 가져오기");
            Console.WriteLine(" [F2] 결과 출력하기");
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
