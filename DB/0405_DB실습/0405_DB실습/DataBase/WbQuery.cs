using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0405_DB실습
{
    internal static class WbQuery
    {
        public static string SelectAll_Table_string()
        {
            string sql = "select * from [table]";

            return sql;
        }        
        public static string PhoneToMid_string(string phone)
        {
            string sql = string.Format("select Mid from member where phone = '{0}';",
                    phone);

            return sql;
        }
        public static string CheckTable_string(int idx)
        {
            string sql = string.Format("select state from [table] where TID = '{0}';",
                    idx);

            return sql;
        }
        public static string CheckInTable_string(int mid,int tid)
        {
            string sql = string.Format("insert into pcdata(mid,tid,stime) values({0},{1},getdate())", mid, tid);
            return sql;
        }
        public static string SetTableUseableState_string(bool b,int tid)
        {
            string sql = string.Format(" update [table]  set state = {0}  where tid = {1} ",b?0:1 ,tid);
            return sql;
        }
        public static string GetCheckOutData_string(int tid)
        {
            string sql = string.Format(" update pcdata set etime = getdate()where tid  ={0} and price = 0", tid);
            sql += string.Format("update pcdata set price = (datediff(SECOND, stime, etime) / 10 * 500) from pcdata where tid  ={0} and price = 0", tid);
            sql += string.Format("select name,stime,etime,price from pcdata join member on pcdata.mid = member.mid  where tid  ={0} and (datediff(SECOND, etime, getdate())<2)", tid);


            return sql;
        }
        public static string CustomTotalCount_string()
        {
            string sql = string.Format("select count(*) from [table]" );           
            return sql;
        }
        public static string CustomUsingCount_string()
        {            
            string  sql = string.Format("select count(*) from [table] where state = 1");
            return sql;
        }
        public static string PrintTotalPrice_string()
        {
            string sql = string.Format("select sum(isnull( price,0)) from pcdata");
            return sql;
        }
        public static string PrintTotalPriceList_string()
        {
            string sql = string.Format("select name,tid,stime,etime,price from pcdata join member on pcdata.mid = member.mid where price<>0 ");
            return sql;
        }
        public static string PrintTablePrice_string()
        {
            string sql = string.Format("select tid, sum(isnull( price,0)) from pcdata group by tid having sum(isnull( price,0))<>0");
            return sql;
        }


    }
}
