using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    static class WbLib
    {
        public static int getNumber(string str)
        {
            Console.Write(str + " : ");
            return int.Parse(Console.ReadLine());
        }

        public static string getString(string str)
        {
            Console.Write(str + " : ");
            return Console.ReadLine();
        }
    
        public static void Pause()
        {
            Console.WriteLine("\n아무키나 누르세요....");
            Console.ReadKey();
        }
    }
}
