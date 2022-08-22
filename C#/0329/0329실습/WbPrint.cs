using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329실습
{
    static class WbPrint
    {
        public static void Logo()
        {

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("   --- 1.조 전화번호 관리 프로그램---");
            Console.WriteLine("-----------------------------------------");
        }
        
        public static void Ending()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("프로그램을 종료합니다");
            Console.WriteLine("-----------------------------------------");
        }
        
        public static void MenuPrint()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("-----------1. 전화번호 추가-----------");
            Console.WriteLine("-----------2. 전화번호 선택-----------");
            Console.WriteLine("-----------3. 전화번호 수정-----------");
            Console.WriteLine("-----------4. 전화번호 삭제-----------");
            Console.WriteLine("--------5. 전화번호 전체 삭제---------");
            Console.WriteLine("-----------6. 전화번호 정렬-----------");           
            Console.WriteLine("-----------7. 프로그램 종료-----------");
            Console.WriteLine("--------------------------------------");
        }
        
        public static void Pause()
        {
            Console.WriteLine("\n아무키나 누르세요....");
            Console.ReadKey();
        }
    }
}
