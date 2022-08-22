using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0424_DBWCFClient.ServiceReference2;
namespace _0424_DBWCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MemberServiceClient ms = new MemberServiceClient();
            Member[] members = ms.SelectAllMember();
            foreach(Member mem in members)
            {
                Console.WriteLine(mem.MemberName + ", "+  mem.MemberPhone);
            }
        }
    }
}
