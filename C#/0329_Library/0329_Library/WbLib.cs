using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_Library
{
    static class WbLib
    {
        public static int GetNumber(string msg)
        {
            Console.Write(msg);
            return int.Parse(Console.ReadLine());
        }

        public static Library GetLibrary(string msg)
        {
            Console.WriteLine(msg);
            String name = GetString("책 제목 : ");
            String publisher = GetString("출판사 : ");
            int price = GetNumber("책 가격 :");
         
            Library lib = new Library(name, publisher, price);
            return lib;
        }
        public static string GetString(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }
    }
}
