using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0426_ImageWCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            Service();
        }

        static void Service()
        {
            ServiceHost host = new ServiceHost(typeof(WbImage));

            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey();
            host.Close();

        }
    }
}
