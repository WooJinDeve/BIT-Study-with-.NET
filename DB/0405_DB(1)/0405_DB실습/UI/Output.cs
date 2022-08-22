using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    internal class Output
    {
        public static void SelectAllPrint_Table(string msg)
        {
            string[] f1 = msg.Split('$');
            string table = null;

            for (int i = 0; i < f1.Length - 1; i++)
            {
                table += f1[i];               
            }
            string[] f2 = table.Split('#');
            for (int i = 0; i < f2.Length; i += 2) 
            {
                
                Console.Write("{0,2} ",f2[i]); 
            }
            Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------");
                        
            for (int i = 1; i < f2.Length; i += 2)
            {
                if(int.Parse(f2[i])==0)
                    Console.Write("{0,2} ", '0');
                else 
                    Console.Write("{0,2} ", '-');
            }
            Console.Write("\n\n");
        }
        public static void PrintCheckOutData(string msg)
        {
            string[] f1 = msg.Split('$');
            string[] f2 = f1[0].Split('#');
            string name = f2[0];
            string stime = f2[1];
            string etime = f2[2];
            int price = int.Parse(f2[3]);

            Console.WriteLine(name + "님이 퇴실을 요청하셨습니다.");
            Console.WriteLine("입실시간: "+stime);
            Console.WriteLine("퇴실시간: "+etime);
            Console.WriteLine("사용금액: "+price +"원");
        }

        public static void PrintCustomTotalCount(int total,int nowuse )
        {

            Console.WriteLine("[전체 좌석수 :      {0}  ]   " , total);
            Console.WriteLine("[사용 중 좌석수 :   {0}  ]   " , nowuse);
            Console.WriteLine("[비어 있는 좌석수 : {0}  ]   " , (total - nowuse));



        }
        public static void PrintTotalPriceList(string msg)
        {
            Console.WriteLine("이름\t 좌석ID\t 입실\t\t\t 퇴실\t\t\t 금액");
            string[] f1 = msg.Split('$');
            for (int i = 0; i < f1.Length - 1; i++)
            {
                string[] f2 = f1[i].Split('#');
                Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}", f2[0], f2[1], f2[2], f2[3], f2[4]);
            }
            Console.WriteLine();

        }
        public static void PrintTotalPrice(int totalprice)
        {
            Console.WriteLine("[전체 수익]\n전체 수익: {0}원", totalprice);
        }
        public static void PrintTablePrice(string msg)
        {
            Console.WriteLine("좌석ID\t 금액");
            string[] f1 = msg.Split('$');
            for (int i = 0; i < f1.Length - 1; i++)
            {
                string[] f2 = f1[i].Split('#');
                Console.WriteLine("{0}\t {1}", f2[0], f2[1]);
            }
        }


    }
}
