using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0426_DBWCF
{
    public class MemberService : IMemberService
    {
        public bool InsertMember(string name, string phone)
        {
            DBControl db = new DBControl();
            return db.InsertMember(name, phone);
        }

        public List<Member> SelectAllMember()
        {
            DBControl db = new DBControl();
            return db.SelectAllMember();
        }

        public Member SelectMember(string name)
        {
            DBControl db = new DBControl();
            return db.SelectMember(name);
        }
    }
}
