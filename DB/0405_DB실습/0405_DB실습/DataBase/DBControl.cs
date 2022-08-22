using System;
using System.Collections.Generic;
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
            string table_str = db.CommandQuery(WbQuery.SelectAll_Table_string());
            return table_str;
        }

        /// <summary>
        /// 테이블 상태 여부 체크 
        /// </summary>
        /// <param name="tableIdx">체크할 테이블의 ID</param>
        /// <returns>테이블이 비어있다면 true</returns>
        public bool IsTableUseable(int tableIdx)
        {
            if ((int)db.CommandScalar(WbQuery.CheckTable_string(tableIdx)) == 0)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 전화번호를 이용해서 회원 ID를 반환
        /// </summary>
        /// <param name="phone">회원 전화번호</param>
        /// <returns>회원ID</returns>
        public int? GetMemberID(string phone)
        {
            int? id = (int)db.CommandScalar(WbQuery.PhoneToMid_string(phone));
            if (id == null)
                return -1;
            else
                return id;
            //int id; 
            //try
            //{
            //    id = (int)db.CommandScalar(WbQuery.PhoneToMid_string(phone));
            //    return id;
            //}
            //catch(Exception)
            //{
            //    return -1;
            //}

        }

        #region 기능
        public void Insert(int mid, int tid)
        {
            db.CommandNonQuery(WbQuery.CheckInTable_string(mid, tid));
            db.CommandNonQuery(WbQuery.SetTableUseableState_string(false, tid));
        }

        public string CheckOut(int tid)
        {            
            db.CommandNonQuery(WbQuery.SetTableUseableState_string(true, tid));

            string sql = WbQuery.GetCheckOutData_string(tid);
            return db.CommandQuery(sql);
        }

        public void CustomTotalCount(out int totalcount, out int usecount)
        {
            totalcount = (int)db.CommandScalar(WbQuery.CustomTotalCount_string());
            usecount = (int)db.CommandScalar(WbQuery.CustomUsingCount_string());
        }

        public void TotalPrice(out int price, out string msg)
        {
             price = (int)db.CommandScalar(WbQuery.PrintTotalPrice_string());
             msg = db.CommandQuery(WbQuery.PrintTotalPriceList_string());
        }

        public void TablePrice(out int price, out string msg)
        {
            price = (int)db.CommandScalar(WbQuery.PrintTotalPrice_string()); 
            msg = db.CommandQuery(WbQuery.PrintTablePrice_string());
        }
        #endregion
    }
}
