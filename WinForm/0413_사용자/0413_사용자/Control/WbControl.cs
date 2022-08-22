using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0413_사용자
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

        #region DB Open + Close
        public void Open()
        {
            doc.Open();
        }

        public void Close()
        {
            doc.Close();
        }
        #endregion

        #region 기능
        //로그인
        public string Login(string id, string pw)
        {
            string name = doc.Login(id, pw);
            return name;
        }

        //insert member
        public void NewMember(string id, string pw, string name, string phone, int age)
        {
            doc.NewMember(new Member(id, pw, name, phone, age));
        }

        //검색
        public Member SelectMember(string id)
        {
            Member mem = doc.IdtoMember(id);
            return mem;
        }

        //삭제
        public void DeleteMember(string delid)
        {
            doc.DeleteMember(delid);
        }

        //수정
        public void UpdateMember(string id, string phone, int age)
        {
            doc.UpdateMember(id, phone, age);
        }

        public void Logout(string id)
        {
            doc.Logout(id);
        }
        #endregion
    }
}
