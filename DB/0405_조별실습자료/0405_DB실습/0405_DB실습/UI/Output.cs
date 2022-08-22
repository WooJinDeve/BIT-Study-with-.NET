using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    internal class Output
    {
        public static void SelectAllPrint(string msg)
        {
            string[] f1 = msg.Split('$');
            for (int i = 0; i < f1.Length - 1; i++)
            {
                string[] f2 = f1[i].Split('#');
                Console.WriteLine("{0}, {1}, {2}, {3}", f2[0], f2[1], f2[2], f2[3]);
            }
        }
    
        public static void PrintPName(string pname, int price)
        {
            Console.WriteLine("제품명 : " + pname);
            Console.WriteLine("가격   : " + price);
        }
    }
}
