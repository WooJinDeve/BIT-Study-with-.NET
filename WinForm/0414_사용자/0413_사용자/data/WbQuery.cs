using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_사용자
{
    static class WbQuery
    {
        public static string IdtoMember(string id)
        {
            string sql = string.Format("select * from Member where id = '{0}'", id);
            return sql;
        }
  
        public static string IsLoginTrueChange(string id)
        {
            string sql = string.Format("update member set islogin = 'true' where id = '{0}'", id);
            return sql;
        }
        public static string NewMember(Member mem)
        {
            string sql = string.Format("insert into Member values('{0}','{1}','{2}','{3}','{4}',{5},getdate())", mem.IsLogin, mem.Id, mem.Pw, mem.Name, mem.Phone, mem.Age);

            return sql;
        }    

        public static string DeleteMember(string delid)
        {
            string sql = string.Format("delete from member where id = '{0}'", delid);
            return sql;
        }

        public static string UpdateMember(string id, string phone, int age)
        {
            string sql = string.Format("update member set phone = '{0}', age = {1} where id = '{2}'",phone,age,id);
            return sql;
        }
        public static string Logout(string id)
        {
            string sql = string.Format("update member set islogin = 'false' where id = '{0}'", id);
            return sql;
        }
    }
}
