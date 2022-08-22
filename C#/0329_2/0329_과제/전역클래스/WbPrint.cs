using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_과제
{
    static class WbPrint
    {
        public static void Logo()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("계좌관리 프로그램 ");
            Console.WriteLine("========================================");
        }

        public static void Ending()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("계좌관리 프로그램 종료");
            Console.WriteLine("========================================");
        }

        public static void MenuPrint()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("[1] 계좌 개설");
            Console.WriteLine("[2] 입 금");
            Console.WriteLine("[3] 출 금");
            Console.WriteLine("[4] 잔액 조회");
            Console.WriteLine("[5] 계좌 삭제");
            Console.WriteLine("[6] 프로그램 종료");
            Console.WriteLine("========================================");
        }

        public static void Pause()
        {
            Console.WriteLine("아무키나 입력하세요.");
            Console.ReadKey();
        }
    }
}
