using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0407_메모리DB구성
{
    static class WbPrint
    {
        public static void Logo()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(" 메모리DB를 활용한 관리 프로그램");
            Console.WriteLine(" 2022-04-07");
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
            Console.WriteLine("[F1]  테이블 디자인");
            Console.WriteLine("[F2]  테이블스키마정보확인");
            Console.WriteLine("[F3]  맴버데이터 초기화(5개정도)");
            Console.WriteLine("[F4]  계좌개설(사용자입력)-Accountio까지 저장");
            Console.WriteLine("[F5]  맴버및계좌전체출력");         
            Console.WriteLine("[ESC] 프로그램 종료");
            Console.WriteLine("--------------------------------------");
            
            return Console.ReadKey().Key;
        }
        
        public static void Pause()
        {
            Console.WriteLine("\n아무키나 누르세요....");
            Console.ReadKey();
        }
    }
}
