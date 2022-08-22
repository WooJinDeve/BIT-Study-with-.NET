using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _0425_실습
{
    public class WbQuery
    {

        public static string InsertMember(string name, string phone)
        {
            string sql = string.Format("insert into Member values('{0}','{1}')", name, phone);
            return sql;
        }

        public static string SelectMember(string name)
        {
            string sql = string.Format("select phone from Member where name = '{0}'", name);
            return sql;
        }

        public static string SelectAllMember()
        {
            string sql = string.Format("select * from Member");
            return sql;
        }


    }
}