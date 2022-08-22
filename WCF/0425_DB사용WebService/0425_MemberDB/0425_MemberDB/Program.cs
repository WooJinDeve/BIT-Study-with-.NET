using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0425_MemberDB.localhost;

namespace _0425_MemberDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MemberService mservice = new MemberService();
            //if (mservice.InsertMember("헝길동", "010-7777-7777") == true)
            //    Console.WriteLine("성공");
            //else
            //    Console.WriteLine("실패");

            Member mem = mservice.SelectMember("허길동");
            Console.WriteLine(mem.Name + ", " + mem.Phone);
        }
    }
}
