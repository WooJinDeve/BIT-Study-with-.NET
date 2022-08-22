using System;
using System.Collections.Generic;
using System.Configuration;

namespace _0425_MemberService
{
    internal class DBControl
    {
        private WbDB db = new WbDB();

        #region Open(Configuration활용) & Close
        public bool Open()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string id = ConfigurationManager.AppSettings["id"];
            string pw = ConfigurationManager.AppSettings["pw"];

            return db.Open(server, database, id, pw);
        }

        public void Close()
        {
            db.Close();
        }

        #endregion 

        public bool InsertMember(string name, string phone)
        {
            try
            {
                Open();
                db.CommandNonQuery(WbQuery.InsertMember(name, phone));
                return true;
            }
            catch (Exception )
            {
                return false;
            }
            finally
            {
                Close();
            }
        }

        public List<Member> SelectAllMember()
        {
            try
            {
                Open();
                List<Member> members = db.CommandQuery1(WbQuery.SelectAllMember());
                return members;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Close();
            }
        }

        public Member SelectMember(string name)
        {
            try
            {
                Open();
                Member member = db.CommandQuery2(WbQuery.SelectMember(name));
                return member;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Close();
            }
        }

    }
}
