using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0426_UseWCF.localhost;

namespace _0426_UseWCF
{
    class Program
    {
        static void Service1()
        {
            HelloWorldWCFService helloWorldClient = new HelloWorldWCFService();
            string msg = helloWorldClient.SayHello();
            Console.WriteLine(msg);
        }
        static void Main(string[] args)
        {

            Service1();
        }
    }
}
