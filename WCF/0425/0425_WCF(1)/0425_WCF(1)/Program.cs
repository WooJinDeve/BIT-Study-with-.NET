using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0425_WCF_1_.localhost;
namespace _0425_WCF_1_
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowHelloService sh = new ShowHelloService();

            Console.WriteLine(sh.ShowHello("홍길동"));
           // Console.WriteLine(sh.Func());
        }
    }
}
