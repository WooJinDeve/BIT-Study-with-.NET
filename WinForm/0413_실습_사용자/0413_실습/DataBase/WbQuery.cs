using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_실습
{
    internal static class WbQuery
    {
        public static string IdToMember(string id)
        {
            string sql = string.Format("select * from Member where ID ='{0}';", id);
            return sql;
        }
        public static string LogIn(string id, string pw)
        {
            string sql = string.Format("select NAME from Member where ID ='{0}' AND PW='{1}';", id, pw);
            return sql;
        }

        public static string NewMember(Member mem)
        {
             string sql = string.Format("insert into Member values('{0}', '{1}', '{2}', '{3}', {4}, 0, getdate());",
                mem.Id, mem.Pw, mem.Name, mem.Phone, mem.Age);
            return sql;
        }

        public static string DeleteMember(string id)
        {
            string sql = string.Format("delete from Member where id = '{0}';",id);
            return sql;
        }

        public static string UpdateMember(string id, string phone, int age)
        {
            string sql = string.Format("update Member set phone = '{0}', age = {1} where id = '{2}';",
                phone, age, id);
            return sql;
        }

        public static string LogOut(string id)
        {
            string sql = string.Format("update Member set islogin = {0} where id = '{1}';",
                0, id);
            return sql;
        }

        public static string LogInUpdate(string id)
        {
            string sql = string.Format("update Member set islogin = {0} where id = '{1}';",
                1, id);
            return sql;
        }
        
    }
}
