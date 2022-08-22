using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0408_OpenAPi_papago_
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg;
            Console.Write(">>");
            msg = Console.ReadLine();

            Console.WriteLine(Sample.Exam1(msg));

            Console.WriteLine();
        }
    }
}
