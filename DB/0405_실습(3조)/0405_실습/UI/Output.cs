using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_실습
{
    internal class Output
    {
        #region Table Print
        public static void TablePrint(List<int> arr)
        {
            int z = 0;
            int x = 1;
            Console.WriteLine("[좌석상태]\n");
            for (int i = 0; i < arr.Count() / 10; i++)
            {
                for (int j = 0; j < 10; j++)
                    Console.Write("{0,5} ", x++);

                Console.WriteLine("\n---------------------------------------------------------------");

                for (int k = 0; k < 10; k++)
                {
                    if (arr[z++] == 1)
                        Console.Write("    - ");
                    else
                        Console.Write("    O ");
                }
                Console.WriteLine("\n---------------------------------------------------------------");
            }
        }

        public static List<int> Table_ArrPrint(string msg)
        {
            List<int> arr = new List<int>();
            string[] f1 = msg.Split('$');

            for (int i = 0; i < f1.Length - 1; i++)
            {
                string[] f2 = f1[i].Split('#');
                arr.Add(int.Parse(f2[0]));
            }
            return arr;
        }
        #endregion

        #region Delete Print
        public static void DeletePrice(object name, object stime, object etime, object price)
        {
            Console.WriteLine("{0} 님이 퇴실을 요청하였습니다.", name);
            Console.WriteLine("입실시간 : {0}", stime);
            Console.WriteLine("퇴실시간 : {0}", etime);
            Console.WriteLine("사용금액 : {0}", price);
        }
        #endregion

        #region SeatCount Print
        public static void SeatCountPrint(int total_seat, int using_seat, int void_seat)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("[전체 좌석수       : {0}]", total_seat);
            Console.WriteLine("[사용중 좌석수     : {0}]", using_seat);
            Console.WriteLine("[비어 있는 좌석수  : {0}]", void_seat);
        }
        #endregion

        #region TotalList Print
        public static void TotalListPrint(string msg)
        {
            try
            {
                string[] f1 = msg.Split('$');
                Console.WriteLine("[거래 정보 리스트]");
                for (int i = 0; i < f1.Length - 1; i++)
                {
                    string[] f2 = f1[i].Split('#');
                    Console.WriteLine("ID : {0} -> ({1}), 좌석({2}), 입실 : {3}, 퇴실 {4}, 금액{5}", f2[0], f2[1], f2[2], f2[3], f2[4],f2[5]);
                }
            }
            catch(Exception)
            { }
        }
        #endregion

        #region TablePrice Print
        public static void SelectAllPrint(string msg)
        {
            string[] f1 = msg.Split('$');
            Console.WriteLine("[좌석별 수입]");
            for (int i = 0; i < f1.Length - 1; i++)
            {
                string[] f2 = f1[i].Split('#');
                Console.WriteLine("좌석 : {0}, 금액 : {1}원", f2[0], f2[1]);
            }
        }
        #endregion
    }
}
