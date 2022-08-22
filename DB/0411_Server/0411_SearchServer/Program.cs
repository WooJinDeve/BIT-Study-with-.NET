using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0411_SearchServer
{
    internal class Program
    {
        private Control con = Control.Instance;

        public bool Init()
        {            
            return con.Init();
        }

        public void Run()
        {
            //while (true)
            //{
            //    Console.Clear();
            //    switch (MenuPrint())
            //    {
            //        case ConsoleKey.F1: con.Parsing();
            //                            con.PrintAll(); break;
            //        case ConsoleKey.F2: con.ImageDownLoad(); break;
            //        case ConsoleKey.Escape: return;
            //        default: Console.WriteLine("잘못된 메뉴 입력"); break;
            //    }
              WbLib.Pause();
            //}
        }

        public void Exit()
        {
            con.Exit();
        }

        private ConsoleKey MenuPrint()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [ESC] 프로그램종료");
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [F1] XML 파싱");
            Console.WriteLine(" [F2] 이미지 다운로드");
            Console.WriteLine("******************************************************");
            ConsoleKey key = Console.ReadKey().Key;
            Console.Write("\b");
            return key;
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            if (program.Init() == true)
                program.Run();
            program.Exit();
        }
    }
}
