using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    static class WbPrint
    {
        public static void Logo()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(" DataBase를 활용한 관리 프로그램");
            Console.WriteLine(" 2022-04-05");
            Console.WriteLine("-----------------------------------------");
        }
        
        public static void Ending()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("프로그램을 종료합니다");
            Console.WriteLine("-----------------------------------------");
        }
        
        public static ConsoleKey MenuPrint()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("[F1]  입실");
            Console.WriteLine("[F2]  퇴실");
            Console.WriteLine("[F3]  고객 수");
            Console.WriteLine("[F4]  전체 수익");
            Console.WriteLine("[F5]  좌석별 수익");           
            Console.WriteLine("[ESC] 프로그램 종료");
            Console.WriteLine("--------------------------------------\n\n");
            
            return Console.ReadKey().Key;
        }
        
        public static void Pause()
        {
            Console.WriteLine("\n아무키나 누르세요....");
            Console.ReadKey();
        }
    }
}
