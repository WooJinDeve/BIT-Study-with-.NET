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

        //Insert/Update/Delete 사용 예제(수정하여 사용할 것)
        public void Insert()
        {
            try
            {
                string pname;
                int price;
                string description;

                Input.InsertData(out pname, out price, out description);
                string sql = WbQuery.Product_InsertCommand(pname, price, description);
                db.CommandNonQuery(sql);
                Console.WriteLine("저장 성공");
            }
            catch(Exception)
            {
                Console.WriteLine("저장 실패");
            }            
        }

        //selectall 사용 예제(수정하여 사용할 것)
        public void Delete()
        {
            try
            {
                string sql = WbQuery.SelectAll();
                string msg = db.CommandQuery(sql);

                Output.SelectAllPrint(msg);
            }
            catch(Exception)
            {
                Console.WriteLine("검색 오류");
            }
        }
                
        //단일값 반환 사용예(수정하여 사용할 것)
        public void CustomTotalCount()
        {
            try
            {
                string pname;

                Input.InputPName(out pname);
                string sql = WbQuery.PNameToPrice(pname);
                object value = db.CommandScalar(sql);
                Output.PrintPName(pname, (int)value);
            }
            catch (Exception)
            {
                Console.WriteLine("검색 실패");
            }
        }
            
        public void TotalPrice()
        {

        }

        public void TablePrice()
        {

        }

        #endregion 


    }
}
