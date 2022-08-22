using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    internal class DBControl
    {
        private WbDB db = new WbDB();

        public bool Open(string server, string database,string id,string pw)
        {
            return db.Open(server, database, id, pw);
        }

        public void Close()
        {
            db.Close();
        }

        public string PrintAll_Table()
        {
            string sql = "SelectAll_Table_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            string table_str = db.CommandQuery(command);
            return table_str;
        }

        /// <summary>
        /// 테이블 상태 여부 체크 
        /// </summary>
        /// <param name="tableIdx">체크할 테이블의 ID</param>
        /// <returns>테이블이 비어있다면 true</returns>
        public bool IsTableUseable(int tableIdx)
        {
            string sql = "CheckTable_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            //파라미터 등록
            SqlParameter param_idx = new SqlParameter("@idx", tableIdx);
            param_idx.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(param_idx);

            SqlParameter param_state = new SqlParameter();
            param_state.Direction = System.Data.ParameterDirection.Output;
            param_state.ParameterName = "@state";
            param_state.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(param_state);

            db.CommandNonQuery(command);  //<-----------------------

            if ((int)param_state.Value == 1)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 전화번호를 이용해서 회원 ID를 반환
        /// </summary>
        /// <param name="phone">회원 전화번호</param>
        /// <returns>회원ID</returns>
        public int? GetMemberID(string phone)
        {
            string sql = "PhoneToMid_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            //파라미터
            //@phone varchar(50),    @mid  int OUTPUT
            SqlParameter param_phone = new SqlParameter("@phone", phone);
            command.Parameters.Add(param_phone);

            SqlParameter param_mid = new SqlParameter();
            param_mid.Direction = System.Data.ParameterDirection.Output;
            param_mid.ParameterName = "@mid";
            param_mid.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(param_mid);

            db.CommandNonQuery(command);  //<-----------------------

            return (int)param_mid.Value;
        }

        #region 기능
        public void Insert(int mid, int tid)
        {
            //db.CommandNonQuery(WbQuery.CheckInTable_string(mid, tid));
            string sql = "CheckInTable_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            //파라미터           
            SqlParameter param_mid = new SqlParameter("@mid", mid);
            param_mid.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(param_mid);

            SqlParameter param_tid = new SqlParameter("@tid", tid);
            param_tid.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(param_tid);

            db.CommandNonQuery(command);  //<-----------------------

            //db.CommandNonQuery(WbQuery.SetTableUseableState_string(false, tid));
            string sql1 = "SetTableUseableState_string";
            SqlCommand command1 = new SqlCommand(sql1);
            command1.CommandType = System.Data.CommandType.StoredProcedure;

            //파라미터  
            SqlParameter param1_input = new SqlParameter("@input", 1);
            param1_input.SqlDbType = System.Data.SqlDbType.Int;
            command1.Parameters.Add(param1_input);

            SqlParameter param1_tid = new SqlParameter("@tid", tid);
            param1_tid.SqlDbType = System.Data.SqlDbType.Int;
            command1.Parameters.Add(param1_tid);

            db.CommandNonQuery(command1);  //<-----------------------
        }

        public string CheckOut(int tid)
        {


            string sql1 = "SetTableUseableState_string";
            SqlCommand command1 = new SqlCommand(sql1);
            command1.CommandType = System.Data.CommandType.StoredProcedure;

            //파라미터  
            SqlParameter param1_input = new SqlParameter("@input", Convert.ToInt32(0));
            param1_input.SqlDbType = System.Data.SqlDbType.Int;
            command1.Parameters.Add(param1_input);

            SqlParameter param1_tid = new SqlParameter("@tid", tid);
            param1_tid.SqlDbType = System.Data.SqlDbType.Int;
            command1.Parameters.Add(param1_tid);

            db.CommandNonQuery(command1);  //<-----------------------

            string sql = "GetCheckOutData_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            //파라미터
            //@phone varchar(50),    @mid  int OUTPUT
            SqlParameter param_tid = new SqlParameter("@tid", tid);
            param_tid.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(param_tid);

            return db.CommandQuery(command);  //<-----------------------

            //db.CommandNonQuery(WbQuery.SetTableUseableState_string(true, tid));

            //string sql = WbQuery.GetCheckOutData_string(tid);
            //return db.CommandQuery(sql);
            
        }

        public void CustomTotalCount(out int totalcount, out int usecount)
        {
            totalcount = 0;
            usecount = 0;

            string sql = "CustomTotalCount_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //파라미터
            //@phone varchar(50),    @mid  int OUTPUT
            SqlParameter param_count = new SqlParameter();
            param_count.Direction = System.Data.ParameterDirection.Output;
            param_count.ParameterName = "@count";
            param_count.SqlDbType = System.Data.SqlDbType.Int;
            command.Parameters.Add(param_count);

            db.CommandNonQuery(command);  //<-----------------------

            totalcount = (int)param_count.Value;


            string sql1 = "CustomUsingCount_string";
            SqlCommand command1 = new SqlCommand(sql1);
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            //파라미터
            //@phone varchar(50),    @mid  int OUTPUT
            SqlParameter param_usecount = new SqlParameter();
            param_usecount.Direction = System.Data.ParameterDirection.Output;
            param_usecount.ParameterName = "@count";
            param_usecount.SqlDbType = System.Data.SqlDbType.Int;
            command1.Parameters.Add(param_usecount);

            db.CommandNonQuery(command1);  //<-----------------------


            usecount = (int)param_usecount.Value;


            //totalcount = (int)db.CommandScalar(WbQuery.CustomTotalCount_string());
            //usecount = (int)db.CommandScalar(WbQuery.CustomUsingCount_string());
        }

        public void TotalPrice(out int price, out string msg)
        {
            price = 0;
            msg = "";

            string sql1 = "PrintTotalPrice_string";
            SqlCommand command1 = new SqlCommand(sql1);
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            //파라미터
            //@phone varchar(50),    @mid  int OUTPUT
            SqlParameter param_usecount = new SqlParameter();
            param_usecount.Direction = System.Data.ParameterDirection.Output;
            param_usecount.ParameterName = "@sum";
            param_usecount.SqlDbType = System.Data.SqlDbType.Int;
            command1.Parameters.Add(param_usecount);
            db.CommandNonQuery(command1);  //<-----------------------
            price = (int)param_usecount.Value;


            
            string sql = "PrintTotalPriceList_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;  
            msg =  db.CommandQuery(command);  //<-----------------------

            //price = (int)db.CommandScalar(WbQuery.PrintTotalPrice_string());
            //msg = db.CommandQuery(WbQuery.PrintTotalPriceList_string());
        }

        public void TablePrice(out int price, out string msg)
        {
            price = 0;
            msg = "";

            string sql1 = "PrintTotalPrice_string";
            SqlCommand command1 = new SqlCommand(sql1);
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            //파라미터
            //@phone varchar(50),    @mid  int OUTPUT
            SqlParameter param_usecount = new SqlParameter();
            param_usecount.Direction = System.Data.ParameterDirection.Output;
            param_usecount.ParameterName = "@sum";
            param_usecount.SqlDbType = System.Data.SqlDbType.Int;
            command1.Parameters.Add(param_usecount);
            db.CommandNonQuery(command1);  //<-----------------------
            price = (int)param_usecount.Value;



            string sql = "PrintTablePrice_string";
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            msg = db.CommandQuery(command);  //<-----------------------

            //price = (int)db.CommandScalar(WbQuery.PrintTotalPrice_string());
            //msg = db.CommandQuery(WbQuery.PrintTotalPriceList_string());










            //price = (int)db.CommandScalar(WbQuery.PrintTotalPrice_string()); 
            //msg = db.CommandQuery(WbQuery.PrintTablePrice_string());
        }
        #endregion
    }
}
