using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_실습
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
        private const string server = "DESKTOP-ETKI1PT\\SQLEXPRESS";
        private const string database = "Sample";
        private const string id = "jwj06011";
        private const string pw = "1234";
        #endregion 

        private WbDB db = new WbDB();

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

        #region Table Data
        private List<int> TableArr; //TABLE DATE 저장 List<int>

        //Insert/Update/Delete 사용 예제(수정하여 사용할 것)
        private void Table_Data()
        {
            try
            {
                string sql = WbQuery.Table_StateCommand();
                string msg = db.CommandQuery(sql);

                TableArr = Output.Table_ArrPrint(msg);

                Output.TablePrint(TableArr);
            }
            catch (Exception)
            { }
        }
        #endregion

        #region Insert + Delete
        public void Insert()
        {
            try
            {
                Table_Data();

                int tid;
                int phone;

                Input.InsertTID(out tid);
                if(TableArr[tid-1] == 1)
                {
                    Console.WriteLine("\n\n사용중인 좌석입니다.");
                    return;
                }
                
                Input.InsertPhone(out phone);
                string sql = WbQuery.PhonetoMID(phone);
                object mid = db.CommandScalar(sql);

                if (mid == null)
                {
                    Console.WriteLine("\n\n없는 번호입니다.");
                    return;
                }

                sql = WbQuery.InsertCommand(tid, (int)mid);
                db.CommandNonQuery(sql);

                sql = WbQuery.UpdateState(tid, 1);
                db.CommandNonQuery(sql);
                Console.WriteLine("좌석 배치가 완료되었습니다.");

            }
            catch (Exception)
            {
                Console.WriteLine("저장 실패");
            }
        }

        public void Delete()
        {
            try
            {
                Table_Data();

                int tid;

                Input.InsertTID(out tid);
                if (TableArr[tid -1] == 0)
                {
                    Console.WriteLine("\n\n비어 있는 좌석입니다..");
                    return;
                }

                string sql1 = WbQuery.TidtoMid(tid);
                object mid = db.CommandScalar(sql1);

                string sql2 = WbQuery.TidtoNAME(tid);
                string sql3 = WbQuery.UpdateEtime((int)mid);
                string sql4 = WbQuery.UpdatePrice((int)mid);
                string sql5 = WbQuery.UpdateState(tid, 0);
                string sql6 = WbQuery.MidtoStime((int)mid, tid);
                string sql7 = WbQuery.MidtoEtime((int)mid,tid);
                string sql8 = WbQuery.MidtoPrice((int)mid,tid);

                object name = db.CommandScalar(sql2);
                object stime = db.CommandScalar(sql6);
                object etime = db.CommandScalar(sql7);
                object price = db.CommandScalar(sql8);

                db.CommandNonQuery(sql3);
                db.CommandNonQuery(sql4);
                db.CommandNonQuery(sql5);

                Output.DeletePrice(name, stime, etime, price);
            }
            catch (Exception ex)
            {
                Console.WriteLine("검색 오류" + ex.Message);
            }
        }
        #endregion

        #region SeatCount
        //TableArr를 통한 좌석 정보 함수
        private void SeatState(ref int total_seat, ref int using_seat, ref int void_seat)
        {
            total_seat = TableArr.Count;

            foreach (int arr in TableArr)
            {
                if (arr == 1)
                    using_seat++;
                void_seat++;
            }
        }
        public void SeatCount()
        {
            try
            {
                Table_Data();
                int total_seat = 0;
                int using_seat = 0;
                int void_seat = 0;

                SeatState(ref total_seat, ref using_seat, ref void_seat);
                Output.SeatCountPrint(total_seat, using_seat, void_seat);
            }
            catch (Exception)
            {
                Console.WriteLine("검색 실패");
            }
        }
        #endregion

        #region TotalPrice + TablePrice
        public void TotalPrice()
        {
            Table_Data();

            string sql1 = WbQuery.TotalListData();
            string sql2 = WbQuery.TotalPrice();

            string msg = db.CommandQuery(sql1);
            object total = db.CommandScalar(sql2);

            Output.TotalListPrint(msg);
            Console.WriteLine("[전체수익 : {0}원]", total);
        }

        public void TablePrice()
        {
            Table_Data();

            string sql1 = WbQuery.TotalPrice();
            string sql2 = WbQuery.GroupSeatprice();

            object total = db.CommandScalar(sql1);
            string msg = db.CommandQuery(sql2);

            Console.WriteLine("[전체수익 : {0}원]", total);
            Output.SelectAllPrint(msg);
        }

        #endregion

        #endregion
    }
}
