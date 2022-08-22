using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace 파파고_WCF
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost host = new ServiceHost(typeof(파파고_WCF.Papago));
            host.Open();

            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey();
            host.Close();
        }
    }
}
