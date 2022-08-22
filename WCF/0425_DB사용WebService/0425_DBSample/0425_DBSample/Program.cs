using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0425_DBSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBControl con = new DBControl();
            //Console.WriteLine(con.InsertMember("허길동", "010-8888-8888"));
            Member mem = con.SelectMember("홍길동");
        }
    }
}
