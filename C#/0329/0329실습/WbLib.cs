using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329실습
{
    static class WbLib
    {
        public static int GetNumber(string msg)
        {
            Console.Write(msg + " : ");
            return int.Parse(Console.ReadLine());
        }

        public static string GetString(string msg)
        {
            Console.Write(msg + " : ");
            return Console.ReadLine();
        }

        public static char GetChar(string msg)
        {
            Console.Write(msg + " : ");
            return char.Parse(Console.ReadLine());
        }

        public static float GetFloat(string msg)
        {
            Console.Write(msg + " : ");
            return float.Parse(Console.ReadLine());
        }
    
        public static ConsoleKey GetKey(string msg)
        {
            Console.Write(msg + " : ");
            ConsoleKeyInfo info =  Console.ReadKey();
            return info.Key;
        }
    }
}
