using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_Library
{
    static class WbPrint
    {
        public static void Logo()
        {
            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│   [3조] 도서관리 프로그램   │");
            Console.WriteLine("└─────────────────────────────┘");

        }

        public static void Ending()
        {
            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│     [3조] 프로그램 종료     │");
            Console.WriteLine("└─────────────────────────────┘");
        }

        public static void MenuPrint()
        {
            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│  [0.] 등록된 도서 전체 검색 │");
            Console.WriteLine("│  [1.] 도서 등록             │");
            Console.WriteLine("│  [2.] 도서 검색             │"); ;
            Console.WriteLine("│  [3.] 도서 수정             │"); ;
            Console.WriteLine("│  [4.] 도서 삭제             │");
            Console.WriteLine("│  [5.] 도서 전체 삭제        │");
            Console.WriteLine("│  [6.] 도서 정렬             │"); ;
            Console.WriteLine("│  [7.] 프로그램 종료         │");
            Console.WriteLine("└─────────────────────────────┘");
        }

        public static void Pause()
        {
            Console.WriteLine("아무키나 입력해주세요.");
            Console.ReadKey();
        }
    }
}
