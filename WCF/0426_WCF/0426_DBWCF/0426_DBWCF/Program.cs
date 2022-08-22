using System;
using System.ServiceModel;

namespace _0426_DBWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(MemberService));

            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey();
        }
    }
}
