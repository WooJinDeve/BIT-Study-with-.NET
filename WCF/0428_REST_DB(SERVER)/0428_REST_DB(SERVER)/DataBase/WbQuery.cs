using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _0428_REST_DB_SERVER_
{
    internal class WbQuery
    {
        public static string GetData()
        {
            string sql = string.Format("select * from [Table]");
            return sql;
        }

        public static string PostData(string name, int age, string phone)
        {
            string sql = string.Format("insert into [Table] values('{0}', {1}, '{2}')", name, age, phone);
            return sql;
        }

        public static string PutData(string name, int age, string phone)
        {
            string sql = string.Format("update [Table] set age = {0}, phone = '{1}' where name = '{2}'", age, phone, name);
            return sql;
        }

        public static string DeleteData(string name)
        {
            string sql = string.Format("Delete from [Table] where name = '{0}'", name);
            return sql;
        }



    }

}