using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0425_DBSample
{
    internal static class WbQuery
    {
        public static string InsertMember(string name, string phone)
        {
            string sql = string.Format("insert into Member values('{0}','{1}')", name, phone);
            return sql;
        }
        public static string SelectAllMember()
        {
            string sql = "select * from Member";
            return sql;
        }
        public static string SelectMember(string name)
        {
            string sql = string.Format("select * from member where name = '{0}';", name);
            return sql;
        }
    }
}
