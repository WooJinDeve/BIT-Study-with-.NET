using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0329_selection
{
    class Program
    {
        private Control1 con = new Control1();
        private void MenuInfo()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(" insert 저장값");
            Console.WriteLine(" insertidx 인덱스 저장값");
            Console.WriteLine(" select 검색키");
            Console.WriteLine(" update 검색값 수정값");
            Console.WriteLine(" delete 삭제키");
            Console.WriteLine(" deleteat 삭제할 인덱스");
            Console.WriteLine(" clear");
            Console.WriteLine(" exit");
            Console.WriteLine("------------------------------------------------");
        }

        public void Run()
        {
            MenuInfo();
           con.SelectAll();
            while (true)
            {
                Console.Write(">> ");
                string msg = Console.ReadLine();

                string[] sp1 = msg.Split(' ');

                switch(sp1[0])
                {
                    case "insert":       con.Insert(int.Parse(sp1[1]));  break;
                    case "insertidx":    con.Insertidx(int.Parse(sp1[1]), int.Parse(sp1[2])); break;
                    case "select":       con.Select(int.Parse(sp1[1])); break;
                    case "update":       con.Update(int.Parse(sp1[1]), int.Parse(sp1[2])); break;
                    case "delete":       con.Delete(int.Parse(sp1[1])); break;
                    case "deleteat":     con.DeleteAt(int.Parse(sp1[1])); break;
                    case "clear":        con.Clear();  break;
                    case "exit": return;
                }

                con.SelectAll();
                Console.WriteLine("\n");
            }

        }

        static void Main(string[] args)
        {
            //Program pr = new Program();
            //pr.Run();

            //익명객체
            new Program().Run();
        }
    }
}
