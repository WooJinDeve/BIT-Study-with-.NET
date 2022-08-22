using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_실습
{
    internal static class WbQuery
    {
        //Control.cs - Table_DATA()
        public static string Table_StateCommand()
        {
            string sql = string.Format("select state from [table]");
            return sql;
        }

        //좌석 변경 Query
        public static string UpdateState(int tid, int check)
        {
            string sql = string.Format("update [Table] set State = {0} where TID = {1};", check, tid);
            return sql;
        }

        #region INSERT
        //전화번호를 통해 MEMBERID 반환 Query
        public static string PhonetoMID(int phone)
        {
            string sql = string.Format("select mid from member where phone = {0}", phone);
            return sql;
        }
        //Data 입력 Query
        public static string InsertCommand(int tid, int mid)
        {
            string sql = string.Format("insert into PCData(mid,tid,stime,etime) values({0},{1},getdate(),null);", mid, tid);
            return sql;
        }
        #endregion


        #region Delete
        //Tid를 통해 이름 반환 Query
        public static string TidtoNAME(int tid)
        {
            string sql = string.Format("select Member.NAME from Member, PCData where Member.MID = PCData.MID and PCData.TID = {0}",tid);
            return sql;
        }
        //Tid를 통해 PCData테이블의 Mid반환 Query
        public static string TidtoMid(int tid)
        {
            string sql = string.Format("select mid from pcdata where tid = {0}", tid);
            return sql;
        }
        //퇴실시 PCData테이블의 Etime -> getdata(); Query
        public static string UpdateEtime(int mid)
        {
            string sql = string.Format("update PCData set ETIME = GETDATE() where mid = {0} and price = 0", mid);
            return sql;
        }
        //10분->500원 : PCData -> Price Query
        public static string UpdatePrice(int mid)
        {
            string sql = string.Format("update PCData set PRICE = (DATEDIFF(MINUTE,STIME,ETIME)/10)*500 where mid = {0} and price = 0", mid);
            return sql;
        }
        //Mid, TID를 통해 시작시간을 가져오는 Query
        public static string MidtoStime(int mid, int tid)
        {
            string sql = string.Format("select stime from pcdata where mid = {0} and tid = {1}", mid, tid);
            return sql;
        }
        //Mid, TID를 통해 종료시간을 가져오는 Query
        public static string MidtoEtime(int mid, int tid)
        {
            string sql = string.Format("select etime from pcdata where mid = {0} and tid = {1};", mid,tid);
            return sql;
        }
        //Mid, TID를 통해 금액을 가져오는 Queru
        public static string MidtoPrice(int mid,int tid)
        {
            string sql = string.Format("select price from pcdata where mid = {0} and tid = {1};", mid, tid);
            return sql;
        }
        #endregion


        #region TotalPrice + TablePrice
        //전체 거래 정보 확인을 위한 List 출력 Query
        public static string TotalListData()
        {
            string sql = string.Format("select pcdata.mid, Member.NAME, PCData.TID, PCData.STIME, PCData.ETIME, PCData.PRICE from pcdata, member where pcdata.MID = Member.MID;");
            return sql;
        }
        //전체 금액 Query
         public static string TotalPrice()
        {
            string sql = string.Format("select sum(price) from pcdata");
            return sql;
        }

        //좌석별 금액 총합 Query
        public static string GroupSeatprice()
        {
            string sql = string.Format("select tid, sum(PRICE) from PCData group by TID Having Sum(price) != 0;");
            return sql;
        }
        #endregion

    }
    
}
