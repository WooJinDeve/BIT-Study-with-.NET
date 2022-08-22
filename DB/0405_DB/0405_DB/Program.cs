using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            WbDB db = new WbDB();
            if (db.Open() ==false)
                return;
            Console.WriteLine("DB 연결 성공");

            //-----------------------------------------
            //db.Product_InsertCommand("환타", 800, "환타입니다.");
            //db.Product_UpdateCommand("콜라", 1500, "가격을 수정했습니다.");
            //db.Product_DeleteCommand("환타");
            //db.SelectAll();
            db.SelectAll1();
            db.FindProDuct(1000);
            db.SelectCount();
            db.PNameToPrice("생수");
            //-----------------------------------------
            if (db.Close() == true)
                Console.WriteLine("DB 연결 해제");            
        }
    }
}
