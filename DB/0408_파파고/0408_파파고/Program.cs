using Newtonsoft.Json.Linq;
using System;
/*
{"message":
    {"result":
       {"srcLangType":"ko","tarLangType":"en","translatedText":"Hello!",
         "engineType":"PRETRANS","pivot":null,"dict":null,"tarDict":null,"modelVer":"Unknown"},"
         @type":"response","@service":"naverservice.nmt.proxy","@version":"1.0.0"}}
*/
namespace _0408_파파고
{
    class Program
    {
        static void exam1()
        {
            while (true)
            {
                string msg;
                Console.Write(">>");
                msg = Console.ReadLine();
                Console.WriteLine(Sample.Exam1(msg));
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Sample.Exam2();             
        }
    }
}
