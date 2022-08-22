using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0426_WCF
{
    class HelloWorldWCFService : IHelloWorld
    {
        public string SayHello()
        {
            return "Hello WCF World";
        }
    }
}
