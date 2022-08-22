using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _0425_실습
{
    public class MemberService1
    {
        #region DB연결정보
        private const string server = "DESKTOP-0I86BTV\\SQLEXPRESS";
        private const string database = "Member";
        private const string id = "wb35";
        private const string pw = "1234";
        #endregion

        private WbDB db = new WbDB();


        public bool InsertMember(string name, string phone)
        {
            try
            {
                db.Open(server, database, id, pw);

                string sql = WbQuery.InsertMember(name, phone);
                db.CommandNonQuery(sql);

                return true;
            }
            catch 
            {
                return false;
            }
            finally
            {

                db.Close();
            }

        }
        public Member SelectMember(string name)
        {
            db.Open(server, database, id, pw);

            string sql = WbQuery.SelectMember(name);
            string phone = (string)db.CommandScalar(sql);

            db.Close();

            if (phone == "")
                return null;          

            return new Member(name, phone);
        }

        public List<Member> SelectAllMember()
        {
            db.Open(server, database, id, pw);

            List<Member> mem = new List<Member>();


            string sql = WbQuery.SelectAllMember();
            string[] msg = db.CommandQuery(sql).Split('$');

            foreach (string sp in msg)
            {
                if (sp == "")
                    break;
                string[] sp1 = sp.Split('#');

                mem.Add(new Member(sp1[0], sp1[1]));
            }

            db.Close();
            return mem;
        }
    }
}