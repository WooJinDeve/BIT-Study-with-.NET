using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_실습
{
    class WbControl
    {
        #region 싱글톤 패턴
        private WbControl() { }
        public static WbControl Instance { get; private set; }
        static WbControl()
        {
            Instance = new WbControl();
        }
        #endregion

        private WbDocument doc = WbDocument.Instance;

        public string Login(string id, string pw)
        {
            string name = doc.Login(id, pw);
            return name;
        }

        public void NewMember(string id, string pw, string name, string phone, int age)
        {
            doc.NewMember(new Member(id, pw, name, phone, age));
        }

        public List<Member> GetMemberListAll()
        {
            List<Member> members = doc.GetMemberListAll();
            return members;
        }

        public Member SelectMember(string id)
        {
            Member mem = doc.IdtoMember(id);
            return mem;
        }

        public void DeleteMember(string delid)
        {
            doc.DeleteMember(delid);
        }

        public void UpdateMember(string id, string phone, int age)
        {
            doc.UpdateMember(id, phone, age);
        }

        public void Logout(string id)
        {
            doc.Logout(id);
        }

        public void Dispose()
        {
            doc.Dispose();
        }
    }
}
