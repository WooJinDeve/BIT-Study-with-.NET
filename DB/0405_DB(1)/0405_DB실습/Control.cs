using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{    
    class Control
    {
        #region 싱글톤

        public static Control Instance { get; private set; }
        static Control()
        {
            Instance = new Control();   
        }
        private Control() { }

        #endregion n

        #region DB연결정보
        private const string server = "DESKTOP-0I86BTV\\SQLEXPRESS";
        private const string database = "Sample";
        private const string id = "wb35";
        private const string pw = "1234";
        #endregion 

        private DBControl db = new DBControl();

        #region DB연결 및 해제

        public bool DB_Open()
        {
            return db.Open(server, database, id, pw);
        }

        public void DB_Close()
        {
            db.Close();
        }

        #endregion 


        #region 기능

        public void Insert()
        {
            try
            {               
                //입력 및 체크
                int tid = WbLib.getNumber("좌석 선택 입력(1~10) >>");
                if (db.IsTableUseable(tid) == false)
                {
                    Console.WriteLine("사용중인 좌석입니다.");
                    return; 
                }
                string phonenumber = WbLib.getString("전화번호를 입력 >>");
                int mid = (int)db.GetMemberID(phonenumber);
                if (mid == -1)
                {
                    Console.WriteLine("없는 회원입니다.");
                    return;
                }

                //본 기능..(좌석 배치 수정, 사용정보 등록)
                db.Insert(mid, tid);

                Console.WriteLine("좌석 배치가 완료되었습니다.");                               
            }
            catch(Exception ex)
            {
                Console.WriteLine("저장 실패___"+ex.Message);
            }            
        }

        public void Delete()
        {
            try
            {
                int tid = WbLib.getNumber("좌석 선택 입력(1~40) >>");
                if (db.IsTableUseable(tid) == true)
                {
                    Console.WriteLine("비어 있는 좌석입니다.");
                    return;
                }

                Output.PrintCheckOutData(db.CheckOut(tid));  
                Console.WriteLine("퇴실 완료되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("퇴실 실패___" + ex.Message);
            }

        }
                
       
        public void CustomTotalCount()
        {
            int totalcount, usecount;

            db.CustomTotalCount(out totalcount, out usecount);
            Output.PrintCustomTotalCount( totalcount, usecount);           
        }
            
        public void TotalPrice()
        {
            int price;
            string msg;
            db.TotalPrice(out price, out msg);
             
            Output.PrintTotalPriceList(msg);
            Output.PrintTotalPrice(price);
        }

        public void TablePrice()
        {
            int price;
            string msg;
            db.TablePrice(out price, out msg);

            Output.PrintTotalPrice(price);
            Output.PrintTablePrice(msg);
        }
        public void PrintAll_Table()
        {
            string str = db.PrintAll_Table();
            Output.SelectAllPrint_Table(str);
        }

        #endregion 
    }
}
