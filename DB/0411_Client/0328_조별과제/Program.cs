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
        private Control con = Control.Instance;
        
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
                    case ConsoleKey.F1: con.SearchBook();      break;
                    case ConsoleKey.F2: con.SelectAllBookName();    break;
                    case ConsoleKey.F3: con.SelectBookName();     break;
                    case ConsoleKey.F4: con.SelectAllBook();    break;
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
            Console.WriteLine(" [F1] 검색 요청");
            Console.WriteLine(" [F2] 검색된 도서명 리스트 요청");
            Console.WriteLine(" [F3] 도서 정보 요청(도서의 이름 전달");
            Console.WriteLine(" [F4] 전체 도서의 모든 정보 요청");
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
